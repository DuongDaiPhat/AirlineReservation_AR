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

            this.Width = 800;
            this.Height = 140;
            this.BorderRadius = 8;
            this.ShadowDecoration.Enabled = true;
            this.ShadowDecoration.Depth = 8;
            this.ShadowDecoration.Color = Color.FromArgb(30, 0, 0, 0);
            this.ShadowDecoration.Shadow = new Padding(0, 2, 4, 4);

            this.FillColor = Color.FromArgb(250, 250, 250);
            this.BorderColor = Color.FromArgb(230, 230, 230);
            this.BorderThickness = 1;
            this.Margin = new Padding(0, 0, 0, 12);

            this.MouseEnter += (s, e) => OnCardHover(true);
            this.MouseLeave += (s, e) => OnCardHover(false);
        }

        public FlightCardControl(FlightResultDTO dto, bool highlight = false) : this()
        {
            _dto = dto;
            BindData(dto, highlight);
        }

        // -----------------------------
        // BIND DATA TO CARD
        // -----------------------------
        public void BindData(FlightResultDTO dto, bool highlight)
        {
            _dto = dto;

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

            lblTimes.Text = $"{dto.DepartureTime:hh\\:mm} → {dto.ArrivalTime:hh\\:mm}";
            lblRoute.Text = dto.FromAirportCode;

            decimal priceInMillions = dto.Price / 1000;
            lblPrice.Text = $"{priceInMillions:N3} VNĐ/khách";

            TimeSpan duration = dto.ArrivalTime - dto.DepartureTime;
            lblDuration.Text = $"{duration.Hours}h {duration.Minutes}m";

            BindSeatInfo(dto);
        }

        // -----------------------------
        // RENDER SEAT INFORMATION
        // -----------------------------
        private void BindSeatInfo(FlightResultDTO dto)
        {
            pnlSeatsInfo.Controls.Clear();

            if (dto.SeatsLeftByClass == null || dto.SeatsLeftByClass.Count == 0)
            {
                Label lblNoData = new Label
                {
                    Text = $"Còn {dto.TotalSeatsLeft} ghế trống",
                    Font = new Font("Segoe UI", 8F),
                    ForeColor = Color.FromArgb(100, 100, 100),
                    AutoSize = true
                };
                pnlSeatsInfo.Controls.Add(lblNoData);
                return;
            }

            // UI config for badge colors
            var classDisplayInfo = new Dictionary<string, (string DisplayName, Color Color)>
            {
                { "Economy", ("Economy", Color.FromArgb(52, 152, 219)) },
                { "Premium Economy", ("Premium Economy", Color.FromArgb(26, 188, 156)) },
                { "Business", ("Business", Color.FromArgb(155, 89, 182)) },
                { "First", ("First", Color.FromArgb(241, 196, 15)) }
            };

            foreach (var kvp in dto.SeatsLeftByClass)
            {
                string className = kvp.Key;  // <-- KEY LÀ DISPLAYNAME
                int seatsLeft = kvp.Value;

                // default
                string displayName = className;
                Color badgeColor = Color.FromArgb(52, 152, 219);

                if (classDisplayInfo.ContainsKey(className))
                {
                    displayName = classDisplayInfo[className].DisplayName;
                    badgeColor = classDisplayInfo[className].Color;
                }

                Guna2Panel badge = CreateSeatBadge(displayName, seatsLeft, badgeColor);
                pnlSeatsInfo.Controls.Add(badge);
            }
        }

        // -----------------------------
        // CREATE BADGE PER SEATCLASS
        // -----------------------------
        private Guna2Panel CreateSeatBadge(string name, int seatsLeft, Color color)
        {
            Guna2Panel badge = new Guna2Panel
            {
                Size = new Size(140, 28),
                BorderRadius = 6,
                FillColor = Color.FromArgb(240, 245, 250),
                BorderColor = color,
                BorderThickness = 1,
                Margin = new Padding(0, 0, 8, 0)
            };

            Label lblIcon = new Label
            {
                Text = "✈",
                Font = new Font("Segoe UI", 9F),
                ForeColor = color,
                Location = new Point(8, 6),
                AutoSize = true
            };

            Label lblClass = new Label
            {
                Text = name,
                Font = new Font("Segoe UI", 8F),
                ForeColor = Color.FromArgb(80, 80, 80),
                Location = new Point(28, 4),
                AutoSize = true
            };

            Label lblSeats = new Label
            {
                Text = $"{seatsLeft} ghế",
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                ForeColor = seatsLeft < 5 ? Color.FromArgb(231, 76, 60) : color,
                Location = new Point(28, 15),
                AutoSize = true
            };

            if (seatsLeft < 5)
            {
                badge.BorderColor = Color.FromArgb(231, 76, 60);
                badge.FillColor = Color.FromArgb(255, 245, 245);
            }

            badge.Controls.Add(lblIcon);
            badge.Controls.Add(lblClass);
            badge.Controls.Add(lblSeats);

            return badge;
        }

        // -----------------------------
        // HOVER CARD EFFECT
        // -----------------------------
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
