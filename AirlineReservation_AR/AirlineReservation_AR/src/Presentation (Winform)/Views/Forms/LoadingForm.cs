using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace AirlineReservation.src.AirlineReservation.Presentation__WinForms_.Views.Forms.Common
{
    public partial class LoadingForm : Form
    {
        private float rotationAngle = 0;
        private const float ROTATION_SPEED = 6f;
        private PictureBox loadingCircle;

        public LoadingForm()
        {
            InitializeComponent();

            // Tạo PictureBox để vẽ vòng xoay
            loadingCircle = new PictureBox
            {
                Size = new Size(90, 90),
                BackColor = Color.Transparent
            };
            loadingCircle.Paint += LoadingCircle_Paint;
            this.Controls.Add(loadingCircle);
            loadingCircle.BringToFront(); 
            loadingCircle.Location = new Point(105, 39);


            // Cấu hình timer
            rotate.Interval = 20; 
            rotate.Tick += rotate_Tick;
            rotate.Start();

            this.FormClosed += (s, e) => rotate.Stop();
        }

        private void rotate_Tick(object sender, EventArgs e)
        {
            rotationAngle += ROTATION_SPEED;
            if (rotationAngle >= 360)
                rotationAngle = 0;

            loadingCircle.Invalidate(); // vẽ lại
        }

        private void LoadingCircle_Paint(object sender, PaintEventArgs e)
        {
            // Bật anti-aliasing cho mượt
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            int size = Math.Min(loadingCircle.Width, loadingCircle.Height);
            int thickness = 7; // ProgressThickness từ Guna
            int padding = thickness / 2 + 2; // thêm padding để không bị cắt

            Rectangle rect = new Rectangle(
                padding,
                padding,
                size - (padding * 2),
                size - (padding * 2)
            );

            // Vẽ vòng tròn nền (màu xám nhạt như Guna)
            using (Pen bgPen = new Pen(Color.FromArgb(37, 35, 39), thickness))
            {
                bgPen.StartCap = LineCap.Round;
                bgPen.EndCap = LineCap.Round;
                e.Graphics.DrawEllipse(bgPen, rect);
            }

            // Vẽ vòng cung progress xoay (Turquoise - 30% = 108 độ)
            using (Pen progressPen = new Pen(Color.FromArgb(14, 146, 203), thickness))
            {
                progressPen.StartCap = LineCap.Round;
                progressPen.EndCap = LineCap.Round;

                // -90 để bắt đầu từ trên cùng (12 giờ)
                e.Graphics.DrawArc(
                    progressPen,
                    rect,
                    rotationAngle - 90,
                    108 // 30% của 360 độ
                );
            }
        }
    }
}