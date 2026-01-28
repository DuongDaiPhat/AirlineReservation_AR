using AirlineReservation_AR.src.Domain.DTOs;
using AirlineReservation_AR.src.Application.Interfaces;
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
        private bool _isUpdatingPagination = false;
        private const int pageSize = 20;
        private int totalRecords = 0;
        private int totalPages = 0;
        private List<FlightListDtoAdmin> allFlights = new List<FlightListDtoAdmin>();
        private List<FlightListDtoAdmin> filteredFlights = new List<FlightListDtoAdmin>();

        private readonly FlightControllerAdmin _flightController = DIContainer.FlightControllerAdmin;
        private readonly ILookupService _lookupService = DIContainer.LookupService;
        
        public FlightManagementControl()
        {
            InitializeComponent();
            InitializeStyles();
            _ = InitializeFiltersAsync();  // Load dropdowns from DB
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

        private async Task InitializeFiltersAsync()
        {
            try
            {
                // Airlines dropdown - Load from DB
                var airlines = await _lookupService.GetAirlinesAsync();
                var airlineList = new List<AirlineSelectDto> 
                { 
                    new AirlineSelectDto { AirlineId = 0, DisplayName = "All" } 
                };
                airlineList.AddRange(airlines);
                
                cboAirline.DataSource = airlineList;
                cboAirline.DisplayMember = "DisplayName";
                cboAirline.ValueMember = "AirlineId";
                // cboAirline.SelectedIndex = 0; // DataSource assignment usually sets it to first item

                // Airports dropdown - Load from DB
                var airports = await _lookupService.GetAirportsAsync();
                var airportList = new List<AirportSelectDto>
                {
                    new AirportSelectDto { AirportId = 0, DisplayName = "All" }
                };
                airportList.AddRange(airports);

                cboDestination.DataSource = airportList;
                cboDestination.DisplayMember = "DisplayName";
                cboDestination.ValueMember = "AirportId";
                // cboDestination.SelectedIndex = 0;

                // Flight statuses dropdown - Load from service
                var statuses = await _lookupService.GetFlightStatusesAsync();
                var statusList = new List<StatusSelectDto>
                {
                    new StatusSelectDto { Code = "All", DisplayName = "All" }
                };
                statusList.AddRange(statuses);

                cboStatus.DataSource = statusList;
                cboStatus.DisplayMember = "DisplayName";
                cboStatus.ValueMember = "Code";
                // cboStatus.SelectedIndex = 0;

                // DateTimePicker
                guna2DateTimePicker1.Value = DateTime.Now;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading filters: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeEvents()
        {
            // Buttons
            btnSearch.Click += BtnSearch_Click;
            // btnAddFlight.Click += BtnAddFlight_Click; // Uncomment khi đã có btnAddFlight trong Designer

            // DataGridView events
            dgvFlights.CellPainting += DgvFlights_CellPainting;
            // dgvFlights.CellClick += DgvFlights_CellClick; // Handled in Designer
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
                // Show loading indicator
                dgvFlights.Enabled = false;
                Cursor = Cursors.WaitCursor;

                if (paginationControl1 != null)
                    paginationControl1.Enabled = false;
                    
                // Fetch data from API
                var flights = await _flightController.GetAllFlightsAsync();

                // Assign to lists
                allFlights = flights.ToList();
                filteredFlights = new List<FlightListDtoAdmin>(allFlights);
                totalRecords = filteredFlights.Count;
                currentPage = 1;
                
                // Display in DataGridView
                LoadFlightData();

                // Hide loading indicator
                dgvFlights.Enabled = true;
                if (paginationControl1 != null)
                    paginationControl1.Enabled = true;
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading flight data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                dgvFlights.Enabled = true;
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
                _isUpdatingPagination = true;
                paginationControl1.TotalPages = totalPages;
                paginationControl1.CurrentPage = currentPage;
                _isUpdatingPagination = false;
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
                e.SuppressKeyPress = true;
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            // Reset UI
            txtFlightNo.Text = "";
            cboAirline.SelectedIndex = 0; // Assuming 0 is "All" or "Select"
            if (cboStatus.Items.Count > 0) cboStatus.SelectedIndex = 0;
            if (cboDestination.Items.Count > 0) cboDestination.SelectedIndex = 0;
            guna2DateTimePicker1.Value = DateTime.Today; // Or Keep Date? Reset to Today seems safe.

            // Reset Data
            filteredFlights = allFlights.ToList();
            
            // Paging Reset
            currentPage = 1;
            totalRecords = filteredFlights.Count;
            
            // Reload
            LoadFlightData();
        }

        private void ApplyFilters()
        {
            try
            {
                // Get filter values
                string flightNo = txtFlightNo.Text.Trim().ToUpper();
                
                int airlineId = 0;
                if (cboAirline.SelectedValue is int aid) airlineId = aid;
                
                int destId = 0; 
                if (cboDestination.SelectedValue is int did) destId = did;
                
                string status = "All";
                if (cboStatus.SelectedValue != null) status = cboStatus.SelectedValue.ToString();
                
                DateTime filterDate = guna2DateTimePicker1.Value.Date;

                // Apply filters
                filteredFlights = allFlights.Where(f =>
                {
                    bool match = true;
                    bool specificSearch = false; // Flag to indicate if user is searching for something specific

                    // Filter by flight code
                    if (!string.IsNullOrEmpty(flightNo))
                    {
                        match &= f.FlightCode.ToUpper().Contains(flightNo);
                        specificSearch = true; 
                    }

                    // Filter by Airline (ID Check)
                    if (airlineId > 0)
                    {
                        match &= (f.AirlineId == airlineId);
                        specificSearch = true;
                    }

                    // Filter by status (String Check)
                    if (status != "All")
                        match &= string.Equals(f.Status, status, StringComparison.OrdinalIgnoreCase);

                    // Filter by Destination (ID Check - using ArrivalAirportId)
                    if (destId > 0)
                    {
                        match &= (f.ArrivalAirportId == destId);
                        // If user selects destination, they usually care about date too.
                    }

                    // Filter by Date
                    // Logic: If user searches by FlightNo, we IGNORE date (to help find it).
                    // If user only selects filters (Airline, Dest, etc.), we RESPECT date.
                    if (!specificSearch || string.IsNullOrEmpty(flightNo)) 
                    {
                         match &= f.FlightDate.Date == filterDate;
                    }

                    return match;
                }).ToList();

                // Reset to first page
                currentPage = 1;
                totalRecords = filteredFlights.Count;

                // Reload data
                LoadFlightData();

                // Show result feedback (Optional: Remove MessageBox to fix 'double notification')
                // Instead of MessageBox, we rely on the Grid being empty.
                // If you really want a notification, ensure it's not double triggered.
                // But removing it is the best UX for Admin Dashboards.
                if (filteredFlights.Count == 0)
                {
                    // Optional: Show a subtle label or nothing.
                    // MessageBox.Show("No matching flights found!", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Search error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                // Button 1: View (Custom Text)
                var btnView = new Rectangle(startX, startY, buttonWidth, buttonHeight);
                DrawActionButton(e.Graphics, btnView, "View", Color.FromArgb(0, 188, 212), "view");

                // Button 2: Edit (Custom Text)
                var btnEdit = new Rectangle(startX + buttonWidth + spacing, startY, buttonWidth, buttonHeight);
                DrawActionButton(e.Graphics, btnEdit, "Edit", Color.FromArgb(0, 123, 255), "edit");

                // Button 3: Disable/Delete (Custom Text)
                var btnDelete = new Rectangle(startX + (buttonWidth + spacing) * 2, startY, buttonWidth, buttonHeight);
                DrawActionButton(e.Graphics, btnDelete, "Del", Color.FromArgb(220, 53, 69), "delete");

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
                    case "Scheduled":
                    case "Active":
                        e.CellStyle.BackColor = Color.FromArgb(200, 230, 201); // Light Green
                        e.CellStyle.ForeColor = Color.FromArgb(46, 125, 50); // Dark Green
                        e.CellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                        break;

                    case "Full":
                    case "Completed":
                        e.CellStyle.BackColor = Color.FromArgb(224, 224, 224); // Light Gray
                        e.CellStyle.ForeColor = Color.FromArgb(97, 97, 97); // Dark Gray
                        e.CellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                        break;

                    case "Delayed":
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
                    HandleEditFlightAsync(flight);
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
                    MessageBox.Show("Flight details not found!", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                // Mở form chi tiết với đầy đủ thông tin
                var detailForm = new FlightDetailForm(flight);
                detailForm.ShowDialog();

                // Nếu có thay đổi, refresh lại danh sách
                if (detailForm.DialogResult == DialogResult.OK)
                {
                    // Refresh lại DataGridView từ Database
                    await LoadFlightDataAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void HandleDisableFlight(FlightListDtoAdmin flight)
        {
            try
            {
                var result = MessageBox.Show(
                    $"Are you sure to DISABLE flight {flight.FlightCode}?\n\n" +
                    $"Ticket booking will be stopped for this flight.",
                    "Confirm Disable",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    await _flightController.CancelFlightAsync(flight.FlightId);

                    RefreshData();

                    MessageBox.Show(
                        $"Flight {flight.FlightCode} disabled successfully.",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async Task HandleEditFlightAsync(FlightListDtoAdmin flight)
        {
            try
            {
                // Load detailed flight data for editing
                // Use the Controller to fetch pure DB data
                var fullFlightData = await _flightController.GetFlightByIdAsync(flight.FlightId);

                if (fullFlightData == null)
                {
                    MessageBox.Show("Flight details not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Open AddEditFlightForm in Edit Mode
                using (var editForm = new AddEditFlightForm(fullFlightData))
                {
                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        // Refresh data from DB to reflect changes
                        await LoadFlightDataAsync(); 
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening edit form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void HandleDeleteFlight(FlightListDtoAdmin flight)
        {
            try
            {
                var result = MessageBox.Show(
                    $"Are you sure you want to DELETE flight {flight.FlightCode}?\n\n" +
                    $"⚠️ This action cannot be undone!",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    await _flightController.DeleteFlightAsync(flight.FlightId);

                    RefreshData();

                    MessageBox.Show(
                        $"Flight {flight.FlightCode} deleted successfully.",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                var addForm = new AddEditFlightForm();
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
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void paginationControl1_PageChanged(object sender, int e)
        {
            if (_isUpdatingPagination) return;
            if (currentPage == e) return;
            currentPage = e;
            LoadFlightData();
        }
    }
}