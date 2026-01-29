using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.Infrastructure.DI;
using AirlineReservation_AR.src.Presentation__Winform_.Controllers;
using AirlineReservation_AR.src.Domain.DTOs;
using Guna.UI2.WinForms;
using AR_Winform.Presentation.UControls.User;
using AirlineReservation_AR.src.AirlineReservation.Presentation__WinForms_.Views.Forms.Common; // IMPORTANT

namespace AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.User
{
    public partial class UC_FlightSearch : UserControl
    {
        public event Action<FlightSearchParams> OnSearchSubmit;

        private readonly CityController _cityController;
        private readonly SeatClassController _seatClassController;
        private List<CitySelectDTO> _cityItems = new();

        private bool _isSwapping = false;
        private bool _isRebuilding = false;

        // NEW: Passenger Control
        private UC_TypeTicket typeTicketPopup;

        private DateTime? selectedStartDate = null;
        private DateTime? selectedReturnDate = null;

        private int adult = 1;
        private int child = 0;
        private int infant = 0;

        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        public UC_FlightSearch()
        {
            InitializeComponent();
            _cityController = DIContainer.CityController;
            _seatClassController = DIContainer.SeatClassController;

            Load += UC_FlightSearch_Load;
        }

        private async void UC_FlightSearch_Load(object? sender, EventArgs e)
        {
            InitCalendars();

            var cities = await _cityController.GetAllCitiesAsync();
            var seatClass = await _seatClassController.GetAll();

            _cityItems = cities.Select(c => new CitySelectDTO(c)).ToList();

            BindSeatClass(seatClass);
            BindComboItems();
            if (typeTicketPopup == null)
            {
                InitPassengerControl();
            }
        }

        // ============================================================
        // 1) PASSENGER POPUP - UC_TypeTicket
        // ============================================================
        private void InitPassengerControl()
        {
            if (this.Parent == null) return;

            typeTicketPopup = new UC_TypeTicket();
            typeTicketPopup.Visible = false;
            typeTicketPopup.BackColor = Color.White;

            typeTicketPopup.DoneClicked += (s, res) =>
            {
                adult = res.Adult;
                child = res.Child;
                infant = res.Infant;

                btnPassenger.Text = $"{adult} Adult, {child} Child, {infant} Ifnant";
                typeTicketPopup.Hide();
            };

            this.Parent.Controls.Add(typeTicketPopup);
            typeTicketPopup.BringToFront();
        }

        private void BtnPassenger_Click(object sender, EventArgs e)
        {
            var screen = btnPassenger.PointToScreen(Point.Empty);
            var pos = Parent.PointToClient(screen);

            typeTicketPopup.Location = new Point(pos.X, pos.Y + btnPassenger.Height + 4);
            typeTicketPopup.BringToFront();
            typeTicketPopup.Show();
        }

        // ============================================================
        // 2) CALENDAR POPUP - UC_FlightDate
        // ============================================================
        private void InitCalendars()
        {
            panelStartCalendar.Visible = false;
            panelReturnCalendar.Visible = false;

            uC_FlightDate1.DaySelected += StartCalendar_DaySelected;
            uC_FlightDate2.DaySelected += ReturnCalendar_DaySelected;
        }


        private void StartCalendar_DaySelected(object sender, DateTime date)
        {
            selectedStartDate = date;
            btnStartDate.Text = date.ToString("dd MMM yyyy");
            panelStartCalendar.Visible = false;

            // Tự động chuyển sang chọn ngày về nếu checkbox được bật
            if (guna2CustomCheckBox1.Checked)
            {
                panelReturnCalendar.BringToFront();
                panelReturnCalendar.Visible = true;
                uC_FlightDate2.RefreshCalendar();
            }
        }

        private void ReturnCalendar_DaySelected(object sender, DateTime date)
        {
            // Không cho chọn ngày trước ngày đi
            if (selectedStartDate != null && date < selectedStartDate)
            {
                MessageBox.Show("Ngày về phải lớn hơn hoặc bằng ngày đi!",
                    "Ngày không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            selectedReturnDate = date;
            btnReturnDate.Text = date.ToString("dd MMM yyyy");

            panelReturnCalendar.Visible = false;
        }

        private void BtnStartDate_Click(object sender, EventArgs e)
        {
            panelReturnCalendar.Visible = false;

            panelStartCalendar.BringToFront();
            panelStartCalendar.Visible = true;

            if (pictureBox1 != null)
            {
                pictureBox1.BringToFront();
            }

            if (uC_FlightDate1 != null)
            {
                uC_FlightDate1.RefreshCalendar();
            }
        }

        private void BtnReturnDate_Click(object sender, EventArgs e)
        {
            if (!guna2CustomCheckBox1.Checked)
                return;

            panelStartCalendar.Visible = false;

            panelReturnCalendar.BringToFront();
            panelReturnCalendar.Visible = true;

            if (pictureBox2 != null)
            {
                pictureBox2.BringToFront();
            }

            if (uC_FlightDate2 != null)
            {
                uC_FlightDate2.RefreshCalendar();
            }
        }
        private void guna2CustomCheckBox1_Click(object sender, EventArgs e)
        {
            bool enabled = guna2CustomCheckBox1.Checked;

            btnReturnDate.Enabled = enabled;
            btnReturnDate.FillColor = enabled ? Color.White : Color.Gainsboro;
            btnReturnDate.ForeColor = enabled ? Color.Black : Color.Gray;

            if (!enabled)
            {
                panelReturnCalendar.Visible = false;
                selectedReturnDate = null;
                btnReturnDate.Text = "Chọn ngày về";
            }
        }

        // ============================================================
        // 3) SEAT CLASS
        // ============================================================
        private void BindSeatClass(List<SeatClass> seatClasses)
        {
            cboSeatClass.DataSource = seatClasses
                .Select(x => new
                {
                    x.SeatClassId,
                    Display = $"{x.DisplayName} – {x.BaggageAllowanceKg ?? 0}kg - x{x.PriceMultiplier}VND"
                })
                .ToList();

            cboSeatClass.DisplayMember = "Display";
            cboSeatClass.ValueMember = "SeatClassId";
        }

        // ============================================================
        // 4) CITY COMBOS
        // ============================================================
        private void BindComboItems()
        {
            cboFrom.DrawMode = DrawMode.OwnerDrawFixed;
            cboTo.DrawMode = DrawMode.OwnerDrawFixed;

            cboFrom.Items.Clear();
            cboTo.Items.Clear();

            foreach (var item in _cityItems)
            {
                cboFrom.Items.Add(item);
                cboTo.Items.Add(item);
            }

            cboFrom.SelectedIndex = -1;
            cboTo.SelectedIndex = -1;

            cboFrom.MaxDropDownItems = 6;
            cboTo.MaxDropDownItems = 6;

            cboFrom.SelectedIndexChanged += ComboChanged;
            cboTo.SelectedIndexChanged += ComboChanged;

            cboFrom.DrawItem += CboFrom_DrawItem;
            cboTo.DrawItem += CboTo_DrawItem;
        }

        // Prevent selecting same city
        private void ComboChanged(object? sender, EventArgs e)
        {
            if (_isSwapping || _isRebuilding) return;
            ApplyFilter();
        }

        private void ApplyFilter()
        {
            if (_isRebuilding) return;

            _isRebuilding = true;

            string fromCode = (cboFrom.SelectedItem as CitySelectDTO)?.Code;
            string toCode = (cboTo.SelectedItem as CitySelectDTO)?.Code;

            var selectedFrom = cboFrom.SelectedItem;

            cboFrom.Items.Clear();
            foreach (var item in _cityItems)
                if (item.Code != toCode)
                    cboFrom.Items.Add(item);

            if (selectedFrom != null && cboFrom.Items.Contains(selectedFrom))
                cboFrom.SelectedItem = selectedFrom;

            var selectedTo = cboTo.SelectedItem;

            cboTo.Items.Clear();
            foreach (var item in _cityItems)
                if (item.Code != fromCode)
                    cboTo.Items.Add(item);

            if (selectedTo != null && cboTo.Items.Contains(selectedTo))
                cboTo.SelectedItem = selectedTo;

            _isRebuilding = false;

            cboFrom.Invalidate();
            cboTo.Invalidate();
        }

        // Custom draw combo item
        private void CboFrom_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawItem(cboFrom, cboTo, e);
        }

        private void CboTo_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawItem(cboTo, cboFrom, e);
        }

        private void DrawItem(Guna2ComboBox current, Guna2ComboBox other, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            var item = current.Items[e.Index] as CitySelectDTO;
            if (item == null) return;

            bool blocked = other.SelectedItem is CitySelectDTO o && o.Code == item.Code;

            if (blocked)
            {
                e.Graphics.FillRectangle(Brushes.White, e.Bounds);
                return;
            }

            bool selected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;

            e.Graphics.FillRectangle(
                new SolidBrush(selected ? SystemColors.Highlight : Color.White),
                e.Bounds
            );

            e.Graphics.DrawString(
                item.DisplayName,
                e.Font,
                new SolidBrush(selected ? Color.White : Color.Black),
                e.Bounds.Left + 5,
                e.Bounds.Top + 3
            );
        }

        // Swap city
        private async void BtnSwap_Click(object sender, EventArgs e)
        {
            if (cboFrom.SelectedItem == null || cboTo.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn điểm đi và điểm đến!");
                return;
            }

            _isSwapping = true;
            _isRebuilding = true;

            await RotateButtonAsync(btnSwap);

            string fromCode = (cboFrom.SelectedItem as CitySelectDTO)?.Code!;
            string toCode = (cboTo.SelectedItem as CitySelectDTO)?.Code!;

            cboFrom.Items.Clear();
            foreach (var item in _cityItems)
                if (item.Code != fromCode)
                    cboFrom.Items.Add(item);

            cboFrom.SelectedItem = cboFrom.Items.Cast<CitySelectDTO>()
                .First(x => x.Code == toCode);

            cboTo.Items.Clear();
            foreach (var item in _cityItems)
                if (item.Code != toCode)
                    cboTo.Items.Add(item);

            cboTo.SelectedItem = cboTo.Items.Cast<CitySelectDTO>()
                .First(x => x.Code == fromCode);

            _isSwapping = false;
            _isRebuilding = false;

            cboFrom.Invalidate();
            cboTo.Invalidate();
        }


        // Swap animation
        private async Task RotateButtonAsync(Guna2CircleButton btn)
        {
            if (btn.Image == null) return;

            Image original = btn.Image;

            for (int angle = 0; angle <= 180; angle += 20)
            {
                var rotated = RotateImage(original, angle);
                btn.Image = rotated;
                await Task.Delay(10);
                if (rotated != original)
                    rotated.Dispose();
            }

            btn.Image = original;
        }

        private Image RotateImage(Image img, float angle)
        {
            Bitmap bmp = new(img.Width, img.Height);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.TranslateTransform(img.Width / 2f, img.Height / 2f);
                g.RotateTransform(angle);
                g.TranslateTransform(-img.Width / 2f, -img.Height / 2f);
                g.DrawImage(img, 0, 0);
            }

            return bmp;
        }

        // ============================================================
        // 6) SEARCH
        // ============================================================
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (cboFrom.SelectedItem == null)
            {
                ShowAnnouncement(
                    "Missing departure city",
                    "Please select a departure city before searching."
                );
                return;
            }

            if (cboTo.SelectedItem == null)
            {
                ShowAnnouncement(
                    "Missing destination city",
                    "Please select a destination city before searching."
                );
                return;
            }

            if (selectedStartDate == null)
            {
                ShowAnnouncement(
                    "Missing departure date",
                    "Please select a departure date."
                );
                return;
            }

            if (guna2CustomCheckBox1.Checked)
            {
                if (selectedReturnDate == null)
                {
                    ShowAnnouncement(
                        "Missing return date",
                        "Please select a return date for round-trip flights."
                    );
                    return;
                }

                if (selectedReturnDate < selectedStartDate)
                {
                    ShowAnnouncement(
                        "Invalid return date",
                        "Return date must be later than or equal to departure date."
                    );
                    return;
                }
            }

            if (cboSeatClass.SelectedIndex < 0)
            {
                ShowAnnouncement(
                    "Seat class not selected",
                    "Please select a seat class."
                );
                return;
            }

            if (adult < 1)
            {
                ShowAnnouncement(
                    "Invalid passenger count",
                    "At least one adult passenger is required."
                );
                return;
            }

            // Business rule:
            // Adults + Children <= 7 (Infants NOT included)
            int totalPeople = adult + child;
            if (totalPeople > 7)
            {
                ShowAnnouncement(
                    "Passenger limit exceeded",
                    "The total number of adults and children must not exceed 7."
                );
                return;
            }

            //  Each adult can accompany only one infant
            if (infant > adult)
            {
                ShowAnnouncement(
                    "Invalid infant count",
                    "Each adult may accompany only one infant."
                );
                return;
            }

            var p = new FlightSearchParams
            {
                FromCode = (cboFrom.SelectedItem as CitySelectDTO).Code,
                ToCode = (cboTo.SelectedItem as CitySelectDTO).Code,
                FromCity = (cboFrom.SelectedItem as CitySelectDTO).DisplayName,
                ToCity = (cboTo.SelectedItem as CitySelectDTO).DisplayName,
                StartDate = selectedStartDate,
                ReturnDate = guna2CustomCheckBox1.Checked ? selectedReturnDate : null,
                RoundTrip = guna2CustomCheckBox1.Checked,
                SeatClassId = (int)cboSeatClass.SelectedValue,
                Adult = adult,
                Child = child,
                Infant = infant
            };

            OnSearchSubmit?.Invoke(p);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            panelStartCalendar.Visible = false;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            panelReturnCalendar.Visible = false;
        }
    private void ShowAnnouncement(string title, string message, bool isSuccess = false)
        {
            AnnouncementForm announcementForm = new AnnouncementForm();
            announcementForm.SetAnnouncement(title, message, isSuccess, null);
            announcementForm.Show();
        }
    }

    }
