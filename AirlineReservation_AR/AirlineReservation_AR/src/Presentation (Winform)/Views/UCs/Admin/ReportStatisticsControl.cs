using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using Guna.UI2.WinForms;

namespace AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.Admin
{
    public partial class ReportStatisticsControl : UserControl
    {
        public ReportStatisticsControl()
        {
            InitializeComponent();
            InitializeCustomStyles();
            InitializeFilters();
            LoadStatCards();
            LoadChartData();
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

        private void StyleGunaButton(Guna2Button btn, Color color)
        {
            btn.FillColor = color;
            btn.ForeColor = Color.White;
            btn.BorderRadius = 8;
            btn.Cursor = Cursors.Hand;
            btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            // Hover effect
            Color hoverColor = ControlPaint.Dark(color, 0.1f);
            btn.HoverState.FillColor = hoverColor;
        }

        private void StyleChartPanel(Panel panel)
        {
            panel.BackColor = Color.White;
            panel.BorderStyle = BorderStyle.None;

            // Rounded corners effect
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

        private void InitializeFilters()
        {
            // ComboBox items
            cboReportType.Items.Clear();
            cboReportType.Items.AddRange(new object[]
            {
                "Tổng quan",
                "Doanh thu",
                "Booking",
                "Khách hàng",
                "Chuyến bay"
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

        // ============================================
        // TẠO STAT CARDS
        // ============================================
        private void LoadStatCards()
        {
            flpStats.Controls.Clear();

            var stats = new[]
            {
                new { Icon = "💰", Title = "Tổng doanh thu", Value = "₫8.5 tỷ", Change = "+12.5%", Color = Color.FromArgb(52, 152, 219) },
                new { Icon = "🎫", Title = "Tổng booking", Value = "1,245", Change = "+8.3%", Color = Color.FromArgb(240, 147, 251) },
                new { Icon = "✈️", Title = "Chuyến bay", Value = "248", Change = "+15", Color = Color.FromArgb(79, 172, 254) },
                new { Icon = "👥", Title = "Khách hàng", Value = "1,856", Change = "+18.7%", Color = Color.FromArgb(67, 233, 123) },
                new { Icon = "📈", Title = "Giá vé TB", Value = "₫6.8M", Change = "-3.2%", Color = Color.FromArgb(250, 112, 154) }
            };

            foreach (var stat in stats)
            {
                var card = CreateStatCard(stat.Icon, stat.Title, stat.Value, stat.Change, stat.Color);
                flpStats.Controls.Add(card);
            }
        }

        private Panel CreateStatCard(string icon, string title, string value, string change, Color color)
        {
            var card = new Panel
            {
                Size = new Size(190, 90),
                BackColor = color,
                Margin = new Padding(5)
            };

            // Rounded corners
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

        // ============================================
        // LOAD CHART DATA
        // ============================================
        private void LoadChartData()
        {
            DrawRevenueChart();
            LoadTopRoutes();
            LoadTopCustomers();
            LoadSummary();
        }

        private void DrawRevenueChart()
        {
            var chartPanel = pnlChart1Content;
            if (chartPanel != null)
            {
                chartPanel.Paint += (s, e) =>
                {
                    var g = e.Graphics;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                    var data = new[] { 85, 120, 95, 140, 110, 160, 130 };
                    var labels = new[] { "T1", "T2", "T3", "T4", "T5", "T6", "T7" };

                    int barWidth = 50;
                    int spacing = 10;
                    int maxHeight = 100;

                    for (int i = 0; i < data.Length; i++)
                    {
                        int barHeight = (int)(data[i] * maxHeight / 160f);
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
                            g.DrawString(labels[i], font, textBrush, x + barWidth / 2, chartPanel.Height - 20, sf);
                        }
                    }
                };
            }
        }

        private void LoadTopRoutes()
        {
            lvTopRoutes.Items.Clear();

            var routes = new[]
            {
                new { Rank = "🥇", Route = "SGN → HAN", Bookings = "456", Revenue = "₫2.8 tỷ" },
                new { Rank = "🥈", Route = "HAN → DAD", Bookings = "389", Revenue = "₫1.9 tỷ" },
                new { Rank = "🥉", Route = "SGN → PQC", Bookings = "312", Revenue = "₫1.5 tỷ" },
                new { Rank = "4", Route = "HAN → SGN", Bookings = "287", Revenue = "₫1.2 tỷ" },
                new { Rank = "5", Route = "DAD → SGN", Bookings = "198", Revenue = "₫890M" }
            };

            foreach (var route in routes)
            {
                var item = new ListViewItem(route.Rank);
                item.SubItems.Add(route.Route);
                item.SubItems.Add(route.Bookings);
                item.SubItems.Add(route.Revenue);
                lvTopRoutes.Items.Add(item);
            }
        }

        private void LoadTopCustomers()
        {
            listView1.Items.Clear();

            var customers = new[]
            {
                new { Rank = "🥇", Name = "Nguyễn Văn An", Email = "an@email.com", Total = "₫45M" },
                new { Rank = "🥈", Name = "Trần Thị Bình", Email = "binh@email.com", Total = "₫38M" },
                new { Rank = "🥉", Name = "Lê Minh Công", Email = "cong@email.com", Total = "₫32M" },
                new { Rank = "4", Name = "Phạm Thu Dung", Email = "dung@email.com", Total = "₫28M" },
                new { Rank = "5", Name = "Hoàng Văn Em", Email = "em@email.com", Total = "₫22M" }
            };

            foreach (var customer in customers)
            {
                var item = new ListViewItem(customer.Rank);
                item.SubItems.Add(customer.Name);
                item.SubItems.Add(customer.Email);
                item.SubItems.Add(customer.Total);
                listView1.Items.Add(item);
            }
        }

        private void LoadSummary()
        {
            var summaryPanel = pnlSummary;
            if (summaryPanel != null)
            {
                summaryPanel.Controls.Clear();

                var summaryData = new[]
                {
                    new { Label = "Tổng chuyến bay:", Value = "248" },
                    new { Label = "Tổng hành khách:", Value = "3,856" },
                    new { Label = "Doanh thu:", Value = "₫8.5B" },
                    new { Label = "Lợi nhuận:", Value = "₫2.1B" },
                    new { Label = "Tỷ lệ hủy:", Value = "3.2%" },
                    new { Label = "Độ hài lòng:", Value = "4.7/5" }
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

        // ============================================
        // EVENTS
        // ============================================
        private void BtnFilter_Click(object sender, EventArgs e)
        {
            try
            {
                string reportType = cboReportType.SelectedItem?.ToString() ?? "Tổng quan";
                DateTime fromDate = dtpFromDate.Value;
                DateTime toDate = guna2DateTimePicker1.Value; // dtpToDate

                if (fromDate > toDate)
                {
                    MessageBox.Show("Ngày bắt đầu phải nhỏ hơn ngày kết thúc!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                LoadChartData();

                MessageBox.Show($"Đang tải báo cáo {reportType}\nTừ {fromDate:dd/MM/yyyy} đến {toDate:dd/MM/yyyy}",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                var saveDialog = new SaveFileDialog
                {
                    Filter = "Excel Files|*.xlsx",
                    Title = "Lưu báo cáo",
                    FileName = $"BaoCao_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show($"Đã xuất báo cáo ra file:\n{saveDialog.FileName}",
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xuất file: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CboReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string reportType = cboReportType.SelectedItem?.ToString() ?? "";

            switch (reportType)
            {
                case "Doanh thu":
                    label4.Text = "📊 Doanh thu theo tháng"; // lblChart1Title
                    label5.Text = "💰 Doanh thu theo tuyến"; // lblChart2Title
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

        // ============================================
        // PUBLIC METHODS
        // ============================================

        public void LoadReportData(string reportType, DateTime fromDate, DateTime toDate)
        {
            cboReportType.SelectedItem = reportType;
            dtpFromDate.Value = fromDate;
            guna2DateTimePicker1.Value = toDate;
            LoadChartData();
        }

        public void RefreshData()
        {
            LoadStatCards();
            LoadChartData();
        }

        public void ClearData()
        {
            flpStats.Controls.Clear();
            lvTopRoutes.Items.Clear();
            listView1.Items.Clear();
            pnlSummary?.Controls.Clear();
        }

        // ============================================
        // HELPER METHODS
        // ============================================
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