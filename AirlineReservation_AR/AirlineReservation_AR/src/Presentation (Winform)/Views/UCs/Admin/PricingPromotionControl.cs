using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.Application.Interfaces;
using AirlineReservation_AR.src.Domain.DTOs;
using AirlineReservation_AR.src.Infrastructure.DI;
using AirlineReservation_AR.src.Presentation__Winform_.Views.Forms.Admin;
using System;
using System.Collections.Generic;
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

        private List<PromotionDtoAdmin> _promotions = new List<PromotionDtoAdmin>();
        private List<PromotionDtoAdmin> _filteredPromotions = new List<PromotionDtoAdmin>();

        private int _currentPricingPage = 1;
        private int _pricingPageSize = 4;
        private int _totalPricingPages = 1;

        private int _currentPromoPage = 1;
        private int _promoPageSize = 3;
        private int _totalPromoPages = 1;
        public PricingPromotionControl()
        {
            InitializeComponent();
            InitializeCustomStyles();
            //LoadSampleData();
            InitializeFilters();
            RegisterEventHandlers();
            //LoadPricingCards();
            //LoadPromoCards();
            //LoadStatCards();
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
        private void PaginationPricing_PageChanged(object sender, int pageNumber)
        {
            _currentPricingPage = pageNumber;
            LoadPricingCards();
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

            // Đăng ký mới
            btnSearchFlights.Click += BtnSearchFlights_Click;
            txtPromoSearch.TextChanged += TxtPromoSearch_TextChanged;
            cboPromoStatus.SelectedIndexChanged += CboPromoStatus_SelectedIndexChanged;
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
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        private async Task LoadPricingDataAsync()
        {
            var response = await DIContainer.PricingControllerAdmin.GetAllPricings();
            if (response.Success && response.Data != null)
            {
                _flightPricings = response.Data.ToList();
                _filteredPricings = _flightPricings.ToList();
                _currentPricingPage = 1; // Reset về trang 1
                UpdatePricingPagination();
            }
            else
            {
                MessageBox.Show(response.Message, "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async Task LoadPromotionDataAsync()
        {
            var response = await DIContainer.PromotionControllerAdmin.GetAllPromotions();
            if (response.Success && response.Data != null)
            {
                _promotions = response.Data.ToList();
                _filteredPromotions = _promotions.ToList();
                _currentPromoPage = 1; // Reset về trang 1
                UpdatePromoPagination();
            }
            else
            {
                MessageBox.Show(response.Message, "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void UpdatePricingPagination()
        {
            int totalRecords = _filteredPricings.Count;
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

            // Style cho DataGridView
            StyleDataGridView();

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

        private void StyleDataGridView()
        {
            dgvHistory.EnableHeadersVisualStyles = false;
            dgvHistory.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(67, 233, 123);
            dgvHistory.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvHistory.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvHistory.ColumnHeadersHeight = 45;
            dgvHistory.RowTemplate.Height = 40;
            dgvHistory.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dgvHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHistory.AllowUserToAddRows = false;
            dgvHistory.ReadOnly = true;
        }
        private void InitializeFilters()
        {
            // Flight Pricing Filters
            cboRoute.Items.AddRange(new object[]
            {
                "Tất cả", "SGN - HAN", "HAN - DAD", "SGN - PQC", "DAD - SGN"
            });
            cboRoute.SelectedIndex = 0;

            cboSeatClass.Items.AddRange(new object[]
            {
                "Tất cả", "Economy", "Business", "First Class"
            });
            cboSeatClass.SelectedIndex = 0;

            cboDiscount.Items.AddRange(new object[]
            {
                "Tất cả", "Trên 10%", "Trên 20%", "Trên 30%"
            });
            cboDiscount.SelectedIndex = 0;

            // Promo Filters
            txtPromoSearch.PlaceholderText = "Tìm mã hoặc tên khuyến mãi...";

            cboPromoStatus.Items.AddRange(new object[]
            {
                "Tất cả", "Đang hoạt động", "Tạm dừng", "Hết hạn"
            });
            cboPromoStatus.SelectedIndex = 0;

            cboPromoType.Items.AddRange(new object[]
            {
                "Tất cả", "Phần trăm", "Số tiền cố định"
            });
            cboPromoType.SelectedIndex = 0;

            cboPromoSort.Items.AddRange(new object[]
            {
                "Mới nhất", "Nhiều lượt dùng", "Giá trị cao nhất"
            });
            cboPromoSort.SelectedIndex = 0;

            //// Events
            //btnSearchFlights.Click += (s, e) => ApplyFlightFilters();
            //txtPromoSearch.TextChanged += (s, e) => ApplyPromoFilters();
            //cboPromoStatus.SelectedIndexChanged += (s, e) => ApplyPromoFilters();
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
            try
            {
                Cursor = Cursors.WaitCursor;

                var filter = new FlightPricingFilterDtoAdmin
                {
                    Route = cboRoute.SelectedItem?.ToString(),
                    SeatClass = cboSeatClass.SelectedItem?.ToString(),
                    MinDiscountPercent = GetMinDiscountPercent()
                };

                var response = await DIContainer.PricingControllerAdmin.SearchPricings(filter);
                if (response.Success && response.Data != null)
                {
                    _filteredPricings = response.Data.ToList();
                    _currentPricingPage = 1; // Reset về trang 1 khi filter
                    UpdatePricingPagination();
                    LoadPricingCards();
                }
                else
                {
                    MessageBox.Show(response.Message, "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        private async void ApplyPromoFilters()
        {
            try
            {
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
                    MessageBox.Show(response.Message, "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        private int? GetMinDiscountPercent()
        {
            var selected = cboDiscount.SelectedItem?.ToString();
            return selected switch
            {
                "Trên 10%" => 10,
                "Trên 20%" => 20,
                "Trên 30%" => 30,
                _ => null
            };
        }
        private bool? GetPromoActiveStatus()
        {
            var selected = cboPromoStatus.SelectedItem?.ToString();
            return selected switch
            {
                "Đang hoạt động" => true,
                "Tạm dừng" => false,
                _ => null
            };
        }
        private string GetPromoSortBy()
        {
            var selected = cboPromoSort.SelectedItem?.ToString();
            return selected switch
            {
                "Nhiều lượt dùng" => "most_used",
                "Giá trị cao nhất" => "highest_value",
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
            flpPricingCards.Controls.Clear();
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
                        Text = "Không có dữ liệu giá vé",
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
                Size = new Size(350, 280),
                BackColor = GetGradientColor(pricing.DiscountPercent),
                Margin = new Padding(10)
            };

            card.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (var path = GetRoundedRectPath(card.ClientRectangle, 15))
                {
                    card.Region = new Region(path);
                }
            };

            var lblFlight = new Label
            {
                Text = $"{pricing.FlightNumber} - {pricing.AirlineName}",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true,
                BackColor = Color.Transparent
            };

            var lblRoute = new Label
            {
                Text = pricing.Route,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10),
                Location = new Point(20, 50),
                AutoSize = true,
                BackColor = Color.Transparent
            };

            var lblDiscount = new Label
            {
                Text = $"🔥 Giảm {pricing.DiscountPercent}%",
                BackColor = Color.FromArgb(100, 255, 255, 255),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(20, 85),
                AutoSize = true,
                Padding = new Padding(10, 5, 10, 5)
            };

            var lblOldPrice = new Label
            {
                Text = $"{pricing.OriginalPrice:N0} ₫",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Strikeout),
                Location = new Point(20, 130),
                AutoSize = true,
                BackColor = Color.Transparent
            };

            var lblNewPrice = new Label
            {
                Text = $"{pricing.DiscountedPrice:N0} ₫",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                Location = new Point(20, 155),
                AutoSize = true,
                BackColor = Color.Transparent
            };

            var pnlInfo = new Panel
            {
                Location = new Point(20, 205),
                Size = new Size(310, 60),
                BackColor = Color.FromArgb(100, 255, 255, 255)
            };

            var lblClass = new Label
            {
                Text = $"Hạng: {pricing.SeatClass}",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9),
                Location = new Point(10, 10),
                AutoSize = true,
                BackColor = Color.Transparent
            };

            var lblSeats = new Label
            {
                Text = $"Đã bán: {pricing.BookedSeats} | Còn: {pricing.AvailableSeats}",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9),
                Location = new Point(10, 35),
                AutoSize = true,
                BackColor = Color.Transparent
            };

            pnlInfo.Controls.AddRange(new Control[] { lblClass, lblSeats });

            card.Controls.AddRange(new Control[]
            {
                lblFlight, lblRoute, lblDiscount, lblOldPrice, lblNewPrice, pnlInfo
            });

            card.Cursor = Cursors.Hand;
            card.Click += (s, e) => EditPricing(pricing);

            return card;
        }
        private void LoadPromoCards()
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
                    Text = "Không có dữ liệu khuyến mãi",
                    Font = new Font("Segoe UI", 12, FontStyle.Italic),
                    ForeColor = Color.Gray,
                    AutoSize = true,
                    Margin = new Padding(20)
                };
                flpPromoCards.Controls.Add(lblEmpty);
            }
        }
        private Panel CreatePromoCard(PromotionDtoAdmin promo)
        {
            var card = new Panel
            {
                Size = new Size(400, 380),
                BackColor = Color.White,
                Margin = new Padding(10),
                BorderStyle = BorderStyle.FixedSingle
            };

            card.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (var path = GetRoundedRectPath(card.ClientRectangle, 15))
                {
                    card.Region = new Region(path);
                }
            };

            // Header
            var pnlHeader = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(400, 100),
                BackColor = Color.FromArgb(67, 233, 123)
            };

            var lblCode = new Label
            {
                Text = promo.PromoCode,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true,
                BackColor = Color.Transparent
            };

            var lblName = new Label
            {
                Text = promo.PromoName,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11),
                Location = new Point(20, 55),
                Size = new Size(360, 30),
                BackColor = Color.Transparent
            };

            pnlHeader.Controls.AddRange(new Control[] { lblCode, lblName });

            // Discount Box
            var pnlDiscount = new Panel
            {
                Location = new Point(20, 120),
                Size = new Size(360, 60),
                BackColor = Color.FromArgb(240, 147, 251)
            };

            var lblDiscountValue = new Label
            {
                Text = promo.DiscountType == "Percent"
                    ? $"{promo.DiscountValue}%"
                    : $"{promo.DiscountValue:N0} ₫",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                Location = new Point(10, 15),
                AutoSize = true,
                BackColor = Color.Transparent
            };

            pnlDiscount.Controls.Add(lblDiscountValue);

            // Info rows
            int yPos = 200;
            AddInfoRow(card, "Giá trị tối thiểu:", $"{promo.MinimumAmount:N0} ₫", yPos);
            yPos += 30;
            AddInfoRow(card, "Giảm tối đa:", $"{promo.MaximumDiscount:N0} ₫", yPos);
            yPos += 30;
            AddInfoRow(card, "Số lần sử dụng:", $"{promo.UsageCount} / {promo.UsageLimit}", yPos);
            yPos += 30;

            // Progress bar
            var pnlProgress = new Panel
            {
                Location = new Point(120, yPos),
                Size = new Size(260, 8),
                BackColor = Color.FromArgb(233, 236, 239)
            };

            int progressWidth = (int)(260 * promo.UsagePercentage / 100);
            var pnlProgressFill = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(progressWidth, 8),
                BackColor = Color.FromArgb(67, 233, 123)
            };

            pnlProgress.Controls.Add(pnlProgressFill);
            card.Controls.Add(pnlProgress);

            yPos += 20;
            AddInfoRow(card, "Thời gian:", $"{promo.ValidFrom:dd/MM} - {promo.ValidTo:dd/MM}", yPos);

            // Action buttons
            var pnlActions = new Panel
            {
                Location = new Point(20, 330),
                Size = new Size(360, 40)
            };

            var btnEdit = new Button
            {
                Text = "✏️ Sửa",
                Location = new Point(0, 0),
                Size = new Size(115, 40),
                BackColor = Color.FromArgb(0, 123, 255),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btnEdit.FlatAppearance.BorderSize = 0;
            btnEdit.Click += (s, e) => EditPromo(promo);

            var btnToggle = new Button
            {
                Text = promo.IsActive ? "⏸ Tạm dừng" : "▶ Kích hoạt",
                Location = new Point(122, 0),
                Size = new Size(115, 40),
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
                Location = new Point(244, 0),
                Size = new Size(115, 40),
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12, FontStyle.Bold)
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
                Font = new Font("Segoe UI", 9),
                Location = new Point(20, yPos),
                AutoSize = true
            };

            var lblValue = new Label
            {
                Text = value,
                ForeColor = Color.Black,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Location = new Point(160, yPos),
                AutoSize = true
            };

            card.Controls.AddRange(new Control[] { lblLabel, lblValue });
        }

        private Color GetGradientColor(int discountPercent)
        {
            if (discountPercent >= 30) return Color.FromArgb(240, 147, 251);
            if (discountPercent >= 20) return Color.FromArgb(79, 172, 254);
            if (discountPercent >= 10) return Color.FromArgb(102, 126, 234);
            return Color.FromArgb(108, 117, 125);
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
            MessageBox.Show($"Chỉnh sửa giá vé: {pricing.PricingId}", "Edit",
                 MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void EditPromo(PromotionDtoAdmin promo)
        {
            MessageBox.Show($"Chỉnh sửa khuyến mãi: {promo.PromoCode}", "Edit",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async Task TogglePromoAsync(PromotionDtoAdmin promo)
        {
            try
            {
                var response = await DIContainer.PromotionControllerAdmin.TogglePromotion(promo.PromotionId);
                if (response.Success)
                {
                    MessageBox.Show(response.Message, "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadPromotionDataAsync();
                    LoadPromoCards();
                }
                else
                {
                    MessageBox.Show(response.Message, "Lỗi",
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
                $"Bạn có chắc chắn muốn xóa mã khuyến mãi '{promo.PromoCode}'?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    var response = await DIContainer.PromotionControllerAdmin.DeletePromotion(promo.PromotionId);
                    if (response.Success)
                    {
                        MessageBox.Show("Đã xóa mã khuyến mãi!", "Thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadPromotionDataAsync();
                        LoadPromoCards();
                    }
                    else
                    {
                        MessageBox.Show(response.Message, "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void ShowAddPromoDialog()
        {
            // TODO: Implement Add Promotion Dialog
            MessageBox.Show("Form thêm khuyến mãi sẽ được triển khai", "Thông báo",
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