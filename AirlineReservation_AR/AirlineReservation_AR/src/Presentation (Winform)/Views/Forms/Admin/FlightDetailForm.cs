using AirlineReservation_AR.src.Application.Interfaces;
using AirlineReservation_AR.src.Domain.DTOs;
using AirlineReservation_AR.src.Infrastructure.DI;
using AirlineReservation_AR.src.Presentation__Winform_.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirlineReservation_AR.src.Presentation__Winform_.Views.Forms.Admin
{
    public partial class FlightDetailForm : Form
    {
        private FlightListDtoAdmin _flight;
        private readonly FlightControllerAdmin _flightController;
        public FlightDetailForm(FlightListDtoAdmin flight)
        {
            InitializeComponent();
            _flight = flight;
            _flightController = DIContainer.FlightControllerAdmin;
            this.Load += FlightDetailForm_Load;
        }
        private void FlightDetailForm_Load(object sender, EventArgs e)
        {
            // Thiết lập form
            this.Text = "Chi tiết chuyến bay - " + _flight.FlightCode;
            this.Size = new Size(1100, 750);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(245, 245, 245);

            // Panel chính
            var mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                AutoScroll = true
            };
            this.Controls.Add(mainPanel);

            // Tiêu đề
            var lblTitle = new Label
            {
                Text = "THÔNG TIN CHUYẾN BAY",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 150, 243),
                AutoSize = true,
                Location = new Point(0, 0)
            };
            mainPanel.Controls.Add(lblTitle);

            // Panel chứa 2 cột
            var contentPanel = new TableLayoutPanel
            {
                Location = new Point(0, 50),
                Size = new Size(1040, 580),
                ColumnCount = 2,
                RowCount = 1
            };
            contentPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            contentPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            mainPanel.Controls.Add(contentPanel);

            // === PHẦN BÊN TRÁI (CHỈ XEM) ===
            var leftPanel = CreateViewPanel();
            contentPanel.Controls.Add(leftPanel, 0, 0);

            // === PHẦN BÊN PHẢI (CHỈNH SỬA) ===
            var rightPanel = CreateEditPanel();
            contentPanel.Controls.Add(rightPanel, 1, 0);

            // === PANEL NÚT BẤM ===
            var buttonPanel = new Panel
            {
                Location = new Point(0, 640),
                Size = new Size(1040, 50),
                Dock = DockStyle.None
            };
            mainPanel.Controls.Add(buttonPanel);

            // Nút Lưu
            var btnSave = new Button
            {
                Text = "💾 Lưu thay đổi",
                Size = new Size(150, 40),
                Location = new Point(720, 5),
                BackColor = Color.FromArgb(76, 175, 80),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += BtnSave_Click;
            buttonPanel.Controls.Add(btnSave);

            // Nút Vô hiệu hóa
            var btnDeactivate = new Button
            {
                Text = "🚫 Vô hiệu hóa",
                Size = new Size(150, 40),
                Location = new Point(880, 5),
                BackColor = Color.FromArgb(244, 67, 54),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnDeactivate.FlatAppearance.BorderSize = 0;
            btnDeactivate.Click += BtnDeactivate_Click;
            buttonPanel.Controls.Add(btnDeactivate);

            // Load dữ liệu
            LoadFlightData();
        }

        private Panel CreateViewPanel()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(15),
                AutoScroll = true
            };

            // Border
            panel.Paint += (s, e) =>
            {
                ControlPaint.DrawBorder(e.Graphics, panel.ClientRectangle,
                    Color.FromArgb(224, 224, 224), ButtonBorderStyle.Solid);
            };

            // Tiêu đề
            var lblHeader = new Label
            {
                Text = "📋 THÔNG TIN HIỆN TẠI",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(66, 66, 66),
                AutoSize = true,
                Location = new Point(15, 15)
            };
            panel.Controls.Add(lblHeader);

            int yPos = 50;
            int spacing = 48;

            // Thông tin chuyến bay
            AddViewField(panel, "Số hiệu:", "txtViewFlightNumber", ref yPos, spacing);
            AddViewField(panel, "Hãng hàng không:", "txtViewAirlineName", ref yPos, spacing);
            AddViewField(panel, "Máy bay:", "txtViewAircraftModel", ref yPos, spacing);

            // Điểm khởi hành
            AddViewField(panel, "Sân bay đi:", "txtViewDepartureAirport", ref yPos, spacing);
            AddViewField(panel, "Thành phố đi:", "txtViewDepartureCity", ref yPos, spacing);
            AddViewField(panel, "Mã IATA đi:", "txtViewDepartureIataCode", ref yPos, spacing);

            // Điểm đến
            AddViewField(panel, "Sân bay đến:", "txtViewArrivalAirport", ref yPos, spacing);
            AddViewField(panel, "Thành phố đến:", "txtViewArrivalCity", ref yPos, spacing);
            AddViewField(panel, "Mã IATA đến:", "txtViewArrivalIataCode", ref yPos, spacing);

            // Thời gian
            AddViewField(panel, "Ngày bay:", "txtViewFlightDate", ref yPos, spacing);
            AddViewField(panel, "Giờ khởi hành:", "txtViewDepartureTime", ref yPos, spacing);
            AddViewField(panel, "Giờ đến:", "txtViewArrivalTime", ref yPos, spacing);
            AddViewField(panel, "Thời gian bay:", "txtViewDuration", ref yPos, spacing);

            // Thông tin khác
            AddViewField(panel, "Giá cơ bản:", "txtViewBasePrice", ref yPos, spacing);
            AddViewField(panel, "Tổng ghế:", "txtViewTotalSeats", ref yPos, spacing);
            AddViewField(panel, "Ghế còn trống:", "txtViewAvailableSeats", ref yPos, spacing);
            AddViewField(panel, "Trạng thái:", "txtViewStatus", ref yPos, spacing);

            return panel;
        }

        private Panel CreateEditPanel()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(15),
                AutoScroll = true
            };

            // Border
            panel.Paint += (s, e) =>
            {
                ControlPaint.DrawBorder(e.Graphics, panel.ClientRectangle,
                    Color.FromArgb(224, 224, 224), ButtonBorderStyle.Solid);
            };

            // Tiêu đề
            var lblHeader = new Label
            {
                Text = "✏️ CHỈNH SỬA",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 150, 243),
                AutoSize = true,
                Location = new Point(15, 15)
            };
            panel.Controls.Add(lblHeader);

            int yPos = 50;
            int spacing = 48;

            // Thông tin chuyến bay
            AddEditField(panel, "Số hiệu:", "txtEditFlightNumber", ref yPos, spacing);
            AddEditField(panel, "Hãng hàng không:", "txtEditAirlineName", ref yPos, spacing);
            AddEditField(panel, "Máy bay:", "txtEditAircraftModel", ref yPos, spacing);

            // Điểm khởi hành
            AddEditField(panel, "Sân bay đi:", "txtEditDepartureAirport", ref yPos, spacing);
            AddEditField(panel, "Thành phố đi:", "txtEditDepartureCity", ref yPos, spacing);
            AddEditField(panel, "Mã IATA đi:", "txtEditDepartureIataCode", ref yPos, spacing);

            // Điểm đến
            AddEditField(panel, "Sân bay đến:", "txtEditArrivalAirport", ref yPos, spacing);
            AddEditField(panel, "Thành phố đến:", "txtEditArrivalCity", ref yPos, spacing);
            AddEditField(panel, "Mã IATA đến:", "txtEditArrivalIataCode", ref yPos, spacing);

            // Thời gian
            AddEditField(panel, "Ngày bay:", "dtpEditFlightDate", ref yPos, spacing, true);
            AddEditField(panel, "Giờ khởi hành:", "dtpEditDepartureTime", ref yPos, spacing, false, false, false, true);
            AddEditField(panel, "Giờ đến:", "dtpEditArrivalTime", ref yPos, spacing, false, false, false, true);
            AddEditField(panel, "Thời gian bay (phút):", "numEditDuration", ref yPos, spacing, false, true);

            // Thông tin khác
            AddEditField(panel, "Giá cơ bản:", "numEditBasePrice", ref yPos, spacing, false, true);
            AddEditField(panel, "Tổng ghế:", "numEditTotalSeats", ref yPos, spacing, false, true);
            AddEditField(panel, "Ghế còn trống:", "numEditAvailableSeats", ref yPos, spacing, false, true);
            AddEditField(panel, "Trạng thái:", "cboEditStatus", ref yPos, spacing, false, false, true);

            return panel;
        }

        private void AddViewField(Panel parent, string labelText, string controlName, ref int yPos, int spacing)
        {
            var lbl = new Label
            {
                Text = labelText,
                Location = new Point(15, yPos),
                Size = new Size(150, 20),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.FromArgb(97, 97, 97)
            };
            parent.Controls.Add(lbl);

            var txt = new TextBox
            {
                Name = controlName,
                Location = new Point(15, yPos + 22),
                Size = new Size(450, 25),
                Font = new Font("Segoe UI", 9),
                ReadOnly = true,
                BackColor = Color.FromArgb(250, 250, 250),
                BorderStyle = BorderStyle.FixedSingle
            };
            parent.Controls.Add(txt);

            yPos += spacing;
        }

        private void AddEditField(Panel parent, string labelText, string controlName, ref int yPos, int spacing,
            bool isDatePicker = false, bool isNumeric = false, bool isComboBox = false, bool isTimePicker = false)
        {
            var lbl = new Label
            {
                Text = labelText,
                Location = new Point(15, yPos),
                Size = new Size(150, 20),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.FromArgb(97, 97, 97)
            };
            parent.Controls.Add(lbl);

            Control control;

            if (isDatePicker)
            {
                control = new DateTimePicker
                {
                    Name = controlName,
                    Location = new Point(15, yPos + 22),
                    Size = new Size(450, 25),
                    Font = new Font("Segoe UI", 9),
                    Format = DateTimePickerFormat.Short
                };
            }
            else if (isTimePicker)
            {
                control = new DateTimePicker
                {
                    Name = controlName,
                    Location = new Point(15, yPos + 22),
                    Size = new Size(450, 25),
                    Font = new Font("Segoe UI", 9),
                    Format = DateTimePickerFormat.Time,
                    ShowUpDown = true
                };
            }
            else if (isNumeric)
            {
                control = new NumericUpDown
                {
                    Name = controlName,
                    Location = new Point(15, yPos + 22),
                    Size = new Size(450, 25),
                    Font = new Font("Segoe UI", 9),
                    Minimum = 0,
                    Maximum = 100000000,
                    DecimalPlaces = 0
                };
            }
            else if (isComboBox)
            {
                var cbo = new ComboBox
                {
                    Name = controlName,
                    Location = new Point(15, yPos + 22),
                    Size = new Size(450, 25),
                    Font = new Font("Segoe UI", 9),
                    DropDownStyle = ComboBoxStyle.DropDownList
                };
                cbo.Items.AddRange(new object[] { "Scheduled", "Active", "Completed", "Cancelled", "Delayed" });
                control = cbo;
            }
            else
            {
                control = new TextBox
                {
                    Name = controlName,
                    Location = new Point(15, yPos + 22),
                    Size = new Size(450, 25),
                    Font = new Font("Segoe UI", 9),
                    BorderStyle = BorderStyle.FixedSingle
                };
            }

            parent.Controls.Add(control);
            yPos += spacing;
        }

        private void LoadFlightData()
        {
            // ----- VIEW (BÊN TRÁI) -----

            SetControlText("txtViewFlightNumber", _flight.FlightCode);
            SetControlText("txtViewAirlineName", _flight.Airline);
            SetControlText("txtViewAircraftModel", _flight.Aircraft);

            // Route dạng: "SGN → HAN"
            SetControlText("txtViewDepartureAirport", _flight.Route.Split('→')[0]?.Trim());
            SetControlText("txtViewArrivalAirport", _flight.Route.Split('→')[1]?.Trim());

            SetControlText("txtViewFlightDate", _flight.FlightDate.ToShortDateString());
            SetControlText("txtViewDepartureTime", _flight.DepartureTime.ToString(@"hh\:mm"));
            SetControlText("txtViewArrivalTime", _flight.ArrivalTime.ToString(@"hh\:mm"));

            // Duration = Arrival - Departure
            var duration = _flight.ArrivalTime - _flight.DepartureTime;
            SetControlText("txtViewDuration", $"{(int)duration.TotalHours}h {duration.Minutes}m");

            SetControlText("txtViewBasePrice", _flight.BasePrice.ToString("N0") + " VND");
            SetControlText("txtViewTotalSeats", _flight.TotalSeats.ToString());
            SetControlText("txtViewAvailableSeats", _flight.AvailableSeats.ToString());
            SetControlText("txtViewStatus", _flight.Status);


            // ----- EDIT (BÊN PHẢI) -----

            SetControlText("txtEditFlightNumber", _flight.FlightCode);
            SetControlText("txtEditAirlineName", _flight.Airline);
            SetControlText("txtEditAircraftModel", _flight.Aircraft);

            // Tương tự View
            SetControlText("txtEditDepartureAirport", _flight.Route.Split('→')[0]?.Trim());
            SetControlText("txtEditArrivalAirport", _flight.Route.Split('→')[1]?.Trim());

            SetDatePickerValue("dtpEditFlightDate", _flight.FlightDate);
            SetTimePickerValue("dtpEditDepartureTime", _flight.DepartureTime);
            SetTimePickerValue("dtpEditArrivalTime", _flight.ArrivalTime);

            // Duration
            SetNumericValue("numEditDuration", (int)duration.TotalMinutes);

            SetNumericValue("numEditBasePrice", _flight.BasePrice);
            SetNumericValue("numEditTotalSeats", _flight.TotalSeats);
            SetNumericValue("numEditAvailableSeats", _flight.AvailableSeats);

            SetComboBoxValue("cboEditStatus", _flight.Status);
        }

        private void SetControlText(string controlName, string value)
        {
            Control[] controls = this.Controls.Find(controlName, true);
            if (controls.Length > 0 && controls[0] is TextBox txt)
                txt.Text = value ?? "";
        }

        private void SetDatePickerValue(string controlName, DateTime value)
        {
            Control[] controls = this.Controls.Find(controlName, true);
            if (controls.Length > 0 && controls[0] is DateTimePicker dtp)
                dtp.Value = value;
        }

        private void SetTimePickerValue(string controlName, TimeSpan value)
        {
            Control[] controls = this.Controls.Find(controlName, true);
            if (controls.Length > 0 && controls[0] is DateTimePicker dtp)
            {
                var today = DateTime.Today;
                dtp.Value = today.Add(value);
            }
        }

        private void SetNumericValue(string controlName, decimal value)
        {
            Control[] controls = this.Controls.Find(controlName, true);
            if (controls.Length > 0 && controls[0] is NumericUpDown num)
                num.Value = value;
        }

        private void SetComboBoxValue(string controlName, string value)
        {
            Control[] controls = this.Controls.Find(controlName, true);
            if (controls.Length > 0 && controls[0] is ComboBox cbo)
            {
                if (cbo.Items.Contains(value))
                    cbo.SelectedItem = value;
                else if (cbo.Items.Count > 0)
                    cbo.SelectedIndex = 0;
            }
        }

        private string GetControlText(string controlName)
        {
            Control[] controls = this.Controls.Find(controlName, true);
            if (controls.Length > 0 && controls[0] is TextBox txt)
                return txt.Text;
            return "";
        }

        private DateTime GetDatePickerValue(string controlName)
        {
            Control[] controls = this.Controls.Find(controlName, true);
            if (controls.Length > 0 && controls[0] is DateTimePicker dtp)
                return dtp.Value;
            return DateTime.Now;
        }

        private TimeSpan GetTimePickerValue(string controlName)
        {
            Control[] controls = this.Controls.Find(controlName, true);
            if (controls.Length > 0 && controls[0] is DateTimePicker dtp)
                return dtp.Value.TimeOfDay;
            return TimeSpan.Zero;
        }

        private decimal GetNumericValue(string controlName)
        {
            Control[] controls = this.Controls.Find(controlName, true);
            if (controls.Length > 0 && controls[0] is NumericUpDown num)
                return num.Value;
            return 0;
        }

        private string GetComboBoxValue(string controlName)
        {
            Control[] controls = this.Controls.Find(controlName, true);
            if (controls.Length > 0 && controls[0] is ComboBox cbo)
                return cbo.SelectedItem?.ToString() ?? "";
            return "";
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var btnSave = sender as Button;
                if (btnSave != null) btnSave.Enabled = false;
                this.Cursor = Cursors.WaitCursor;

                // Lấy lại Route từ hai textbox
                string departure = GetControlText("txtEditDepartureAirport");
                string arrival = GetControlText("txtEditArrivalAirport");
                string route = $"{departure} → {arrival}";

                var updatedFlight = new FlightListDtoAdmin
                {
                    FlightId = _flight.FlightId,
                    FlightCode = GetControlText("txtEditFlightNumber"),
                    Airline = GetControlText("txtEditAirlineName"),
                    Aircraft = GetControlText("txtEditAircraftModel"),
                    Route = route,

                    FlightDate = GetDatePickerValue("dtpEditFlightDate"),
                    DepartureTime = GetTimePickerValue("dtpEditDepartureTime"),
                    ArrivalTime = GetTimePickerValue("dtpEditArrivalTime"),

                    BasePrice = GetNumericValue("numEditBasePrice"),
                    TotalSeats = (int)GetNumericValue("numEditTotalSeats"),

                    Status = GetComboBoxValue("cboEditStatus")
                };

                // TODO: Thêm logic lưu vào database ở đây
                // Ví dụ: await _flightService.UpdateFlightAsync(updatedFlight);
                bool result = await _flightController.UpdateFlightAsync(updatedFlight);
                if (result)
                {
                    MessageBox.Show(
                       "Đã lưu thay đổi thành công!\n\n" +
                       $"Số hiệu: {updatedFlight.FlightCode}\n" +
                       $"Tuyến: {updatedFlight.Route}\n" +
                       $"Ngày: {updatedFlight.FlightDate:dd/MM/yyyy}\n" +
                       $"Giờ: {updatedFlight.DepartureTime:hh\\:mm} - {updatedFlight.ArrivalTime:hh\\:mm}\n" +
                       $"Giá: {updatedFlight.BasePrice:N0} VND\n" +
                       $"Trạng thái: {updatedFlight.Status}",
                       "Thành công",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Information
                   );

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(
                        "❌ Không thể lưu thay đổi. Vui lòng thử lại!",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"❌ Lỗi khi lưu:\n\n{ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            finally
            {
                // Reset cursor và enable buttons
                this.Cursor = Cursors.Default;
                var btnSave = sender as Button;
                if (btnSave != null) btnSave.Enabled = true;
            }
        }

        private async void BtnDeactivate_Click(object sender, EventArgs e)
        {
            try
            {
                // Tách Route: "SGN → HAN"
                string departure = _flight.Route.Split('→')[0].Trim();
                string arrival = _flight.Route.Split('→')[1].Trim();

                var result = MessageBox.Show(
                    $"Bạn có chắc muốn vô hiệu hóa chuyến bay {_flight.FlightCode}?\n\n" +
                    $"Tuyến: {departure} → {arrival}\n" +
                    $"Ngày: {_flight.FlightDate:dd/MM/yyyy}\n\n" +
                    "Hành động này sẽ hủy chuyến bay!",
                    "Xác nhận vô hiệu hóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    // TODO: Thêm logic vô hiệu hóa trong database
                    // Ví dụ:
                    // await _flightService.CancelFlightAsync(_flight.FlightId);
                    bool cancelResult = await _flightController.CancelFlightAsync(_flight.FlightId);

                    MessageBox.Show(
                        $"Đã vô hiệu hóa chuyến bay {_flight.FlightCode}!",
                        "Thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Lỗi khi vô hiệu hóa: {ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}
