using AirlineReservation_AR.src.AirlineReservation.Application.Services;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.Domain.DTOs;
using AirlineReservation_AR.src.Infrastructure.DI;
using AirlineReservation_AR.src.Presentation__Winform_.Controllers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient; // Thay vì System.Data.SqlClient

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

        public BookingAndPaymentControl()
        {
            InitializeComponent();
            dgvBooking.Columns.Clear();
            InitializeDataGridView();
            InitializeStyles();
            InitializeEvents();
            PopulateComboBoxes();
            InitializePagination();
            this.Load += BookingAndPaymentControl_Load;
        }

        private async void BookingAndPaymentControl_Load(object sender, EventArgs e)
        {
            await LoadBookingsAsync();
        }
        private void InitializePagination()
        {
            // Đăng ký event khi người dùng chuyển trang
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
            dgvBooking.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvBooking.GridColor = Color.FromArgb(240, 240, 240);

            AddCustomColumns();
        }

        private void AddCustomColumns()
        {
            // Cột Mã Booking
            var colBookingID = new DataGridViewTextBoxColumn
            {
                Name = "colBookingID",
                HeaderText = "MÃ BOOKING",
                Width = 150,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(231, 76, 60),
                    Alignment = DataGridViewContentAlignment.MiddleLeft
                }
            };
            dgvBooking.Columns.Add(colBookingID);

            // Cột Khách hàng
            var colKH = new DataGridViewTextBoxColumn
            {
                Name = "colKH",
                HeaderText = "KHÁCH HÀNG",
                Width = 220,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
                    Alignment = DataGridViewContentAlignment.MiddleLeft
                }
            };
            dgvBooking.Columns.Add(colKH);

            // Cột Chuyến bay
            var colChuyenBay = new DataGridViewTextBoxColumn
            {
                Name = "colChuyenBay",
                HeaderText = "CHUYẾN BAY",
                Width = 250,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new Font("Segoe UI", 9.5F),
                    Alignment = DataGridViewContentAlignment.MiddleLeft
                }
            };
            dgvBooking.Columns.Add(colChuyenBay);

            // Cột Số hành khách
            var colMember = new DataGridViewTextBoxColumn
            {
                Name = "colMember",
                HeaderText = "SỐ HÀNH KHÁCH",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 9.5F, FontStyle.Bold)
                }
            };
            dgvBooking.Columns.Add(colMember);

            // Cột Ngày đặt
            var colNgayDat = new DataGridViewTextBoxColumn
            {
                Name = "colNgayDat",
                HeaderText = "NGÀY ĐẶT",
                Width = 150,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 9F)
                }
            };
            dgvBooking.Columns.Add(colNgayDat);

            // Cột Tổng tiền
            var colTotalPrice = new DataGridViewTextBoxColumn
            {
                Name = "colTotalPrice",
                HeaderText = "TỔNG TIỀN",
                Width = 140,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(230, 81, 0),
                    Format = "#,##0 ₫"
                }
            };
            dgvBooking.Columns.Add(colTotalPrice);

            // Cột Thanh toán (phương thức)
            var colPayment = new DataGridViewTextBoxColumn
            {
                Name = "colPayment",
                HeaderText = "THANH TOÁN",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 9F)
                }
            };
            dgvBooking.Columns.Add(colPayment);

            // Cột Trạng thái
            var colStatus = new DataGridViewTextBoxColumn
            {
                Name = "colStatus",
                HeaderText = "TRẠNG THÁI",
                Width = 150,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold)
                }
            };
            dgvBooking.Columns.Add(colStatus);

            // Cột Thao tác (buttons)
            var colThaoTac = new DataGridViewButtonColumn
            {
                Name = "colThaoTac",
                HeaderText = "THAO TÁC",
                Width = 120,
                Text = "•••",
                UseColumnTextForButtonValue = false,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    BackColor = Color.FromArgb(52, 152, 219),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 12F, FontStyle.Bold)
                }
            };
            dgvBooking.Columns.Add(colThaoTac);
        }

        private void InitializeStyles()
        {
            dgvBooking.EnableHeadersVisualStyles = false;
            dgvBooking.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(155, 89, 182);
            dgvBooking.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvBooking.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgvBooking.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvBooking.ColumnHeadersDefaultCellStyle.Padding = new Padding(5);
            dgvBooking.ColumnHeadersHeight = 50;

            dgvBooking.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            dgvBooking.DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 240, 255);
            dgvBooking.DefaultCellStyle.SelectionForeColor = Color.FromArgb(52, 73, 94);
            dgvBooking.DefaultCellStyle.Padding = new Padding(5);
            dgvBooking.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 252);
            dgvBooking.RowTemplate.Height = 80;
            dgvBooking.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBooking.Cursor = Cursors.Hand;
        }

        private void InitializeEvents()
        {
            btnGetAll.Click += (s, e) => ResetFilters();
            btnGetChoThanhToan.Click += (s, e) => { cboStatusPayment.SelectedIndex = 2; ApplyFilters(); };
            btnGetToday.Click += (s, e) => { dtpTuNgay.Value = dtpDenNgay.Value = DateTime.Today; ApplyFilters(); };
            btnGetDaHuy.Click += (s, e) => { cboStatusBooking.SelectedIndex = 3; ApplyFilters(); };

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

        private void PopulateComboBoxes()
        {
            cboStatusBooking.Items.AddRange(new[] { "Tất cả", "Đã xác nhận", "Chờ xác nhận", "Đã hủy" });
            cboStatusPayment.Items.AddRange(new[] { "Tất cả", "Đã thanh toán", "Chờ thanh toán", "Thất bại" });
            cboStatusBooking.SelectedIndex = cboStatusPayment.SelectedIndex = 0;
        }

        private async Task LoadBookingsAsync()
        {
            try
            {
                dgvBooking.Enabled = false;
                this.Cursor = Cursors.WaitCursor;

                var data = await Task.Run(async () =>
                {
                    return await _bookingController.GetAllAsync();
                });

                if (data != null)
                {
                    bookings = data.ToList();
                    filteredBookings = new List<BookingDtoAdmin>(bookings);
                    ApplyFilters();

                    MessageBox.Show($"✓ Đã tải {bookings.Count} booking thành công!",
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    bookings = new List<BookingDtoAdmin>();
                    filteredBookings = new List<BookingDtoAdmin>();
                    RefreshDataGridView();

                    MessageBox.Show("Không có dữ liệu booking nào!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (TimeoutException)
            {
                MessageBox.Show(
                    "⏱ Kết nối database bị timeout!\n\n" +
                    "Nguyên nhân có thể:\n" +
                    "• SQL Server chưa chạy\n" +
                    "• Connection string sai\n" +
                    "• Database quá lớn\n" +
                    "• Mạng chậm\n\n" +
                    "Hãy kiểm tra SQL Server và connection string!",
                    "Lỗi Timeout",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (SqlException sqlEx)
            {
                // Lấy tất cả inner exception
                var innerMessage = sqlEx.InnerException?.Message ?? "Không có inner exception";

                MessageBox.Show(
                    $"❌ Lỗi SQL Server:\n\n" +
                    $"Message: {sqlEx.Message}\n\n" +
                    $"Inner Exception: {innerMessage}\n\n" +
                    $"Error Number: {sqlEx.Number}\n" +
                    $"Line Number: {sqlEx.LineNumber}\n" +
                    $"Server: {sqlEx.Server}\n" +
                    $"Procedure: {sqlEx.Procedure}\n\n" +
                    $"Stack Trace:\n{sqlEx.StackTrace}",
                    "Lỗi Database",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"❌ Lỗi không xác định:\n\n{ex.Message}\n\n" +
                    $"Stack Trace:\n{ex.StackTrace}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                dgvBooking.Enabled = true;
                this.Cursor = Cursors.Default;
            }
        }

        private void ResetFilters()
        {
            txtBookingID.Clear();
            textEmailSDT.Clear();
            dtpTuNgay.Value = DateTime.Now.AddMonths(-3);
            dtpDenNgay.Value = DateTime.Now;
            cboStatusBooking.SelectedIndex = cboStatusPayment.SelectedIndex = 0;
            _currentPage = 1;
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            var data = bookings.AsEnumerable();

            // Lọc theo Booking Reference
            if (!string.IsNullOrWhiteSpace(txtBookingID.Text))
                data = data.Where(b => b.BookingReference.Contains(txtBookingID.Text, StringComparison.OrdinalIgnoreCase));

            // Lọc theo Email/SĐT/Tên khách hàng (sử dụng thuộc tính DTO)
            if (!string.IsNullOrWhiteSpace(textEmailSDT.Text))
                data = data.Where(b =>
                    (b.ContactEmail?.Contains(textEmailSDT.Text, StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (b.ContactPhone?.Contains(textEmailSDT.Text) ?? false) ||
                    (b.CustomerName?.Contains(textEmailSDT.Text, StringComparison.OrdinalIgnoreCase) ?? false));

            // Lọc theo ngày
            data = data.Where(b => b.BookingDate.Date >= dtpTuNgay.Value.Date &&
                                  b.BookingDate.Date <= dtpDenNgay.Value.Date);

            // Lọc theo trạng thái booking
            if (cboStatusBooking.SelectedIndex > 0)
            {
                string statusFilter = cboStatusBooking.SelectedItem.ToString();
                if (statusFilter == "Đã xác nhận") statusFilter = "Confirmed";
                else if (statusFilter == "Chờ xác nhận") statusFilter = "Pending";
                else if (statusFilter == "Đã hủy") statusFilter = "Cancelled";

                data = data.Where(b => b.Status?.Equals(statusFilter, StringComparison.OrdinalIgnoreCase) ?? false);
            }

            // Lọc theo trạng thái thanh toán (sử dụng PaymentStatus từ DTO)
            if (cboStatusPayment.SelectedIndex > 0)
            {
                string paymentFilter = cboStatusPayment.SelectedItem.ToString();
                if (paymentFilter == "Đã thanh toán") paymentFilter = "Completed";
                else if (paymentFilter == "Chờ thanh toán") paymentFilter = "Pending";
                else if (paymentFilter == "Thất bại") paymentFilter = "Failed";

                data = data.Where(b => b.PaymentStatus?.Equals(paymentFilter, StringComparison.OrdinalIgnoreCase) ?? false);
            }

            filteredBookings = data.ToList();
            if (_currentPage > _totalPages) _currentPage = _totalPages;
            if (_currentPage < 1) _currentPage = 1;

            UpdatePagination();
            RefreshDataGridView();
        }
        private void UpdatePagination()
        {
            _totalRecords = filteredBookings.Count;
            _totalPages = _totalRecords > 0
                ? (int)Math.Ceiling((double)_totalRecords / _pageSize)
                : 1;

            if (_currentPage > _totalPages)
            {
                _currentPage = _totalPages;
            }

            // Cập nhật PaginationControl
            paginationControl.TotalPages = _totalPages;
            paginationControl.CurrentPage = _currentPage;
        }
        private List<BookingDtoAdmin> GetPagedData()
        {
            int skip = (_currentPage - 1) * _pageSize;
            return filteredBookings.Skip(skip).Take(_pageSize).ToList();
        }
        private void RefreshDataGridView()
        {
            dgvBooking.Rows.Clear();
            var pagedData = GetPagedData();
            foreach (var b in pagedData)
            {
                // Sử dụng FlightInfo từ DTO
                var flightInfo = b.FlightInfo != null
                    ? $"{b.FlightInfo.FlightNumber}\n{b.FlightInfo.DepartureAirport} → {b.FlightInfo.ArrivalAirport}\n{b.FlightInfo.FlightDate:dd/MM/yyyy} {b.FlightInfo.DepartureTime}"
                    : "Chưa có chuyến bay";

                // Sử dụng thuộc tính trực tiếp từ DTO
                var customerInfo = $"{b.CustomerName}\n{b.ContactEmail}\n{b.ContactPhone}";

                // Chuyển status sang tiếng Việt
                string displayStatus = b.Status switch
                {
                    "Confirmed" => "Đã xác nhận",
                    "Pending" => "Chờ xử lý",
                    "Cancelled" => "Đã hủy",
                    _ => b.Status ?? "N/A"
                };

                string displayPaymentStatus = b.PaymentStatus switch
                {
                    "Completed" => "Đã thanh toán",
                    "Pending" => "Chờ thanh toán",
                    "Failed" => "Thất bại",
                    _ => b.PaymentStatus ?? "N/A"
                };

                int rowIndex = dgvBooking.Rows.Add(
                    b.BookingReference,                              // colBookingID
                    customerInfo,                                    // colKH
                    flightInfo,                                      // colChuyenBay
                    $"{b.PassengerCount} người",                    // colMember
                    b.BookingDate.ToString("dd/MM/yyyy\nHH:mm"),    // colNgayDat
                    $"{b.TotalAmount:#,##0} ₫",                     // colTotalPrice
                    b.PaymentMethod ?? "—",                          // colPayment
                    displayPaymentStatus,                            // colStatus
                    "•••"                                            // colThaoTac
                );

                // Lưu booking DTO vào Tag
                dgvBooking.Rows[rowIndex].Tag = b;
            }
        }

        private void DgvBooking_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Format cho cột Status với màu badge đẹp
            if (dgvBooking.Columns[e.ColumnIndex].Name == "colStatus" && e.Value != null)
            {
                string status = e.Value.ToString();

                if (status.Contains("Đã xác nhận") || status.Contains("Confirmed"))
                {
                    e.CellStyle.BackColor = Color.FromArgb(212, 237, 218);
                    e.CellStyle.ForeColor = Color.FromArgb(27, 94, 32);
                }
                else if (status.Contains("Chờ") || status.Contains("Pending"))
                {
                    e.CellStyle.BackColor = Color.FromArgb(255, 243, 205);
                    e.CellStyle.ForeColor = Color.FromArgb(102, 60, 0);
                }
                else if (status.Contains("Đã hủy") || status.Contains("Cancelled") || status.Contains("Thất bại"))
                {
                    e.CellStyle.BackColor = Color.FromArgb(248, 215, 218);
                    e.CellStyle.ForeColor = Color.FromArgb(114, 28, 36);
                }
            }
        }

        private void DgvBooking_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Vẽ custom button cho cột Thao tác
            if (e.RowIndex >= 0 && dgvBooking.Columns[e.ColumnIndex].Name == "colThaoTac")
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);

                var rect = new Rectangle(
                    e.CellBounds.X + (e.CellBounds.Width - 80) / 2,
                    e.CellBounds.Y + (e.CellBounds.Height - 35) / 2,
                    80, 35);

                using (var brush = new SolidBrush(Color.FromArgb(52, 152, 219)))
                {
                    e.Graphics.FillRectangle(brush, rect);
                }

                TextRenderer.DrawText(e.Graphics, "•••",
                    new Font("Segoe UI", 14F, FontStyle.Bold),
                    rect, Color.White,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

                e.Handled = true;
            }
        }

        private async void DgvBooking_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Lấy DTO từ Tag
            var booking = dgvBooking.Rows[e.RowIndex].Tag as BookingDtoAdmin;
            if (booking == null) return;

            // Nếu click vào cột Thao tác
            if (dgvBooking.Columns[e.ColumnIndex].Name == "colThaoTac")
            {
                ShowActionMenu(booking, e.RowIndex);
            }
        }

        private void ShowActionMenu(BookingDtoAdmin booking, int rowIndex)
        {
            var menu = new ContextMenuStrip();
            menu.Font = new Font("Segoe UI", 10F);
            menu.BackColor = Color.White;

            // Xem chi tiết
            var viewItem = new ToolStripMenuItem("👁 Xem chi tiết");
            viewItem.Click += async (s, e) => await ViewBookingDetailAsync(booking.BookingReference);
            menu.Items.Add(viewItem);

            // Xác nhận thanh toán (chỉ hiện nếu chưa thanh toán)
            if (booking.PaymentStatus != "Completed")
            {
                var confirmItem = new ToolStripMenuItem("✓ Xác nhận thanh toán");
                confirmItem.Click += async (s, e) => await ConfirmBookingAsync(booking.BookingReference);
                menu.Items.Add(confirmItem);
            }

            // In vé
            var printItem = new ToolStripMenuItem("🖨 In vé");
            menu.Items.Add(printItem);

            menu.Items.Add(new ToolStripSeparator());

            // Hủy đặt chỗ (chỉ hiện nếu chưa hủy)
            if (booking.Status != "Cancelled")
            {
                var cancelItem = new ToolStripMenuItem("✕ Hủy đặt chỗ");
                cancelItem.ForeColor = Color.FromArgb(231, 76, 60);
                cancelItem.Click += async (s, e) => await CancelBookingAsync(booking.BookingReference);
                menu.Items.Add(cancelItem);
            }

            var cellRect = dgvBooking.GetCellDisplayRectangle(dgvBooking.Columns["colThaoTac"].Index, rowIndex, true);
            menu.Show(dgvBooking, new Point(cellRect.Left, cellRect.Bottom));
        }

        private async Task ViewBookingDetailAsync(string bookingRef)
        {
            try
            {
                var booking = await _bookingController.GetByReferenceAsync(bookingRef);
                if (booking == null)
                {
                    MessageBox.Show("Không tìm thấy đặt chỗ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Sử dụng thuộc tính từ DTO
                var flightInfo = booking.FlightInfo;

                string details = $"━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n" +
                                $"           CHI TIẾT ĐẶT CHỖ\n" +
                                $"━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n\n" +
                                $"📋 Mã đặt chỗ: {booking.BookingReference}\n" +
                                $"👤 Khách hàng: {booking.CustomerName}\n" +
                                $"📧 Email: {booking.ContactEmail}\n" +
                                $"📞 SĐT: {booking.ContactPhone}\n\n" +
                                $"✈ Chuyến bay: {flightInfo?.FlightNumber ?? "N/A"}\n" +
                                $"🛫 Khởi hành: {flightInfo?.DepartureAirport} ({flightInfo?.FlightDate:dd/MM/yyyy} {flightInfo?.DepartureTime})\n" +
                                $"🛬 Đến: {flightInfo?.ArrivalAirport} ({flightInfo?.ArrivalTime})\n" +
                                $"✈️ Máy bay: {flightInfo?.AircraftName} ({flightInfo?.AircraftType})\n" +
                                $"💺 Hạng ghế: {flightInfo?.SeatClass}\n\n" +
                                $"👥 Số hành khách: {booking.PassengerCount}\n" +
                                $"📅 Ngày đặt: {booking.BookingDate:dd/MM/yyyy HH:mm}\n\n" +
                                $"💰 Tổng tiền: {booking.TotalAmount:#,##0} ₫\n" +
                                $"💳 Phương thức: {booking.PaymentMethod ?? "N/A"}\n" +
                                $"📊 Trạng thái: {booking.Status}\n" +
                                $"💵 Thanh toán: {booking.PaymentStatus}\n" +
                                $"━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━";

                MessageBox.Show(details, $"Chi tiết đặt chỗ {bookingRef}",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task ConfirmBookingAsync(string bookingRef)
        {
            var confirm = MessageBox.Show(
                $"Xác nhận thanh toán và kích hoạt vé cho mã đặt chỗ:\n{bookingRef}\n\nBạn có chắc chắn?",
                "Xác nhận thanh toán", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                var success = await _bookingController.ConfirmBookingAsync(bookingRef);
                if (success)
                {
                    MessageBox.Show("Đã xác nhận thanh toán thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadBookingsAsync(); // Refresh lại danh sách
                }
                else
                {
                    MessageBox.Show("Xác nhận thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async Task CancelBookingAsync(string bookingRef)
        {
            var confirm = MessageBox.Show(
                $"Bạn có chắc muốn HỦY đặt chỗ:\n{bookingRef}\n\nHành khách sẽ được hoàn tiền (nếu có).",
                "Xác nhận hủy", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                var success = await _bookingController.CancelBookingAsync(bookingRef);
                if (success)
                {
                    MessageBox.Show("Đã hủy đặt chỗ thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadBookingsAsync();
                }
                else
                {
                    MessageBox.Show("Hủy thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}