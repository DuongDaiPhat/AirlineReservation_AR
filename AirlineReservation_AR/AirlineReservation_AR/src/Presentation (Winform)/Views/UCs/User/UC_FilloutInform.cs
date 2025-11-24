using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.Domain.DTOs;
using AirlineReservation_AR.src.Infrastructure.DI;
using AirlineReservation_AR.src.Presentation__Winform_.Views.popup;

namespace AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.User
{
    public partial class UC_FilloutInform : UserControl
    {
        private FlightResultDTO _selectedFlight;
        private FlightSearchParams _searchParams;
        private List<PassengerBaggageDTO> _savedBaggage = new();

        public UC_FilloutInform()
        {
            InitializeComponent();
        }

        public void SetFlightData(FlightResultDTO flight, FlightSearchParams p)
        {
            _selectedFlight = flight;
            _searchParams = p;
            RenderContactForm();
            RenderPassengers();
            RenderDefaultSummary();
        }

        private void RenderContactForm()
        {
            contactPnl.Controls.Clear();
            var contact = new ContactFormFill();
            contact.Dock = DockStyle.Top;
            contactPnl.Controls.Add(contact);
        }

        private void RenderSummary(List<PassengerDTO> passengers)
        {
            pnlSummary.Controls.Clear();
            var summary = new SummaryBookingControl();
            summary.Dock = DockStyle.Fill;
            summary.SetData(_selectedFlight, _searchParams, passengers);
            pnlSummary.Controls.Add(summary);
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
            return _selectedFlight.FromAirportCode != "VN"
                   && _selectedFlight.ToAirportCode != "VN";
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
            var types = new List<string>();

            // Lấy đúng loại từ PassengerFormFill
            foreach (var pf in GetPassengerForms())
                types.Add(pf.PassengerType);

            var popup = new PopupAddBaggage(_selectedFlight, types, _savedBaggage);

            if (popup.ShowDialog() == DialogResult.OK)
            {
                var list = BuildPassengerList();
                if (list != null) RenderSummary(list);

                var baggageList = popup.SelectedBaggage;
                UpdateSummaryWithBaggage(baggageList);

                MessageBox.Show("Đã chọn hành lý!",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _savedBaggage = popup.SelectedBaggage;
            }

        }

        private void UpdateSummaryWithBaggage(List<PassengerBaggageDTO> bag)
        {
            var summary = GetSummaryControl();
            if (summary == null) return;

            decimal baggageTotal = bag.Sum(x => x.Price);
            summary.ClearExtraCosts();
            summary.AddExtraCost("Hành lý", baggageTotal);
        }




        private void guna2Button2_Click(object sender, EventArgs e)
        {
            // 1. Validate contact info
            var contact = GetContactForm();
            if (contact == null || !contact.ValidateContact())
            {
                MessageBox.Show("Vui lòng kiểm tra lại thông tin liên hệ.",
                    "Thông tin chưa hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Validate all passenger forms
            var passengers = BuildPassengerList();
            if (passengers == null)
                return;

            // 3. Build DTO
            var dto = BuildBookingDTO();

            // 4. Save to DB
            int bookingId = DIContainer.BookingController.CreateBooking(dto);

            // 5. Payment
            var form = new MomoQR.MomoQR();
            form.SetPayment(bookingId, dto.TotalAmount);
            form.ShowDialog();
        }


        private SummaryBookingControl GetSummaryControl()
        {
            return pnlSummary.Controls.OfType<SummaryBookingControl>().FirstOrDefault();
        }

        private ContactFormFill GetContactForm()
        {
            return contactPnl.Controls.OfType<ContactFormFill>().FirstOrDefault();
        }

        private List<PassengerFormFill> GetPassengerForms()
        {
            return formPnl.Controls.OfType<PassengerFormFill>().ToList();
        }
        private List<Passenger> BuildPassengerEntityList()
        {
            var result = new List<Passenger>();

            foreach (var pf in GetPassengerForms())
            {
                var dto = pf.GetPassenger();

                string gender = null;

                switch (dto.Title?.Trim().ToLower())
                {
                    case "Mr":
                        gender = "Male";
                        break;

                    case "Ms":
                    case "Mrs":
                    case "Miss":
                        gender = "Female";
                        break;

                    default:
                        gender = dto.Gender;
                        break;
                }

                var passenger = new Passenger
                {
                    PassengerType = dto.PassengerType,
                    Title = dto.Title,
                    FirstName = dto.FirstName,
                    MiddleName = dto.MiddleName,
                    LastName = dto.LastName,
                    DateOfBirth = dto.DateOfBirth,
                    Gender = gender,
                    IdNumber = dto.PassportNumber,
                    Nationality = dto.Nationality,
                    CountryIssue = dto.CountryIssue,
                    ExpireDatePassport = dto.PassportExpire
                };

                result.Add(passenger);
            }

            return result;
        }


        private BookingCreateDTO BuildBookingDTO()
        {
            var contactForm = GetContactForm();
            var contact = contactForm?.GetContact();
            var passengers = GetPassengerForms().Select(x => x.GetPassenger()).ToList();
            var summary = GetSummaryControl();
            var user = DIContainer.CurrentUser;

            return new BookingCreateDTO
            {
                UserId = user.UserId,
                ContactEmail = contact?.Email ?? "",
                ContactPhone = contact?.Phone ?? "",
                SpecialRequest = "",
                FlightId = _selectedFlight.FlightId,
                TripType = _searchParams.ReturnDate == null ? "OneWay" : "RoundTrip",
                TotalAmount = summary != null ? summary.TotalPrice : 0,
                TaxAmount = summary!= null ? summary.TotalPrice*0.1M : 0,
                TotalFee = 0,
                Passengers = BuildPassengerEntityList(),
                Baggages = _savedBaggage

            };
        }

        private void RenderDefaultSummary()
        {
            pnlSummary.Controls.Clear();

            var defaultPassengers = new List<PassengerDTO>();
            int adults = _searchParams.Adult;
            int childs = _searchParams.Child;
            int infants = _searchParams.Infant;

            // Tạo DTO tạm mà không cần validate
            for (int i = 0; i < adults; i++)
                defaultPassengers.Add(new PassengerDTO { PassengerType = "Adult" });

            for (int i = 0; i < childs; i++)
                defaultPassengers.Add(new PassengerDTO { PassengerType = "Child" });

            for (int i = 0; i < infants; i++)
                defaultPassengers.Add(new PassengerDTO { PassengerType = "Infant" });

            var summary = new SummaryBookingControl();
            summary.Dock = DockStyle.Fill;
            summary.SetData(_selectedFlight, _searchParams, defaultPassengers);

            pnlSummary.Controls.Add(summary);
        }

    }
}
