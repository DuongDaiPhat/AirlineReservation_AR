using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AirlineReservation_AR.src.Domain.DTOs;

namespace AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.User
{

    public partial class SummaryBookingControl : UserControl
    {
        private FlightResultDTO _flight;
        private FlightSearchParams _params;
        private List<PassengerDTO> _passengers;

        public SummaryBookingControl()
        {
            InitializeComponent();
        }

        public void SetData(
            FlightResultDTO flight,
            FlightSearchParams p,
            List<PassengerDTO> list)
        {
            _flight = flight;
            _params = p;
            _passengers = list;

            RenderFlightInfo();
            RenderSummaryList();
            RenderTotal();
        }

        // -----------------------------------------------------------
        //  Flight Info Box
        // -----------------------------------------------------------
        private void RenderFlightInfo()
        {
            lblRoute.Text = $"{_flight.FromAirportCode} → {_flight.ToAirportCode}";
            lblTime.Text = $"{_flight.DepartureTime:hh\\:mm} → {_flight.ArrivalTime:hh\\:mm}";
            lblAirline.Text = $"{_flight.AirlineName}";
            lblDates.Text = $"{_flight.FlightDate:ddd, dd MMM yyyy}";
        }

        // -----------------------------------------------------------
        //  Price List
        // -----------------------------------------------------------
        private void RenderSummaryList()
        {
            flowPriceList.Controls.Clear();

            int adults = _params.Adult;
            int children = _params.Child;
            int infants = _params.Infant;

            AddItem($"Người lớn x{adults}",
                adults * _flight.Price);

            if (children > 0)
                AddItem($"Trẻ em x{children}",
                    children * (_flight.Price * 0.67M)); // ví dụ Traveloka

            if (infants > 0)
                AddItem($"Em bé x{infants}",
                    infants * (_flight.Price * 0.1M));
        }

        private void AddItem(string name, decimal price)
        {
            var lbl = new Label
            {
                Text = $"{name}",
                Font = new Font("Segoe UI", 10),
                AutoSize = true
            };

            var lblP = new Label
            {
                Text = $"{price:N0} VND",
                Font = new Font("Segoe UI", 10),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleRight
            };

            var panel = new Panel
            {
                Width = 320,
                Height = 25
            };

            lbl.Location = new Point(0, 3);
            lblP.Location = new Point(200, 3);

            panel.Controls.Add(lbl);
            panel.Controls.Add(lblP);

            flowPriceList.Controls.Add(panel);
        }

        // -----------------------------------------------------------
        // Total
        // -----------------------------------------------------------
        private void RenderTotal()
        {
            decimal total = 0, totalVAT = 0;


            total += (_params.Adult * _flight.Price);
            total += (_params.Child * (_flight.Price * 0.9M));
            total += (_params.Infant * (_flight.Price * 0.7M));
            totalVAT += total * 1.1M;
            pricingVAT.Text = $"{total:N0} VND";
            lblTotalPrice.Text = $"{totalVAT:N0} VND";
        }

        public decimal TotalPrice
        {
            get
            {
                var text = lblTotalPrice.Text
                    .Replace("VND", "")
                    .Replace(",", "")
                    .Trim();

                if (decimal.TryParse(text, out var value))
                    return value;

                return 0;
            }
        }

    }
}
