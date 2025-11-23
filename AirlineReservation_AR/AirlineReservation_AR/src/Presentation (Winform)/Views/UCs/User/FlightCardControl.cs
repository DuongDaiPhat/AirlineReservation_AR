using System;
using System.Drawing;
using System.Windows.Forms;
using AirlineReservation_AR.src.Domain.DTOs;
using Guna.UI2.WinForms;

namespace AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.User
{
    public partial class FlightCardControl : Guna2Panel
    {
        public event Action<FlightResultDTO> OnSelected;
        private FlightResultDTO _dto;
        public FlightCardControl()
        {
            InitializeComponent();

            // Traveloka style - compact and clean
            this.Width = 800;
            this.Height = 100;
            this.BorderRadius = 8;
            this.ShadowDecoration.Enabled = true;
            this.ShadowDecoration.Depth = 8;
            this.ShadowDecoration.Color = Color.FromArgb(30, 0, 0, 0);
            this.ShadowDecoration.Shadow = new Padding(0, 2, 4, 4);

            // Light gray background like Traveloka
            this.FillColor = Color.FromArgb(250, 250, 250);
            this.BorderColor = Color.FromArgb(230, 230, 230);
            this.BorderThickness = 1;

            this.Margin = new Padding(0, 0, 0, 12);

            // Enable hover effect
            this.MouseEnter += (s, e) => OnCardHover(true);
            this.MouseLeave += (s, e) => OnCardHover(false);
        }

        // Constructor runtime để truyền DTO
        public FlightCardControl(FlightResultDTO dto, bool highlight = false) : this()
        {
            _dto = dto;
            BindData(dto, highlight);

        }

        // === Bind dữ liệu vào UI ===
        public void BindData(FlightResultDTO dto, bool highlight)
        {
            // Highlight style
            if (highlight)
            {
                this.FillColor = Color.FromArgb(255, 250, 240);
                this.BorderColor = Color.FromArgb(255, 200, 150);
            }
            else
            {
                this.FillColor = Color.FromArgb(250, 250, 250);
                this.BorderColor = Color.FromArgb(230, 230, 230);
            }

            picLogo.ImageLocation = dto.AirlineLogo;
            lblAirline.Text = dto.AirlineName;

            // Format times
            lblTimes.Text = $"{dto.DepartureTime.ToString(@"hh\:mm")} → {dto.ArrivalTime.ToString(@"hh\:mm")}";

            // Only show departure airport code in route
            lblRoute.Text = dto.FromAirportCode;

            // Format price with decimal
            decimal priceInMillions = dto.Price / 1000;
            lblPrice.Text = $"{priceInMillions:N3} VNĐ/khách";

            // Calculate and display duration
            TimeSpan duration = dto.ArrivalTime - dto.DepartureTime;
            lblDuration.Text = $"{duration.Hours}h {duration.Minutes}m";
        }

        private void OnCardHover(bool isHover)
        {
            if (isHover)
            {
                this.FillColor = Color.FromArgb(245, 245, 248);
                this.ShadowDecoration.Depth = 12;
                this.BorderColor = Color.FromArgb(200, 200, 200);
            }
            else
            {
                this.FillColor = Color.FromArgb(250, 250, 250);
                this.ShadowDecoration.Depth = 8;
                this.BorderColor = Color.FromArgb(230, 230, 230);
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            OnSelected?.Invoke(_dto);
        }
    }
}