using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
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

        private List<FlightResultDTO> _outboundOriginal = new();
        private List<FlightResultDTO> _outboundFiltered = new();

        private const int MinPrice = 500_000;
        private const int MaxPrice = 10_000_000;
        private const int PriceBuffer = 500_000;

        private string _selectedBudget = "ALL";
        private string _selectedTimeRange = "ALL";

        public UC_FlightSearchResult(FlightSearchParams p)
        {
            InitializeComponent();
            _params = p;
            _controller = DIContainer.FlightController;
        }

        private async void UC_FlightSearchResult_Load(object sender, EventArgs e)
        {
            var result = await _controller.SearchAsync(_params);

            _outboundOriginal = result.OutboundFlights ?? new();
            _outboundFiltered = new List<FlightResultDTO>(_outboundOriginal);

            InitPriceTrackBar();
            InitBudgetButtons();
            InitDepartureTimeButtons();
            DisableReturnTimeButtons();

            RenderDayTabs(result.DayTabs);
            RenderAirlineFilters(result.AirlineFilters);
            RenderAllFlights(_outboundFiltered);

            LoadYourFlights();
        }

        private void LoadYourFlights()
        {
            fromAirportLeftLB.Text = _params.FromCity;
            toAirportLeftLB.Text = _params.ToCity;
            deDateLeftLB.Text = _params.StartDate?.ToString("ddd, dd MMM yyyy");

            reLeftPicture.Visible = false;
            reDateLeftLB.Visible = false;
            fromAirportReLeftLB.Visible = false;
            leftIcon.Visible = false;
            toAirportReLeftLB.Visible = false;

            tableLayoutPanel2.RowStyles[3].Height = 0;
            tableLayoutPanel2.RowStyles[4].Height = 0;
        }

        private void DisableReturnTimeButtons()
        {
            reBtn1.Enabled = false;
            reBtn2.Enabled = false;
            reBtn3.Enabled = false;
            reBtn4.Enabled = false;

            reBtn1.FillColor = Color.LightGray;
            reBtn2.FillColor = Color.LightGray;
            reBtn3.FillColor = Color.LightGray;
            reBtn4.FillColor = Color.LightGray;
        }

        // Departure Time Buttons
        private void InitDepartureTimeButtons()
        {
            deBtn1.Click += (s, e) => SelectTimeRange("00-06", deBtn1);
            deBtn2.Click += (s, e) => SelectTimeRange("06-12", deBtn2);
            deBtn3.Click += (s, e) => SelectTimeRange("12-18", deBtn3);
            deBtn4.Click += (s, e) => SelectTimeRange("18-24", deBtn4);
        }

        private void SelectTimeRange(string range, Guna2Button btn)
        {
            _selectedTimeRange = range;
            ResetTimeButtonsStyle();
            HighlightButton(btn);
            ApplyAllFilters();
        }

        private void ResetTimeButtonsStyle()
        {
            var list = new[] { deBtn1, deBtn2, deBtn3, deBtn4 };
            foreach (var b in list)
            {
                b.FillColor = Color.White;
                b.ForeColor = Color.DodgerBlue;
                b.BorderColor = Color.DodgerBlue;
            }
        }

        private void HighlightButton(Guna2Button btn)
        {
            btn.FillColor = Color.DodgerBlue;
            btn.ForeColor = Color.White;
        }

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
                    Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold),
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
                        var r = await _controller.SearchAsync(_params);

                        _outboundOriginal = r.OutboundFlights ?? new();
                        _outboundFiltered = new List<FlightResultDTO>(_outboundOriginal);

                        RenderDayTabs(r.DayTabs);
                        RenderAirlineFilters(r.AirlineFilters);

                        ApplyAllFilters();
                        LoadYourFlights();
                    };
                }

                flowDayTabs.Controls.Add(panel);
            }
        }

        private void RenderAllFlights(List<FlightResultDTO> list)
        {
            flowFlightCards.Controls.Clear();
            list = list.OrderBy(f => f.DepartureTime).ToList();

            foreach (var f in list)
            {
                var card = new FlightCardControl(f);
                card.Margin = new Padding(
                    (flowFlightCards.Width - card.Width) / 2,
                    10,
                    0,
                    10
                );


                card.OnSelected += HandleSelectedFlight;
                flowFlightCards.Controls.Add(card);
            }
        }

        private void HandleSelectedFlight(FlightResultDTO selected)
        {
            int totalPeople = _params.Adult + _params.Child;

            selected.SeatsLeftByClass.TryGetValue(selected.SelectedSeatClassName, out int seatsLeft);

            if (seatsLeft < totalPeople)
            {
                var popup = new AdjustSearchParamsForm(_params);
                if (popup.ShowDialog() != DialogResult.OK) return;

                _params.Adult = popup.UpdatedParams.Adult;
                _params.Child = popup.UpdatedParams.Child;
                _params.Infant = popup.UpdatedParams.Infant;
                _params.SeatClassId = popup.UpdatedParams.SeatClassId;
            }

            OpenFilloutInform(selected);
        }

        private void OpenFilloutInform(FlightResultDTO flight)
        {
            var form = this.FindForm() as MainTravelokaForm;
            if (form == null) return;

            var screen = new UC_FilloutInform();
            screen.SetFlightData(flight, _params);
            form.SwitchScreen(screen);
        }

        private void RenderAirlineFilters(List<AirlineFilterDTO> list)
        {
            airlineCombobox.Items.Clear();
            airlineCombobox.Items.Add("Tất cả hãng");

            foreach (var item in list)
                airlineCombobox.Items.Add(item.AirlineName);

            airlineCombobox.SelectedIndex = 0;
            airlineCombobox.SelectedIndexChanged += (s, e) => ApplyAllFilters();
        }

        private void InitPriceTrackBar()
        {
            trackBarPrice.Minimum = 0;
            trackBarPrice.Maximum = 100;
            trackBarPrice.Value = 100;

            labelPriceValue.Text = $"{MinPrice:N0} VND - {MaxPrice:N0} VND";

            trackBarPrice.ValueChanged += (s, e) =>
            {
                int realPrice = ScalePrice(trackBarPrice.Value);
                labelPriceValue.Text = $"{MinPrice:N0} VND - {realPrice:N0} VND";
                ApplyAllFilters();
            };
        }

        private int ScalePrice(int percent)
        {
            float p = percent / 100f;
            return (int)(MinPrice + p * (MaxPrice - MinPrice));
        }

        private void InitBudgetButtons()
        {
            economyBtn.Click += (s, e) => SelectBudget("BUDGET", economyBtn);
            preEconomyBtn.Click += (s, e) => SelectBudget("LOWER", preEconomyBtn);
            businessBtn.Click += (s, e) => SelectBudget("UPPER", businessBtn);
            firstBtn.Click += (s, e) => SelectBudget("PREMIUM", firstBtn);
        }

        private void SelectBudget(string budget, Guna2Button btn)
        {
            _selectedBudget = budget;

            ResetBudgetButtonsStyle();
            HighlightButton(btn);

            ApplyAllFilters();
        }

        private void ResetBudgetButtonsStyle()
        {
            var list = new[] { economyBtn, preEconomyBtn, businessBtn, firstBtn };
            foreach (var b in list)
            {
                b.FillColor = Color.White;
                b.ForeColor = Color.DodgerBlue;
            }
        }

        private void ApplyAllFilters()
        {
            var list = _outboundOriginal.ToList();

            if (airlineCombobox.SelectedIndex > 0)
            {
                string airline = airlineCombobox.SelectedItem.ToString();
                list = list.Where(f => f.AirlineName == airline).ToList();
            }

            int price = ScalePrice(trackBarPrice.Value) + PriceBuffer;
            list = list.Where(f => f.Price <= price).ToList();

            switch (_selectedBudget)
            {
                case "BUDGET":
                    list = list.Where(f => f.Price <= 2_500_000).ToList();
                    break;
                case "LOWER":
                    list = list.Where(f => f.Price > 2_500_000 && f.Price <= 5_000_000).ToList();
                    break;
                case "UPPER":
                    list = list.Where(f => f.Price > 5_000_000 && f.Price <= 7_000_000).ToList();
                    break;
                case "PREMIUM":
                    list = list.Where(f => f.Price > 7_000_000).ToList();
                    break;
            }

            if (_selectedTimeRange != "ALL")
            {
                var parts = _selectedTimeRange.Split('-');
                int from = int.Parse(parts[0]);
                int to = int.Parse(parts[1]);

                list = list.Where(f =>
                {
                    double h = f.DepartureTime.TotalHours;
                    return h >= from && h < to;
                }).ToList();
            }

            list = list.OrderBy(f => f.DepartureTime).ToList();

            _outboundFiltered = list;
            RenderAllFlights(list);
        }

        private void resetBtn_Click(object sender, EventArgs e)
        {
            // Airline
            airlineCombobox.SelectedIndex = 0;

            // Price
            trackBarPrice.Value = 100;
            labelPriceValue.Text = $"{MinPrice:N0} VND - {MaxPrice:N0} VND";

            // Budget
            _selectedBudget = "ALL";
            ResetBudgetButtonsStyle();

            // Time range
            _selectedTimeRange = "ALL";
            ResetTimeButtonsStyle();

            // Restore original flight list
            _outboundFiltered = new List<FlightResultDTO>(_outboundOriginal);

            // Render
            RenderAllFlights(_outboundFiltered);
        }
    }
}
