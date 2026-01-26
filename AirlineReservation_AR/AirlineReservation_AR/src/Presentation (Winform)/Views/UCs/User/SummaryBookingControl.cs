using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using AirlineReservation_AR.src.Domain.DTOs;
using AirlineReservation_AR.src.Infrastructure.DI;

namespace AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.User
{
    public partial class SummaryBookingControl : UserControl
    {
        public enum SummaryViewMode
        {
            Outbound,
            Return
        }

        private SummaryViewMode _currentMode = SummaryViewMode.Outbound;

        private FlightResultDTO _outboundFlight;
        private FlightResultDTO _returnFlight;

        private Dictionary<string, ServiceOption> _outboundServices;
        private Dictionary<string, ServiceOption> _returnServices;

        private Dictionary<string, ServiceOption> _currentPassengerData;

        private decimal _segmentServicePrice;

        public SummaryBookingControl()
        {
            InitializeComponent();
        }

        // =========================
        // PUBLIC ENTRY
        // =========================
        public void SetData(
            FlightResultDTO outboundFlight,
            FlightResultDTO returnFlight,
            Dictionary<string, ServiceOption> outboundServices,
            Dictionary<string, ServiceOption> returnServices)
        {
            _outboundFlight = outboundFlight;
            _returnFlight = returnFlight;
            _outboundServices = outboundServices;
            _returnServices = returnServices;

            var user = DIContainer.CurrentUser;
            txtPassengerInfo.RightToLeft = RightToLeft.Yes;
            txtPassengerInfo.Text = $"Client: {user.FullName}";

            _currentMode = SummaryViewMode.Outbound;
            RenderCurrentMode();
        }

        // =========================
        // CORE RENDER
        // =========================
        private void RenderCurrentMode()
        {
            if (_currentMode == SummaryViewMode.Outbound)
            {
                RenderFlight(_outboundFlight, _outboundServices);
            }
            else
            {
                RenderFlight(_returnFlight, _returnServices);
            }

            RenderTotal();
        }

        private void RenderFlight(
            FlightResultDTO flight,
            Dictionary<string, ServiceOption> dataPassenger)
        {
            if (flight == null || dataPassenger == null) return;

            _currentPassengerData = dataPassenger;

            txtFromToPlace.Text =
                $"{flight.FromAirportName} ({flight.FromAirportCode}) → {flight.ToAirportName} ({flight.ToAirportCode})";

            txtAirline.Text = flight.AirlineName;
            txtSeatClass.Text = flight.SelectedSeatClassName;

            txtTOTime.Text = flight.DepartureTime.ToString();
            txtTODay.Text = flight.FlightDate.ToString("ddd, dd MMM", CultureInfo.InvariantCulture);
            txtTOYear.Text = flight.FlightDate.ToString("yyyy");

            txtArrTime.Text = flight.ArrivalTime.ToString();
            txtArrDay.Text = flight.FlightDate
                .AddMinutes(flight.DurationMinutes)
                .ToString("ddd, dd MMM", CultureInfo.InvariantCulture);
            txtArrYear.Text = flight.FlightDate
                .AddMinutes(flight.DurationMinutes)
                .ToString("yyyy");

            txtEstimateTime.Text =
                $"{flight.DurationMinutes / 60:00}h {flight.DurationMinutes % 60:00}m";

            pasengerCbx.DataSource = _currentPassengerData.ToList();
            pasengerCbx.DisplayMember = "Key";
            pasengerCbx.ValueMember = "Value";

           txtFlightPrice.Text =
                $"{CalculateTotalFlight(_outboundFlight, _outboundServices):N0} VND";
            
        }

        // =========================
        // PRICE CALCULATION
        // =========================
        private void CalculateSegmentPrice(
            FlightResultDTO flight,
            Dictionary<string, ServiceOption> data)
        {
            _segmentServicePrice = data.Values.Sum(s => s.totalPrice);

            txtServicePrice.Text = $"{_segmentServicePrice:N0} VND";
        }

        private decimal CalculateTotalFlight(
            FlightResultDTO flight,
            Dictionary<string, ServiceOption> passengers)
        {
            if (flight == null || passengers == null) return 0;

            int nAdult = passengers.Count(p => p.Key.StartsWith("Adult"));
            int nChild = passengers.Count(p => p.Key.StartsWith("Child"));
            int nInfant = passengers.Count(p => p.Key.StartsWith("Infant"));

            return
                (flight.Price * nAdult)
              + (flight.Price * 0.75m * nChild)
              + (flight.Price * 0.1m * nInfant);
        }

        private void RenderTotal()
        {
            decimal outboundFlightPrice =
                CalculateTotalFlight(_outboundFlight, _outboundServices);

            decimal returnFlightPrice =
                CalculateTotalFlight(_returnFlight, _returnServices);

            decimal serviceTotal =
                (_outboundServices?.Values.Sum(s => s.totalPrice) ?? 0)
              + (_returnServices?.Values.Sum(s => s.totalPrice) ?? 0);

            txtReturnPrice.Text =
                returnFlightPrice > 0 ? $"{returnFlightPrice:N0} VND" : "0 VND";

            txtTotal.Text =
                $"{(outboundFlightPrice + returnFlightPrice + serviceTotal):N0} VND";

            txtServicePrice.Text = $"{serviceTotal:N0} VND";
        }

        // =========================
        // PASSENGER SERVICE VIEW
        // =========================
        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pasengerCbx.SelectedItem is KeyValuePair<string, ServiceOption> item)
            {
                loadService(item.Value);
            }
        }

        private void loadService(ServiceOption option)
        {
            baggageTxt.Text = option.Baggage?.ServiceName ?? "No Baggage";
            baggagePrice.Text = option.Baggage != null
                ? $"{option.Baggage.BasePrice:N0} VND"
                : "0";

            mealTxt.Text = option.Meal?.ServiceName ?? "No Meal";
            mealPrice.Text = option.Meal != null
                ? $"{option.Meal.BasePrice:N0} VND"
                : "0";

            priorityTxt.Text = option.Priority?.ServiceName ?? "No Priority";
            priorityPrice.Text = option.Priority != null
                ? $"{option.Priority.BasePrice:N0} VND"
                : "0";
        }

        // =========================
        // MODE SWITCH
        // =========================
        private void OneWay_Click(object sender, EventArgs e)
        {
            _currentMode = SummaryViewMode.Outbound;
            RenderCurrentMode();
            OneWay.ForeColor = Color.FromArgb(37, 99, 235);
            OneWay.FillColor = Color.White;
            RoundTrip.ForeColor = Color.DimGray;
            RoundTrip.FillColor = Color.FromArgb(224, 224, 224);
        }

        private void RoundTrip_Click(object sender, EventArgs e)
        {
            if (_returnFlight == null) return;

            _currentMode = SummaryViewMode.Return;
            RenderCurrentMode();
            OneWay.ForeColor = Color.DimGray;
            OneWay.FillColor = Color.FromArgb(224, 224, 224);
            RoundTrip.ForeColor = Color.FromArgb(37, 99, 235);
            RoundTrip.FillColor = Color.White;
        }
    }
}
