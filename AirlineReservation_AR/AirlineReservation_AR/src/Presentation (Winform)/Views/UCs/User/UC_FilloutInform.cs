using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.Domain.DTOs;
using AirlineReservation_AR.src.Presentation__Winform_.Views.popup;

namespace AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.User
{
    public partial class UC_FilloutInform : UserControl
    {
        private FlightResultDTO _selectedFlight;
        private FlightSearchParams _searchParams;
        private List<BaggageDTO> _selectedBaggage = new List<BaggageDTO>();
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

            int y = 20;
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

            foreach (var control in contactPnl.Controls)
            {
                if (control is PassengerFormFill pf)
                {
                    // Validate form
                    if (!pf.ValidatePassenger())
                    {
                        MessageBox.Show("Vui lòng điền đầy đủ thông tin hành khách!",
                            "Thiếu thông tin",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return null;
                    }

                    // Convert sang DTO
                    var dto = pf.GetPassenger();
                    list.Add(dto);
                }
            }

            return list;
        }

        private void btnAddBaggage_Click(object sender, EventArgs e)
        {
            //var popup = new PopupAddBaggage(_selectedFli);
            var list = new List<PassengerDTO>();
            list = BuildPassengerList();
            RenderSummary(list);


        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }
    }
}
