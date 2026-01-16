using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.Domain.DTOs;
using AirlineReservation_AR.src.Infrastructure.DI;

namespace AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.User
{
    public partial class SummaryBookingControl : UserControl
    {
        private Dictionary<string, ServiceOption> _dataPassenger;
        public decimal _totalFlightPrice = 0;
        public decimal _totalServicePrice = 0;
        public SummaryBookingControl()
        {
            InitializeComponent();

        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {

        }

        private void FlightTicketSummary_Load(object sender, EventArgs e)
        {

        }
        public void SetData(FlightResultDTO flight,FlightSearchParams _param, Dictionary<string, ServiceOption> dataPasenger)
        {
            _dataPassenger = dataPasenger;
            var user = DIContainer.CurrentUser;
            txtPassengerInfo.RightToLeft = RightToLeft.Yes;
            txtPassengerInfo.Text = $"Client: {user.FullName.ToString()}";
            txtFromToPlace.Text = $"{flight.FromAirportName.ToString()}({flight.FromAirportCode.ToString()}) -> {flight.ToAirportName.ToString()}({flight.ToAirportCode.ToString()})";
            txtAirline.Text = $"{flight.AirlineName.ToString()}";
            txtSeatClass.Text = $"{flight.SelectedSeatClassName.ToString()}";
            txtTOTime.Text = $"{flight.DepartureTime.ToString()}";
            txtTODay.Text = $"{flight.FlightDate.ToString("ddd, dd MMM", System.Globalization.CultureInfo.InvariantCulture)}";
            txtTOYear.Text = $"{flight.FlightDate.ToString("yyyy", System.Globalization.CultureInfo.InvariantCulture)}";
            txtArrTime.Text = $"{flight.ArrivalTime.ToString()}";
            txtArrDay.Text = $"{flight.FlightDate.AddMinutes(flight.DurationMinutes).ToString("ddd, dd MMM", System.Globalization.CultureInfo.InvariantCulture)}";
            txtArrYear.Text = $"{flight.FlightDate.AddMinutes(flight.DurationMinutes).ToString("yyyy", System.Globalization.CultureInfo.InvariantCulture)}";
            txtEstimateTime.Text = $"{(flight.DurationMinutes / 60).ToString("00")}h {(flight.DurationMinutes % 60).ToString("00")}m";
            pasengerCbx.DataSource = _dataPassenger.ToList();
            pasengerCbx.DisplayMember = "Key";
            pasengerCbx.ValueMember = "Value";

            int nAdult = _dataPassenger.Count(kvp => kvp.Key.StartsWith("Adult"));
            int nChild = _dataPassenger.Count(kvp => kvp.Key.StartsWith("Child"));
            int nInfant = _dataPassenger.Count(kvp => kvp.Key.StartsWith("Infant"));

            _totalFlightPrice = (flight.Price * nAdult) + (flight.Price * 0.75m * nChild) + (flight.Price * 0.1m * nInfant);
            _totalServicePrice = _dataPassenger.Values.Sum(s => s.totalPrice);
            txtFlightPrice.Text = $"{_totalFlightPrice.ToString("N0")} VND";
            txtServicePrice.Text = $"{_totalServicePrice.ToString("N0")} VND";
            txtTotal.Text = $"{(_totalFlightPrice + _totalServicePrice).ToString("N0")} VND";
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pasengerCbx.SelectedItem is KeyValuePair<string, ServiceOption> item)
            {
                loadService(item.Value);
            }
        }
        private void loadService(ServiceOption option)
        {
            if (option.Baggage != null)
            {
                baggageTxt.Text = $"{option.Baggage.ServiceName.ToString()}";
                baggagePrice.Text = $"{option.Baggage.BasePrice.ToString("N0")} VND";
            }
            else
            {
                baggageTxt.Text = "No Baggage";
                baggagePrice.Text = "0";
            }

            if (option.Meal != null)
            {
                mealTxt.Text = $"{option.Meal.ServiceName.ToString()} VND";
                mealPrice.Text =$"{ option.Meal.BasePrice.ToString("N0")} VND" ;
            }
            else
            {
                mealTxt.Text = "No Meal";
                mealPrice.Text = "0";
            }
            if (option.Priority != null)
            {
                priorityTxt.Text = $"{option.Priority.ServiceName.ToString()}";
                priorityPrice.Text = $"{option.Priority.BasePrice.ToString("N0")} VND";
            }
            else
            {
                priorityTxt.Text = "No Priority";
                priorityPrice.Text = "0";

            }
        }
        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void txtPassengerInfo_Click(object sender, EventArgs e)
        {

        }

    }
}