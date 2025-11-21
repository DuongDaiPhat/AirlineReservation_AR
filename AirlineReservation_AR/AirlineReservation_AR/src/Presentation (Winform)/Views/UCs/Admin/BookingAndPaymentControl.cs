using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.Presentation__Winform_.Controllers;
using AirlineReservation_AR.src.Infrastructure.DI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.Admin
{
    public partial class BookingAndPaymentControl : UserControl
    {
        private List<Booking> bookings = new();
        private List<Booking> filteredBookings = new();
        private readonly BookingController _bookingController = DIContainer.BookingController;

        public BookingAndPaymentControl()
        {
            InitializeComponent();
            InitializeStyles();
            InitializeEvents();
            PopulateComboBoxes();
            this.Load += BookingAndPaymentControl_Load;
        }

        private async void BookingAndPaymentControl_Load(object sender, EventArgs e)
        {
            await LoadBookingsAsync();
        }

        private async Task LoadBookingsAsync()
        {
            try
            {
                // Hiển thị loading
                //pnlLoading.Visible = true;
                dgvBooking.Enabled = false;

                var data = await _bookingController.GetAllAsync();
                bookings = data.ToList();
                filteredBookings = new List<Booking>(bookings);

                ApplyFilters();
                //lblTotal.Text = $"Tổng: {bookings.Count} đặt chỗ";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu đặt chỗ:\n{ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //pnlLoading.Visible = false;
                dgvBooking.Enabled = true;
            }
        }

        private void InitializeStyles()
        {
            dgvBooking.EnableHeadersVisualStyles = false;
            dgvBooking.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(46, 80, 144);
            dgvBooking.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvBooking.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvBooking.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvBooking.ColumnHeadersHeight = 50;

            dgvBooking.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            dgvBooking.DefaultCellStyle.SelectionBackColor = Color.FromArgb(227, 242, 253);
            dgvBooking.DefaultCellStyle.SelectionForeColor = Color.FromArgb(25, 118, 210);
            dgvBooking.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgvBooking.RowTemplate.Height = 60;
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
        }

        private void PopulateComboBoxes()
        {
            cboStatusBooking.Items.AddRange(new[] { "Tất cả", "Đã xác nhận", "Chờ xác nhận", "Đã hủy" });
            cboStatusPayment.Items.AddRange(new[] { "Tất cả", "Đã thanh toán", "Chờ thanh toán", "Thất bại" });
            cboStatusBooking.SelectedIndex = cboStatusPayment.SelectedIndex = 0;
        }

        private void ResetFilters()
        {
            txtBookingID.Clear();
            textEmailSDT.Clear();
            dtpTuNgay.Value = DateTime.Now.AddMonths(-3);
            dtpDenNgay.Value = DateTime.Now;
            cboStatusBooking.SelectedIndex = cboStatusPayment.SelectedIndex = 0;
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            var data = bookings.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(txtBookingID.Text))
                data = data.Where(b => b.BookingReference.Contains(txtBookingID.Text, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(textEmailSDT.Text))
                data = data.Where(b =>
                    (b.ContactEmail?.Contains(textEmailSDT.Text, StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (b.ContactPhone?.Contains(textEmailSDT.Text) ?? false));

            data = data.Where(b => b.BookingDate.Date >= dtpTuNgay.Value.Date && b.BookingDate.Date <= dtpDenNgay.Value.Date);

            if (cboStatusBooking.SelectedIndex > 0)
                data = data.Where(b => b.Status == cboStatusBooking.SelectedItem.ToString());

            if (cboStatusPayment.SelectedIndex > 0)
                data = data.Where(b => b.Payments?.FirstOrDefault()?.Status == cboStatusPayment.SelectedItem.ToString());

            filteredBookings = data.ToList();
            RefreshDataGridView();
            //lblTotal.Text = $"Hiển thị: {filteredBookings.Count} / {bookings.Count} đặt chỗ";
        }

        private void RefreshDataGridView()
        {
            dgvBooking.Rows.Clear();

            foreach (var b in filteredBookings)
            {
                var flight = b.BookingFlights?.FirstOrDefault()?.Flight;
                var flightInfo = flight != null
                    ? $"{flight.FlightNumber} {flight.DepartureAirport?.AirportName} → {flight.ArrivalAirport?.AirportName}"
                    : "Chưa có chuyến bay";

                var payment = b.Payments?.FirstOrDefault();
                var amount = payment?.Amount ?? 0;
                var method = payment?.PaymentMethod ?? "—";
                var status = payment?.Status ?? "Chưa thanh toán";

                dgvBooking.Rows.Add(
                    b.BookingReference,
                    flightInfo,
                    $"{b.Passengers?.Count ?? 0} người",
                    b.BookingDate.ToString("dd/MM/yyyy HH:mm"),
                    $"{amount:#,##0} ₫",
                    method,
                    status,
                    "Thao tác"
                );
            }
        }

        private async void DgvBooking_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != colThaoTac.Index) return;

            var refCode = dgvBooking.Rows[e.RowIndex].Cells[colBookingID.Index].Value?.ToString();
            if (string.IsNullOrEmpty(refCode)) return;

            var menu = new ContextMenuStrip();
            menu.Font = new Font("Segoe UI", 9.5F);

            menu.Items.Add("Xem chi tiết vé", Properties.Resources.booking ?? null, async (s, ev) => await ViewBookingDetailAsync(refCode));
            menu.Items.Add("Xác nhận thanh toán", Properties.Resources.booking ?? null, async (s, ev) => await ConfirmBookingAsync(refCode));
            menu.Items.Add("Hủy đặt chỗ", Properties.Resources.booking ?? null, async (s, ev) => await CancelBookingAsync(refCode));

            menu.Show(dgvBooking, dgvBooking.PointToClient(Cursor.Position));
        }

        private async Task ViewBookingDetailAsync(string bookingRef)
        {
            var booking = await _bookingController.GetByReferenceAsync(bookingRef);
            if (booking == null)
            {
                MessageBox.Show("Không tìm thấy đặt chỗ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Bạn có thể mở form chi tiết đẹp ở đây
            MessageBox.Show(
                $"Mã đặt chỗ: {booking.BookingReference}\n" +
                $"Khách hàng: {booking.ContactEmail} - {booking.ContactPhone}\n" +
                $"Ngày đặt: {booking.BookingDate:dd/MM/yyyy HH:mm}\n" +
                $"Số hành khách: {booking.Passengers?.Count ?? 0}\n" +
                $"Tổng tiền: {(booking.Payments?.FirstOrDefault()?.Amount ?? 0):#,##0} ₫\n" +
                $"Trạng thái: {booking.Status} | Thanh toán: {booking.Payments?.FirstOrDefault()?.Status ?? "Chưa có"}",
                $"Chi tiết đặt chỗ {bookingRef}",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
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