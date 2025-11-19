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

        public UC_FlightSearch()
        {
            InitializeComponent();
            _cityController = DIContainer.CityController;
            Load += UC_FlightSearch_Load;
        }

        // Load city list
        private async void UC_FlightSearch_Load(object? sender, EventArgs e)
        {
            var cities = await _cityController.GetAllCitiesAsync();
            _cityItems = cities.Select(c => new CitySelectDTO(c)).ToList();
            BindComboItems();
        }

        // Build calendar UI
        private void BuildCalendar(
            Guna2Panel parent,
            FlowLayoutPanel flow,
            List<Guna2Button> buttons,
            Action<DateTime> onSelect)
        {
            parent.Controls.Clear();

            // xác định lịch nào
            bool isStart = parent.Tag?.ToString() == "start";

            DateTime month = isStart ? startMonth : returnMonth;

            Label lblMonth = new Label()
            {
                Text = $"Tháng {month:MM, yyyy}",
                Font = new Font("Segoe UI Semibold", 12),
                Location = new Point(15, 10),
                AutoSize = true
            };
            parent.Controls.Add(lblMonth);

            // PREV
            var btnPrev = new Guna2Button()
            {
                Text = "<",
                Width = 30,
                Height = 30,
                BorderRadius = 6,
                FillColor = Color.WhiteSmoke,
                Location = new Point(245, 5)
            };

            btnPrev.Click += (s, e) =>
            {
                month = month.AddMonths(-1);

                if (isStart) startMonth = month;
                else returnMonth = month;

                lblMonth.Text = $"Tháng {month:MM, yyyy}";
                RenderCalendar(month, buttons);
            };
            parent.Controls.Add(btnPrev);

            // NEXT
            var btnNext = new Guna2Button()
            {
                Text = ">",
                Width = 30,
                Height = 30,
                BorderRadius = 6,
                FillColor = Color.WhiteSmoke,
                Location = new Point(280, 5)
            };

            btnNext.Click += (s, e) =>
            {
                month = month.AddMonths(1);

                if (isStart) startMonth = month;
                else returnMonth = month;

                lblMonth.Text = $"Tháng {month:MM, yyyy}";
                RenderCalendar(month, buttons);
            };
            parent.Controls.Add(btnNext);

            // FLOW
            flow.Controls.Clear();
            parent.Controls.Add(flow);

            buttons.Clear();

            for (int i = 1; i <= 31; i++)
            {
                var btnDay = new Guna2Button()
                {
                    Width = 45,
                    Height = 45,
                    BorderRadius = 8,
                    FillColor = Color.FromArgb(245, 245, 245),
                    Text = i.ToString()
                };

                btnDay.Click += (s, e) =>
                {
                    foreach (var b in buttons)
                        b.FillColor = Color.FromArgb(245, 245, 245);

                    btnDay.FillColor = Color.FromArgb(0, 164, 239);

                    DateTime selected = new DateTime(month.Year, month.Month, int.Parse(btnDay.Text));
                    onSelect(selected);

                    parent.Visible = false;
                };

                buttons.Add(btnDay);
                flow.Controls.Add(btnDay);
            }

            RenderCalendar(month, buttons);
        }

        // Hide days over max days of month
        private void RenderCalendar(DateTime month, List<Guna2Button> buttons)
        {
            int days = DateTime.DaysInMonth(month.Year, month.Month);

            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Visible = (i < days);
                if (i < days)
                    buttons[i].Text = (i + 1).ToString();
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

            panelReturnCalendar.Visible = enabled;

            foreach (var btn in returnDayButtons)
                btn.Enabled = enabled;

            panelReturnCalendar.FillColor = enabled
                ? Color.White
                : Color.FromArgb(230, 230, 230);
        }

        // Click return date button
        private void BtnReturnDate_Click(object sender, EventArgs e)
        {
            if (!guna2CustomCheckBox1.Checked)
                return;

            panelReturnCalendar.Tag = "return";

            panelReturnCalendar.Location = new Point(
                btnReturnDate.Left,
                btnReturnDate.Bottom + 5
            );

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

            panelStartCalendar.Location = new Point(
                btnStartDate.Left,
                btnStartDate.Bottom + 5
            );

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

    }
}
