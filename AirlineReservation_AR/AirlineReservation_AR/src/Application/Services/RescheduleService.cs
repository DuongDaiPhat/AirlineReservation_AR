using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.Application.Interfaces;
using AirlineReservation_AR.src.Domain.DTOs;
using AirlineReservation_AR.src.Domain.Entities;
using AirlineReservation_AR.src.Infrastructure.DI;
using AR_AirlineReservation_AR.src.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Application.Services
{
    public class RescheduleService : IRescheduleService
    {
        private IFareRuleService _fareRuleService = new FareRuleService();
        // 1. CheckEligibilityAsync (Logic giữ nguyên, chỉ sửa hiển thị ngày giờ nếu cần)
        public async Task<RescheduleEligibilityResult> CheckEligibilityAsync(Guid ticketId)
        {
            using var db = DIContainer.CreateDb();
            var result = new RescheduleEligibilityResult { CanReschedule = false };

            var ticket = await db.Tickets
                .Include(t => t.BookingFlight)
                .ThenInclude(bf => bf.Booking)
                .Include(t => t.BookingFlight.Flight)
                .FirstOrDefaultAsync(t => t.TicketId == ticketId);

            if (ticket == null)
            {
                result.Reason = "Ticket not found.";
                return result;
            }

            // Map DTO
            DateTime flightDate = ticket.BookingFlight.Flight.FlightDate;
            TimeSpan flightTime = ticket.BookingFlight.Flight.DepartureTime;
            DateTime fullDepartureTime = flightDate + flightTime;

            result.TicketInfo = new TicketBasicInfoDto
            {
                TicketId = ticket.TicketId,
                Status = Enum.Parse<TicketStatus>(ticket.Status ?? "Issued"),
                FlightCode = ticket.BookingFlight.Flight.FlightNumber,
                DepartureTime = fullDepartureTime
            };

            // Validate Status
            if (ticket.Status != TicketStatus.Issued.ToString())
            {
                result.Reason = $"Ticket status is {ticket.Status}, only Issued tickets can be rescheduled.";
                return result;
            }
            if (ticket.BookingFlight.Booking.Status != "Confirmed")
            {
                result.Reason = "Booking is not Confirmed.";
                return result;
            }

            // Validate Rule
            var fareRule = await _fareRuleService.GetFareRuleForTicketAsync(ticketId);
            if (fareRule == null || !fareRule.AllowChange)
            {
                result.Reason = "Ticket Fare Rule implies Non-Changeable.";
                return result;
            }

            // Validate Time
            var hoursLeft = (fullDepartureTime - DateTime.Now).TotalHours;
            if (hoursLeft < fareRule.MinHoursBeforeChange)
            {
                result.Reason = $"Too late to reschedule. Must be {fareRule.MinHoursBeforeChange} hours before departure.";
                return result;
            }

            result.CanReschedule = true;
            result.MinHoursBeforeChange = fareRule.MinHoursBeforeChange;
            return result;
        }

        // 2. GetQuoteAsync (Giữ nguyên logic tính giá dựa trên Pricing)
        public async Task<RescheduleQuoteResultDto> GetQuoteAsync(RescheduleQuoteRequestDto request)
        {
            using var db = DIContainer.CreateDb();
            var result = new RescheduleQuoteResultDto { IsSuccess = false, Currency = "VND" };

            try
            {
                var oldTicket = await db.Tickets
                    .Include(t => t.BookingFlight)
                    .FirstOrDefaultAsync(t => t.TicketId == request.TicketId);

                if (oldTicket == null) throw new Exception("Ticket not found");

                // Lấy giá cũ từ Pricing (vì Ticket không lưu Price)
                var oldPricing = await db.FlightPricings
                    .FirstOrDefaultAsync(fp => fp.FlightId == oldTicket.BookingFlight.FlightId
                                            && fp.SeatClassId == oldTicket.SeatClassId);

                decimal originalPrice = oldPricing?.Price ?? 0;

                // Lấy giá mới
                var newPricing = await db.FlightPricings
                    .Include(fp => fp.FareRule)
                    .FirstOrDefaultAsync(fp => fp.FlightId == request.NewFlightId
                                            && fp.SeatClassId == oldTicket.SeatClassId); // Giữ nguyên hạng ghế

                if (newPricing == null) throw new Exception("New flight/class not found.");

                decimal newPrice = newPricing.Price;

                // Lấy phí phạt
                var oldRule = await _fareRuleService.GetFareRuleForTicketAsync(request.TicketId);
                decimal penaltyFee = oldRule?.ChangeFee ?? 0;

                decimal fareDiff = Math.Max(0, newPrice - originalPrice);
                decimal total = fareDiff + penaltyFee;

                result.IsSuccess = true;
                result.OriginalBasePrice = originalPrice;
                result.NewBasePrice = newPrice;
                result.FareDifference = fareDiff;
                result.PenaltyFee = penaltyFee;
                result.TotalAmount = total;
                result.FareCode = newPricing.FareRule?.FareCode;
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        // 3. ConfirmRescheduleAsync (SỬA NHIỀU ĐỂ KHỚP ENTITY)
        public async Task<RescheduleResultDto> ConfirmRescheduleAsync(RescheduleQuoteRequestDto request)
        {
            // 1. Re-check Eligibility
            var check = await CheckEligibilityAsync(request.TicketId);
            if (!check.CanReschedule)
                return new RescheduleResultDto { IsSuccess = false, ErrorMessage = check.Reason };

            // 2. Re-calc Quote
            var quote = await GetQuoteAsync(request);
            if (!quote.IsSuccess)
                return new RescheduleResultDto { IsSuccess = false, ErrorMessage = quote.ErrorMessage };

            using var db = DIContainer.CreateDb();
            using var transaction = await db.Database.BeginTransactionAsync();

            try
            {
                var oldTicket = await db.Tickets
                    .Include(t => t.BookingFlight)
                    .FirstOrDefaultAsync(t => t.TicketId == request.TicketId);

                // --- BƯỚC A: TẠO PAYMENT TRƯỚC (Vì RescheduleRequest cần PaymentId) ---
                var payment = new Payment
                {
                    BookingId = oldTicket.BookingFlight.BookingId, // Payment thuộc về Booking gốc
                    PaymentMethod = "CreditCard",   // Giả định thanh toán thẻ
                    PaymentProvider = "VNPay",      // Fake provider
                    Amount = quote.TotalAmount,
                    Currency = "VND",
                    Status = "Completed",           // Giả lập thanh toán thành công ngay
                    TransactionId = Guid.NewGuid().ToString().Substring(0, 10).ToUpper(),
                    ProcessedAt = DateTime.Now,     // Thay cho PaymentDate
                    CompletedAt = DateTime.Now
                };

                db.Payments.Add(payment);
                await db.SaveChangesAsync(); // Save ngay để lấy PaymentId

                // --- BƯỚC B: TẠO RESCHEDULE REQUEST ---
                var rescheduleReq = new RescheduleRequest
                {
                    BookingId = oldTicket.BookingFlight.BookingId,
                    BookingFlightId = oldTicket.BookingFlightId,
                    OldFlightId = oldTicket.BookingFlight.FlightId,
                    NewFlightId = request.NewFlightId,
                    FareDifference = quote.FareDifference,
                    PenaltyFee = quote.PenaltyFee,
                    TotalAmount = quote.TotalAmount,
                    Status = "Completed",
                    CreatedAt = DateTime.Now,
                    PaymentId = payment.PaymentId // Link tới Payment vừa tạo
                };

                db.RescheduleRequests.Add(rescheduleReq);
                // Không cần SaveChanges ngay nếu DB hỗ trợ tracking tốt, nhưng save cho chắc
                await db.SaveChangesAsync();

                // --- BƯỚC C: XỬ LÝ VÉ CŨ ---
                oldTicket.Status = TicketStatus.Rescheduled.ToString();

                // --- BƯỚC D: XỬ LÝ BOOKING FLIGHT MỚI ---
                var existingNewBF = await db.BookingFlights
                    .FirstOrDefaultAsync(bf => bf.BookingId == oldTicket.BookingFlight.BookingId
                                            && bf.FlightId == request.NewFlightId);

                BookingFlight targetBookingFlight;
                if (existingNewBF != null)
                {
                    targetBookingFlight = existingNewBF;
                }
                else
                {
                    targetBookingFlight = new BookingFlight
                    {
                        BookingId = oldTicket.BookingFlight.BookingId,
                        FlightId = request.NewFlightId,
                        Status = BookingFlightStatus.Booked.ToString()
                    };
                    db.BookingFlights.Add(targetBookingFlight);
                    await db.SaveChangesAsync(); // Cần ID cho vé mới
                }

                // --- BƯỚC E: TẠO VÉ MỚI (CLONE) ---
                var newTicket = new Ticket
                {
                    TicketId = Guid.NewGuid(),
                    BookingFlightId = targetBookingFlight.BookingFlightId,

                    // Copy thông tin Passenger từ vé cũ (Dùng ID)
                    PassengerId = oldTicket.PassengerId,
                    SeatClassId = oldTicket.SeatClassId,

                    // Sinh mã vé mới (vd: Random hoặc format riêng)
                    TicketNumber = "TK" + DateTime.Now.Ticks.ToString().Substring(10),

                    Status = TicketStatus.Issued.ToString(),

                    // Không gán Price vì class Ticket của bạn không có thuộc tính Price
                    SeatNumber = null // Reset ghế, user sẽ chọn sau hoặc auto-assign
                };
                db.Tickets.Add(newTicket);

                await db.SaveChangesAsync();
                await transaction.CommitAsync();

                return new RescheduleResultDto
                {
                    IsSuccess = true,
                    RescheduleRequestId = rescheduleReq.RescheduleRequestId,
                    TicketId = oldTicket.TicketId,
                    OldFlightId = oldTicket.BookingFlight.FlightId,
                    NewFlightId = request.NewFlightId
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new RescheduleResultDto { IsSuccess = false, ErrorMessage = "System Error: " + ex.Message };
            }
        }
        public async Task<List<AvailableFlightDto>> SearchAvailableFlightsForRescheduleAsync(Guid ticketId, DateTime newDate)
        {
            using var db = DIContainer.CreateDb();

            // Lấy vé gốc + flight + seat class
            var ticket = await db.Tickets
                .Include(t => t.BookingFlight)
                    .ThenInclude(bf => bf.Flight)
                .FirstOrDefaultAsync(t => t.TicketId == ticketId);

            if (ticket == null)
                throw new Exception("Ticket not found");

            var originalFlight = ticket.BookingFlight.Flight;
            var seatClassId = ticket.SeatClassId;

            var dateOnly = newDate.Date;
            var now = DateTime.Now;

            // Query flight cùng route, cùng airline, đúng ngày mới
            var flightsQuery = db.Flights
                .Include(f => f.FlightPricings)
                    .ThenInclude(fp => fp.FareRule)
                .Where(f =>
                    f.AirlineId == originalFlight.AirlineId &&
                    f.DepartureAirportId == originalFlight.DepartureAirportId &&
                    f.ArrivalAirportId == originalFlight.ArrivalAirportId &&
                    f.FlightDate == dateOnly &&
                    f.Status == "Available");

            // Nếu là hôm nay thì lọc thêm giờ bay > hiện tại
            flightsQuery = flightsQuery.Where(f =>
                f.FlightDate > now.Date ||
                (f.FlightDate == now.Date && f.DepartureTime > now.TimeOfDay));

            var flights = await flightsQuery.ToListAsync();

            // Map sang DTO – KHÔNG dùng First mà dùng FirstOrDefault rồi skip null
            var result = flights
                .Select(f =>
                {
                    var pricing = f.FlightPricings
                        .FirstOrDefault(fp => fp.SeatClassId == seatClassId);

                    // Flight này không có pricing cho hạng ghế của vé gốc → bỏ qua
                    if (pricing == null)
                        return null;

                    return new AvailableFlightDto
                    {
                        FlightId = f.FlightId,
                        FlightNumber = f.FlightNumber,
                        DepartureTime = f.DepartureTime,
                        ArrivalTime = f.ArrivalTime,
                        Price = pricing.Price,
                        FareRuleCode = pricing.FareRule?.FareCode ?? "UNKNOWN"
                    };
                })
                .Where(dto => dto != null)
                .ToList()!; // đã filter null

            return result;
        }
    }
}
