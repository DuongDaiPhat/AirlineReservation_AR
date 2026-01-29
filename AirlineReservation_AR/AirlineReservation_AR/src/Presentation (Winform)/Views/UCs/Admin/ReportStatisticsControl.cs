using AirlineReservation_AR.src.Application.Interfaces;
using AirlineReservation_AR.src.Domain.DTOs;
using AirlineReservation_AR.src.Infrastructure.DI;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.Admin
{
    public partial class ReportStatisticsControl : UserControl
    {
        private Panel _loadingPanel;
        public ReportStatisticsControl()
        {
            InitializeComponent();
            InitializeCustomStyles();
            InitializeFilters();
            InitializeLoadingPanel();
        }
        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                await LoadInitialDataAsync();
            }
        }

        private void InitializeCustomStyles()
        {
            pnlFilters.BackColor = Color.White;

            StyleGunaButton(btnFilter, Color.FromArgb(52, 152, 219));
            StyleGunaButton(btnExport, Color.FromArgb(40, 167, 69));

            // Style cho FlowLayoutPanel
            flpStats.BackColor = Color.FromArgb(245, 245, 245);

            // Style cho Chart Panels
            StyleChartPanel(pnlChart1);
            StyleChartPanel(pnlChart2);
            StyleChartPanel(pnlChart3);
            StyleChartPanel(pnlChart4);

            // Style cho ListViews
            StyleListView(lvTopRoutes);
            StyleListView(listView1); // lvTopCustomers

            // Events
            btnFilter.Click += BtnFilter_Click;
            btnExport.Click += BtnExport_Click;
            cboReportType.SelectedIndexChanged += CboReportType_SelectedIndexChanged;
        }

        private void InitializeFilters()
        {
            // ComboBox items
            cboReportType.Items.Clear();
            cboReportType.Items.AddRange(new object[]
            {
                "Overview",
                "Revenue",
                "Bookings",
                "Customers",
                "Flights"
            });
            cboReportType.SelectedIndex = 0;

            // Date range - guna2DateTimePicker1 là dtpToDate
            dtpFromDate.Value = DateTime.Now.AddMonths(-1);
            guna2DateTimePicker1.Value = DateTime.Now;

            // ListView columns
            InitializeListViewColumns();
        }

        private void InitializeListViewColumns()
        {
            // Top Routes
            lvTopRoutes.Columns.Clear();
            lvTopRoutes.Columns.Add("STT", 50);
            lvTopRoutes.Columns.Add("Tuyến bay", 150);
            lvTopRoutes.Columns.Add("Booking", 100);
            lvTopRoutes.Columns.Add("Doanh thu", 140);

            // Top Customers (listView1)
            listView1.Columns.Clear();
            listView1.Columns.Add("STT", 50);
            listView1.Columns.Add("Khách hàng", 150);
            listView1.Columns.Add("Email", 150);
            listView1.Columns.Add("Tổng chi", 100);
        }

        private void InitializeLoadingPanel()
        {
            _loadingPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(200, 255, 255, 255),
                Visible = false
            };

            var lblLoading = new Label
            {
                Text = "Đang tải dữ liệu...",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 152, 219),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };

            _loadingPanel.Controls.Add(lblLoading);
            this.Controls.Add(_loadingPanel);
            _loadingPanel.BringToFront();
        }

        private async System.Threading.Tasks.Task LoadInitialDataAsync()
        {
            try
            {
                ShowLoading(true);

                var fromDate = DateTime.Now.AddMonths(-1);
                var toDate = DateTime.Now;

                await LoadAllReportDataAsync(fromDate, toDate);

                ShowLoading(false);
            }
            catch (Exception ex)
            {
                ShowLoading(false);
                ShowError($"Lỗi khi tải dữ liệu: {ex.Message}");
            }
        }

        private async System.Threading.Tasks.Task LoadAllReportDataAsync(DateTime fromDate, DateTime toDate)
        {
            try
            {
                // Lấy controller từ DIContainer
                var reportController = DIContainer.ReportControllerAdmin;

                // Load stat cards
                var statCards = await reportController.GetStatCardsAsync(fromDate, toDate);
                DisplayStatCards(statCards);

                // Load monthly revenue
                var monthlyRevenue = await reportController.GetMonthlyRevenueAsync(fromDate, toDate);
                DisplayRevenueChart(monthlyRevenue);

                // Load top routes
                var topRoutes = await reportController.GetTopRoutesAsync(fromDate, toDate);
                DisplayTopRoutes(topRoutes);

                // Load top customers
                var topCustomers = await reportController.GetTopCustomersAsync(fromDate, toDate);
                DisplayTopCustomers(topCustomers);

                // Load summary
                var summary = await reportController.GetReportSummaryAsync(fromDate, toDate);
                DisplaySummary(summary);
            }
            catch (Exception ex)
            {
                ShowError($"Lỗi khi tải báo cáo: {ex.Message}");
            }
        }

        // ============================================
        // LOAD SPECIFIC REPORTS
        // ============================================

        private async System.Threading.Tasks.Task LoadRevenueReportAsync(DateTime fromDate, DateTime toDate)
        {
            var reportController = DIContainer.ReportControllerAdmin;

            var statCards = await reportController.GetStatCardsAsync(fromDate, toDate);
            DisplayStatCards(statCards);

            var revenueByRoute = await reportController.GetRevenueByRouteAsync(fromDate, toDate);
            DisplayRevenueByRoute(revenueByRoute);

            var monthlyRevenue = await reportController.GetMonthlyRevenueAsync(fromDate, toDate);
            DisplayRevenueChart(monthlyRevenue);
        }

        private async System.Threading.Tasks.Task LoadBookingReportAsync(DateTime fromDate, DateTime toDate)
        {
            var reportController = DIContainer.ReportControllerAdmin;

            var bookingStatuses = await reportController.GetBookingStatusAnalysisAsync(fromDate, toDate);
            // TODO: Display booking statuses chart (implement later)

            var bookingTrends = await reportController.GetBookingTrendsAsync(fromDate, toDate);
            // TODO: Display booking trends chart (implement later)

            // Tạm thời hiển thị thông báo
            ShowSuccess("Báo cáo Booking đang được phát triển");
        }

        private async System.Threading.Tasks.Task LoadCustomerReportAsync(DateTime fromDate, DateTime toDate)
        {
            var reportController = DIContainer.ReportControllerAdmin;

            var topCustomers = await reportController.GetTopCustomersAsync(fromDate, toDate);
            DisplayTopCustomers(topCustomers);
        }

        private async System.Threading.Tasks.Task LoadFlightReportAsync(DateTime fromDate, DateTime toDate)
        {
            var reportController = DIContainer.ReportControllerAdmin;

            var flightPerformance = await reportController.GetFlightPerformanceAsync(fromDate, toDate);
            // TODO: Display flight performance (implement later)

            // Tạm thời hiển thị thông báo
            ShowSuccess("Báo cáo Chuyến bay đang được phát triển");
        }

        // ============================================
        // EVENTS
        // ============================================
        private async void BtnFilter_Click(object sender, EventArgs e)
        {
            try
            {
                string reportType = cboReportType.SelectedItem?.ToString() ?? "Overview";
                DateTime fromDate = dtpFromDate.Value;
                DateTime toDate = guna2DateTimePicker1.Value;

                var reportController = DIContainer.ReportControllerAdmin;

                // Validate date range
                if (!reportController.ValidateDateRange(fromDate, toDate, out string errorMessage))
                {
                    ShowWarning(errorMessage);
                    return;
                }

                ShowLoading(true);

                switch (reportType)
                {
                    case "Revenue":
                        await LoadRevenueReportAsync(fromDate, toDate);
                        break;
                    case "Bookings":
                        await LoadBookingReportAsync(fromDate, toDate);
                        break;
                    case "Customers":
                        await LoadCustomerReportAsync(fromDate, toDate);
                        break;
                    case "Flights":
                        await LoadFlightReportAsync(fromDate, toDate);
                        break;
                    default:
                        await LoadAllReportDataAsync(fromDate, toDate);
                        break;
                }

                ShowLoading(false);
                ShowSuccess($"Loaded {reportType} report from {fromDate:dd/MM/yyyy} to {toDate:dd/MM/yyyy}");
            }
            catch (Exception ex)
            {
                ShowLoading(false);
                ShowError($"Error filtering data: {ex.Message}");
            }
        }

        private async void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                var saveDialog = new SaveFileDialog
                {
                    Filter = "Excel Files|*.xlsx",
                    Title = "Lưu báo cáo",
                    FileName = $"Report_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    ShowLoading(true);

                    string reportType = cboReportType.SelectedItem?.ToString() ?? "Overview";
                    DateTime fromDate = dtpFromDate.Value;
                    DateTime toDate = guna2DateTimePicker1.Value;

                    var request = new ReportRequestDtoAdmin
                    {
                        ReportType = reportType,
                        FromDate = fromDate,
                        ToDate = toDate
                    };

                    var reportController = DIContainer.ReportControllerAdmin;
                    var excelData = await reportController.ExportReportToExcelAsync(request);

                    if (excelData != null && excelData.Length > 0)
                    {
                        await System.IO.File.WriteAllBytesAsync(saveDialog.FileName, excelData);
                        ShowSuccess($"Report exported successfully!\n{saveDialog.FileName}");
                    }
                    else
                    {
                        ShowWarning("Chức năng xuất Excel đang được phát triển.");
                    }

                    ShowLoading(false);
                }
            }
            catch (Exception ex)
            {
                ShowLoading(false);
                ShowError($"Lỗi khi xuất file: {ex.Message}");
            }
        }

        private void CboReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string reportType = cboReportType.SelectedItem?.ToString() ?? "";

            switch (reportType)
            {
                case "Doanh thu":
                    label4.Text = "📊 Doanh thu theo tháng";
                    label5.Text = "💰 Doanh thu theo tuyến";
                    break;
                case "Booking":
                    label4.Text = "📈 Số lượng booking";
                    label5.Text = "🎫 Booking theo hãng";
                    break;
                case "Khách hàng":
                    label4.Text = "👥 Khách hàng mới";
                    label5.Text = "⭐ Top khách hàng";
                    break;
                default:
                    label4.Text = "📊 Biểu đồ doanh thu";
                    label5.Text = "📈 Top tuyến bay";
                    break;
            }
        }

        private void DisplayStatCards(List<StatCardDtoAdmin> statCards)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => DisplayStatCards(statCards)));
                return;
            }

            flpStats.Controls.Clear();

            foreach (var stat in statCards)
            {
                var color = GetColorForCard(stat.Icon);
                var card = CreateStatCard(stat.Icon, stat.Title, stat.Value, stat.Change, color);
                flpStats.Controls.Add(card);
            }
        }

        private void DisplayRevenueChart(List<MonthlyRevenueDtoAdmin> monthlyRevenue)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => DisplayRevenueChart(monthlyRevenue)));
                return;
            }

            var chartPanel = pnlChart1Content;
            if (chartPanel != null)
            {
                chartPanel.Invalidate();
                chartPanel.Paint -= ChartPanel_Paint;
                chartPanel.Paint += (s, e) => DrawRevenueChart(e.Graphics, monthlyRevenue, chartPanel);
            }
        }

        private void DisplayTopRoutes(List<TopRouteDtoAdmin> topRoutes)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => DisplayTopRoutes(topRoutes)));
                return;
            }

            lvTopRoutes.Items.Clear();

            foreach (var route in topRoutes)
            {
                var rankIcon = GetRankIcon(route.Rank);
                var item = new ListViewItem(rankIcon);
                item.SubItems.Add(route.Route);
                item.SubItems.Add(route.BookingCount.ToString());
                item.SubItems.Add(FormatCurrency(route.TotalRevenue));
                lvTopRoutes.Items.Add(item);
            }
        }

        private void DisplayTopCustomers(List<TopCustomerDtoAdmin> topCustomers)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => DisplayTopCustomers(topCustomers)));
                return;
            }

            listView1.Items.Clear();

            foreach (var customer in topCustomers)
            {
                var rankIcon = GetRankIcon(customer.Rank);
                var item = new ListViewItem(rankIcon);
                item.SubItems.Add(customer.CustomerName);
                item.SubItems.Add(customer.Email);
                item.SubItems.Add(FormatCurrency(customer.TotalSpent));
                listView1.Items.Add(item);
            }
        }

        private void DisplaySummary(ReportSummaryDtoAdmin summary)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => DisplaySummary(summary)));
                return;
            }

            var summaryPanel = pnlSummary;
            if (summaryPanel != null)
            {
                summaryPanel.Controls.Clear();

                var summaryData = new[]
                {
                    new { Label = "Tổng chuyến bay:", Value = summary.TotalFlights.ToString() },
                    new { Label = "Tổng hành khách:", Value = summary.TotalPassengers.ToString("N0") },
                    new { Label = "Doanh thu:", Value = FormatCurrency(summary.TotalRevenue) },
                    new { Label = "Lợi nhuận:", Value = FormatCurrency(summary.TotalProfit) },
                    new { Label = "Tỷ lệ hủy:", Value = $"{summary.CancellationRate:N1}%" },
                    new { Label = "Độ hài lòng:", Value = $"{summary.AverageSatisfactionScore:N1}/5" }
                };

                int yPos = 10;
                foreach (var item in summaryData)
                {
                    var lblLabel = new Label
                    {
                        Text = item.Label,
                        Location = new Point(20, yPos),
                        AutoSize = true,
                        Font = new Font("Segoe UI", 9),
                        ForeColor = Color.Gray
                    };

                    var lblValue = new Label
                    {
                        Text = item.Value,
                        Location = new Point(250, yPos),
                        AutoSize = true,
                        Font = new Font("Segoe UI", 9, FontStyle.Bold),
                        ForeColor = Color.Black
                    };

                    summaryPanel.Controls.AddRange(new Control[] { lblLabel, lblValue });
                    yPos += 22;
                }
            }
        }
        private void DisplayRevenueByRoute(List<RevenueByRouteDtoAdmin> revenueByRoute)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => DisplayRevenueByRoute(revenueByRoute)));
                return;
            }

            lvTopRoutes.Items.Clear();

            int rank = 1;
            foreach (var route in revenueByRoute.Take(10))
            {
                var item = new ListViewItem(rank.ToString());
                item.SubItems.Add(route.Route);
                item.SubItems.Add(route.BookingCount.ToString());
                item.SubItems.Add(FormatCurrency(route.TotalRevenue));
                lvTopRoutes.Items.Add(item);
                rank++;
            }
        }

        public void ClearData()
        {
            flpStats.Controls.Clear();
            lvTopRoutes.Items.Clear();
            listView1.Items.Clear();
            pnlSummary?.Controls.Clear();
        }

        private void ShowLoading(bool isLoading)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => ShowLoading(isLoading)));
                return;
            }

            _loadingPanel.Visible = isLoading;
            if (isLoading)
                _loadingPanel.BringToFront();
        }

        private void ShowError(string message)
        {
            MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ShowWarning(string message)
        {
            MessageBox.Show(message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void ShowSuccess(string message)
        {
            MessageBox.Show(message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ============================================
        // HELPER METHODS
        // ============================================
        private void StyleGunaButton(Guna2Button btn, Color color)
        {
            btn.FillColor = color;
            btn.ForeColor = Color.White;
            btn.BorderRadius = 8;
            btn.Cursor = Cursors.Hand;
            btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            Color hoverColor = ControlPaint.Dark(color, 0.1f);
            btn.HoverState.FillColor = hoverColor;
        }

        private void StyleChartPanel(Panel panel)
        {
            panel.BackColor = Color.White;
            panel.BorderStyle = BorderStyle.None;

            panel.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (var pen = new Pen(Color.FromArgb(233, 236, 239), 2))
                {
                    var rect = new Rectangle(1, 1, panel.Width - 2, panel.Height - 2);
                    e.Graphics.DrawRectangle(pen, rect);
                }
            };
        }

        private void StyleListView(ListView lv)
        {
            lv.View = View.Details;
            lv.FullRowSelect = true;
            lv.GridLines = true;
            lv.Font = new Font("Segoe UI", 9);
            lv.BackColor = Color.White;
            lv.BorderStyle = BorderStyle.None;
        }

        private Panel CreateStatCard(string icon, string title, string value, string change, Color color)
        {
            var card = new Panel
            {
                Size = new Size(190, 90),
                BackColor = color,
                Margin = new Padding(5)
            };

            card.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (var path = GetRoundedRectPath(card.ClientRectangle, 10))
                {
                    card.Region = new Region(path);
                }
            };

            var lblIcon = new Label
            {
                Text = icon,
                Font = new Font("Segoe UI", 20),
                ForeColor = Color.White,
                Location = new Point(15, 10),
                AutoSize = true,
                BackColor = Color.Transparent
            };

            var lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.White,
                Location = new Point(60, 15),
                AutoSize = true,
                BackColor = Color.Transparent
            };

            var lblValue = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(60, 35),
                AutoSize = true,
                BackColor = Color.Transparent
            };

            var lblChange = new Label
            {
                Text = change,
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.FromArgb(200, 255, 255, 255),
                Location = new Point(60, 63),
                AutoSize = true,
                BackColor = Color.Transparent
            };

            card.Controls.AddRange(new Control[] { lblIcon, lblTitle, lblValue, lblChange });

            card.Cursor = Cursors.Hand;
            card.MouseEnter += (s, e) => card.BackColor = ControlPaint.Dark(color, 0.05f);
            card.MouseLeave += (s, e) => card.BackColor = color;

            return card;
        }

        private void DrawRevenueChart(Graphics g, List<MonthlyRevenueDtoAdmin> data, Panel chartPanel)
        {
            if (data == null || !data.Any()) return;

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int barWidth = 50;
            int spacing = 10;
            int maxHeight = 100;
            decimal maxRevenue = data.Max(d => d.Revenue);

            for (int i = 0; i < data.Count; i++)
            {
                int barHeight = maxRevenue > 0
                    ? (int)((data[i].Revenue / maxRevenue) * maxHeight)
                    : 0;

                int x = 20 + i * (barWidth + spacing);
                int y = chartPanel.Height - barHeight - 30;

                using (var brush = new SolidBrush(Color.FromArgb(52, 152, 219)))
                {
                    g.FillRectangle(brush, x, y, barWidth, barHeight);
                }

                using (var font = new Font("Segoe UI", 8))
                using (var textBrush = new SolidBrush(Color.Gray))
                {
                    var sf = new StringFormat { Alignment = StringAlignment.Center };
                    g.DrawString(data[i].MonthLabel, font, textBrush, x + barWidth / 2, chartPanel.Height - 20, sf);
                }
            }
        }

        private void ChartPanel_Paint(object sender, PaintEventArgs e)
        {
            // Placeholder
        }
        private Color GetColorForCard(string icon)
        {
            return icon switch
            {
                "💰" => Color.FromArgb(52, 152, 219),
                "🎫" => Color.FromArgb(240, 147, 251),
                "✈️" => Color.FromArgb(79, 172, 254),
                "👥" => Color.FromArgb(67, 233, 123),
                "📈" => Color.FromArgb(250, 112, 154),
                _ => Color.FromArgb(52, 152, 219)
            };
        }

        private string GetRankIcon(int rank)
        {
            return rank switch
            {
                1 => "🥇",
                2 => "🥈",
                3 => "🥉",
                _ => rank.ToString()
            };
        }

        private string FormatCurrency(decimal amount)
        {
            if (amount >= 1_000_000_000)
                return $"₫{amount / 1_000_000_000:N1} tỷ";
            if (amount >= 1_000_000)
                return $"₫{amount / 1_000_000:N1}M";
            return $"₫{amount:N0}";
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
    }
}