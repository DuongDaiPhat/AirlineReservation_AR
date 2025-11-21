namespace AirlineReservation_AR.src.Presentation__Winform_.Views.popup
{
    partial class PopupAddBaggage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private Panel pnlLeft;
        private Panel pnlRight;
        private Panel pnlFooter;

        private Label lblFlightRoute;
        private FlowLayoutPanel flowPassengerList;
        private FlowLayoutPanel flowFlightList;

        private Button btnSave;
        private Label lblTotalPrice;
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlLeft = new Panel();
            this.pnlRight = new Panel();
            this.pnlFooter = new Panel();

            this.lblFlightRoute = new Label();
            this.flowPassengerList = new FlowLayoutPanel();
            this.flowFlightList = new FlowLayoutPanel();

            this.btnSave = new Button();
            this.lblTotalPrice = new Label();

            // FORM
            this.Text = "Chọn hành lý ký gửi";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Size = new Size(1000, 650);
            this.BackColor = Color.White;

            // LEFT PANEL
            pnlLeft.Dock = DockStyle.Left;
            pnlLeft.Width = 380;
            pnlLeft.Padding = new Padding(20);
            pnlLeft.BackColor = Color.FromArgb(245, 246, 250);

            var lblLeftTitle = new Label()
            {
                Text = "Chọn chuyến bay",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(5, 5)
            };
            pnlLeft.Controls.Add(lblLeftTitle);

            flowFlightList.FlowDirection = FlowDirection.TopDown;
            flowFlightList.WrapContents = false;
            flowFlightList.AutoScroll = true;
            flowFlightList.Location = new Point(5, 40);
            flowFlightList.Size = new Size(340, 500);

            pnlLeft.Controls.Add(flowFlightList);

            // RIGHT PANEL
            pnlRight.Dock = DockStyle.Fill;
            pnlRight.Padding = new Padding(20);
            pnlRight.BackColor = Color.White;

            lblFlightRoute.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            lblFlightRoute.AutoSize = true;
            lblFlightRoute.Location = new Point(5, 5);
            pnlRight.Controls.Add(lblFlightRoute);

            flowPassengerList.FlowDirection = FlowDirection.TopDown;
            flowPassengerList.WrapContents = false;
            flowPassengerList.AutoScroll = true;
            flowPassengerList.Location = new Point(5, 50);
            flowPassengerList.Size = new Size(550, 480);
            pnlRight.Controls.Add(flowPassengerList);

            // FOOTER
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Height = 70;
            pnlFooter.Padding = new Padding(20);
            pnlFooter.BackColor = Color.WhiteSmoke;

            lblTotalPrice.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblTotalPrice.AutoSize = true;
            lblTotalPrice.Location = new Point(20, 25);
            pnlFooter.Controls.Add(lblTotalPrice);

            btnSave.Text = "Lưu";
            btnSave.Width = 150;
            btnSave.Height = 40;
            btnSave.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnSave.BackColor = Color.OrangeRed;
            btnSave.ForeColor = Color.White;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Location = new Point(780, 15);
            btnSave.Click += btnSave_Click;

            pnlFooter.Controls.Add(btnSave);

            this.Controls.Add(pnlRight);
            this.Controls.Add(pnlLeft);
            this.Controls.Add(pnlFooter);
        }

        #endregion
    }
}