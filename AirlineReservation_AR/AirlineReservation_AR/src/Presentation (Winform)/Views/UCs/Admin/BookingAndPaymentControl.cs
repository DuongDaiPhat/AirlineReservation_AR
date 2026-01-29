using AirlineReservation_AR.src.AirlineReservation.Application.Services;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.Application.Interfaces;
using AirlineReservation_AR.src.Domain.DTOs;
using AirlineReservation_AR.src.Infrastructure.DI;
using AirlineReservation_AR.src.Presentation__Winform_.Controllers;
using AirlineReservation_AR.src.Presentation__Winform_.Views.Forms.Admin;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.Admin
{
    public partial class BookingAndPaymentControl : UserControl
    {
        private int _currentPage = 1;
        private int _pageSize = 20;
        private int _totalRecords = 0;
        private int _totalPages = 0;

        private List<BookingDtoAdmin> bookings = new();
        private List<BookingDtoAdmin> filteredBookings = new();
        private readonly BookingControllerAdmin _bookingController = DIContainer.BookingControllerAdmin;
        private readonly ILookupService _lookupService = DIContainer.LookupService;

        public BookingAndPaymentControl()
        {
            InitializeComponent();
            InitializeDataGridView(); // Re-columning to English
            InitializeStyles();
            InitializeEvents();
            _ = InitializeFiltersAsync();
            InitializePagination();
            this.Load += BookingAndPaymentControl_Load;
        }

        private async void BookingAndPaymentControl_Load(object sender, EventArgs e)
        {
            await LoadBookingsAsync();
        }

        private void InitializePagination()
        {
            paginationControl.PageChanged += PaginationControl_PageChanged;
            paginationControl.CurrentPage = 1;
            paginationControl.TotalPages = 1;
        }

        private void PaginationControl_PageChanged(object sender, int pageNumber)
        {
            _currentPage = pageNumber;
            RefreshDataGridView();
        }

        private void InitializeDataGridView()
        {
            dgvBooking.Columns.Clear();
            dgvBooking.Rows.Clear();
            dgvBooking.AutoGenerateColumns = false;
            dgvBooking.AllowUserToAddRows = false;
            dgvBooking.AllowUserToDeleteRows = false;
            dgvBooking.ReadOnly = false;
            dgvBooking.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBooking.MultiSelect = false;
            dgvBooking.RowHeadersVisible = false;
            dgvBooking.BackgroundColor = Color.White;
            dgvBooking.BorderStyle = BorderStyle.None;
            
            // Enable text wrapping
            dgvBooking.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvBooking.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            // Columns
            AddColumn("colBookingID", "BOOKING REF", 150, true);
            AddColumn("colCustomer", "CUSTOMER", 250); // Increased width
            AddColumn("colFlight", "FLIGHT", 300);    // Increased width
            AddColumn("colPassengers", "PAX", 80, true);
            AddColumn("colDate", "DATE", 150, true);
            AddColumn("colTotal", "TOTAL", 140, false, true);
            AddColumn("colMethod", "METHOD", 120, true);
            AddColumn("colStatus", "STATUS", 150, true);
            
            // Action Column
            var colAction = new DataGridViewButtonColumn
            {
                Name = "colAction",
                HeaderText = "ACTION",
                Width = 100,
                Text = "•••",
                UseColumnTextForButtonValue = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    BackColor = Color.FromArgb(52, 152, 219),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 12F, FontStyle.Bold)
                }
            };
            dgvBooking.Columns.Add(colAction);
        }
        
        private void AddColumn(string name, string header, int width, bool center = false, bool isCurrency = false)
        {
            var col = new DataGridViewTextBoxColumn
            {
                Name = name,
                HeaderText = header,
                Width = width,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new Font("Segoe UI", 9.5F),
                    Alignment = center ? DataGridViewContentAlignment.MiddleCenter : 
                               (isCurrency ? DataGridViewContentAlignment.MiddleRight : DataGridViewContentAlignment.MiddleLeft),
                    ForeColor = isCurrency ? Color.FromArgb(230, 81, 0) : Color.Black
                }
            };
            if(name == "colBookingID") col.DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvBooking.Columns.Add(col);
        }

        private void InitializeStyles()
        {
            dgvBooking.EnableHeadersVisualStyles = false;
            dgvBooking.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(13, 27, 42); // Navy
            dgvBooking.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvBooking.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgvBooking.ColumnHeadersHeight = 50;
            dgvBooking.RowTemplate.Height = 60;
            
            // DateTime Picker Style - Include Time
            dtpTuNgay.Format = DateTimePickerFormat.Custom;
            dtpTuNgay.CustomFormat = "dd/MM/yyyy HH:mm";

            dtpDenNgay.Format = DateTimePickerFormat.Custom;
            dtpDenNgay.CustomFormat = "dd/MM/yyyy HH:mm";
        }

        private void InitializeEvents()
        {
            btnGetAll.Click += (s, e) => ResetFilters();
            btnGetChoThanhToan.Click += (s, e) => { cboStatusPayment.SelectedIndex = 2; ApplyFilters(); }; // Assuming Pending is index 2
            btnGetToday.Click += (s, e) => { dtpTuNgay.Value = DateTime.Today; dtpDenNgay.Value = DateTime.Today.AddDays(1).AddSeconds(-1); ApplyFilters(); };
            btnGetDaHuy.Click += (s, e) => { cboStatusBooking.SelectedIndex = 3; ApplyFilters(); }; // Assuming Cancelled index 3

            txtBookingID.TextChanged += (s, e) => ApplyFilters();
            textEmailSDT.TextChanged += (s, e) => ApplyFilters();
            dtpTuNgay.ValueChanged += (s, e) => ApplyFilters();
            dtpDenNgay.ValueChanged += (s, e) => ApplyFilters();
            cboStatusBooking.SelectedIndexChanged += (s, e) => ApplyFilters();
            cboStatusPayment.SelectedIndexChanged += (s, e) => ApplyFilters();

            dgvBooking.CellContentClick += DgvBooking_CellContentClick;
            dgvBooking.CellFormatting += DgvBooking_CellFormatting;
            dgvBooking.CellPainting += DgvBooking_CellPainting; 
        }

        private async Task InitializeFiltersAsync()
        {
            try
            {
                // Booking statuses
                var bookingStatuses = await _lookupService.GetBookingStatusesAsync();
                cboStatusBooking.Items.Clear();
                cboStatusBooking.Items.Add("All");
                foreach (var status in bookingStatuses) cboStatusBooking.Items.Add(status);
                cboStatusBooking.DisplayMember = "DisplayName";
                cboStatusBooking.SelectedIndex = 0;

                // Payment statuses
                var paymentStatuses = await _lookupService.GetPaymentStatusesAsync();
                cboStatusPayment.Items.Clear();
                cboStatusPayment.Items.Add("All");
                foreach (var status in paymentStatuses) cboStatusPayment.Items.Add(status);
                cboStatusPayment.DisplayMember = "DisplayName";
                cboStatusPayment.SelectedIndex = 0;

                // Date Filter Defaults
                dtpTuNgay.Value = DateTime.Now.AddMonths(-6);
                dtpDenNgay.Value = DateTime.Now;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading filters: {ex.Message}");
            }
        }

        private async Task LoadBookingsAsync()
        {
            try
            {
                dgvBooking.Enabled = false;
                Cursor = Cursors.WaitCursor;

                var data = await Task.Run(() => _bookingController.GetAllAsync());
                
                if (data != null)
                {
                    bookings = data.ToList();
                    filteredBookings = new List<BookingDtoAdmin>(bookings);
                    ApplyFilters();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading bookings: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dgvBooking.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private void ResetFilters()
        {
            txtBookingID.Clear();
            textEmailSDT.Clear();
            dtpTuNgay.Value = DateTime.Now.AddMonths(-3);
            dtpDenNgay.Value = DateTime.Now;
            cboStatusBooking.SelectedIndex = 0;
            cboStatusPayment.SelectedIndex = 0;
            _currentPage = 1;
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            var data = bookings.AsEnumerable();

            bool isSpecificSearch = !string.IsNullOrWhiteSpace(txtBookingID.Text) || !string.IsNullOrWhiteSpace(textEmailSDT.Text);

            if (!string.IsNullOrWhiteSpace(txtBookingID.Text))
                data = data.Where(b => b.BookingReference.Contains(txtBookingID.Text.Trim(), StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(textEmailSDT.Text))
            {
                string kw = textEmailSDT.Text.Trim();
                data = data.Where(b =>
                    (b.ContactEmail?.Contains(kw, StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (b.ContactPhone?.Contains(kw, StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (b.CustomerName?.Contains(kw, StringComparison.OrdinalIgnoreCase) ?? false));
            }

            // Only apply Date Filter if NOT searching specifically (Smart Search UX)
            if (!isSpecificSearch)
            {
                data = data.Where(b => b.BookingDate >= dtpTuNgay.Value && b.BookingDate <= dtpDenNgay.Value);
            }

            if (cboStatusBooking.SelectedIndex > 0 && cboStatusBooking.SelectedItem is StatusSelectDto bs)
                data = data.Where(b => b.Status?.Equals(bs.Code, StringComparison.OrdinalIgnoreCase) ?? false);

            if (cboStatusPayment.SelectedIndex > 0 && cboStatusPayment.SelectedItem is StatusSelectDto ps)
                data = data.Where(b => b.PaymentStatus?.Equals(ps.Code, StringComparison.OrdinalIgnoreCase) ?? false);

            filteredBookings = data.ToList();
            if (_currentPage > _totalPages) _currentPage = 1;

            UpdatePagination();
            RefreshDataGridView();
        }

        private void UpdatePagination()
        {
            _totalRecords = filteredBookings.Count;
            _totalPages = _totalRecords > 0 ? (int)Math.Ceiling((double)_totalRecords / _pageSize) : 1;
            
            if(_currentPage > _totalPages) _currentPage = _totalPages;
            
            paginationControl.TotalPages = _totalPages;
            paginationControl.CurrentPage = _currentPage;
        }

        private void RefreshDataGridView()
        {
            dgvBooking.Rows.Clear();
            var pagedData = filteredBookings.Skip((_currentPage - 1) * _pageSize).Take(_pageSize);

            foreach (var b in pagedData)
            {
                var flightInfo = b.FlightInfo != null
                    ? $"{b.FlightInfo.FlightNumber}\n{b.FlightInfo.DepartureAirport} -> {b.FlightInfo.ArrivalAirport}\n{b.FlightInfo.FlightDate:dd/MM/yyyy} {b.FlightInfo.DepartureTime:hh\\:mm}"
                    : "N/A";

                var customerInfo = $"Name: {b.CustomerName}\nPhone: {b.ContactPhone}\nEmail: {b.ContactEmail}";

                int rowIndex = dgvBooking.Rows.Add(
                    b.BookingReference,
                    customerInfo,
                    flightInfo,
                    b.PassengerCount,
                    b.BookingDate.ToString("dd/MM/yy HH:mm"),
                    $"{b.TotalAmount:N0} VND",
                    b.PaymentMethod ?? "-",
                    b.Status, 
                    "•••"
                );
                dgvBooking.Rows[rowIndex].Tag = b;
                dgvBooking.Rows[rowIndex].Height = 90; // Increased height for 3 lines with labels
            }
        }

        private void DgvBooking_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.Value == null) return;

            if (dgvBooking.Columns[e.ColumnIndex].Name == "colStatus")
            {
                string status = e.Value.ToString();
                if (status.Equals("Confirmed", StringComparison.OrdinalIgnoreCase))
                {
                    e.CellStyle.ForeColor = Color.Green;
                    e.CellStyle.SelectionForeColor = Color.Green;
                }
                else if (status.Equals("Pending", StringComparison.OrdinalIgnoreCase))
                {
                    e.CellStyle.ForeColor = Color.Goldenrod;
                    e.CellStyle.SelectionForeColor = Color.Goldenrod;
                }
                else if (status.Equals("Cancelled", StringComparison.OrdinalIgnoreCase))
                {
                    e.CellStyle.ForeColor = Color.Red;
                    e.CellStyle.SelectionForeColor = Color.Red;
                }
            }
        }

        private void DgvBooking_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Custom button painting if desired (simplified here, assuming standart button column works well enough or uses Guna styles)
            // Keeping original logic for consistency if possible, but simplified.
            if (e.RowIndex >= 0 && dgvBooking.Columns[e.ColumnIndex].Name == "colAction")
            {
               // Using default painting for now or custom logic from previous if prefered
               e.Paint(e.CellBounds, DataGridViewPaintParts.All);
               e.Handled = true;
            }
        }

        private void DgvBooking_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvBooking.Columns[e.ColumnIndex].Name == "colAction")
            {
                var booking = dgvBooking.Rows[e.RowIndex].Tag as BookingDtoAdmin;
                if (booking != null) ShowActionMenu(booking, e.RowIndex);
            }
        }

        private void ShowActionMenu(BookingDtoAdmin booking, int rowIndex)
        {
            var menu = new ContextMenuStrip();
            menu.Font = new Font("Segoe UI", 10F);

            var viewItem = new ToolStripMenuItem("View Details");
            viewItem.Click += (s, args) => 
            {
                 var form = new BookingDetailForm(booking);
                 form.ShowDialog();
            };
            menu.Items.Add(viewItem);

            // Edit Option (Allow editing unless Cancelled)
            if (!string.Equals(booking.Status, "Cancelled", StringComparison.OrdinalIgnoreCase))
            {
                var editItem = new ToolStripMenuItem("Edit Customer Info");
                editItem.Click += (s, args) => EditCustomerInfo(booking);
                menu.Items.Add(editItem);
            }

            // Confirm Payment only if Booking is Pending (not Confirmed or Cancelled)
            // Assuming "Pending" is the initial status. If DB uses different code, adjust.
            // Based on DgvBooking_CellFormatting, status is "Pending" or "Confirmed".
            if (string.Equals(booking.Status, "Pending", StringComparison.OrdinalIgnoreCase))
            {
                var confirmItem = new ToolStripMenuItem("Confirm Payment");
                confirmItem.Click += async (s, args) => await ConfirmBookingAsync(booking.BookingReference);
                menu.Items.Add(confirmItem);
            }

            var printItem = new ToolStripMenuItem("Print Ticket");
            printItem.Click += (s, args) => MessageBox.Show("Feature coming soon!", "Print", MessageBoxButtons.OK, MessageBoxIcon.Information);
            menu.Items.Add(printItem);

            menu.Items.Add(new ToolStripSeparator());

            if (booking.Status != "Cancelled")
            {
                var cancelItem = new ToolStripMenuItem("Cancel Booking");
                cancelItem.ForeColor = Color.Red;
                cancelItem.Click += async (s, args) => await CancelBookingAsync(booking.BookingReference);
                menu.Items.Add(cancelItem);
            }

            // Show menu
            if (dgvBooking.Rows.Count > rowIndex)
            {
                var cellRect = dgvBooking.GetCellDisplayRectangle(dgvBooking.Columns["colAction"].Index, rowIndex, true);
                menu.Show(dgvBooking, new Point(cellRect.Left, cellRect.Bottom));
            }
        }

        private async void EditCustomerInfo(BookingDtoAdmin booking)
        {
            var form = new AirlineReservation_AR.src.Presentation__Winform_.Views.Forms.Admin.EditBookingCustomerForm(
                booking.CustomerName, 
                booking.ContactPhone, 
                booking.ContactEmail
            );

            if (form.ShowDialog() == DialogResult.OK)
            {
                var success = await _bookingController.UpdateCustomerAsync(
                    booking.BookingReference, 
                    form.FullName, 
                    form.Phone, 
                    form.Email
                );

                if (success)
                {
                    MessageBox.Show("Customer info updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Refresh
                    bookings = (await _bookingController.GetAllAsync()).ToList();
                    ApplyFilters();
                }
                else
                {
                     MessageBox.Show("Failed to update customer info.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async Task ConfirmBookingAsync(string refCode)
        {
             var result = MessageBox.Show($"Confirm payment for booking {refCode}?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
             if(result == DialogResult.Yes)
             {
                 bool success = await _bookingController.ConfirmBookingAsync(refCode);
                 if(success)
                 {
                     MessageBox.Show("Payment confirmed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                     await LoadBookingsAsync();
                 }
                 else
                 {
                     MessageBox.Show("Failed to confirm.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 }
             }
        }

        private async Task CancelBookingAsync(string refCode)
        {
             var result = MessageBox.Show($"Cancel booking {refCode}?\nThis action cannot be undone.", "Cancel Booking", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
             if(result == DialogResult.Yes)
             {
                 bool success = await _bookingController.CancelBookingAsync(refCode);
                 if(success)
                 {
                     MessageBox.Show("Booking cancelled.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                     await LoadBookingsAsync();
                 }
                 else
                 {
                     MessageBox.Show("Failed to cancel.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 }
             }
        }
    }
}