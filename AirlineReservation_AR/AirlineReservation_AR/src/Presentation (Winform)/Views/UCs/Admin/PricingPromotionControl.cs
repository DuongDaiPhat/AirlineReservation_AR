using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.Application.Interfaces;
using AirlineReservation_AR.src.Domain.DTOs;
using AirlineReservation_AR.src.Infrastructure.DI;
using AirlineReservation_AR.src.Presentation__Winform_.Views.Forms.Admin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.Admin
{
    public partial class PricingPromotionControl : UserControl
    {
        //private List<FlightPricingDtoAdmin> _flightPricings = new List<FlightPricingDtoAdmin>();
        //private List<PromotionDtoAdmin> _promotions = new List<PromotionDtoAdmin>();
        private List<FlightPricingDtoAdmin> _flightPricings = new List<FlightPricingDtoAdmin>();
        private List<FlightPricingDtoAdmin> _filteredPricings = new List<FlightPricingDtoAdmin>();
        
        // Lazy Loading Pagination Fields
        private int _serverChunkSize = 50;
        private int _totalRecordCount = 0;
        private FlightPricingFilterDtoAdmin _currentPricingFilter = new FlightPricingFilterDtoAdmin();

        private List<PromotionDtoAdmin> _promotions = new List<PromotionDtoAdmin>();
        private List<PromotionDtoAdmin> _filteredPromotions = new List<PromotionDtoAdmin>();

        private int _currentPricingPage = 1;
        private int _pricingPageSize = 8;
        private int _totalPricingPages = 1;

        private int _currentPromoPage = 1;
        private int _promoPageSize = 8;
        private int _totalPromoPages = 1;
        
        // Concurrency flags
        private bool _isPromoProcessing = false;
        private bool _isPricingProcessing = false;
        private readonly ILookupService _lookupService = DIContainer.LookupService;
        
        public PricingPromotionControl()
        {
            InitializeComponent();
            InitializeCustomStyles();
            _ = InitializeFiltersAsync();  // Load from DB
            RegisterEventHandlers();
            LoadDataAsync();
            InitializePagination();
        }
        private void InitializePagination()
        {
            // Đăng ký events cho Pricing Pagination
            paginationPricing.PageChanged += PaginationPricing_PageChanged;
            paginationPricing.CurrentPage = 1;
            paginationPricing.TotalPages = 1;

            // Đăng ký events cho Promo Pagination
            paginationPromo.PageChanged += PaginationPromo_PageChanged;
            paginationPromo.CurrentPage = 1;
            paginationPromo.TotalPages = 1;
        }
        private async void PaginationPricing_PageChanged(object sender, int pageNumber)
        {
            _currentPricingPage = pageNumber;

            // Lazy Loading Logic
            int maxItemIndexNeeded = _currentPricingPage * _pricingPageSize;

            if (maxItemIndexNeeded > _filteredPricings.Count && _filteredPricings.Count < _totalRecordCount)
            {
                // Load Next Chunk
                Cursor = Cursors.WaitCursor;
                await LoadPricingChunkAsync(reset: false);
                Cursor = Cursors.Default;
            }
            else
            {
                // Have enough data, just render
                LoadPricingCards();
            }
        }

        private void PaginationPromo_PageChanged(object sender, int pageNumber)
        {
            _currentPromoPage = pageNumber;
            LoadPromoCards();
        }
        private void RegisterEventHandlers()
        {
            // Hủy đăng ký cũ (phòng trường hợp được gọi nhiều lần)
            btnSearchFlights.Click -= BtnSearchFlights_Click;
            txtPromoSearch.TextChanged -= TxtPromoSearch_TextChanged;
            cboPromoStatus.SelectedIndexChanged -= CboPromoStatus_SelectedIndexChanged;
            cboPromoType.SelectedIndexChanged -= CboPromoType_SelectedIndexChanged;
            cboPromoSort.SelectedIndexChanged -= CboPromoSort_SelectedIndexChanged;
            btnRefreshPricing.Click -= BtnRefreshPricing_Click;
            btnRefreshPromo.Click -= BtnRefreshPromo_Click;

            // Đăng ký mới
            btnSearchFlights.Click += BtnSearchFlights_Click;
            txtPromoSearch.TextChanged += TxtPromoSearch_TextChanged;
            cboPromoStatus.SelectedIndexChanged += CboPromoStatus_SelectedIndexChanged;
            cboPromoType.SelectedIndexChanged += CboPromoType_SelectedIndexChanged;
            cboPromoSort.SelectedIndexChanged += CboPromoSort_SelectedIndexChanged;
            btnRefreshPricing.Click += BtnRefreshPricing_Click;
            btnRefreshPromo.Click += BtnRefreshPromo_Click;
        }

        // Event handlers riêng biệt
        private void BtnSearchFlights_Click(object sender, EventArgs e)
        {
            ApplyFlightFilters();
        }

        private void TxtPromoSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyPromoFilters();
        }

        private void CboPromoStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyPromoFilters();
        }

        private void CboPromoType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyPromoFilters();
        }

        private void CboPromoSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyPromoFilters();
        }

        private void BtnRefreshPricing_Click(object sender, EventArgs e)
        {
            if (cboRoute.Items.Count > 0) cboRoute.SelectedIndex = 0;
            if (cboSeatClass.Items.Count > 0) cboSeatClass.SelectedIndex = 0;
            ApplyFlightFilters();
        }

        private void BtnRefreshPromo_Click(object sender, EventArgs e)
        {
            txtPromoSearch.Text = string.Empty;
            if (cboPromoStatus.Items.Count > 0) cboPromoStatus.SelectedIndex = 0;
            if (cboPromoType.Items.Count > 0) cboPromoType.SelectedIndex = 0;
            if (cboPromoSort.Items.Count > 0) cboPromoSort.SelectedIndex = 0;
        }
        private async void LoadDataAsync()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                // Load data using Static DI Container
                await LoadPricingDataAsync();
                await LoadPromotionDataAsync();

                // Update UI
                LoadPricingCards();
                LoadPromoCards();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        private async Task LoadPricingDataAsync()
        {
            _currentPricingFilter = new FlightPricingFilterDtoAdmin();
            await LoadPricingChunkAsync(reset: true);
        }

        private async Task LoadPricingChunkAsync(bool reset)
        {
            try 
            {
                if (reset)
                {
                    _currentPricingPage = 1;
                    _filteredPricings.Clear();
                }

                int currentCount = _filteredPricings.Count;
                // Calculate next chunk page index (1-based)
                int pageIndex = (currentCount / _serverChunkSize) + 1;

                var response = await DIContainer.PricingControllerAdmin.GetPricingsPage(
                    pageIndex, 
                    _serverChunkSize, 
                    _currentPricingFilter);

                if (response.Success)
                {
                    // Access named tuple elements
                    var items = response.Data.Items;
                    var total = response.Data.TotalCount;

                    if (items != null)
                    {
                        _filteredPricings.AddRange(items);
                        _totalRecordCount = total;
                    }
                    
                    UpdatePricingPagination();
                    
                    // Only reload cards if resetting or if current view is affected
                    if (reset || _currentPricingPage * _pricingPageSize > currentCount)
                    {
                        LoadPricingCards();
                    }
                }
                else
                {
                   if (reset) MessageBox.Show(response.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data chunk: {ex.Message}");
            }
        }

        private async Task LoadPromotionDataAsync()
        {
            var response = await DIContainer.PromotionControllerAdmin.GetAllPromotions();
            if (response.Success && response.Data != null)
            {
                _promotions = response.Data.ToList();
                _filteredPromotions = _promotions.ToList();
                _currentPromoPage = 1; // Reset to page 1
                UpdatePromoPagination();
            }
            else
            {
                MessageBox.Show(response.Message, "Notification",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void UpdatePricingPagination()
        {
            int totalRecords = _totalRecordCount; // Use Total Server Count
            _totalPricingPages = totalRecords > 0
                ? (int)Math.Ceiling((double)totalRecords / _pricingPageSize)
                : 1;

            if (_currentPricingPage > _totalPricingPages)
            {
                _currentPricingPage = _totalPricingPages;
            }

            paginationPricing.TotalPages = _totalPricingPages;
            paginationPricing.CurrentPage = _currentPricingPage;
        }

        private List<FlightPricingDtoAdmin> GetPagedPricings()
        {
            if (_filteredPricings == null || _filteredPricings.Count == 0)
                return new List<FlightPricingDtoAdmin>();

            _totalPricingPages = (int)Math.Ceiling((double)_filteredPricings.Count / _pricingPageSize);

            if (_currentPricingPage < 1)
                _currentPricingPage = 1;

            if (_currentPricingPage > _totalPricingPages)
                _currentPricingPage = _totalPricingPages;

            int skip = (_currentPricingPage - 1) * _pricingPageSize;

            return _filteredPricings
                .Skip(skip)
                .Take(_pricingPageSize)
                .ToList();
        }
        private void UpdatePromoPagination()
        {
            int totalRecords = _filteredPromotions.Count;
            _totalPromoPages = totalRecords > 0
                ? (int)Math.Ceiling((double)totalRecords / _promoPageSize)
                : 1;

            if (_currentPromoPage > _totalPromoPages)
            {
                _currentPromoPage = _totalPromoPages;
            }

            paginationPromo.TotalPages = _totalPromoPages;
            paginationPromo.CurrentPage = _currentPromoPage;
        }

        private List<PromotionDtoAdmin> GetPagedPromotions()
        {
            if (_filteredPromotions == null || _filteredPromotions.Count == 0)
                return new List<PromotionDtoAdmin>();

            _totalPromoPages = (int)Math.Ceiling((double)_filteredPromotions.Count / _promoPageSize);
            if (_currentPromoPage < 1)
                _currentPromoPage = 1;

            if (_currentPromoPage > _totalPromoPages)
                _currentPromoPage = _totalPromoPages;

            int skip = (_currentPromoPage - 1) * _promoPageSize;

            return _filteredPromotions
                .Skip(skip)
                .Take(_promoPageSize)
                .ToList();
        }

        private void InitializeCustomStyles()
        {
            // Style cho TabControl
            tabMain.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            tabMain.ItemSize = new Size(200, 45);
            tabMain.SizeMode = TabSizeMode.Fixed;

            // FIX: Set BackColor explicitly to prevent transparency overlap issues
            tabFlightPricing.BackColor = Color.White;
            tabPromotions.BackColor = Color.White;

            // FIX: Configure Layout for Grid View (Vertical Scroll)
            flpPricingCards.WrapContents = true;
            flpPricingCards.AutoScroll = true;
            flpPricingCards.FlowDirection = FlowDirection.LeftToRight;

            flpPromoCards.WrapContents = true;
            flpPromoCards.AutoScroll = true;
            flpPromoCards.FlowDirection = FlowDirection.LeftToRight;

            // FIX: Force Refresh on Tab Switch to prevent visual glitches
            tabMain.SelectedIndexChanged += (s, e) =>
            {
                tabMain.SelectedTab?.Refresh();
                
                if (tabMain.SelectedTab == tabFlightPricing) 
                {
                    flpPricingCards.Visible = false; // Toggle visibility to force redraw
                    flpPricingCards.Visible = true;
                }
                else if (tabMain.SelectedTab == tabPromotions)
                {
                    flpPromoCards.Visible = false;
                    flpPromoCards.Visible = true;
                }
            };

            // Style cho Panels
            StylePanel(pnlFlightFilters, Color.White);
            StylePanel(pnlPromoTop, Color.FromArgb(67, 233, 123));
            //StylePanel(pnlPromoStats, Color.WhiteSmoke);
            StylePanel(pnlPromoFilters, Color.White);

            // Style cho FlowLayoutPanels
            flpPricingCards.BackColor = Color.WhiteSmoke;
            flpPricingCards.Padding = new Padding(20);
            flpPromoCards.BackColor = Color.WhiteSmoke;
            flpPromoCards.Padding = new Padding(20);

            // Style cho Buttons
            StyleButton(btnSearchFlights, Color.FromArgb(52, 152, 219));
            StyleButton(btnAddPromo, Color.FromArgb(40, 167, 69));
        }
        private void StylePanel(Panel panel, Color backColor)
        {
            if (panel == null) return;
            panel.BackColor = backColor;
        }

        private void StyleButton(Control btn, Color color)
        {
            if (btn == null) return;

            btn.BackColor = color;
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
            btn.Height = 40;

            // Hover effect
            btn.MouseEnter += (s, e) =>
            {
                btn.BackColor = ControlPaint.Dark(color, 0.1f);
            };
            btn.MouseLeave += (s, e) =>
            {
                btn.BackColor = color;
            };

            if (btn is Button winButton)
            {
                winButton.FlatStyle = FlatStyle.Flat;
                winButton.FlatAppearance.BorderSize = 0;
            }
        }


        private async Task InitializeFiltersAsync()
        {
            try
            {
                // Flight Pricing Filters
                
                // Routes - Load from actual flight data
                var routes = await _lookupService.GetActiveRoutesAsync();
                cboRoute.Items.Clear();
                cboRoute.Items.Add("All");
                foreach (var route in routes)
                {
                    cboRoute.Items.Add(route);
                }
                cboRoute.DisplayMember = "DisplayName";
                cboRoute.SelectedIndex = 0;

                // Seat Classes - Load from DB
                var seatClasses = await _lookupService.GetSeatClassesAsync();
                cboSeatClass.Items.Clear();
                cboSeatClass.Items.Add("All");
                foreach (var sc in seatClasses)
                {
                    cboSeatClass.Items.Add(sc);
                }
                cboSeatClass.DisplayMember = "DisplayName";
                cboSeatClass.SelectedIndex = 0;

                // Promo Filters
                txtPromoSearch.PlaceholderText = "Search promo code or name...";

                cboPromoStatus.Items.AddRange(new object[]
                {
                    "All", "Active", "Paused", "Expired"
                });
                cboPromoStatus.SelectedIndex = 0;

                cboPromoType.Items.AddRange(new object[]
                {
                    "All", "Percent", "Fixed"
                });
                cboPromoType.SelectedIndex = 0;

                cboPromoSort.Items.AddRange(new object[]
                {
                    "Newest", "Most Used", "Highest Value"
                });
                cboPromoSort.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading filters: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //private void LoadStatCards()
        //{
        //    flpStats.Controls.Clear();

        //    var stats = new[]
        //    {
        //        new { Title = "Tổng mã KM", Value = "48", Color = Color.FromArgb(67, 233, 123) },
        //        new { Title = "Đang hoạt động", Value = "32", Color = Color.FromArgb(102, 126, 234) },
        //        new { Title = "Đã sử dụng", Value = "1,245", Color = Color.FromArgb(240, 147, 251) },
        //        new { Title = "Tiết kiệm", Value = "₫285M", Color = Color.FromArgb(79, 172, 254) }
        //    };

        //    foreach (var stat in stats)
        //    {
        //        var card = CreateStatCard(stat.Title, stat.Value, stat.Color);
        //        flpStats.Controls.Add(card);
        //    }
        //}
        //private Panel CreateStatCard(string title, string value, Color color)
        //{
        //    var card = new Panel
        //    {
        //        Size = new Size(250, 80),
        //        BackColor = color,
        //        Margin = new Padding(10)
        //    };

        //    // Rounded corners
        //    card.Paint += (s, e) =>
        //    {
        //        e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        //        using (var path = GetRoundedRectPath(card.ClientRectangle, 12))
        //        {
        //            card.Region = new Region(path);
        //        }
        //    };

        //    var lblTitle = new Label
        //    {
        //        Text = title,
        //        ForeColor = Color.White,
        //        Font = new Font("Segoe UI", 9),
        //        Location = new Point(15, 15),
        //        AutoSize = true,
        //        BackColor = Color.Transparent
        //    };

        //    var lblValue = new Label
        //    {
        //        Text = value,
        //        ForeColor = Color.White,
        //        Font = new Font("Segoe UI", 24, FontStyle.Bold),
        //        Location = new Point(15, 35),
        //        AutoSize = true,
        //        BackColor = Color.Transparent
        //    };

        //    card.Controls.AddRange(new Control[] { lblTitle, lblValue });

        //    // Hover effect
        //    card.Cursor = Cursors.Hand;
        //    card.MouseEnter += (s, e) => card.BackColor = ControlPaint.Dark(color, 0.1f);
        //    card.MouseLeave += (s, e) => card.BackColor = color;

        //    return card;
        //}
        private void LoadSampleData()
        {
            _flightPricings = new List<FlightPricingDtoAdmin>
            {
                //new FlightPricingDtoAdmin
                //{
                //    FlightNumber = "VN210",
                //    AirlineName = "Vietnam Airlines",
                //    Route = "SGN → HAN",
                //    SeatClass = "Economy",
                //    OriginalPrice = 2000000,
                //    DiscountPercent = 25,
                //    DiscountedPrice = 1500000,
                //    BookedSeats = 45,
                //    AvailableSeats = 135
                //},
                //new FlightPricingDtoAdmin
                //{
                //    FlightNumber = "VJ123",
                //    AirlineName = "VietJet Air",
                //    Route = "HAN → DAD",
                //    SeatClass = "Economy",
                //    OriginalPrice = 1200000,
                //    DiscountPercent = 30,
                //    DiscountedPrice = 840000,
                //    BookedSeats = 120,
                //    AvailableSeats = 60
                //}
            };

            _promotions = new List<PromotionDtoAdmin>
            {
                new PromotionDtoAdmin
                {
                    PromoCode = "SUMMER2024",
                    PromoName = "Ưu đãi mùa hè 2024",
                    DiscountType = "Percent",
                    DiscountValue = 15,
                    MinimumAmount = 1000000,
                    MaximumDiscount = 500000,
                    UsageCount = 245,
                    UsageLimit = 1000,
                    ValidFrom = DateTime.Now.AddDays(-30),
                    ValidTo = DateTime.Now.AddDays(30),
                    IsActive = true
                },
                new PromotionDtoAdmin
                {
                    PromoCode = "WELCOME50",
                    PromoName = "Khách hàng mới",
                    DiscountType = "Fixed",
                    DiscountValue = 50000,
                    MinimumAmount = 500000,
                    MaximumDiscount = 50000,
                    UsageCount = 89,
                    UsageLimit = 500,
                    ValidFrom = DateTime.Now.AddDays(-60),
                    ValidTo = DateTime.Now.AddDays(60),
                    IsActive = true
                }
            };
        }
        private async void ApplyFlightFilters()
        {
            if (_isPricingProcessing) return;
            
            try
            {
                _isPricingProcessing = true;
                Cursor = Cursors.WaitCursor;

                // Extract actual filter values from DTO objects
                string? routeFilter = null;
                if (cboRoute.SelectedItem is RouteSelectDto routeDto)
                {
                    routeFilter = routeDto.Code; // "HAN-SGN" format
                }
                else if (cboRoute.SelectedItem is string routeStr && routeStr != "All")
                {
                    routeFilter = routeStr;
                }

                string? seatClassFilter = null;
                if (cboSeatClass.SelectedItem is SeatClassSelectDto seatDto)
                {
                    seatClassFilter = seatDto.Code; // ClassName
                }
                else if (cboSeatClass.SelectedItem is string scStr && scStr != "All")
                {
                    seatClassFilter = scStr;
                }

                _currentPricingFilter = new FlightPricingFilterDtoAdmin
                {
                    Route = routeFilter,
                    SeatClass = seatClassFilter
                };

                await LoadPricingChunkAsync(reset: true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
                _isPricingProcessing = false;
            }
        }
        private async void ApplyPromoFilters()
        {
            // Prevent concurrent execution
            if (_isPromoProcessing) return;

            try
            {
                _isPromoProcessing = true;
                Cursor = Cursors.WaitCursor;

                var filter = new PromotionFilterDtoAdmin
                {
                    SearchTerm = txtPromoSearch.Text,
                    IsActive = GetPromoActiveStatus(),
                    DiscountType = cboPromoType.SelectedItem?.ToString(),
                    SortBy = GetPromoSortBy()
                };

                var response = await DIContainer.PromotionControllerAdmin.SearchPromotions(filter);
                if (response.Success && response.Data != null)
                {
                    _filteredPromotions = response.Data.ToList();
                    _currentPromoPage = 1; // Reset về trang 1 khi filter
                    UpdatePromoPagination();
                    LoadPromoCards();
                }
                else
                {
                    // Silent failure or log, avoiding popup spam on search
                    // Debug.WriteLine(response.Message);
                }
            }
            catch (Exception ex)
            {
                // Inspect exception to ignore task canceled or known concurrency issues during rapid typing
                if (!ex.Message.Contains("second operation")) {
                     MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                Cursor = Cursors.Default;
                _isPromoProcessing = false;
            }
        }

        private bool? GetPromoActiveStatus()
        {
            var selected = cboPromoStatus.SelectedItem?.ToString();
            return selected switch
            {
                "Active" => true,
                "Paused" => false,
                _ => null
            };
        }
        private string GetPromoSortBy()
        {
            var selected = cboPromoSort.SelectedItem?.ToString();
            return selected switch
            {
                "Most Used" => "most_used",
                "Highest Value" => "highest_value",
                _ => "newest"
            };
        }

        private bool _isLoadingPricing = false;
        private void LoadPricingCards()
        {
            if (_isLoadingPricing)
            {
                System.Diagnostics.Debug.WriteLine("⚠️ LoadPricingCards đang chạy, bỏ qua lần gọi này");
                return;
            }

            _isLoadingPricing = true;
            try
            {
                flpPricingCards.SuspendLayout();
                flpPricingCards.Controls.Clear();
                var paged = GetPagedPricings();
                System.Diagnostics.Debug.WriteLine($"📊 Loading {paged.Count} pricing cards");
                foreach (var pricing in paged)
                {
                    var card = CreatePricingCard(pricing);
                    flpPricingCards.Controls.Add(card);
                }

                if (paged.Count == 0)
                {
                    var lblEmpty = new Label
                    {

                        Text = "No pricing data available",
                        Font = new Font("Segoe UI", 12, FontStyle.Italic),
                        ForeColor = Color.Gray,
                        AutoSize = true,
                        Margin = new Padding(20)
                    };
                    flpPricingCards.Controls.Add(lblEmpty);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ Lỗi LoadPricingCards: {ex.Message}");
                MessageBox.Show($"Lỗi hiển thị giá vé: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                flpPricingCards.ResumeLayout();
                _isLoadingPricing = false;
            }
        }

        private Panel CreatePricingCard(FlightPricingDtoAdmin pricing)
        {
            var card = new Panel
            {
                Size = new Size(230, 280),
                BackColor = GetGradientColor(pricing.DiscountPercent),
                Margin = new Padding(10)
            };

            card.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (var path = GetRoundedRectPath(card.ClientRectangle, 10))
                {
                    card.Region = new Region(path);
                }
            };

            var lblFlight = new Label
            {
                Text = $"{pricing.FlightNumber}",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(10, 15),
                AutoSize = true,
                BackColor = Color.Transparent
            };

            var lblAirline = new Label 
            {
                 Text = pricing.AirlineName,
                 ForeColor = Color.White,
                 Font = new Font("Segoe UI", 8),
                 Location = new Point(10, 40),
                 AutoSize = true,
                 BackColor = Color.Transparent
            };

            var lblRoute = new Label
            {
                Text = pricing.Route,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9),
                Location = new Point(10, 60),
                AutoSize = true,
                BackColor = Color.Transparent
            };

            var lblDiscount = new Label
            {

                Text = $"{pricing.DiscountPercent}% OFF",
                BackColor = Color.FromArgb(100, 255, 255, 255),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Location = new Point(130, 15),
                AutoSize = true,
                Padding = new Padding(5, 2, 5, 2)
            };

            var lblOldPrice = new Label
            {
                Text = $"{pricing.OriginalPrice:N0} ₫",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Strikeout),
                Location = new Point(10, 110),
                AutoSize = true,
                BackColor = Color.Transparent
            };

            var lblNewPrice = new Label
            {
                Text = $"{pricing.DiscountedPrice:N0} ₫",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(10, 130),
                AutoSize = true,
                BackColor = Color.Transparent
            };

            var pnlInfo = new Panel
            {
                Location = new Point(10, 180),
                Size = new Size(210, 90),
                BackColor = Color.FromArgb(50, 255, 255, 255)
            };

            var lblClass = new Label
            {

                Text = $"Class: {pricing.SeatClass}",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 8),
                Location = new Point(5, 5),
                AutoSize = true,
                BackColor = Color.Transparent
            };

            var lblSeats = new Label
            {

                Text = $"Sold: {pricing.BookedSeats} | Avail: {pricing.AvailableSeats}",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 8),
                Location = new Point(5, 25),
                AutoSize = true,
                BackColor = Color.Transparent
            };

            pnlInfo.Controls.AddRange(new Control[] { lblClass, lblSeats });

            card.Controls.AddRange(new Control[]
            {
                lblFlight, lblAirline, lblRoute, lblDiscount, lblOldPrice, lblNewPrice, pnlInfo
            });

            card.Cursor = Cursors.Hand;
            card.Click += (s, e) => EditPricing(pricing);

            return card;
        }
        private void LoadPromoCards()
        {
            flpPromoCards.SuspendLayout();
            try
            {
                flpPromoCards.Controls.Clear();
                var paged = GetPagedPromotions();
                foreach (var promo in paged)
                {
                    var card = CreatePromoCard(promo);
                    flpPromoCards.Controls.Add(card);
                }

                if (paged.Count == 0)
                {
                    var lblEmpty = new Label
                    {
                        Text = "No promotion data available",
                        Font = new Font("Segoe UI", 12, FontStyle.Italic),
                        ForeColor = Color.Gray,
                        AutoSize = true,
                        Margin = new Padding(20)
                    };
                    flpPromoCards.Controls.Add(lblEmpty);
                }
            }
            finally
            {
                flpPromoCards.ResumeLayout();
            }
        }
        private Panel CreatePromoCard(PromotionDtoAdmin promo)
        {
            var card = new Panel
            {
                Size = new Size(230, 280),
                BackColor = Color.White,
                Margin = new Padding(10),
                BorderStyle = BorderStyle.FixedSingle
            };

            card.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (var path = GetRoundedRectPath(card.ClientRectangle, 10))
                {
                    card.Region = new Region(path);
                }
            };

            // Header - Color based on discount type and status
            var pnlHeader = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(230, 60),
                BackColor = GetPromoHeaderColor(promo.DiscountType, promo.IsActive)
            };

            var lblCode = new Label
            {
                Text = promo.PromoCode,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(10, 10),
                AutoSize = true,
                BackColor = Color.Transparent
            };

            var lblName = new Label
            {
                Text = promo.PromoName,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 8),
                Location = new Point(10, 35),
                Size = new Size(210, 20),
                BackColor = Color.Transparent,
                AutoEllipsis = true
            };

            pnlHeader.Controls.AddRange(new Control[] { lblCode, lblName });

            // Discount Box - Color based on discount type
            var pnlDiscount = new Panel
            {
                Location = new Point(10, 70),
                Size = new Size(210, 40),
                BackColor = GetPromoDiscountBoxColor(promo.DiscountType)
            };

            var lblDiscountValue = new Label
            {
                Text = promo.DiscountType == "Percent"
                    ? $"{promo.DiscountValue}%"
                    : $"{promo.DiscountValue:N0} ₫",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(5, 5),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            pnlDiscount.Controls.Add(lblDiscountValue);

            // Info rows
            int yPos = 120;
            AddInfoRow(card, "Min:", $"{promo.MinimumAmount:N0} ₫", yPos); // Shortened label
            yPos += 20;
            AddInfoRow(card, "Max Disc:", $"{promo.MaximumDiscount:N0} ₫", yPos);
            yPos += 20;
            
            // Usage with progress
            AddInfoRow(card, "Usage:", $"{promo.UsageCount}/{promo.UsageLimit}", yPos);
            yPos += 20;
            
            var pnlProgress = new Panel
            {
                Location = new Point(80, yPos - 5), // Adjusted below usage text
                Size = new Size(140, 4),
                BackColor = Color.FromArgb(233, 236, 239)
            };
            int progressWidth = (int)(140 * promo.UsagePercentage / 100);
            var pnlProgressFill = new Panel
            {
                 Location = new Point(0, 0),
                 Size = new Size(progressWidth, 4),
                 BackColor = Color.FromArgb(67, 233, 123)
            };
            pnlProgress.Controls.Add(pnlProgressFill);
            card.Controls.Add(pnlProgress);

            yPos += 15;
            AddInfoRow(card, "Valid:", $"{promo.ValidFrom:dd/MM}-{promo.ValidTo:dd/MM}", yPos);

            // Action buttons
            var pnlActions = new Panel
            {
                Location = new Point(10, 240),
                Size = new Size(210, 30) // 3 buttons of 70 width
            };

            var btnEdit = new Button
            {
                Text = "✏️",
                Location = new Point(0, 0),
                Size = new Size(65, 30),
                BackColor = Color.FromArgb(0, 123, 255),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btnEdit.FlatAppearance.BorderSize = 0;
            btnEdit.Click += (s, e) => EditPromo(promo);

            var btnToggle = new Button
            {
                Text = promo.IsActive ? "⏸" : "▶", // Compact icons
                Location = new Point(72, 0),
                Size = new Size(65, 30),
                BackColor = promo.IsActive ? Color.FromArgb(40, 167, 69) : Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btnToggle.FlatAppearance.BorderSize = 0;
            btnToggle.Click += async (s, e) => await TogglePromoAsync(promo);

            var btnDelete = new Button
            {
                Text = "🗑️",
                Location = new Point(144, 0),
                Size = new Size(65, 30),
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.Click += async (s, e) => await DeletePromoAsync(promo);

            pnlActions.Controls.AddRange(new Control[] { btnEdit, btnToggle, btnDelete });

            card.Controls.AddRange(new Control[] { pnlHeader, pnlDiscount, pnlActions });

            return card;
        }

        private void AddInfoRow(Panel card, string label, string value, int yPos)
        {
             var lblLabel = new Label
             {
                 Text = label,
                 ForeColor = Color.Gray,
                 Font = new Font("Segoe UI", 8),
                 Location = new Point(10, yPos),
                 AutoSize = true
             };

             var lblValue = new Label
             {
                 Text = value,
                 ForeColor = Color.Black,
                 Font = new Font("Segoe UI", 8, FontStyle.Bold),
                 Location = new Point(80, yPos), // Reduced X for smaller card
                 AutoSize = true
             };

             card.Controls.AddRange(new Control[] { lblLabel, lblValue });
        }

        private Color GetGradientColor(int discountPercent)
        {
            // Modern color palette for pricing cards
            if (discountPercent >= 30) return Color.FromArgb(102, 126, 234);  // Deep Purple-Blue
            if (discountPercent >= 20) return Color.FromArgb(79, 172, 254);   // Sky Blue
            if (discountPercent >= 10) return Color.FromArgb(67, 203, 212);   // Teal
            if (discountPercent > 0)   return Color.FromArgb(52, 152, 219);   // Blue
            return Color.FromArgb(108, 117, 125);  // Neutral Gray for 0%
        }

        private Color GetPromoHeaderColor(string discountType, bool isActive)
        {
            if (!isActive) return Color.FromArgb(149, 165, 166); // Gray for inactive
            return discountType == "Percent" 
                ? Color.FromArgb(52, 152, 219)    // Blue for Percent
                : Color.FromArgb(39, 174, 96);    // Green for Fixed
        }

        private Color GetPromoDiscountBoxColor(string discountType)
        {
            return discountType == "Percent"
                ? Color.FromArgb(155, 89, 182)    // Purple for Percent  
                : Color.FromArgb(230, 126, 34);   // Orange for Fixed
        }

        private System.Drawing.Drawing2D.GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            int diameter = radius * 2;

            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();

            return path;
        }
        private void EditPricing(FlightPricingDtoAdmin pricing)
        {
            using (var form = new Views.Forms.Admin.EditPricingForm(pricing))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadDataAsync();
                }
            }
        }

        private void EditPromo(PromotionDtoAdmin promo)
        {
            using (var form = new Views.Forms.Admin.AddPromotionForm(promo))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadDataAsync();
                }
            }
        }

        private async Task TogglePromoAsync(PromotionDtoAdmin promo)
        {
            try
            {
                var response = await DIContainer.PromotionControllerAdmin.TogglePromotion(promo.PromotionId);
                if (response.Success)
                {
                    MessageBox.Show(response.Message, "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadPromotionDataAsync();
                    LoadPromoCards();
                }
                else
                {
                    MessageBox.Show(response.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task DeletePromoAsync(PromotionDtoAdmin promo)
        {
            var result = MessageBox.Show(
                $"Are you sure you want to delete promotion '{promo.PromoCode}'?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    var response = await DIContainer.PromotionControllerAdmin.DeletePromotion(promo.PromotionId);
                    if (response.Success)
                    {
                        MessageBox.Show("Promotion deleted successfully!", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadPromotionDataAsync();
                        LoadPromoCards();
                    }
                    else
                    {
                        MessageBox.Show(response.Message, "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void ShowAddPromoDialog()
        {
            // TODO: Implement Add Promotion Dialog
            MessageBox.Show("Add Promotion feature to be implemented", "Notification",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnAddPromo_Click(object sender, EventArgs e)
        {
            using (var addForm = new AddPromotionForm())
            {
                if (addForm.ShowDialog() == DialogResult.OK)
                {
                    LoadDataAsync();
                }
            }
        }
    }
}