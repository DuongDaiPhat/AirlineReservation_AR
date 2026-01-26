namespace AirlineReservation_AR.src.Presentation__Winform_.Views.popup
{
    partial class PopupAddBaggage
    {
        private System.ComponentModel.IContainer components = null;
        private Panel pnlRight;
        private Panel pnlFooter;
        private Label lblFlightSubtitle;
        private Button btnSave;
        private Label lblTotalPrice;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopupAddBaggage));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            pnlRight = new Panel();
            rightBody = new Helpers.BetterFlowLayoutPanel();
            noteBaggage = new Guna.UI2.WinForms.Guna2Panel();
            guna2HtmlLabel4 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            noteTextTile = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pictureBox5 = new PictureBox();
            guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            guna2GradientPanel1 = new Guna.UI2.WinForms.Guna2GradientPanel();
            pictureBox6 = new PictureBox();
            txtAirline = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2HtmlLabel3 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtFromToPlace = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pictureBox4 = new PictureBox();
            txtPassengerInfo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            bodyLeft = new Guna.UI2.WinForms.Guna2Panel();
            guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            OneWay = new Guna.UI2.WinForms.Guna2Button();
            RoundTrip = new Guna.UI2.WinForms.Guna2Button();
            guna2ShadowPanel1 = new Guna.UI2.WinForms.Guna2ShadowPanel();
            guna2HtmlLabel9 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pictureBox1 = new PictureBox();
            guna2ShadowPanel2 = new Guna.UI2.WinForms.Guna2ShadowPanel();
            pictureBox2 = new PictureBox();
            guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2ShadowPanel3 = new Guna.UI2.WinForms.Guna2ShadowPanel();
            pictureBox3 = new PictureBox();
            guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblFlightSubtitle = new Label();
            pnlFooter = new Panel();
            lblTotalPrice = new Label();
            btnSave = new Button();
            sqlCommandBuilder1 = new Microsoft.Data.SqlClient.SqlCommandBuilder();
            pnlRight.SuspendLayout();
            rightBody.SuspendLayout();
            noteBaggage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            guna2Panel1.SuspendLayout();
            guna2GradientPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            bodyLeft.SuspendLayout();
            guna2Panel2.SuspendLayout();
            guna2ShadowPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            guna2ShadowPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            guna2ShadowPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            pnlFooter.SuspendLayout();
            SuspendLayout();
            // 
            // pnlRight
            // 
            pnlRight.BackColor = Color.FromArgb(240, 242, 245);
            pnlRight.Controls.Add(rightBody);
            pnlRight.Controls.Add(guna2Panel1);
            pnlRight.Controls.Add(bodyLeft);
            pnlRight.Controls.Add(lblFlightSubtitle);
            pnlRight.Dock = DockStyle.Fill;
            pnlRight.Location = new Point(0, 0);
            pnlRight.Name = "pnlRight";
            pnlRight.Padding = new Padding(25, 20, 25, 20);
            pnlRight.Size = new Size(984, 550);
            pnlRight.TabIndex = 0;
            // 
            // rightBody
            // 
            rightBody.AutoScroll = true;
            rightBody.Controls.Add(noteBaggage);
            rightBody.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            rightBody.Location = new Point(359, 128);
            rightBody.Name = "rightBody";
            rightBody.RightToLeft = RightToLeft.Yes;
            rightBody.Size = new Size(625, 416);
            rightBody.TabIndex = 4;
            // 
            // noteBaggage
            // 
            noteBaggage.BackColor = Color.FromArgb(239, 246, 255);
            noteBaggage.BorderColor = Color.FromArgb(191, 219, 254);
            noteBaggage.BorderRadius = 8;
            noteBaggage.BorderThickness = 2;
            noteBaggage.Controls.Add(guna2HtmlLabel4);
            noteBaggage.Controls.Add(noteTextTile);
            noteBaggage.Controls.Add(pictureBox5);
            noteBaggage.CustomizableEdges = customizableEdges1;
            noteBaggage.Location = new Point(15, 3);
            noteBaggage.Margin = new Padding(15, 3, 3, 3);
            noteBaggage.Name = "noteBaggage";
            noteBaggage.ShadowDecoration.CustomizableEdges = customizableEdges2;
            noteBaggage.Size = new Size(595, 72);
            noteBaggage.TabIndex = 0;
            // 
            // guna2HtmlLabel4
            // 
            guna2HtmlLabel4.BackColor = Color.Transparent;
            guna2HtmlLabel4.Font = new Font("Segoe UI Historic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            guna2HtmlLabel4.ForeColor = Color.FromArgb(63, 164, 241);
            guna2HtmlLabel4.Location = new Point(79, 39);
            guna2HtmlLabel4.Name = "guna2HtmlLabel4";
            guna2HtmlLabel4.Size = new Size(155, 19);
            guna2HtmlLabel4.TabIndex = 2;
            guna2HtmlLabel4.Text = "Included in the ticket price";
            // 
            // noteTextTile
            // 
            noteTextTile.BackColor = Color.Transparent;
            noteTextTile.Font = new Font("Segoe UI Emoji", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            noteTextTile.ForeColor = Color.FromArgb(30, 58, 153);
            noteTextTile.Location = new Point(79, 12);
            noteTextTile.Name = "noteTextTile";
            noteTextTile.Size = new Size(233, 23);
            noteTextTile.TabIndex = 1;
            noteTextTile.Text = "Hand luggage allowance: 7kg";
            // 
            // pictureBox5
            // 
            pictureBox5.Image = (Image)resources.GetObject("pictureBox5.Image");
            pictureBox5.Location = new Point(22, 12);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(30, 32);
            pictureBox5.TabIndex = 0;
            pictureBox5.TabStop = false;
            // 
            // guna2Panel1
            // 
            guna2Panel1.Controls.Add(guna2GradientPanel1);
            guna2Panel1.CustomizableEdges = customizableEdges5;
            guna2Panel1.Location = new Point(0, 3);
            guna2Panel1.Name = "guna2Panel1";
            guna2Panel1.ShadowDecoration.CustomizableEdges = customizableEdges6;
            guna2Panel1.Size = new Size(984, 122);
            guna2Panel1.TabIndex = 5;
            // 
            // guna2GradientPanel1
            // 
            guna2GradientPanel1.BorderRadius = 15;
            guna2GradientPanel1.Controls.Add(pictureBox6);
            guna2GradientPanel1.Controls.Add(txtAirline);
            guna2GradientPanel1.Controls.Add(guna2HtmlLabel3);
            guna2GradientPanel1.Controls.Add(txtFromToPlace);
            guna2GradientPanel1.Controls.Add(pictureBox4);
            guna2GradientPanel1.Controls.Add(txtPassengerInfo);
            guna2GradientPanel1.CustomizableEdges = customizableEdges3;
            guna2GradientPanel1.Dock = DockStyle.Fill;
            guna2GradientPanel1.FillColor = Color.LightBlue;
            guna2GradientPanel1.FillColor2 = Color.Honeydew;
            guna2GradientPanel1.Location = new Point(0, 0);
            guna2GradientPanel1.Margin = new Padding(3, 2, 3, 2);
            guna2GradientPanel1.Name = "guna2GradientPanel1";
            guna2GradientPanel1.ShadowDecoration.CustomizableEdges = customizableEdges4;
            guna2GradientPanel1.Size = new Size(984, 122);
            guna2GradientPanel1.TabIndex = 23;
            guna2GradientPanel1.Paint += guna2GradientPanel1_Paint;
            // 
            // pictureBox6
            // 
            pictureBox6.BackColor = Color.Transparent;
            pictureBox6.Image = (Image)resources.GetObject("pictureBox6.Image");
            pictureBox6.Location = new Point(930, 0);
            pictureBox6.Name = "pictureBox6";
            pictureBox6.Size = new Size(42, 36);
            pictureBox6.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox6.TabIndex = 31;
            pictureBox6.TabStop = false;
            pictureBox6.Click += pictureBox6_Click;
            // 
            // txtAirline
            // 
            txtAirline.BackColor = Color.Transparent;
            txtAirline.Font = new Font("Segoe UI", 12.75F);
            txtAirline.Location = new Point(159, 85);
            txtAirline.Margin = new Padding(3, 2, 3, 2);
            txtAirline.Name = "txtAirline";
            txtAirline.Size = new Size(136, 25);
            txtAirline.TabIndex = 30;
            txtAirline.Text = "Vietname Airlines";
            // 
            // guna2HtmlLabel3
            // 
            guna2HtmlLabel3.BackColor = Color.Transparent;
            guna2HtmlLabel3.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            guna2HtmlLabel3.ForeColor = Color.RoyalBlue;
            guna2HtmlLabel3.Location = new Point(159, 8);
            guna2HtmlLabel3.Margin = new Padding(3, 2, 3, 2);
            guna2HtmlLabel3.Name = "guna2HtmlLabel3";
            guna2HtmlLabel3.Size = new Size(91, 39);
            guna2HtmlLabel3.TabIndex = 29;
            guna2HtmlLabel3.Text = "ROUTE";
            // 
            // txtFromToPlace
            // 
            txtFromToPlace.BackColor = Color.Transparent;
            txtFromToPlace.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            txtFromToPlace.Location = new Point(159, 51);
            txtFromToPlace.Margin = new Padding(3, 2, 3, 2);
            txtFromToPlace.Name = "txtFromToPlace";
            txtFromToPlace.Size = new Size(315, 30);
            txtFromToPlace.TabIndex = 16;
            txtFromToPlace.Text = "TP HCM (SGN) -> Bangkok (BKK)";
            // 
            // pictureBox4
            // 
            pictureBox4.BackColor = Color.Transparent;
            pictureBox4.Image = (Image)resources.GetObject("pictureBox4.Image");
            pictureBox4.Location = new Point(3, 3);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(129, 116);
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.TabIndex = 15;
            pictureBox4.TabStop = false;
            // 
            // txtPassengerInfo
            // 
            txtPassengerInfo.Anchor = AnchorStyles.Right;
            txtPassengerInfo.AutoSize = false;
            txtPassengerInfo.BackColor = Color.Transparent;
            txtPassengerInfo.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPassengerInfo.Location = new Point(1280, 44);
            txtPassengerInfo.Margin = new Padding(3, 2, 3, 2);
            txtPassengerInfo.Name = "txtPassengerInfo";
            txtPassengerInfo.Size = new Size(29, 20);
            txtPassengerInfo.TabIndex = 13;
            txtPassengerInfo.Text = "Kha";
            txtPassengerInfo.TextAlignment = ContentAlignment.MiddleRight;
            // 
            // bodyLeft
            // 
            bodyLeft.Controls.Add(guna2Panel2);
            bodyLeft.Controls.Add(guna2ShadowPanel1);
            bodyLeft.Controls.Add(guna2ShadowPanel2);
            bodyLeft.Controls.Add(guna2ShadowPanel3);
            bodyLeft.CustomBorderColor = Color.Black;
            bodyLeft.CustomizableEdges = customizableEdges13;
            bodyLeft.Location = new Point(3, 128);
            bodyLeft.Name = "bodyLeft";
            bodyLeft.ShadowDecoration.CustomizableEdges = customizableEdges14;
            bodyLeft.Size = new Size(350, 422);
            bodyLeft.TabIndex = 4;
            // 
            // guna2Panel2
            // 
            guna2Panel2.BorderRadius = 10;
            guna2Panel2.Controls.Add(OneWay);
            guna2Panel2.Controls.Add(RoundTrip);
            guna2Panel2.CustomizableEdges = customizableEdges11;
            guna2Panel2.FillColor = Color.FromArgb(224, 224, 224);
            guna2Panel2.Location = new Point(169, 15);
            guna2Panel2.Name = "guna2Panel2";
            guna2Panel2.ShadowDecoration.CustomizableEdges = customizableEdges12;
            guna2Panel2.Size = new Size(170, 43);
            guna2Panel2.TabIndex = 4;
            // 
            // OneWay
            // 
            OneWay.BackColor = Color.FromArgb(224, 224, 224);
            OneWay.BorderRadius = 8;
            OneWay.CustomizableEdges = customizableEdges7;
            OneWay.DisabledState.BorderColor = Color.DarkGray;
            OneWay.DisabledState.CustomBorderColor = Color.DarkGray;
            OneWay.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            OneWay.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            OneWay.FillColor = Color.White;
            OneWay.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            OneWay.ForeColor = Color.FromArgb(37, 99, 235);
            OneWay.Location = new Point(5, 3);
            OneWay.Name = "OneWay";
            OneWay.ShadowDecoration.CustomizableEdges = customizableEdges8;
            OneWay.Size = new Size(81, 36);
            OneWay.TabIndex = 0;
            OneWay.Text = "Oneway";
            OneWay.Click += OnewayClick;
            // 
            // RoundTrip
            // 
            RoundTrip.BackColor = Color.FromArgb(224, 224, 224);
            RoundTrip.BorderRadius = 8;
            RoundTrip.CustomizableEdges = customizableEdges9;
            RoundTrip.DisabledState.BorderColor = Color.DarkGray;
            RoundTrip.DisabledState.CustomBorderColor = Color.DarkGray;
            RoundTrip.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            RoundTrip.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            RoundTrip.FillColor = Color.FromArgb(224, 224, 224);
            RoundTrip.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            RoundTrip.ForeColor = Color.DimGray;
            RoundTrip.Location = new Point(87, 3);
            RoundTrip.Name = "RoundTrip";
            RoundTrip.ShadowDecoration.CustomizableEdges = customizableEdges10;
            RoundTrip.Size = new Size(79, 36);
            RoundTrip.TabIndex = 1;
            RoundTrip.Text = "Arround";
            RoundTrip.Click += RoundTripClick;
            // 
            // guna2ShadowPanel1
            // 
            guna2ShadowPanel1.BackColor = Color.Transparent;
            guna2ShadowPanel1.Controls.Add(guna2HtmlLabel9);
            guna2ShadowPanel1.Controls.Add(pictureBox1);
            guna2ShadowPanel1.Cursor = Cursors.AppStarting;
            guna2ShadowPanel1.FillColor = Color.White;
            guna2ShadowPanel1.Location = new Point(23, 86);
            guna2ShadowPanel1.Name = "guna2ShadowPanel1";
            guna2ShadowPanel1.Radius = 8;
            guna2ShadowPanel1.ShadowColor = Color.Black;
            guna2ShadowPanel1.Size = new Size(308, 77);
            guna2ShadowPanel1.TabIndex = 0;
            // 
            // guna2HtmlLabel9
            // 
            guna2HtmlLabel9.BackColor = Color.Transparent;
            guna2HtmlLabel9.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            guna2HtmlLabel9.ForeColor = Color.RoyalBlue;
            guna2HtmlLabel9.Location = new Point(67, 25);
            guna2HtmlLabel9.Margin = new Padding(3, 2, 3, 2);
            guna2HtmlLabel9.Name = "guna2HtmlLabel9";
            guna2HtmlLabel9.Size = new Size(143, 25);
            guna2HtmlLabel9.TabIndex = 28;
            guna2HtmlLabel9.Text = "Baggage Services";
            guna2HtmlLabel9.Click += BaggageService_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(28, 25);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(24, 24);
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // guna2ShadowPanel2
            // 
            guna2ShadowPanel2.BackColor = Color.Transparent;
            guna2ShadowPanel2.Controls.Add(pictureBox2);
            guna2ShadowPanel2.Controls.Add(guna2HtmlLabel1);
            guna2ShadowPanel2.FillColor = Color.White;
            guna2ShadowPanel2.Location = new Point(23, 188);
            guna2ShadowPanel2.Name = "guna2ShadowPanel2";
            guna2ShadowPanel2.Radius = 8;
            guna2ShadowPanel2.ShadowColor = Color.Black;
            guna2ShadowPanel2.Size = new Size(308, 77);
            guna2ShadowPanel2.TabIndex = 2;
            guna2ShadowPanel2.Click += MealService_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(28, 25);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(24, 24);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 29;
            pictureBox2.TabStop = false;
            // 
            // guna2HtmlLabel1
            // 
            guna2HtmlLabel1.BackColor = Color.Transparent;
            guna2HtmlLabel1.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            guna2HtmlLabel1.ForeColor = Color.RoyalBlue;
            guna2HtmlLabel1.Location = new Point(67, 25);
            guna2HtmlLabel1.Margin = new Padding(3, 2, 3, 2);
            guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            guna2HtmlLabel1.Size = new Size(181, 25);
            guna2HtmlLabel1.TabIndex = 28;
            guna2HtmlLabel1.Text = "Food & Drink Services";
            guna2HtmlLabel1.Click += MealService_Click;
            // 
            // guna2ShadowPanel3
            // 
            guna2ShadowPanel3.BackColor = Color.Transparent;
            guna2ShadowPanel3.Controls.Add(pictureBox3);
            guna2ShadowPanel3.Controls.Add(guna2HtmlLabel2);
            guna2ShadowPanel3.FillColor = Color.White;
            guna2ShadowPanel3.Location = new Point(23, 293);
            guna2ShadowPanel3.Name = "guna2ShadowPanel3";
            guna2ShadowPanel3.Radius = 8;
            guna2ShadowPanel3.ShadowColor = Color.Black;
            guna2ShadowPanel3.Size = new Size(308, 77);
            guna2ShadowPanel3.TabIndex = 3;
            guna2ShadowPanel3.Click += PriorityService_Click;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(28, 25);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(24, 24);
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.TabIndex = 29;
            pictureBox3.TabStop = false;
            // 
            // guna2HtmlLabel2
            // 
            guna2HtmlLabel2.BackColor = Color.Transparent;
            guna2HtmlLabel2.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            guna2HtmlLabel2.ForeColor = Color.RoyalBlue;
            guna2HtmlLabel2.Location = new Point(67, 25);
            guna2HtmlLabel2.Margin = new Padding(3, 2, 3, 2);
            guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            guna2HtmlLabel2.Size = new Size(123, 25);
            guna2HtmlLabel2.TabIndex = 28;
            guna2HtmlLabel2.Text = "Priority Serices";
            guna2HtmlLabel2.Click += PriorityService_Click;
            // 
            // lblFlightSubtitle
            // 
            lblFlightSubtitle.AutoSize = true;
            lblFlightSubtitle.Font = new Font("Segoe UI", 9.5F);
            lblFlightSubtitle.ForeColor = Color.FromArgb(142, 142, 147);
            lblFlightSubtitle.Location = new Point(28, 48);
            lblFlightSubtitle.Name = "lblFlightSubtitle";
            lblFlightSubtitle.Size = new Size(13, 17);
            lblFlightSubtitle.TabIndex = 1;
            lblFlightSubtitle.Text = "-";
            // 
            // pnlFooter
            // 
            pnlFooter.BackColor = Color.White;
            pnlFooter.BorderStyle = BorderStyle.FixedSingle;
            pnlFooter.Controls.Add(lblTotalPrice);
            pnlFooter.Controls.Add(btnSave);
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Location = new Point(0, 550);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Padding = new Padding(25, 12, 25, 12);
            pnlFooter.Size = new Size(984, 68);
            pnlFooter.TabIndex = 2;
            // 
            // lblTotalPrice
            // 
            lblTotalPrice.AutoSize = true;
            lblTotalPrice.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblTotalPrice.ForeColor = Color.FromArgb(255, 59, 48);
            lblTotalPrice.Location = new Point(25, 20);
            lblTotalPrice.Name = "lblTotalPrice";
            lblTotalPrice.Size = new Size(118, 25);
            lblTotalPrice.TabIndex = 0;
            lblTotalPrice.Text = "Total: 0 VND";
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(0, 102, 204);
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(855, 13);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(110, 40);
            btnSave.TabIndex = 2;
            btnSave.Text = "✓ Confirm";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click_1;
            // 
            // PopupAddBaggage
            // 
            BackColor = Color.FromArgb(240, 242, 245);
            ClientSize = new Size(984, 618);
            Controls.Add(pnlRight);
            Controls.Add(pnlFooter);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PopupAddBaggage";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Add Baggage";
            Load += PopupAddBaggage_Load;
            pnlRight.ResumeLayout(false);
            pnlRight.PerformLayout();
            rightBody.ResumeLayout(false);
            noteBaggage.ResumeLayout(false);
            noteBaggage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            guna2Panel1.ResumeLayout(false);
            guna2GradientPanel1.ResumeLayout(false);
            guna2GradientPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            bodyLeft.ResumeLayout(false);
            guna2Panel2.ResumeLayout(false);
            guna2ShadowPanel1.ResumeLayout(false);
            guna2ShadowPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            guna2ShadowPanel2.ResumeLayout(false);
            guna2ShadowPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            guna2ShadowPanel3.ResumeLayout(false);
            guna2ShadowPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            pnlFooter.ResumeLayout(false);
            pnlFooter.PerformLayout();
            ResumeLayout(false);
        }

        private Guna.UI2.WinForms.Guna2ShadowPanel guna2ShadowPanel1;
        private PictureBox pictureBox1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel9;
        private Guna.UI2.WinForms.Guna2ShadowPanel guna2ShadowPanel3;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna.UI2.WinForms.Guna2ShadowPanel guna2ShadowPanel2;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private Guna.UI2.WinForms.Guna2Panel bodyLeft;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2GradientPanel guna2GradientPanel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel txtPassengerInfo;
        private PictureBox pictureBox4;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel3;
        private Guna.UI2.WinForms.Guna2HtmlLabel txtFromToPlace;
        private Guna.UI2.WinForms.Guna2HtmlLabel txtAirline;
        private Guna.UI2.WinForms.Guna2Panel noteBaggage;
        private Guna.UI2.WinForms.Guna2HtmlLabel noteTextTile;
        private PictureBox pictureBox5;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel4;
        private UCs.User.PanelBaggagePassenger panelPassenger1;
        private PictureBox pictureBox6;
        private Helpers.BetterFlowLayoutPanel rightBody;
        private Microsoft.Data.SqlClient.SqlCommandBuilder sqlCommandBuilder1;
        private Guna.UI2.WinForms.Guna2Button OneWay;
        private Guna.UI2.WinForms.Guna2Button RoundTrip;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel2;
    }
}
