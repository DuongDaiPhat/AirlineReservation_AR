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

        private Dictionary<string, ServiceOption> _passengerServices = new();
        List<PassengerWithServicesDTO> _passengerBundles = new();
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
            renderPassengerSummary();
            RenderDefaultSummary();
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
            renderPassengerSummary();
            var popup = new PopupAddBaggage(_passengerServices);
            popup.OnServicesChanged += service =>
            {
                _passengerServices = service;
                RenderDefaultSummary();

            };
            popup.ShowDialog();
        }

        private void renderPassengerSummary()
        {
            // Adult
            for (int i = 1; i <= _searchParams.Adult; i++)
            {
                string key = $"Adult {i}";
                if (!_passengerServices.ContainsKey(key))
                    _passengerServices[key] = new ServiceOption();
            }

            // Child
            for (int i = 1; i <= _searchParams.Child; i++)
            {
                string key = $"Child {i}";
                if (!_passengerServices.ContainsKey(key))
                    _passengerServices[key] = new ServiceOption();
            }

            // Infant
            for (int i = 1; i <= _searchParams.Infant; i++)
            {
                string key = $"Infant {i}";
                if (!_passengerServices.ContainsKey(key))
                    _passengerServices[key] = new ServiceOption();
            }
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
            _passengerBundles.Clear();
            foreach (var pf in GetPassengerForms())
            {
                if (!pf.ValidatePassenger())
                {
                    MessageBox.Show("Passenger information is invalid");
                    return;
                }

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
                string key = $"{dto.PassengerType} {dto.Index}";
                var service = _passengerServices[key];

                _passengerBundles.Add(new PassengerWithServicesDTO
                {
                    Passenger = passenger,
                    SelectedServices = service
                });

            }

        }


        private BookingCreateDTO BuildBookingDTO()
        {
            var contactForm = GetContactForm();
            var contact = contactForm?.GetContact();
            var passengers = GetPassengerForms().Select(x => x.GetPassenger()).ToList();
            BuildPassengerEntityList();
            var user = DIContainer.CurrentUser;
            return new BookingCreateDTO
            {
                UserId = user.UserId,
                ContactEmail = contact?.Email ?? "",
                ContactPhone = contact?.Phone ?? "",
                SpecialRequest = "",
                FlightId = _selectedFlight.FlightId,
                TripType = _searchParams.ReturnDate == null ? "OneWay" : "RoundTrip",
                TotalAmount = summaryBookingControl1._totalServicePrice * 1.1M + summaryBookingControl1._totalFlightPrice,
                TaxAmount = (summaryBookingControl1._totalServicePrice * 0.1M),
                TotalFee = 0,
                Passengers = _passengerBundles,
            };
        }

        private void RenderDefaultSummary()
        {
          summaryBookingControl1.SetData(_selectedFlight, _searchParams, _passengerServices);
        }


    }
}
