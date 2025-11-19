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

namespace AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.User
{
    public partial class UC_FlightSearch : UserControl
    {
        private readonly CityController _cityController;
        private List<CitySelectDTO> _cityItems = new();

        private bool _isSwapping = false;     // ngan Filter chay khi swap
        private bool _isRebuilding = false;   // ngan vong lap khi Clear/Add Items

        public UC_FlightSearch()
        {
            InitializeComponent();
            _cityController = DIContainer.CityController;
            Load += UC_FlightSearch_Load;
        }

        // Load du lieu thanh pho
        private async void UC_FlightSearch_Load(object? sender, EventArgs e)
        {
            var cities = await _cityController.GetAllCitiesAsync();
            _cityItems = cities.Select(c => new CitySelectDTO(c)).ToList();

            BindComboItems();
        }

        // Bind items ban dau
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

        // Xu ly khi user chon
        private void ComboChanged(object? sender, EventArgs e)
        {
            if (_isSwapping || _isRebuilding) return;

            ApplyFilter();
        }

        // Filter item trung bang cach rebuild Items
        private void ApplyFilter()
        {
            if (_isRebuilding) return;

            _isRebuilding = true;

            string fromCode = (cboFrom.SelectedItem as CitySelectDTO)?.Code;
            string toCode = (cboTo.SelectedItem as CitySelectDTO)?.Code;

            // Rebuild cboFrom
            var selectedFrom = cboFrom.SelectedItem;

            cboFrom.Items.Clear();
            foreach (var item in _cityItems)
            {
                if (item.Code != toCode)
                    cboFrom.Items.Add(item);
            }

            if (selectedFrom != null && cboFrom.Items.Contains(selectedFrom))
                cboFrom.SelectedItem = selectedFrom;

            // Rebuild cboTo
            var selectedTo = cboTo.SelectedItem;

            cboTo.Items.Clear();
            foreach (var item in _cityItems)
            {
                if (item.Code != fromCode)
                    cboTo.Items.Add(item);
            }

            if (selectedTo != null && cboTo.Items.Contains(selectedTo))
                cboTo.SelectedItem = selectedTo;

            _isRebuilding = false;

            cboFrom.Invalidate();
            cboTo.Invalidate();
        }

        // Render cboFrom items
        private void CboFrom_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawItem(cboFrom, cboTo, e);
        }

        // Render cboTo items
        private void CboTo_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawItem(cboTo, cboFrom, e);
        }

        // Ham ve chung
        private void DrawItem(
            Guna.UI2.WinForms.Guna2ComboBox current,
            Guna.UI2.WinForms.Guna2ComboBox other,
            DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            var item = current.Items[e.Index] as CitySelectDTO;
            if (item == null) return;

            bool isBlocked = other.SelectedItem is CitySelectDTO o &&
                             o.Code == item.Code;

            if (isBlocked)
            {
                // item can an khong ve
                e.Graphics.FillRectangle(Brushes.White, e.Bounds);
                return;
            }

            bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;

            e.Graphics.FillRectangle(
                new SolidBrush(isSelected ? SystemColors.Highlight : Color.White),
                e.Bounds
            );

            e.Graphics.DrawString(
                item.DisplayName,
                e.Font,
                new SolidBrush(isSelected ? Color.White : Color.Black),
                e.Bounds.X + 5,
                e.Bounds.Y + 3
            );
        }

        // Swap logic
        private async void BtnSwap_Click(object sender, EventArgs e)
        {
            if (cboFrom.SelectedItem == null || cboTo.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn điểm đi và điểm đến!");
                return;
            }

            // Khoa moi thu
            _isSwapping = true;
            _isRebuilding = true;

            await RotateButtonAsync(btnSwap);

            // Luu code de swap
            string fromCode = (cboFrom.SelectedItem as CitySelectDTO)?.Code!;
            string toCode = (cboTo.SelectedItem as CitySelectDTO)?.Code!;

            // Rebuild cboFrom voi filter fromCode cu
            cboFrom.Items.Clear();
            foreach (var item in _cityItems)
            {
                if (item.Code != fromCode)
                    cboFrom.Items.Add(item);
            }
            cboFrom.SelectedItem = cboFrom.Items.Cast<CitySelectDTO>().First(x => x.Code == toCode);

            // Rebuild cboTo voi filter toCode cu
            cboTo.Items.Clear();
            foreach (var item in _cityItems)
            {
                if (item.Code != toCode)
                    cboTo.Items.Add(item);
            }
            cboTo.SelectedItem = cboTo.Items.Cast<CitySelectDTO>().First(x => x.Code == fromCode);

            // Mo lai
            _isSwapping = false;
            _isRebuilding = false;

            cboFrom.Invalidate();
            cboTo.Invalidate();
        }

        // Animation icon
        private async Task RotateButtonAsync(Guna.UI2.WinForms.Guna2CircleButton btn)
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

        // Rotate image
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
    }
}