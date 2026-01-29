using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.AirlineReservation.Presentation__WinForms_.Views.Forms.Common;
using AirlineReservation_AR.src.Domain.DTOs;
using AirlineReservation_AR.src.Domain.Exceptions;
using AirlineReservation_AR.src.Infrastructure.DI;
using AirlineReservation_AR.src.Presentation__Winform_.Views.popup;

namespace AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.User
{
    public partial class UC_FilloutInform : UserControl
    {
        //private FlightResultDTO _selectedFlight, _selectedRetrunFlight;

        //private Dictionary<string, ServiceOption> _passengerServices = new();
        //List<PassengerWithServicesDTO> _passengerBundles = new();

        //private Dictionary<string, ServiceOption> _passengerServicesReturn = new();
        //List<PassengerWithServicesDTO> _passengerBundlesReturn = new();

        private FlightSearchParams _searchParams;

        private FlightSegmentContext _outboundSegment;
        private FlightSegmentContext _returnSegment; // null nếu one-way
        private bool isRoundTripCheck = false;
        private LoadingForm _loadingForm;
        public UC_FilloutInform()
        {
            InitializeComponent();
        }

        //public void SetFlightData(FlightResultDTO flight, FlightResultDTO returnFlight ,FlightSearchParams p)
        //{
        //    _selectedFlight = flight;
        //    _selectedRetrunFlight = returnFlight;
        //    _searchParams = p;
        //    RenderContactForm();
        //    RenderPassengers();
        //    renderPassengerSummary();
        //    RenderDefaultSummary();
        //}

        public void SetFlightData(FlightResultDTO flight, FlightResultDTO returnFlight, FlightSearchParams p, bool isRoudTrip)
                {
                    _searchParams = p;
                    isRoundTripCheck = isRoudTrip;

                    _outboundSegment = new FlightSegmentContext
                    {
                        Flight = flight
                    };

                    if (returnFlight != null)
                    {
                        _returnSegment = new FlightSegmentContext
                        {
                            Flight = returnFlight
                        };
                    }

                    RenderContactForm();
                    RenderPassengers();

                    InitPassengerServices(_outboundSegment);

                    if (_returnSegment != null)
                        InitPassengerServices(_returnSegment);

                    RenderDefaultSummary();
        }

        private void InitPassengerServices(FlightSegmentContext segment)
        {
            for (int i = 1; i <= _searchParams.Adult; i++)
                segment.PassengerServices.TryAdd($"Adult {i}", new ServiceOption());

            for (int i = 1; i <= _searchParams.Child; i++)
                segment.PassengerServices.TryAdd($"Child {i}", new ServiceOption());

            for (int i = 1; i <= _searchParams.Infant; i++)
                segment.PassengerServices.TryAdd($"Infant {i}", new ServiceOption());
        }



        private void RenderContactForm()
        {
            contactPnl.Controls.Clear();
            var contact = new ContactFormFill();
            contact.Dock = DockStyle.Top;
            contactPnl.Controls.Add(contact);
        }

        private void RenderPassengers()
        {
            formPnl.Controls.Clear();

            void AddPassenger(string type, int index)
            {
                var pass = new PassengerFormFill();
                pass.PassengerType = type;
                pass.PassengerIndex = index;
                pass.PassengerTitle = $"{type} {index}";
                pass.TogglePassportSection(IsInternationalFlight());
                pass.Margin = new Padding(0, 10, 0, 10);
                formPnl.Controls.Add(pass);
            }

            for (int i = 1; i <= _searchParams.Adult; i++) AddPassenger("Adult", i);
            for (int i = 1; i <= _searchParams.Child; i++) AddPassenger("Child", i);
            for (int i = 1; i <= _searchParams.Infant; i++) AddPassenger("Infant", i);
        }

        private bool IsInternationalFlight()
        {
            return _outboundSegment.Flight.FromAirportCode != "VN"
                   && _outboundSegment.Flight.ToAirportCode != "VN";
        }

        private List<PassengerDTO> BuildPassengerList()
        {
            var list = new List<PassengerDTO>();

            foreach (var control in formPnl.Controls) // FIX HERE
            {
                if (control is PassengerFormFill pf)
                {
                    if (!pf.ValidatePassenger())
                    {
                        MessageBox.Show("Vui lòng điền đầy đủ thông tin hành khách!",
                            "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return null;
                    }

                    list.Add(pf.GetPassenger());
                }
            }
            return list;
        }
        private void btnAddBaggage_Click(object sender, EventArgs e)
        {
            var popup = new PopupAddBaggage(_outboundSegment.PassengerServices, _returnSegment.PassengerServices, isRoundTripCheck);

            popup.OnServicesChanged += service =>
            {
                _outboundSegment.PassengerServices = service;
                RenderDefaultSummary();

            };
            popup.OnServicesReturnChanged += service =>
            {
                _returnSegment.PassengerServices = service;
                RenderDefaultSummary();
            };
            popup.ShowDialog();
        }

        //private void renderPassengerSummary()
        //{
        //    // Adult
        //    for (int i = 1; i <= _searchParams.Adult; i++)
        //    {
        //        string key = $"Adult {i}";
        //        if (!_passengerServices.ContainsKey(key))
        //            _passengerServices[key] = new ServiceOption();
        //    }

        //    // Child
        //    for (int i = 1; i <= _searchParams.Child; i++)
        //    {
        //        string key = $"Child {i}";
        //        if (!_passengerServices.ContainsKey(key))
        //            _passengerServices[key] = new ServiceOption();
        //    }

        //    // Infant
        //    for (int i = 1; i <= _searchParams.Infant; i++)
        //    {
        //        string key = $"Infant {i}";
        //        if (!_passengerServices.ContainsKey(key))
        //            _passengerServices[key] = new ServiceOption();
        //    }
        //}
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            ShowLoading();

            try
            {
                // 1. Validate contact info
                var contact = GetContactForm();
                if (contact == null || !contact.ValidateContact())
                {
                        AnnouncementForm announcementForm = new AnnouncementForm();
                        announcementForm.SetAnnouncement("Vui long kiểm tra lại thông tin liên hệ", "Thông tin liên hệ chưa hợp lệ" , false, null);
                        announcementForm.Show();
                    return;
                }

                // 2. Validate passenger info
                var passengers = BuildPassengerList();
                if (passengers == null)
                    return;

                // 3. Build DTOs
                var outboundDto = BuildBookingDTO(_outboundSegment);
                BookingCreateDTO returnDto = null;

                if (_returnSegment != null)
                    returnDto = BuildBookingDTO(_returnSegment);

                // 4. Save booking
                int bookingId = DIContainer.BookingController.CreateBooking(
                    outboundDto,
                    returnDto
                );

                // 5. Payment
                var form = new MomoQR.MomoQR();
                form.SetPayment(bookingId, outboundDto.TotalAmount);
                form.ShowDialog();
        }
            catch (BusinessException ex)
            {
                AnnouncementForm announcementForm = new AnnouncementForm();
                announcementForm.SetAnnouncement("Lỗi nghiệp vụ", ex.Message, false, null);
                announcementForm.Show();
            }
            catch (Exception ex)
            {
                // TODO: log ex (file / db / serilog)
                AnnouncementForm announcementForm = new AnnouncementForm();
                announcementForm.SetAnnouncement("Hệ thống gặp sự cố, vui lòng thử lại sau", "Lỗi hệ thống:" + ex.Message , false, null);
                announcementForm.Show();

            }
            finally
            {
    CloseLoading();
        }
        }



        private ContactFormFill GetContactForm()
        {
            return contactPnl.Controls.OfType<ContactFormFill>().FirstOrDefault();
        }

        private List<PassengerFormFill> GetPassengerForms()
        {
            return formPnl.Controls.OfType<PassengerFormFill>().ToList();
        }
        private void BuildPassengerEntityList()
        {
            _outboundSegment.PassengerBundles.Clear();
            _returnSegment?.PassengerBundles.Clear();

            foreach (var pf in GetPassengerForms())
            {
                var dto = pf.GetPassenger();
                string key = $"{dto.PassengerType} {dto.Index}";

                var passenger = new Passenger
                {
                    PassengerType = dto.PassengerType,
                    Title = dto.Title,
                    FirstName = dto.FirstName,
                    MiddleName = dto.MiddleName,
                    LastName = dto.LastName,
                    DateOfBirth = dto.DateOfBirth,
                    Gender = dto.Gender,
                    IdNumber = dto.PassportNumber,
                    Nationality = dto.Nationality,
                    CountryIssue = dto.CountryIssue,
                    ExpireDatePassport = dto.PassportExpire
                };

                // OUTBOUND
                _outboundSegment.PassengerBundles.Add(new PassengerWithServicesDTO
                {
                    Passenger = passenger,
                    SelectedServices = _outboundSegment.PassengerServices[key]
                });

                // RETURN
                if (_returnSegment != null)
                {
                    _returnSegment.PassengerBundles.Add(new PassengerWithServicesDTO
                    {
                        Passenger = passenger,
                        SelectedServices = _returnSegment.PassengerServices[key]
                    });
                }
            }
        }



        private BookingCreateDTO BuildBookingDTO(FlightSegmentContext _selectedFlight) 
        { 
            var contactForm = GetContactForm();
            var contact = contactForm?.GetContact();
            var passengers = GetPassengerForms().Select(x => x.GetPassenger()).ToList();
            BuildPassengerEntityList();
            var pricing = CalculatePricing(_selectedFlight, _searchParams.Adult, _searchParams.Child, _searchParams.Infant);
            var user = DIContainer.CurrentUser;
            return new BookingCreateDTO
            {
                UserId = user.UserId,
                ContactEmail = contact?.Email ?? "",
                ContactPhone = contact?.Phone ?? "",
                SpecialRequest = "",
                FlightId = _selectedFlight.Flight.FlightId,
                TripType = _searchParams.ReturnDate == null ? "OneWay" : "RoundTrip",
                TotalAmount = pricing.TotalAmount,
                TaxAmount = pricing.TaxAmount,
                TotalFee = 0,
                Passengers = _selectedFlight.PassengerBundles,
            };
        }

        private void RenderDefaultSummary()
        {
            summaryBookingControl1.SetData(
                _outboundSegment.Flight,
                _returnSegment?.Flight,
                _outboundSegment.PassengerServices,
                _returnSegment?.PassengerServices
            );
        }

        private void CloseLoading()
        {
            if (_loadingForm != null && !_loadingForm.IsDisposed)
            {
                _loadingForm.Close();
                _loadingForm = null;
            }
        }

        private void ShowLoading()
        {
            if (_loadingForm == null || _loadingForm.IsDisposed)
            {
                _loadingForm = new LoadingForm();
                _loadingForm.Show();
                _loadingForm.BringToFront();
            }
        }

        public static PricingResult CalculatePricing(
            FlightSegmentContext segment,
            int adult,
            int child,
            int infant)
        {
            if (segment == null)
                throw new BusinessException("Flight segment is null");

            if (segment.Flight == null)
                throw new BusinessException("Flight data is missing");

            var basePrice = segment.Flight.Price;

            // 1 Flight price (GIỐNG SUMMARY)
            decimal flightPrice =
                (basePrice * adult) +
                (basePrice * 0.75m * child) +
                (basePrice * 0.1m * infant);

            //  Service price
            decimal servicePrice = segment.PassengerServices
                .Values
                .Sum(s => s.totalPrice);

            // 3 Tax (10% trên flight)
            decimal taxRate = 0.1M;
            decimal tax = flightPrice * taxRate;

            // 4 Total
            decimal total = flightPrice + servicePrice + tax;

            // 5 Set ngược lại context (để UI + DTO dùng)
            segment.TotalFlightPrice = flightPrice;
            segment.TotalServicePrice = servicePrice;

            return new PricingResult
            {
                FlightPrice = flightPrice,
                ServicePrice = servicePrice,
                TaxAmount = tax,
                TotalAmount = total
            };
        }



    }
}
