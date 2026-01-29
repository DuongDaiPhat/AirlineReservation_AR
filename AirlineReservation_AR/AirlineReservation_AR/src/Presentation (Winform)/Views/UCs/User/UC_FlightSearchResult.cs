using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using AirlineReservation_AR.src.AirlineReservation.Presentation__WinForms_.Views.Forms.Common;
using AirlineReservation_AR.src.Application.Services.AI_Service;
using AirlineReservation_AR.src.Domain.DTOs;
using AirlineReservation_AR.src.Domain.DTOs.AI_DTO;
using AirlineReservation_AR.src.Infrastructure.DI;
using AirlineReservation_AR.src.Presentation__Winform_.Controllers;
using AirlineReservation_AR.src.Presentation__Winform_.Views.popup;
using Guna.UI2.WinForms;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.User
{
    public partial class UC_FlightSearchResult : UserControl
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private const string API_BASE_URL = "http://localhost:5080";

        private readonly FlightSearchParams _params;
        private readonly FlightController _controller;

        private List<FlightResultDTO> _outboundOriginal = new();
        private List<FlightResultDTO> _outboundFiltered = new();

        private List<FlightResultDTO> _returnOriginal = new();
        private List<FlightResultDTO> _retrurnFlightFillterd = new();
        private const int MinPrice = 500_000;
        private const int MaxPrice = 10_000_000;
        private const int PriceBuffer = 500_000;

        private string _selectedBudget = "ALL";
        private string _selectedTimeRange = "ALL";

        private FlightResultDTO _selectedOutboundFlight = new();
        private FlightResultDTO _selectedReturnFlight = new();
        private LoadingForm _loadingForm;
        private bool isReturnPage = false;
        private DateTime _startDateOrigin;

        private System.Windows.Forms.Timer _chatAnimTimer;
        private bool _isOpening = false;
        private int _targetWidth = 322;   // width khi mở hoàn toàn
        private int _step = 20;

        public List<FlightDayPriceDTO> _originDayTabs { get; set; }
        public List<FlightDayPriceDTO> _originDayTabReturn { get; set; }

        public List<AirlineFilterDTO> _originReturnAirlineFilters { get; set; }

        public UC_FlightSearchResult(FlightSearchParams p)
        {
            InitializeComponent();
            _params = p;
            _controller = DIContainer.FlightController;
            _startDateOrigin = p.StartDate ?? DateTime.Today;
        }

        private async void UC_FlightSearchResult_Load(object sender, EventArgs e)
        {

            ShowLoading();
            var result = await _controller.SearchAsync(_params);

            _outboundOriginal = result.OutboundFlights ?? new();
            _outboundFiltered = new List<FlightResultDTO>(_outboundOriginal);
            _returnOriginal = result.ReturnFlights ?? new();
            _retrurnFlightFillterd = new List<FlightResultDTO>(_returnOriginal);
            _originDayTabs = result.DayTabs;
            _originDayTabReturn = result.DayTabReturn;
            _originReturnAirlineFilters = result.RetrunAirlineFilters;
            guna2CustomCheckBox1.Checked = _params.RoundTrip;

            _chatAnimTimer = new System.Windows.Forms.Timer();
            _chatAnimTimer.Interval = 15; // càng nhỏ càng mượt
            _chatAnimTimer.Tick += ChatAnimTimer_Tick;

            ChatBox.Width = 0;
            ChatBox.Visible = false;
            InitPriceTrackBar();
            InitBudgetButtons();
            InitDepartureTimeButtons();
            DisableReturnTimeButtons();

            RenderDayTabs(result.DayTabs);
            RenderAirlineFilters(result.AirlineFilters);
            RenderAllFlights(_outboundFiltered);
            LoadYourFlights();
            CloseLoading();
        }

        public async Task ReLoad()
        {
            ShowLoading();
            var result = await _controller.SearchAsync(_params);

            _outboundOriginal = result.OutboundFlights ?? new();
            _outboundFiltered = new List<FlightResultDTO>(_outboundOriginal);
            _returnOriginal = result.ReturnFlights ?? new();
            _retrurnFlightFillterd = new List<FlightResultDTO>(_returnOriginal);
            _originDayTabs = result.DayTabs;
            _originDayTabReturn = result.DayTabReturn;
            _originReturnAirlineFilters = result.RetrunAirlineFilters;
            guna2CustomCheckBox1.Checked = _params.RoundTrip;
            _chatAnimTimer = new System.Windows.Forms.Timer();
            _chatAnimTimer.Interval = 15; // càng nhỏ càng mượt
            _chatAnimTimer.Tick += ChatAnimTimer_Tick;

            ChatBox.Width = 0;
            ChatBox.Visible = false;
            InitPriceTrackBar();
            InitBudgetButtons();
            InitDepartureTimeButtons();
            DisableReturnTimeButtons();

            RenderDayTabs(result.DayTabs);
            RenderAirlineFilters(result.AirlineFilters);
            RenderAllFlights(_outboundFiltered);

            LoadYourFlights();
            CloseLoading();
        }
        private void LoadYourFlights()
        {
            fromAirportLeftLB.Text = _params.FromCity;
            toAirportLeftLB.Text = _params.ToCity;
            deDateLeftLB.Text = _params.StartDate?.ToString("ddd, dd MMM yyyy");

            if (_params.RoundTrip)
            {
                reDateLeftLB.Text = _params.ReturnDate?.ToString("ddd, dd MMM yyyy");
                reLeftPicture.Enabled = true;
                fromAirportReLeftLB.Text = _params.ToCity;
                leftIcon.Enabled = true;
                toAirportReLeftLB.Text = _params.FromCity;
                guna2PictureBox1.Enabled = true;
                toAirportLeftLB.Enabled = true;
                leftIcon.Enabled = true;

                reDateLeftLB.ForeColor = Color.DimGray;
                reLeftPicture.FillColor = Color.DodgerBlue;
                fromAirportReLeftLB.ForeColor = Color.DodgerBlue;
                leftIcon.FillColor = Color.DodgerBlue;
                toAirportReLeftLB.ForeColor = Color.DodgerBlue;

                reBtn1.Enabled = true;
                reBtn2.Enabled = true;
                reBtn3.Enabled = true;
                reBtn4.Enabled = true;
                reBtn1.FillColor = Color.White;
                reBtn2.FillColor = Color.White;
                reBtn3.FillColor = Color.White;
                reBtn4.FillColor = Color.White;
            }
            else
            {
                reLeftPicture.Enabled = false;
                reDateLeftLB.Enabled = false;
                fromAirportReLeftLB.Enabled = false;
                leftIcon.Enabled = false;
                toAirportReLeftLB.Enabled = false;
                leftIcon.Enabled = false;
                guna2PictureBox1.Enabled = false;
                reLeftPicture.FillColor = Color.LightGray;
                fromAirportReLeftLB.ForeColor = Color.LightGray;
                leftIcon.FillColor = Color.LightGray;
                toAirportReLeftLB.ForeColor = Color.LightGray;
                DisableReturnTimeButtons();
            }


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
                DateTime selectedDate = isReturnPage
                        ? _params.ReturnDate.Value.Date
                        : _params.StartDate.Value.Date;

                bool isSelected = item.Date.Date == selectedDate;
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
                        ShowLoading();
                        if (isReturnPage == false)
                        {
                            _params.StartDate = item.Date;
                            var r = await _controller.SearchAsync(_params);

                            _outboundOriginal = r.OutboundFlights ?? new();
                            _outboundFiltered = new List<FlightResultDTO>(_outboundOriginal);
                            _returnOriginal = r.ReturnFlights ?? new();
                            _retrurnFlightFillterd = new List<FlightResultDTO>(_returnOriginal);

                            RenderDayTabs(r.DayTabs);
                            RenderAirlineFilters(r.AirlineFilters);

                            ApplyAllFilters();
                            LoadYourFlights();
                            CloseLoading();
                        }
                        else
                        {
                            _params.ReturnDate = item.Date;
                            var r = await _controller.SearchAsync(_params);

                            _outboundOriginal = r.OutboundFlights ?? new();
                            _outboundFiltered = new List<FlightResultDTO>(_outboundOriginal);
                            _returnOriginal = r.ReturnFlights ?? new();
                            _retrurnFlightFillterd = new List<FlightResultDTO>(_returnOriginal);

                            RenderDayTabs(r.DayTabReturn);
                            RenderAirlineFilters(r.RetrunAirlineFilters);

                            ApplyAllFilters();
                            CloseLoading();
                        }

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



        private void ShowReturnFlightUI(List<FlightResultDTO> list)
        {
            ShowLoading();
            // 1. Xóa card outbound
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

                // Khi chọn chuyến về → qua fill thông tin
                card.OnSelected += (selectedReturn) =>
                {
                    _selectedReturnFlight = selectedReturn;
                    int sl = _params.Adult + _params.Child + _params.Infant;
                    var owner = this.FindForm();

                    var comFirm = new ComfirmFlights();
                    comFirm.setData(_selectedOutboundFlight, _selectedReturnFlight, sl, isReturnPage);
                    comFirm.StartPosition = FormStartPosition.CenterParent;
                    comFirm.OnComfirm += () =>
                    {
                        OpenFilloutInform(_selectedOutboundFlight, _selectedReturnFlight);

                        return;
                    };
                    comFirm.OnCancel += async () =>
                    {
                        await ReLoad();

                        AnnouncementForm announcementForm = new AnnouncementForm();
                        announcementForm.SetAnnouncement("Chuyến bay đã được hũy thành công", "Quý khách có thể chọn lại chuyến bay mới", true, null);
                        announcementForm.Show();

                        return;
                    };
                    comFirm.ShowDialog(owner);
                    CloseLoading();
                    //OpenFilloutInform(_selectedOutboundFlight, _selectedReturnFlight);
                };

                flowFlightCards.Controls.Add(card);
            }
        }


        private void HandleSelectedFlight(FlightResultDTO selected)
        {
            ShowLoading();
            int totalPeople = _params.Adult + _params.Child;

            selected.SeatsLeftByClass.TryGetValue(selected.SelectedSeatClassName, out int seatsLeft);

            if ((seatsLeft < totalPeople))
            {
                var popup = new AdjustSearchParamsForm(_params);
                if (popup.ShowDialog() != DialogResult.OK) return;

                _params.Adult = popup.UpdatedParams.Adult;
                _params.Child = popup.UpdatedParams.Child;
                _params.Infant = popup.UpdatedParams.Infant;
                _params.SeatClassId = popup.UpdatedParams.SeatClassId;
            }
            if (_params.RoundTrip)
            {

                _selectedOutboundFlight = selected;
                isReturnPage = true;
                RenderDayTabs(_originDayTabReturn);
                RenderAirlineFilters(_originReturnAirlineFilters);
                ShowReturnFlightUI(_retrurnFlightFillterd);
                CloseLoading();
                return;


            }
            int sl = _params.Adult + _params.Child + _params.Infant;
            var owner = this.FindForm();

            var comFirm = new ComfirmFlights();
            comFirm.setData(selected, _selectedReturnFlight, sl, isReturnPage);
            comFirm.StartPosition = FormStartPosition.CenterParent;

            comFirm.OnComfirm += () =>
            {
                OpenFilloutInform(selected, _selectedReturnFlight);

            };
            comFirm.OnCancel += async () =>
            {
                await ReLoad();
                AnnouncementForm announcementForm = new AnnouncementForm();
                announcementForm.SetAnnouncement("Chuyến bay đã được hũy thành công", "Quý khách có thể chọn lại chuyến bay mới", false, null);
                announcementForm.Show();
                return;
            };
            comFirm.ShowDialog(owner);
            CloseLoading();
            //OpenFilloutInform(selected, _selectedReturnFlight);
        }

        private void OpenFilloutInform(FlightResultDTO flight, FlightResultDTO retrunFlight)
        {
            var form = this.FindForm() as MainTravelokaForm;
            if (form == null) return;

            var screen = new UC_FilloutInform();
            screen.SetFlightData(flight, retrunFlight, _params, isReturnPage);
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
            List<FlightResultDTO> list = isReturnPage
                ? _returnOriginal.ToList()
                : _outboundOriginal.ToList();


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

            if (isReturnPage)
            {
                ShowReturnFlightUI(list);
                return;
            }
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
            _retrurnFlightFillterd = new List<FlightResultDTO>(_returnOriginal);
            // Render
            if (isReturnPage)
            {

                RenderAllFlights(_retrurnFlightFillterd);
                return;
            }
            RenderAllFlights(_outboundFiltered);

        }

        private void CloseLoading()
        {
            if (_loadingForm != null && !_loadingForm.IsDisposed)
            {
                _loadingForm.Close();
                _loadingForm = null;
            }
        }

        private void ShowLoading()
        {
            if (_loadingForm == null || _loadingForm.IsDisposed)
            {
                _loadingForm = new LoadingForm();
                _loadingForm.Show();
                _loadingForm.BringToFront();
            }
        }

        private async void RoundTripChecked(object sender, EventArgs e)
        {

            _params.RoundTrip = guna2CustomCheckBox1.Checked;
            isReturnPage = false;

            _params.ReturnDate = _params.RoundTrip
                ? _params.ReturnDate ?? _startDateOrigin.AddDays(1)
                : null;
            await ReLoad();
        }

        private async void Swap_Click(object sender, EventArgs e)
        {
            string tmpCity = _params.FromCity;
            string tmpCityCode = _params.FromCode;

            _params.FromCity = _params.ToCity;
            _params.FromCode = _params.ToCode;
            _params.ToCity = tmpCity;
            _params.ToCode = tmpCityCode;

            await ReLoad();
        }

        private void AIClick_Click(object sender, EventArgs e)
        {
            _isOpening = !ChatBox.Visible;
            ChatBox.Visible = !ChatBox.Visible;
            _chatAnimTimer.Start();
        }

        private void ChatAnimTimer_Tick(object sender, EventArgs e)
        {
            if (_isOpening)
            {
                ChatBox.Width += _step;

                if (ChatBox.Width >= _targetWidth)
                {
                    ChatBox.Width = _targetWidth;
                    _chatAnimTimer.Stop();
                }
            }
            else
            {
                ChatBox.Width -= _step;

                if (ChatBox.Width <= 0)
                {
                    ChatBox.Width = 0;
                    ChatBox.Visible = false;
                    _chatAnimTimer.Stop();
                }
            }
        }


        private async void CallAI_Click(object sender, EventArgs e)
        {
            try
            {
                ShowLoading();
                if (string.IsNullOrWhiteSpace(txtAI.Text))
                {
                    MessageBox.Show("Please enter preference", "Notification");
                    return;
                }


                // Clear input
                string userInput = txtAI.Text;
                txtAI.Clear();

                var request = new
                {
                    userText = userInput
                };
                var json = JsonSerializer.Serialize(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(
                    $"{API_BASE_URL}/v1/api/AI/analyze-preference",
                    content
                );

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"API Error: {errorContent}");
                }

                var responseJson = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<ApiResponse>(
                     responseJson,
                     new JsonSerializerOptions
                     {
                         PropertyNameCaseInsensitive = true
                     }
                 );

                if (!result.success)
                {
                    throw new Exception(result.error ?? "Unknown error");
                }

                var userPreference = result.data;

                List<FlightResultDTO> sourceFlights = isReturnPage
                    ? _returnOriginal
                    : _outboundOriginal;

                var bestFlightService = new BestFlightService(sourceFlights);
                var rankedFlights = bestFlightService.RankFlights(
                    sourceFlights,
                    userPreference,
                    f => 0
                );

                if (isReturnPage)
                {
                    ShowReturnFlightUI(rankedFlights);
                    CloseLoading();
                    return;
                }
                RenderAllFlights(rankedFlights);
                CloseLoading();
            }
            catch (Exception ex)
            {
                CloseLoading();
                AnnouncementForm announcementForm = new AnnouncementForm();
                announcementForm.SetAnnouncement(
                    "System Error",
                    $"Cannot connect to AI: {ex.Message}",
                    false,
                    null
                );
                announcementForm.Show();
            }
        }

    }
}
