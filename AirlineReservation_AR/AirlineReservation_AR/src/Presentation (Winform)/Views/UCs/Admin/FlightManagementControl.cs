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
    public partial class FlightManagementControl : UserControl
    {
        private int currentPage = 1;
        private int pageSize = 20;
        private int totalRecords = 0;
        private int totalPages = 0;
        private List<FlightData> allFlights = new List<FlightData>();
        private List<FlightData> filteredFlights = new List<FlightData>();
        public FlightManagementControl()
        {
            InitializeComponent();
            InitializeStyles();
            InitializeData();
            InitializeEvents();
            LoadFlightData();
        }
        private void InitializeStyles()
        {

            // DataGridView Header Style
            dgvFlights.EnableHeadersVisualStyles = false;
            dgvFlights.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(46, 80, 144); // #2E5090
            dgvFlights.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvFlights.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvFlights.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvFlights.ColumnHeadersHeight = 45;

            // DataGridView Row Style
            dgvFlights.DefaultCellStyle.BackColor = Color.White;
            dgvFlights.DefaultCellStyle.ForeColor = Color.FromArgb(51, 51, 51);
            dgvFlights.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            dgvFlights.DefaultCellStyle.SelectionBackColor = Color.FromArgb(227, 242, 253); // #E3F2FD
            dgvFlights.DefaultCellStyle.SelectionForeColor = Color.FromArgb(25, 118, 210); // #1976D2
            dgvFlights.DefaultCellStyle.Padding = new Padding(5);

            // Alternating Row Style
            dgvFlights.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);

            // Grid Lines
            dgvFlights.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvFlights.GridColor = Color.FromArgb(224, 224, 224);

            // Row Template
            dgvFlights.RowTemplate.Height = 50;

            // Auto resize columns
            //dgvFlights.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Cursor
            dgvFlights.Cursor = Cursors.Hand;
        }

        private void InitializeData()
        {
            // ComboBox - Hãng hàng không
            cboAirline.Items.Clear();
            cboAirline.Items.AddRange(new object[]
            {
                "Tất cả",
                "Vietnam Airlines",
                "VietJet Air",
                "Bamboo Airways",
                "Vietravel Airlines",
                "Pacific Airlines"
            });
            cboAirline.SelectedIndex = 0;

            // ComboBox - Trạng thái
            cboStatus.Items.Clear();
            cboStatus.Items.AddRange(new object[]
            {
                "Tất cả",
                "Còn chỗ",
                "Đã đầy",
                "Đã hủy"
            });
            cboStatus.SelectedIndex = 0;

            // ComboBox - Sân bay đến
            cboDestination.Items.Clear();
            cboDestination.Items.AddRange(new object[]
            {
                "Tất cả",
                "Hà Nội (HAN)",
                "Đà Nẵng (DAD)",
                "Phú Quốc (PQC)",
                "Nha Trang (CXR)",
                "Cần Thơ (VCA)",
                "Huế (HUI)",
                "Vinh (VII)"
            });
            cboDestination.SelectedIndex = 0;

            // DateTimePicker
            guna2DateTimePicker1.Value = DateTime.Now;

            // Load sample data
            LoadSampleData();
        }

        private void InitializeEvents()
        {
            // Buttons
            btnSearch.Click += BtnSearch_Click;
            // btnAddFlight.Click += BtnAddFlight_Click; // Uncomment khi đã có btnAddFlight trong Designer

            // DataGridView events
            dgvFlights.CellPainting += DgvFlights_CellPainting;
            dgvFlights.CellClick += DgvFlights_CellClick;
            dgvFlights.CellFormatting += DgvFlights_CellFormatting;

            // Enter key in textbox
            txtFlightNo.KeyDown += TxtFlightNo_KeyDown;
        }

        private void LoadSampleData()
        {
            // Sample data - thay thế bằng data từ database
            allFlights = new List<FlightData>
            {
                new FlightData { FlightCode = "VN210", Airline = "Vietnam Airlines", Route = "SGN → HAN",
                    Date = "15/12/2024", Departure = "06:00", Duration = "2h 15m",
                    Aircraft = "Boeing 787", Price = "1.500.000 ₫", Seats = "45/180", Status = "Còn chỗ" },

                new FlightData { FlightCode = "VJ123", Airline = "VietJet Air", Route = "HAN → DAD",
                    Date = "15/12/2024", Departure = "08:30", Duration = "1h 30m",
                    Aircraft = "Airbus A321", Price = "890.000 ₫", Seats = "0/180", Status = "Đã đầy" },

                new FlightData { FlightCode = "QH206", Airline = "Bamboo Airways", Route = "SGN → PQC",
                    Date = "16/12/2024", Departure = "14:45", Duration = "1h 00m",
                    Aircraft = "Airbus A320", Price = "1.200.000 ₫", Seats = "78/160", Status = "Còn chỗ" },

                new FlightData { FlightCode = "VN315", Airline = "Vietnam Airlines", Route = "HAN → SGN",
                    Date = "16/12/2024", Departure = "16:20", Duration = "2h 10m",
                    Aircraft = "Boeing 787", Price = "1.450.000 ₫", Seats = "23/180", Status = "Còn chỗ" },

                new FlightData { FlightCode = "VJ456", Airline = "VietJet Air", Route = "DAD → SGN",
                    Date = "17/12/2024", Departure = "10:15", Duration = "1h 20m",
                    Aircraft = "Airbus A321", Price = "750.000 ₫", Seats = "0/180", Status = "Đã hủy" },
                
                // Thêm nhiều data hơn để test pagination
                new FlightData { FlightCode = "VN100", Airline = "Vietnam Airlines", Route = "SGN → HAN",
                    Date = "17/12/2024", Departure = "07:00", Duration = "2h 15m",
                    Aircraft = "Boeing 787", Price = "1.500.000 ₫", Seats = "120/180", Status = "Còn chỗ" },

                new FlightData { FlightCode = "QH300", Airline = "Bamboo Airways", Route = "HAN → PQC",
                    Date = "18/12/2024", Departure = "09:30", Duration = "2h 30m",
                    Aircraft = "Airbus A320", Price = "1.800.000 ₫", Seats = "45/160", Status = "Còn chỗ" },
            };

            filteredFlights = new List<FlightData>(allFlights);
            totalRecords = filteredFlights.Count;
        }

        // ============================================
        // LOAD & DISPLAY DATA
        // ============================================

        private void LoadFlightData()
        {
            // Calculate pagination
            totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            if (currentPage > totalPages && totalPages > 0)
                currentPage = totalPages;

            // Get data for current page
            var pagedData = filteredFlights
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            //// Clear and load to DataGridView
            dgvFlights.Rows.Clear();

            foreach (var flight in pagedData)
            {
                int rowIndex = dgvFlights.Rows.Add(
                    flight.FlightCode,
                    flight.Airline,
                    flight.Route,
                    flight.Date,
                    flight.Duration,
                    flight.Departure,
                    flight.Aircraft,
                    flight.Price,
                    flight.Seats,
                    flight.Status,
                    "•••" // Actions column placeholder
                );

                // Store full data object in Tag
                dgvFlights.Rows[rowIndex].Tag = flight;
            }

            // Update pagination info (nếu có pagination panel)
            // UpdatePaginationInfo();
        }

        // ============================================
        // SEARCH & FILTER
        // ============================================

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void TxtFlightNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ApplyFilters();
                e.SuppressKeyPress = true;
            }
        }

        private void ApplyFilters()
        {
            try
            {
                // Get filter values
                string flightNo = txtFlightNo.Text.Trim().ToUpper();
                string airline = cboAirline.SelectedItem?.ToString() ?? "Tất cả";
                string status = cboStatus.SelectedItem?.ToString() ?? "Tất cả";
                string destination = cboDestination.SelectedItem?.ToString() ?? "Tất cả";
                DateTime filterDate = guna2DateTimePicker1.Value.Date;

                // Apply filters
                filteredFlights = allFlights.Where(f =>
                {
                    bool match = true;

                    // Filter by flight code
                    if (!string.IsNullOrEmpty(flightNo))
                        match = match && f.FlightCode.Contains(flightNo);

                    // Filter by airline
                    if (airline != "Tất cả")
                        match = match && f.Airline == airline;

                    // Filter by status
                    if (status != "Tất cả")
                        match = match && f.Status == status;

                    // Filter by destination
                    if (destination != "Tất cả")
                        match = match && f.Route.Contains(destination.Split('(')[1].TrimEnd(')'));

                    // Filter by date (optional - uncomment if needed)
                    // match = match && DateTime.Parse(f.Date).Date == filterDate;

                    return match;
                }).ToList();

                // Reset to first page
                currentPage = 1;
                totalRecords = filteredFlights.Count;

                // Reload data
                LoadFlightData();

                // Show result
                if (filteredFlights.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy chuyến bay nào phù hợp!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ============================================
        // DATAGRIDVIEW CUSTOM PAINTING
        // ============================================

        private void DgvFlights_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Paint header
            if (e.RowIndex == -1 && e.ColumnIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);

                using (var brush = new SolidBrush(Color.White))
                using (var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                {
                    e.Graphics.DrawString(e.FormattedValue?.ToString() ?? "", e.CellStyle.Font, brush, e.CellBounds, sf);
                }

                e.Handled = true;
                return;
            }

            // Paint Actions column with 3 buttons
            if (e.ColumnIndex == dgvFlights.Columns["colActions"].Index && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.Background | DataGridViewPaintParts.SelectionBackground);

                var rect = e.CellBounds;
                int buttonWidth = 35;
                int buttonHeight = 30;
                int spacing = 10;
                int startX = rect.X + (rect.Width - (buttonWidth * 3 + spacing * 2)) / 2;
                int startY = rect.Y + (rect.Height - buttonHeight) / 2;

                // Button 1: View (Cyan)
                var btnView = new Rectangle(startX, startY, buttonWidth, buttonHeight);
                DrawActionButton(e.Graphics, btnView, "👁", Color.FromArgb(0, 188, 212), "view");

                // Button 2: Edit (Blue)
                var btnEdit = new Rectangle(startX + buttonWidth + spacing, startY, buttonWidth, buttonHeight);
                DrawActionButton(e.Graphics, btnEdit, "✏", Color.FromArgb(33, 150, 243), "edit");

                // Button 3: Delete (Red)
                var btnDelete = new Rectangle(startX + (buttonWidth + spacing) * 2, startY, buttonWidth, buttonHeight);
                DrawActionButton(e.Graphics, btnDelete, "🗑", Color.FromArgb(244, 67, 54), "delete");

                e.Handled = true;
            }
        }

        private void DrawActionButton(Graphics g, Rectangle rect, string icon, Color color, string action)
        {
            // Draw rounded rectangle
            using (var path = GetRoundedRectPath(rect, 8))
            using (var brush = new SolidBrush(color))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.FillPath(brush, path);
            }

            // Draw icon
            using (var font = new Font("Segoe UI", 12))
            using (var textBrush = new SolidBrush(Color.White))
            using (var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            {
                g.DrawString(icon, font, textBrush, rect, sf);
            }
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

        // ============================================
        // CELL FORMATTING (Status Badges)
        // ============================================

        private void DgvFlights_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Format Status column with colored badges
            if (dgvFlights.Columns[e.ColumnIndex].Name == "colStatus" && e.RowIndex >= 0)
            {
                string status = e.Value?.ToString() ?? "";

                switch (status)
                {
                    case "Còn chỗ":
                        e.CellStyle.BackColor = Color.FromArgb(200, 230, 201); // Light Green
                        e.CellStyle.ForeColor = Color.FromArgb(46, 125, 50); // Dark Green
                        e.CellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                        break;

                    case "Đã đầy":
                        e.CellStyle.BackColor = Color.FromArgb(255, 249, 196); // Light Yellow
                        e.CellStyle.ForeColor = Color.FromArgb(245, 127, 23); // Dark Yellow
                        e.CellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                        break;

                    case "Đã hủy":
                        e.CellStyle.BackColor = Color.FromArgb(255, 205, 210); // Light Red
                        e.CellStyle.ForeColor = Color.FromArgb(198, 40, 40); // Dark Red
                        e.CellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                        break;
                }

                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        // ============================================
        // CELL CLICK EVENTS
        // ============================================

        private void DgvFlights_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Check if clicked on Actions column
            if (e.ColumnIndex == dgvFlights.Columns["colActions"].Index)
            {
                var rect = dgvFlights.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                var clickPoint = dgvFlights.PointToClient(Cursor.Position);

                // Calculate button positions (same as in CellPainting)
                int buttonWidth = 35;
                int spacing = 10;
                int startX = rect.X + (rect.Width - (buttonWidth * 3 + spacing * 2)) / 2;

                var localX = clickPoint.X - startX;

                // Get flight data
                var flight = dgvFlights.Rows[e.RowIndex].Tag as FlightData;
                if (flight == null) return;

                // Determine which button was clicked
                if (localX >= 0 && localX < buttonWidth)
                {
                    // View button
                    HandleViewFlight(flight);
                }
                else if (localX >= buttonWidth + spacing && localX < buttonWidth * 2 + spacing)
                {
                    // Disable button
                    HandleEditFlight(flight);
                }
                else if (localX >= (buttonWidth + spacing) * 2 && localX < buttonWidth * 3 + spacing * 2)
                {
                    // Delete button
                    HandleDisableFlight(flight);
                }
            }
        }

        // ============================================
        // ACTION HANDLERS
        // ============================================

        private void HandleViewFlight(FlightData flight)
        {
            try
            {
                // TODO: Mở form xem chi tiết chuyến bay (readonly)
                MessageBox.Show(
                    $"Xem chi tiết chuyến bay:\n\n" +
                    $"Mã: {flight.FlightCode}\n" +
                    $"Hãng: {flight.Airline}\n" +
                    $"Tuyến: {flight.Route}\n" +
                    $"Ngày: {flight.Date} {flight.Departure}\n" +
                    $"Máy bay: {flight.Aircraft}\n" +
                    $"Giá: {flight.Price}\n" +
                    $"Ghế: {flight.Seats}\n" +
                    $"Trạng thái: {flight.Status}",
                    "Chi tiết chuyến bay",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // Hoặc mở form khác:
                // var detailForm = new FlightDetailForm(flight);
                // detailForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HandleDisableFlight(FlightData flight)
        {
            try
            {
                var result = MessageBox.Show(
                    $"Bạn có chắc muốn VÔ HIỆU HÓA chuyến bay {flight.FlightCode}?\n\n" +
                    $"Chuyến bay sẽ không thể đặt vé nữa.",
                    "Xác nhận vô hiệu hóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    // TODO: Call API/Service để vô hiệu hóa chuyến bay
                    // await _flightService.DisableFlightAsync(flight.FlightCode);

                    flight.Status = "Đã hủy";
                    LoadFlightData();

                    MessageBox.Show(
                        $"Đã vô hiệu hóa chuyến bay {flight.FlightCode}",
                        "Thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void HandleEditFlight(FlightData flight)
        {
            try
            {
                // TODO: Mở form AddEditFlightForm ở chế độ EDIT
                // var editForm = new AddEditFlightForm(FormMode.Edit, flight);
                // if (editForm.ShowDialog() == DialogResult.OK)
                // {
                //     RefreshData();
                // }

                MessageBox.Show(
                    $"Form chỉnh sửa chuyến bay:\n\n" +
                    $"Mã: {flight.FlightCode}\n" +
                    $"Hãng: {flight.Airline}\n" +
                    $"Tuyến: {flight.Route}\n\n" +
                    $"Cho phép chỉnh sửa:\n" +
                    $"- Ngày giờ khởi hành\n" +
                    $"- Máy bay\n" +
                    $"- Giá vé\n" +
                    $"- Trạng thái\n\n" +
                    $"KHÔNG cho sửa:\n" +
                    $"- Mã chuyến bay\n" +
                    $"- Hãng hàng không\n" +
                    $"- Tuyến bay",
                    "Chỉnh sửa chuyến bay",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HandleDeleteFlight(FlightData flight)
        {
            try
            {
                var result = MessageBox.Show(
                    $"Bạn có chắc muốn XÓA chuyến bay {flight.FlightCode}?\n\n" +
                    $"⚠️ Hành động này không thể hoàn tác!",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    // TODO: Call API/Service để xóa chuyến bay
                    // await _flightService.DeleteFlightAsync(flight.FlightCode);

                    allFlights.Remove(flight);
                    filteredFlights.Remove(flight);
                    totalRecords = filteredFlights.Count;
                    LoadFlightData();

                    MessageBox.Show(
                        $"Đã xóa chuyến bay {flight.FlightCode}",
                        "Thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void RefreshData()
        {
            LoadSampleData();
            LoadFlightData();
        }

        public void ClearFilters()
        {
            txtFlightNo.Clear();
            cboAirline.SelectedIndex = 0;
            cboStatus.SelectedIndex = 0;
            cboDestination.SelectedIndex = 0;
            guna2DateTimePicker1.Value = DateTime.Now;

            filteredFlights = new List<FlightData>(allFlights);
            currentPage = 1;
            totalRecords = filteredFlights.Count;
            LoadFlightData();
        }

        private void btnAddFlight_Click(object sender, EventArgs e)
        {
            try
            {
                // TODO: Tạo và mở form AddEditFlightForm ở chế độ ADD
                var addForm = new AddEditFlightForm(FormMode.Add);
                if (addForm.ShowDialog() == DialogResult.OK)
                {
                    RefreshData();
                }

                //MessageBox.Show(
                //    "Form 'Thêm chuyến bay mới' sẽ hiển thị ở đây.\n\n" +
                //    "Tạo form AddEditFlightForm với các fields:\n" +
                //    "- Số hiệu chuyến bay\n" +
                //    "- Hãng hàng không\n" +
                //    "- Sân bay đi/đến\n" +
                //    "- Ngày/Giờ khởi hành\n" +
                //    "- Máy bay\n" +
                //    "- Giá vé\n" +
                //    "- Số ghế",
                //    "Thêm chuyến bay mới",
                //    MessageBoxButtons.OK,
                //    MessageBoxIcon.Information
                //);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    public class FlightData
    {
        public string FlightCode { get; set; }
        public string Airline { get; set; }
        public string Route { get; set; }
        public string Date { get; set; }
        public string Departure { get; set; }
        public string Duration { get; set; }
        public string Aircraft { get; set; }
        public string Price { get; set; }
        public string Seats { get; set; }
        public string Status { get; set; }
    }
}
