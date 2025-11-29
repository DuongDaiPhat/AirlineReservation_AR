using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AirlineReservation_AR.src.Domain.DTOs;
using AirlineReservation_AR.src.Infrastructure.DI;
using AirlineReservation_AR.src.Presentation__Winform_.Controllers;
using AirlineReservation_AR.src.Presentation__Winform_.Views.popup;
using Guna.UI2.WinForms;

namespace AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.User
{
    public partial class UC_FlightSearchResult : UserControl
    {
        private readonly FlightSearchParams _params;
        private readonly FlightController _controller;

        public UC_FlightSearchResult(FlightSearchParams p)
        {
            InitializeComponent();
            _params = p;
            _controller = DIContainer.FlightController;
        }

        private async void UC_FlightSearchResult_Load(object sender, EventArgs e)
        {
            var result = await _controller.SearchAsync(_params);

            RenderDayTabs(result.DayTabs);
            RenderAllFlights(result.AllFlights);
            RenderAirlineFilters(result.AirlineFilters);
        }


        // ------------------------
        // Render Day Tabs
        // ------------------------
        private void RenderDayTabs(List<FlightDayPriceDTO> list)
        {
            flowDayTabs.Controls.Clear();

            DateTime today = DateTime.Today;

            foreach (var item in list)
            {
                bool isSelected = item.Date.Date == _params.StartDate.Value.Date;
                bool isPast = item.Date.Date < today;

                var panel = new Guna2Panel
                {
                    Width = 140,
                    Height = 65,
                    BorderRadius = 10,
                    FillColor = isSelected
                                ? Color.FromArgb(0, 64, 200)
                                : (isPast ? Color.FromArgb(235, 235, 235) : Color.White),
                    BackColor = Color.Transparent,
                    Margin = new Padding(8),
                    Cursor = isPast ? Cursors.Default : Cursors.Hand,
                    Enabled = !isPast   // Disable ngày quá khứ
                };

                Label lblDate = new Label
                {
                    Text = item.Date.ToString("ddd, dd MMM"),
                    Dock = DockStyle.Top,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = isSelected
                                ? Color.White
                                : (isPast ? Color.Gray : Color.Black)
                };

                Label lblPrice = new Label
                {
                    Text = item.LowestPrice == 0 ? "-" : $"{item.LowestPrice:N0} VND",
                    Dock = DockStyle.Bottom,
                    Font = new Font("Segoe UI", 9, FontStyle.Regular),
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = isSelected
                                ? Color.White
                                : (isPast ? Color.Gray : Color.Black)
                };

                panel.Controls.Add(lblPrice);
                panel.Controls.Add(lblDate);

                // chỉ allow click nếu còn hợp lệ
                if (!isPast)
                {
                    panel.Click += async (s, e) =>
                    {
                        _params.StartDate = item.Date;
                        var result = await _controller.SearchAsync(_params);

                        RenderDayTabs(result.DayTabs);
                        RenderAllFlights(result.AllFlights);
                    };
                }

                flowDayTabs.Controls.Add(panel);
            }
        }




        // ------------------------
        // Render All Flights
        // ------------------------
        private void RenderAllFlights(List<FlightResultDTO> list)
        {
            flowFlightCards.Controls.Clear();

            foreach (var f in list)
            {
                var card = new FlightCardControl(f);
                card.Margin = new Padding(
                    (flowFlightCards.Width - card.Width) / 2,
                    10,
                    0,
                    10
                );

                card.OnSelected += async (selectedFlight) =>
                {
                    // Tổng số người lớn + trẻ em
                    int totalPeople = _params.Adult + _params.Child;

                    // Lấy DisplayName của seatclass hiện tại
                    string selectedClassName = selectedFlight.SelectedSeatClassName;

                    // Ghế còn lại
                    int seatsLeft = 0;
                    selectedFlight.SeatsLeftByClass.TryGetValue(selectedClassName, out seatsLeft);

                    // Nếu không đủ ghế → bật popup
                    if (seatsLeft < totalPeople)
                    {
                        var popup = new AdjustSearchParamsForm(_params);

                        if (popup.ShowDialog() == DialogResult.OK)
                        {
                            // cập nhật lại params
                            _params.Adult = popup.UpdatedParams.Adult;
                            _params.Child = popup.UpdatedParams.Child;
                            _params.Infant = popup.UpdatedParams.Infant;
                            _params.SeatClassId = popup.UpdatedParams.SeatClassId;

                            // Lấy lại displayName sau update
                            string updatedClassName = selectedFlight.SeatClassesMap[_params.SeatClassId];

                            int updatedTotal = _params.Adult + _params.Child;
                            int updatedSeats = selectedFlight.SeatsLeftByClass[updatedClassName];

                            if (updatedSeats < updatedTotal)
                            {
                                MessageBox.Show(
                                    "Hạng ghế này vẫn không đủ số ghế.\nVui lòng chọn chuyến bay khác.",
                                    "Không đủ ghế",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning
                                );
                                return;
                            }
                        }
                        else
                        {
                            return;
                        }
                    }

                    // đủ ghế → đi tiếp
                    OpenFilloutInform(selectedFlight);
                };

                flowFlightCards.Controls.Add(card);
            }
        }



        // ------------------------
        // Airline Filter
        // ------------------------
        private void RenderAirlineFilters(List<AirlineFilterDTO> list)
        {
            airlineCombobox.Items.Clear();

            foreach (var a in list)
                airlineCombobox.Items.Add(a.AirlineName);
        }



        private void OpenFilloutInform(FlightResultDTO flight)
        {
            var form = this.FindForm() as MainTravelokaForm;
            if (form == null) return;

            var screen = new UC_FilloutInform();
            screen.SetFlightData(flight, _params);

            form.SwitchScreen(screen);
        }
    }
}
