using AirlineReservation_AR.src.Domain.DTOs;
using AirlineReservation_AR.src.Infrastructure.DI;
using AirlineReservation_AR.src.Presentation__Winform_.Controllers;
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
        private const int pageSize = 20;
        private int totalRecords = 0;
        private int totalPages = 0;
        private List<FlightListDtoAdmin> allFlights = new List<FlightListDtoAdmin>();
        private List<FlightListDtoAdmin> filteredFlights = new List<FlightListDtoAdmin>();

        private readonly FlightControllerAdmin _flightController = DIContainer.FlightControllerAdmin;
        public FlightManagementControl()
        {
            InitializeComponent();
            InitializeStyles();
            InitializeData();
            InitializeEvents();

            LoadFlightDataAsync();
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
                "All",
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
                "All",
                "Available",
                "Full",
                "Cancelled"
            });
            cboStatus.SelectedIndex = 0;

            // ComboBox - Sân bay đến
            cboDestination.Items.Clear();
            cboDestination.Items.AddRange(new object[]
            {
                "All",
                "Hanoi (HAN)",
                "Da Nang (DAD)",
                "Phu Quoc (PQC)",
                "Nha Trang (CXR)",
                "Can Tho (VCA)",
                "Hue (HUI)",
                "Vinh (VII)"
            });
            cboDestination.SelectedIndex = 0;

            // DateTimePicker
            guna2DateTimePicker1.Value = DateTime.Now;
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
            paginationControl1.PageChanged += paginationControl1_PageChanged;
        }

        // ============================================
        // LOAD & DISPLAY DATA
        // ============================================

        private async Task LoadFlightDataAsync()
        {
            try
            {
                // Hiển thị loading (optional)
                dgvFlights.Enabled = false;
                Cursor = Cursors.WaitCursor;

                if (paginationControl1 != null)
                    paginationControl1.Enabled = false;
                // GỌI API LẤY DỮ LIỆU
                var flights = await _flightController.GetAllFlightsAsync();

                // Gán vào list
                allFlights = flights.ToList();
                filteredFlights = new List<FlightListDtoAdmin>(allFlights);
                totalRecords = filteredFlights.Count;
                currentPage = 1;
                // Hiển thị lên DataGridView
                LoadFlightData();

                // Tắt loading
                dgvFlights.Enabled = true;
                if (paginationControl1 != null)
                    paginationControl1.Enabled = true;
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                dgvFlights.Enabled = true;
                //if (paginationControl1 != null)
                //    paginationControl1.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        // GIỮ NGUYÊN method này (chỉ hiển thị, không load từ DB)
        private void LoadFlightData()
        {
            // Calculate pagination
            totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            if (currentPage > totalPages && totalPages > 0)
                currentPage = totalPages;

            if (paginationControl1 != null)
            {
                paginationControl1.TotalPages = totalPages;
                paginationControl1.CurrentPage = currentPage;
            }

            // Get data for current page
            var pagedData = filteredFlights
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Clear and load to DataGridView
            dgvFlights.Rows.Clear();

            foreach (var flight in pagedData)
            {
                int rowIndex = dgvFlights.Rows.Add(
                    flight.FlightCode,
                    flight.Airline,
                    flight.Route,
                    flight.FlightDate.ToString("yyyy-MM-dd"),
                    flight.DepartureTime.ToString(@"hh\:mm"),
                    flight.ArrivalTime.ToString(@"hh\:mm"),
                    flight.Aircraft,
                    flight.BasePrice,
                    flight.AvailableSeats,
                    flight.Status,
                    "•••"
                );

                dgvFlights.Rows[rowIndex].Tag = flight;
            }
            UpdatePaginationInfo();
        }
        private void UpdatePaginationInfo()
        {
            int firstItem = totalRecords > 0 ? (currentPage - 1) * pageSize + 1 : 0;
            int lastItem = Math.Min(currentPage * pageSize, totalRecords);
        }

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
                string airline = cboAirline.SelectedItem?.ToString() ?? "All";
                string status = cboStatus.SelectedItem?.ToString() ?? "All";
                string destination = cboDestination.SelectedItem?.ToString() ?? "All";
                DateTime filterDate = guna2DateTimePicker1.Value.Date;

                // Apply filters
                filteredFlights = allFlights.Where(f =>
                {
                    bool match = true;

                    // Filter by flight code
                    if (!string.IsNullOrEmpty(flightNo))
                        match = match && f.FlightCode.Contains(flightNo);

                    // Filter by airline
                    if (airline != "All" && airline != "Tất cả")
                        match = match && f.Airline == airline;

                    // Filter by status
                    if (status != "All" && status != "Tất cả")
                        match = match && f.Status == status;

                    // Filter by destination
                    if (destination != "All" && destination != "Tất cả")
                    {
                        string destCode = destination.Split('(', ')')[1].Trim();
                        match &= f.Route.EndsWith(destCode)
                        || f.Route.Contains(destCode);
                    }

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

        private void DgvFlights_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Format Status column with colored badges
            if (dgvFlights.Columns[e.ColumnIndex].Name == "colStatus" && e.RowIndex >= 0)
            {
                string status = e.Value?.ToString() ?? "";

                switch (status)
                {
                    case "Available":
                        e.CellStyle.BackColor = Color.FromArgb(200, 230, 201); // Light Green
                        e.CellStyle.ForeColor = Color.FromArgb(46, 125, 50); // Dark Green
                        e.CellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                        break;

                    case "Full":
                        e.CellStyle.BackColor = Color.FromArgb(255, 249, 196); // Light Yellow
                        e.CellStyle.ForeColor = Color.FromArgb(245, 127, 23); // Dark Yellow
                        e.CellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                        break;

                    case "Cancelled":
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
                var flight = dgvFlights.Rows[e.RowIndex].Tag as FlightListDtoAdmin;
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

        private async void HandleViewFlight(FlightListDtoAdmin flight)
        {
            try
            {
                var fullFlightData = await _flightController.GetFlightByIdAsync(flight.FlightId);

                if (fullFlightData == null)
                {
                    MessageBox.Show("Không tìm thấy thông tin chuyến bay!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                // Mở form chi tiết với đầy đủ thông tin
                var detailForm = new FlightDetailForm(flight);
                detailForm.ShowDialog();

                // Nếu có thay đổi, refresh lại danh sách
                if (detailForm.DialogResult == DialogResult.OK)
                {
                    // Refresh lại DataGridView
                    LoadFlightData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HandleDisableFlight(FlightListDtoAdmin flight)
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
        private void HandleEditFlight(FlightListDtoAdmin flight)
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

        private void HandleDeleteFlight(FlightListDtoAdmin flight)
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

        public async void RefreshData()
        {
            await LoadFlightDataAsync();
        }

        public void ClearFilters()
        {
            txtFlightNo.Clear();
            cboAirline.SelectedIndex = 0;
            cboStatus.SelectedIndex = 0;
            cboDestination.SelectedIndex = 0;
            guna2DateTimePicker1.Value = DateTime.Now;

            filteredFlights = new List<FlightListDtoAdmin>(allFlights);
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

        private void paginationControl1_PageChanged(object sender, int e)
        {
            currentPage = e;
            LoadFlightData();
        }
    }
}