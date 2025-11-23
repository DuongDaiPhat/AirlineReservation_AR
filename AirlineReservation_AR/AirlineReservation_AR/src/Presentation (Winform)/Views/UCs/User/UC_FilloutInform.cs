using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.Domain.DTOs;
using AirlineReservation_AR.src.Infrastructure.DI;

namespace AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.User
{
    public partial class UC_FilloutInform : UserControl
    {
        private FlightResultDTO _selectedFlight;
        private FlightSearchParams _searchParams;
        private List<BaggageDTO> _selectedBaggage = new List<BaggageDTO>();
        private List<Passenger> _passengerList;

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
            var list = BuildPassengerList();
            if (list != null) RenderSummary(list);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            var dto = BuildBookingDTO();
            int bookingId = DIContainer.BookingController.CreateBooking(dto);
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
                Passengers = BuildPassengerEntityList()
            };
        }
    }
}
