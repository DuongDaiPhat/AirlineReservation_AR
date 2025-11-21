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
using AirlineReservation_AR.src.Infrastructure.DI;
using AirlineReservation_AR.src.Presentation__Winform_.Controllers;
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
            //RenderBestFlight(result.BestFlight);
            RenderAllFlights(result.AllFlights);
            RenderAirlineFilters(result.AirlineFilters);
        }

        // ------------------------
        // Render Day Tabs
        // ------------------------
        private void RenderDayTabs(List<FlightDayPriceDTO> list)
        {
            flowDayTabs.Controls.Clear();

            foreach (var item in list)
            {
                var panel = new Guna2Panel
                {
                    Width = 140,
                    Height = 65,
                    BorderRadius = 10,
                    FillColor = item.Date.Date == _params.StartDate.Value.Date
                                ? Color.FromArgb(0, 64, 200)
                                : Color.White,
                    BackColor = Color.Transparent,
                    Margin = new Padding(8),
                    Cursor = Cursors.Hand
                };

                Label lblDate = new Label
                {
                    Text = item.Date.ToString("ddd, dd MMM"),
                    Dock = DockStyle.Top,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = item.Date == _params.StartDate ? Color.White : Color.Black
                };

                Label lblPrice = new Label
                {
                    Text = item.LowestPrice == 0 ? "-" : $"{item.LowestPrice:N0} VND",
                    Dock = DockStyle.Bottom,
                    Font = new Font("Segoe UI", 9, FontStyle.Regular),
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = item.Date == _params.StartDate ? Color.White : Color.Black
                };

                panel.Controls.Add(lblPrice);
                panel.Controls.Add(lblDate);

                panel.Click += async (s, e) =>
                {
                    _params.StartDate = item.Date;
                    var result = await _controller.SearchAsync(_params);

                    RenderDayTabs(result.DayTabs);
                    //RenderBestFlight(result.BestFlight);
                    RenderAllFlights(result.AllFlights);
                };

                flowDayTabs.Controls.Add(panel);
            }
        }


        //private void RenderBestFlight(FlightResultDTO dto)
        //{
        //    if (dto == null)
        //    {
        //        pnlBestFlight.Visible = false;
        //        return;
        //    }

        //    pnlBestFlight.Controls.Clear();
        //    pnlBestFlight.Visible = true;

        //    var card = new FlightCardControl(dto, highlight: true);
        //    card.Dock = DockStyle.Top;
        //    pnlBestFlight.Controls.Add(card);
        //}


        private void RenderAllFlights(List<FlightResultDTO> list)
        {
            flowFlightCards.Controls.Clear();

            foreach (var f in list)
            {
                var card = new FlightCardControl(f);
                card.Margin = new Padding(
                (flowFlightCards.Width - card.Width) / 2, // căn giữa
                10, // top
                0,
                10  // bottom
                );
                card.OnSelected += (selectedFlight) =>
                {
                    OpenFilloutInform(selectedFlight);
                };

                flowFlightCards.Controls.Add(card);
            }
        }

        private void RenderAirlineFilters(List<AirlineFilterDTO> list)
        {
            flowFilters.Controls.Clear();

            foreach (var a in list)
            {
                var chk = new Guna2CheckBox
                {
                    Text = a.AirlineName,
                    Font = new Font("Segoe UI", 10),
                    AutoSize = true,
                    Margin = new Padding(5)
                };

                flowFilters.Controls.Add(chk);
            }
        }

        private void OpenFilloutInform(FlightResultDTO flight)
        {
            var form = this.FindForm() as MainTravelokaForm;
            if (form == null) return;

            var screen = new UC_FilloutInform();
            screen.SetFlightData(flight, _params); // truyền dữ liệu search + flight đã chọn

            form.SwitchScreen(screen);
        }
        

    }
}
