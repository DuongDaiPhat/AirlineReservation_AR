using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirlineReservation_AR.src.AirlineReservation.Presentation__Winform_.Views.UCs.Admin
{
    public partial class adminDasboard : UserControl
    {
        private int direction = 1;
        private int moveRange = 3;
        private int speed = 1;
        private int baseTop;
        private int gradientPosition = 0;
        private bool isHover = false;
        private bool isMouseInside = false;
        public adminDasboard()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            foreach (Control ctrl in this.Controls)
            {
                ctrl.MouseMove += AnyControl_MouseMove;
                ctrl.MouseLeave += AnyControl_MouseLeave;
            }
            this.MouseMove += AnyControl_MouseMove;
            this.MouseLeave += AnyControl_MouseLeave;
            this.Click += OnControlClick;
            foreach (Control ctrl in this.Controls)
            {
                ctrl.Click += OnControlClick;

                // Nếu có control lồng nhau
                foreach (Control subCtrl in ctrl.Controls)
                {
                    subCtrl.Click += OnControlClick;
                }
            }
        }
        private void OnControlClick(object sender, EventArgs e)
        {
            if (ReferenceEquals(sender, this)) return;
            this.OnClick(e);
        }
        private void adminDasboard_Load(object sender, EventArgs e)
        {
            baseTop = pictureBoxAdminDashboardControl.Top;
        }
        public string title
        {
            get { return labelAdminDashboardControlTitle.Text; }
            set { labelAdminDashboardControlTitle.Text = value; }
        }
        public string description
        {
            get { return labelAdminDashboardControlDescription.Text; }
            set { labelAdminDashboardControlDescription.Text = value; }
        }
        public Image dashboardImage
        {
            get { return pictureBoxAdminDashboardControl.Image; }
            set { pictureBoxAdminDashboardControl.Image = value; }
        }
        public void SetCardData(String title, String description, Image dashboardImage)
        {
            this.title = title;
            this.description = description;
            this.dashboardImage = dashboardImage;
        }

        private void timerMoving_Tick(object sender, EventArgs e)
        {
            pictureBoxAdminDashboardControl.Top += direction * speed;
            if (pictureBoxAdminDashboardControl.Top >= baseTop + moveRange || pictureBoxAdminDashboardControl.Top <= baseTop - moveRange)
            {
                direction *= -1;
            }
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(
                panelTopBorder.ClientRectangle,
                Color.FromArgb(255, 255, 255),
                Color.FromArgb(220, 220, 220),
                LinearGradientMode.Horizontal))
            {
                Rectangle rect = new Rectangle(0, 0, gradientPosition, panelTopBorder.Height);
                e.Graphics.FillRectangle(brush, rect);
            }
        }

        private void timerGradient_Tick(object sender, EventArgs e)
        {
            if (isHover)
            {
                if (gradientPosition < panelTopBorder.Width)
                {
                    gradientPosition += 10;
                }
                else
                {
                    timerGradient.Stop();
                }
            }
            else
            {
                if (gradientPosition > 0)
                {
                    gradientPosition -= 10;
                }
                else
                {
                    timerGradient.Stop();
                }
            }
            panelTopBorder.Invalidate();
        }
        private void AnyControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isMouseInside)
            {
                isMouseInside = true;
                isHover = true;
                timerGradient.Start();
            }
        }

        private void AnyControl_MouseLeave(object sender, EventArgs e)
        {
            Point cursorPos = this.PointToClient(Cursor.Position);
            if (!this.ClientRectangle.Contains(cursorPos))
            {
                isMouseInside = false;
                isHover = false;
                timerGradient.Start();
            }
        }
    }
    public enum AdminDashboardControlType
    {
        DatVe_ThanhToan,
        GiaVe_KhuyenMai,
        QuanLyChuyenBay,
        BaoCao_ThongKe,
        KhachHang
    }
}
