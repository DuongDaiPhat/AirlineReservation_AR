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

namespace AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.User
{
    public partial class UC_FlightSearch : UserControl
    {
        //chyển hướng
        public event Action<FlightSearchParams> OnSearchSubmit;
        // Controller + data
        private readonly CityController _cityController;
        private List<CitySelectDTO> _cityItems = new();

        // Swap / filter flags
        private bool _isSwapping = false;
        private bool _isRebuilding = false;

        // Calendar data
        private FlowLayoutPanel flowStartDays;
        private FlowLayoutPanel flowReturnDays;

        private DateTime startMonth = DateTime.Now;
        private DateTime returnMonth = DateTime.Now;

        private List<Guna2Button> startDayButtons = new();
        private List<Guna2Button> returnDayButtons = new();

        private DateTime? selectedStartDate = null;
        private DateTime? selectedReturnDate = null;

        private Guna2Panel passengerPopup;

        private int adult = 1;
        private int child = 0;
        private int infant = 0;

        public UC_FlightSearch()
        {
            InitializeComponent();
            _cityController = DIContainer.CityController;
            InitializeCalendarFlowPanels();
            CreatePassengerPopup();
            Load += UC_FlightSearch_Load;
        }


       



        // Khởi tạo FlowLayoutPanel cho lịch
        private void InitializeCalendarFlowPanels()
        {
            flowStartDays = new FlowLayoutPanel
            {
                Location = new Point(10, 90),
                Size = new Size(340, 270),
                AutoScroll = false,
                WrapContents = true,
                Padding = new Padding(0),
                BackColor = Color.Transparent
            };

            flowReturnDays = new FlowLayoutPanel
            {
                Location = new Point(10, 90),
                Size = new Size(340, 270),
                AutoScroll = false,
                WrapContents = true,
                Padding = new Padding(0),
                BackColor = Color.Transparent
            };
        }

        // Load city list
        private async void UC_FlightSearch_Load(object? sender, EventArgs e)
        {
            var cities = await _cityController.GetAllCitiesAsync();
            _cityItems = cities.Select(c => new CitySelectDTO(c)).ToList();
            BindComboItems();
            if (this.ParentForm != null)
                this.ParentForm.Controls.Add(passengerPopup);

            passengerPopup.BringToFront();
        }

        private void CreatePassengerPopup()
        {
            passengerPopup = new Guna2Panel
            {
                Size = new Size(350, 260),
                BorderRadius = 12,
                FillColor = Color.White,
                Visible = false,
                ShadowDecoration = { Enabled = true, Depth = 15, Shadow = new Padding(0, 2, 8, 8) },
            };

      

            // ===== TITLE =====
            Label lblTitle = new Label
            {
                Text = "Số hành khách",
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                Location = new Point(15, 10),
                AutoSize = true
            };
            passengerPopup.Controls.Add(lblTitle);

            // ===== CLOSE =====
            Guna2Button btnClose = new Guna2Button
            {
                Text = "×",
                FillColor = Color.Transparent,
                ForeColor = Color.Black,
                Size = new Size(40, 40),
                Location = new Point(300, 5),
                BorderRadius = 8,
                Cursor = Cursors.Hand
            };
            btnClose.Click += (s, e) => passengerPopup.Visible = false;
            passengerPopup.Controls.Add(btnClose);

            // ===== ROWS =====
            AddPassengerRow("Người lớn", "Từ 12 tuổi", "adult", 55);
            AddPassengerRow("Trẻ em", "Từ 2 – 11 tuổi", "child", 120);
            AddPassengerRow("Em bé", "Dưới 2 tuổi", "infant", 185);

            // ===== BUTTON DONE =====
            Guna2Button btnDone = new Guna2Button
            {
                Text = "Xong",
                FillColor = Color.FromArgb(0, 147, 255),
                Size = new Size(300, 42),
                Location = new Point(25, 215),
                BorderRadius = 8,
                Font = new Font("Segoe UI", 11, FontStyle.Bold)
            };
            btnDone.Click += BtnDone_Click;
            passengerPopup.Controls.Add(btnDone);
        }

        private void UpdatePassengerPopupPosition()
        {
            if (this.ParentForm == null) return;

            var btnScreen = btnPassenger.PointToScreen(Point.Empty);
            var btnInForm = ParentForm.PointToClient(btnScreen);

            passengerPopup.Location = new Point(
                btnInForm.X,
                btnInForm.Y + btnPassenger.Height + 6
            );
        }

        private void AddPassengerRow(string title, string sub, string type, int y)
        {
            Label lbl = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Location = new Point(15, y),
                AutoSize = true
            };
            passengerPopup.Controls.Add(lbl);

            Label lblSub = new Label
            {
                Text = sub,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray,
                Location = new Point(15, y + 25),
                AutoSize = true
            };
            passengerPopup.Controls.Add(lblSub);

            Guna2Button btnMinus = new Guna2Button
            {
                Text = "-",
                Size = new Size(36, 36),
                BorderRadius = 10,
                FillColor = Color.Transparent,
                BorderThickness = 1,
                BorderColor = Color.Silver,
                ForeColor = Color.Black,
                Location = new Point(200, y),
                Cursor = Cursors.Hand
            };

            Label lblNumber = new Label
            {
                Text = GetCounter(type).ToString(),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(45, 36),
                Location = new Point(240, y + 2)
            };

            Guna2Button btnPlus = new Guna2Button
            {
                Text = "+",
                Size = new Size(36, 36),
                BorderRadius = 10,
                FillColor = Color.Transparent,
                BorderThickness = 1,
                BorderColor = Color.Silver,
                ForeColor = Color.Black,
                Location = new Point(285, y),
                Cursor = Cursors.Hand
            };

            // Minus
            btnMinus.Click += (s, e) =>
            {
                int v = GetCounter(type);
                if (v > 0) v--;
                SetCounter(type, v);
                lblNumber.Text = v.ToString();
                UpdatePassengerText();
            };

            // Plus
            btnPlus.Click += (s, e) =>
            {
                int v = GetCounter(type);
                v++;
                SetCounter(type, v);
                lblNumber.Text = v.ToString();
                UpdatePassengerText();
            };

            passengerPopup.Controls.Add(btnMinus);
            passengerPopup.Controls.Add(lblNumber);
            passengerPopup.Controls.Add(btnPlus);
        }


        private int GetCounter(string type)
        {
            return type switch
            {
                "adult" => adult,
                "child" => child,
                "infant" => infant,
                _ => 0
            };
        }

        private void SetCounter(string type, int value)
        {
            switch (type)
            {
                case "adult": adult = value; break;
                case "child": child = value; break;
                case "infant": infant = value; break;
            }
        }

        private void UpdatePassengerText()
        {
            btnPassenger.Text = $"{adult} Người lớn, {child} Trẻ em, {infant} Em bé";
        }

        private void BtnPassenger_Click(object sender, EventArgs e)
        {
            UpdatePassengerPopupPosition();

            passengerPopup.Visible = !passengerPopup.Visible;
            passengerPopup.BringToFront();
        }



        private void BtnDone_Click(object sender, EventArgs e)
        {
            passengerPopup.Visible = false;
        }

        // Build calendar UI - Thiết kế mới
        private void BuildCalendar(
            Guna2Panel parent,
            FlowLayoutPanel flow,
            List<Guna2Button> buttons,
            Action<DateTime> onSelect)
        {
            parent.Controls.Clear();
            parent.BorderRadius = 16;
            parent.ShadowDecoration.Enabled = true;
            parent.ShadowDecoration.Depth = 15;
            parent.ShadowDecoration.Shadow = new Padding(0, 2, 6, 6);
            parent.ShadowDecoration.Color = Color.FromArgb(0, 0, 0, 50);

            bool isStart = parent.Tag?.ToString() == "start";
            DateTime month = isStart ? startMonth : returnMonth;

            // Header với tháng/năm
            Label lblMonth = new Label()
            {
                Text = month.ToString("MMMM yyyy"),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Location = new Point(20, 15),
                Size = new Size(200, 30),
                ForeColor = Color.FromArgb(30, 30, 30)
            };
            parent.Controls.Add(lblMonth);

            // Nút Previous
            var btnPrev = new Guna2Button()
            {
                Text = "◀",
                Width = 35,
                Height = 35,
                BorderRadius = 8,
                FillColor = Color.FromArgb(240, 240, 240),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(100, 100, 100),
                Location = new Point(240, 12),
                Cursor = Cursors.Hand
            };
            btnPrev.Click += (s, e) =>
            {
                month = month.AddMonths(-1);
                if (isStart) startMonth = month;
                else returnMonth = month;
                lblMonth.Text = month.ToString("MMMM yyyy");
                RenderCalendar(month, buttons, flow, onSelect, isStart);
            };
            parent.Controls.Add(btnPrev);

            // Nút Next
            var btnNext = new Guna2Button()
            {
                Text = "▶",
                Width = 35,
                Height = 35,
                BorderRadius = 8,
                FillColor = Color.FromArgb(240, 240, 240),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(100, 100, 100),
                Location = new Point(285, 12),
                Cursor = Cursors.Hand
            };
            btnNext.Click += (s, e) =>
            {
                month = month.AddMonths(1);
                if (isStart) startMonth = month;
                else returnMonth = month;
                lblMonth.Text = month.ToString("MMMM yyyy");
                RenderCalendar(month, buttons, flow, onSelect, isStart);
            };
            parent.Controls.Add(btnNext);

            // Thêm header các ngày trong tuần
            Panel weekHeader = new Panel
            {
                Location = new Point(10, 60),
                Size = new Size(340, 28),
                BackColor = Color.Transparent
            };

            string[] dayNames = { "CN", "T2", "T3", "T4", "T5", "T6", "T7" };
            for (int i = 0; i < 7; i++)
            {
                Label lblDay = new Label
                {
                    Text = dayNames[i],
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    ForeColor = Color.FromArgb(120, 120, 120),
                    Location = new Point(i * 48 + 1, 0),
                    Size = new Size(48, 28),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                weekHeader.Controls.Add(lblDay);
            }
            parent.Controls.Add(weekHeader);

            // Flow panel cho các ngày
            flow.Controls.Clear();
            flow.Location = new Point(10, 90);
            flow.Size = new Size(340, 270);
            parent.Controls.Add(flow);

            buttons.Clear();
            RenderCalendar(month, buttons, flow, onSelect, isStart);
        }

        // Render calendar với layout cải tiến
        private void RenderCalendar(DateTime month, List<Guna2Button> buttons,
            FlowLayoutPanel flow, Action<DateTime> onSelect, bool isStart)
        {
            flow.Controls.Clear();
            buttons.Clear();

            int daysInMonth = DateTime.DaysInMonth(month.Year, month.Month);
            DateTime firstDay = new DateTime(month.Year, month.Month, 1);
            int startDayOfWeek = (int)firstDay.DayOfWeek;

            // Thêm các ô trống cho ngày đầu tháng
            for (int i = 0; i < startDayOfWeek; i++)
            {
                Panel emptyCell = new Panel
                {
                    Width = 47,
                    Height = 45,
                    Margin = new Padding(0)
                };
                flow.Controls.Add(emptyCell);
            }

            DateTime today = DateTime.Now.Date;

            // Tạo button cho mỗi ngày trong tháng
            for (int day = 1; day <= daysInMonth; day++)
            {
                DateTime currentDate = new DateTime(month.Year, month.Month, day);
                bool isPast = currentDate < today;

                var btnDay = new Guna2Button()
                {
                    Width = 45,
                    Height = 43,
                    Margin = new Padding(1),
                    BorderRadius = 10,
                    FillColor = Color.White,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    ForeColor = isPast ? Color.FromArgb(180, 180, 180) : Color.FromArgb(60, 60, 60),
                    Text = day.ToString(),
                    Cursor = isPast ? Cursors.Default : Cursors.Hand,
                    Enabled = !isPast,
                    Tag = currentDate
                };

                // Đánh dấu ngày hôm nay
                if (currentDate == today)
                {
                    btnDay.BorderColor = Color.FromArgb(0, 164, 239);
                    btnDay.BorderThickness = 2;
                }

                // Đánh dấu ngày đã chọn
                if ((isStart && selectedStartDate == currentDate) ||
                    (!isStart && selectedReturnDate == currentDate))
                {
                    btnDay.FillColor = Color.FromArgb(0, 164, 239);
                    btnDay.ForeColor = Color.White;
                }

                if (!isPast)
                {
                    btnDay.HoverState.FillColor = Color.FromArgb(230, 245, 255);
                    btnDay.HoverState.ForeColor = Color.FromArgb(0, 164, 239);

                    btnDay.Click += (s, e) =>
                    {
                        // Reset tất cả button
                        foreach (var b in buttons)
                        {
                            b.FillColor = Color.White;
                            b.ForeColor = Color.FromArgb(60, 60, 60);

                            // Giữ border cho ngày hôm nay
                            if ((DateTime)b.Tag == today)
                            {
                                b.BorderColor = Color.FromArgb(0, 164, 239);
                                b.BorderThickness = 2;
                            }
                            else
                            {
                                b.BorderThickness = 0;
                            }
                        }

                        // Highlight button được chọn
                        btnDay.FillColor = Color.FromArgb(0, 164, 239);
                        btnDay.ForeColor = Color.White;
                        btnDay.BorderThickness = 0;

                        DateTime selected = (DateTime)btnDay.Tag;

                        if (isStart)
                            selectedStartDate = selected;
                        else
                            selectedReturnDate = selected;

                        onSelect(selected);

                        // Ẩn panel sau khi chọn
                        ((Guna2Panel)flow.Parent).Visible = false;
                    };
                }

                buttons.Add(btnDay);
                flow.Controls.Add(btnDay);
            }
        }

        // Bind city list
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

        // Toggle return date
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

        // Click return date button
        private void BtnReturnDate_Click(object sender, EventArgs e)
        {
            if (!guna2CustomCheckBox1.Checked)
                return;

            panelReturnCalendar.Tag = "return";
            panelReturnCalendar.Size = new Size(360, 370);
            panelReturnCalendar.Location = new Point(754, 56);
            panelReturnCalendar.BringToFront();

            BuildCalendar(
                panelReturnCalendar,
                flowReturnDays,
                returnDayButtons,
                selected =>
                {
                    btnReturnDate.Text = selected.ToString("dd MMM yyyy");
                });

            panelReturnCalendar.Visible = true;
        }

        private void BtnStartDate_Click(object sender, EventArgs e)
        {
            panelStartCalendar.Tag = "start";
            panelStartCalendar.Size = new Size(360, 370);
            panelStartCalendar.Location = new Point(368, 56);
            panelStartCalendar.BringToFront();

            BuildCalendar(
                panelStartCalendar,
                flowStartDays,
                startDayButtons,
                selected =>
                {
                    btnStartDate.Text = selected.ToString("dd MMM yyyy");
                });

            panelStartCalendar.Visible = true;

        }

        //chuyển hướng
        private void btnSearch_Click(object sender, EventArgs e)
        {
            var p = new FlightSearchParams
            {
                FromCode = (cboFrom.SelectedItem as CitySelectDTO).Code,
                ToCode = (cboTo.SelectedItem as CitySelectDTO).Code,
                StartDate = selectedStartDate,
                SeatClassId = cboSeatClass.SelectedIndex + 1
            };

            OnSearchSubmit?.Invoke(p);
        }
    }
}