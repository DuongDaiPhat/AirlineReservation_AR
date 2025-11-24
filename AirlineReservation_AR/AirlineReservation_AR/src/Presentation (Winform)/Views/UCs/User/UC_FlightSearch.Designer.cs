using Guna.UI2.WinForms;

namespace AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.User
{
    partial class UC_FlightSearch
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Designer

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_FlightSearch));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges26 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges27 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges16 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges17 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges18 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges19 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges20 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges21 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges22 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges23 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges24 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges25 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            pnlMain = new Panel();
            pnlSearchContainer = new Guna2Panel();
            lblTitle = new Label();
            pnlTripType = new Panel();
            guna2Button1 = new Guna2Button();
            btnRoundTrip = new Guna2Button();
            pnlLabels = new Panel();
            guna2HtmlLabel1 = new Guna2HtmlLabel();
            guna2CustomCheckBox1 = new Guna2CustomCheckBox();
            lblFrom = new Label();
            lblTo = new Label();
            lblDate = new Label();
            pnlInputs = new Panel();
            cboFrom = new Guna2ComboBox();
            btnSwap = new Guna2CircleButton();
            panelReturnCalendar = new Guna2Panel();
            cboTo = new Guna2ComboBox();
            btnSearch = new Guna2Button();
            btnStartDate = new Guna2Button();
            btnReturnDate = new Guna2Button();
            panelStartCalendar = new Guna2Panel();
            cboSeatClass = new Guna2ComboBox();
            btnPassenger = new Guna2Button();
            flowDeals = new FlowLayoutPanel();
            flowStartDays = new FlowLayoutPanel();
            flowReturnDays = new FlowLayoutPanel();
            pnlMain.SuspendLayout();
            pnlSearchContainer.SuspendLayout();
            pnlTripType.SuspendLayout();
            pnlLabels.SuspendLayout();
            pnlInputs.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMain
            // 
            pnlMain.BackColor = Color.White;
            pnlMain.BackgroundImage = (Image)resources.GetObject("pnlMain.BackgroundImage");
            pnlMain.Controls.Add(pnlSearchContainer);
            pnlMain.Controls.Add(flowDeals);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 0);
            pnlMain.Name = "pnlMain";
            pnlMain.Padding = new Padding(40, 20, 40, 20);
            pnlMain.Size = new Size(1650, 840);
            pnlMain.TabIndex = 0;
            // 
            // pnlSearchContainer
            // 
            pnlSearchContainer.BackColor = Color.Transparent;
            pnlSearchContainer.BorderRadius = 16;
            pnlSearchContainer.Controls.Add(lblTitle);
            pnlSearchContainer.Controls.Add(pnlTripType);
            pnlSearchContainer.Controls.Add(pnlLabels);
            pnlSearchContainer.Controls.Add(pnlInputs);
            pnlSearchContainer.Controls.Add(cboSeatClass);
            pnlSearchContainer.Controls.Add(btnPassenger);
            pnlSearchContainer.CustomizableEdges = customizableEdges26;
            pnlSearchContainer.Location = new Point(118, 23);
            pnlSearchContainer.Name = "pnlSearchContainer";
            pnlSearchContainer.ShadowDecoration.BorderRadius = 16;
            pnlSearchContainer.ShadowDecoration.Color = Color.FromArgb(0, 0, 0);
            pnlSearchContainer.ShadowDecoration.CustomizableEdges = customizableEdges27;
            pnlSearchContainer.ShadowDecoration.Depth = 20;
            pnlSearchContainer.ShadowDecoration.Enabled = true;
            pnlSearchContainer.ShadowDecoration.Shadow = new Padding(0, 0, 8, 8);
            pnlSearchContainer.Size = new Size(1409, 634);
            pnlSearchContainer.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(463, 17);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(567, 51);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "App du lịch hàng đầu, một chạm đi bất cứ đâu";
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlTripType
            // 
            pnlTripType.BackColor = Color.Transparent;
            pnlTripType.Controls.Add(guna2Button1);
            pnlTripType.Controls.Add(btnRoundTrip);
            pnlTripType.Location = new Point(144, 71);
            pnlTripType.Name = "pnlTripType";
            pnlTripType.Size = new Size(371, 42);
            pnlTripType.TabIndex = 1;
            // 
            // guna2Button1
            // 
            guna2Button1.Animated = true;
            guna2Button1.BackColor = Color.Transparent;
            guna2Button1.BorderRadius = 20;
            guna2Button1.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            guna2Button1.CheckedState.FillColor = Color.FromArgb(0, 164, 239);
            guna2Button1.CheckedState.ForeColor = Color.White;
            guna2Button1.Cursor = Cursors.Hand;
            guna2Button1.CustomizableEdges = customizableEdges1;
            guna2Button1.DisabledState.BorderColor = Color.DarkGray;
            guna2Button1.DisabledState.CustomBorderColor = Color.DarkGray;
            guna2Button1.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            guna2Button1.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            guna2Button1.FillColor = Color.Transparent;
            guna2Button1.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            guna2Button1.ForeColor = Color.FromArgb(100, 100, 100);
            guna2Button1.HoverState.FillColor = Color.FromArgb(230, 245, 255);
            guna2Button1.Location = new Point(19, 0);
            guna2Button1.Name = "guna2Button1";
            guna2Button1.ShadowDecoration.CustomizableEdges = customizableEdges2;
            guna2Button1.Size = new Size(150, 42);
            guna2Button1.TabIndex = 2;
            guna2Button1.Text = "Một chiều/ Khứ hồi";
            // 
            // btnRoundTrip
            // 
            btnRoundTrip.Animated = true;
            btnRoundTrip.BackColor = Color.Transparent;
            btnRoundTrip.BorderRadius = 20;
            btnRoundTrip.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            btnRoundTrip.CheckedState.FillColor = Color.FromArgb(0, 164, 239);
            btnRoundTrip.CheckedState.ForeColor = Color.White;
            btnRoundTrip.Cursor = Cursors.Hand;
            btnRoundTrip.CustomizableEdges = customizableEdges3;
            btnRoundTrip.DisabledState.BorderColor = Color.DarkGray;
            btnRoundTrip.DisabledState.CustomBorderColor = Color.DarkGray;
            btnRoundTrip.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnRoundTrip.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnRoundTrip.FillColor = Color.Transparent;
            btnRoundTrip.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnRoundTrip.ForeColor = Color.FromArgb(100, 100, 100);
            btnRoundTrip.HoverState.FillColor = Color.FromArgb(230, 245, 255);
            btnRoundTrip.Location = new Point(201, 0);
            btnRoundTrip.Name = "btnRoundTrip";
            btnRoundTrip.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnRoundTrip.Size = new Size(150, 42);
            btnRoundTrip.TabIndex = 1;
            btnRoundTrip.Text = "Nhiều thành phố";
            // 
            // pnlLabels
            // 
            pnlLabels.BackColor = Color.Transparent;
            pnlLabels.Controls.Add(guna2HtmlLabel1);
            pnlLabels.Controls.Add(guna2CustomCheckBox1);
            pnlLabels.Controls.Add(lblFrom);
            pnlLabels.Controls.Add(lblTo);
            pnlLabels.Controls.Add(lblDate);
            pnlLabels.Location = new Point(133, 150);
            pnlLabels.Name = "pnlLabels";
            pnlLabels.Size = new Size(1120, 35);
            pnlLabels.TabIndex = 2;
            // 
            // guna2HtmlLabel1
            // 
            guna2HtmlLabel1.BackColor = Color.Transparent;
            guna2HtmlLabel1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            guna2HtmlLabel1.ForeColor = Color.White;
            guna2HtmlLabel1.Location = new Point(864, 1);
            guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            guna2HtmlLabel1.Size = new Size(63, 23);
            guna2HtmlLabel1.TabIndex = 4;
            guna2HtmlLabel1.Text = "Khứ hồi";
            // 
            // guna2CustomCheckBox1
            // 
            guna2CustomCheckBox1.CheckedState.BorderColor = Color.FromArgb(94, 148, 255);
            guna2CustomCheckBox1.CheckedState.BorderRadius = 2;
            guna2CustomCheckBox1.CheckedState.BorderThickness = 0;
            guna2CustomCheckBox1.CheckedState.FillColor = Color.FromArgb(94, 148, 255);
            guna2CustomCheckBox1.CustomizableEdges = customizableEdges5;
            guna2CustomCheckBox1.Location = new Point(829, 1);
            guna2CustomCheckBox1.Name = "guna2CustomCheckBox1";
            guna2CustomCheckBox1.ShadowDecoration.CustomizableEdges = customizableEdges6;
            guna2CustomCheckBox1.Size = new Size(20, 20);
            guna2CustomCheckBox1.TabIndex = 3;
            guna2CustomCheckBox1.Text = "guna2CustomCheckBox1";
            guna2CustomCheckBox1.UncheckedState.BorderColor = Color.FromArgb(125, 137, 149);
            guna2CustomCheckBox1.UncheckedState.BorderRadius = 2;
            guna2CustomCheckBox1.UncheckedState.BorderThickness = 0;
            guna2CustomCheckBox1.UncheckedState.FillColor = Color.FromArgb(125, 137, 149);
            guna2CustomCheckBox1.Click += guna2CustomCheckBox1_Click;
            // 
            // lblFrom
            // 
            lblFrom.AutoSize = true;
            lblFrom.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFrom.ForeColor = Color.White;
            lblFrom.Location = new Point(5, 0);
            lblFrom.Name = "lblFrom";
            lblFrom.Size = new Size(30, 21);
            lblFrom.TabIndex = 0;
            lblFrom.Text = "Từ";
            // 
            // lblTo
            // 
            lblTo.AutoSize = true;
            lblTo.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTo.ForeColor = Color.White;
            lblTo.Location = new Point(359, -1);
            lblTo.Name = "lblTo";
            lblTo.Size = new Size(41, 21);
            lblTo.TabIndex = 1;
            lblTo.Text = "Đến";
            // 
            // lblDate
            // 
            lblDate.AutoSize = true;
            lblDate.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDate.ForeColor = Color.White;
            lblDate.Location = new Point(645, 2);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(132, 21);
            lblDate.TabIndex = 2;
            lblDate.Text = "Ngày khởi hành";
            // 
            // pnlInputs
            // 
            pnlInputs.BackColor = Color.Transparent;
            pnlInputs.Controls.Add(cboFrom);
            pnlInputs.Controls.Add(btnSwap);
            pnlInputs.Controls.Add(panelReturnCalendar);
            pnlInputs.Controls.Add(cboTo);
            pnlInputs.Controls.Add(btnSearch);
            pnlInputs.Controls.Add(btnStartDate);
            pnlInputs.Controls.Add(btnReturnDate);
            pnlInputs.Controls.Add(panelStartCalendar);
            pnlInputs.Location = new Point(133, 188);
            pnlInputs.Name = "pnlInputs";
            pnlInputs.Size = new Size(1120, 432);
            pnlInputs.TabIndex = 3;
            // 
            // cboFrom
            // 
            cboFrom.Animated = true;
            cboFrom.BackColor = Color.Transparent;
            cboFrom.BorderColor = Color.FromArgb(220, 220, 220);
            cboFrom.BorderRadius = 10;
            cboFrom.CustomizableEdges = customizableEdges7;
            cboFrom.DrawMode = DrawMode.OwnerDrawFixed;
            cboFrom.DropDownStyle = ComboBoxStyle.DropDownList;
            cboFrom.FocusedColor = Color.FromArgb(0, 164, 239);
            cboFrom.FocusedState.BorderColor = Color.FromArgb(0, 164, 239);
            cboFrom.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            cboFrom.ForeColor = Color.FromArgb(50, 50, 50);
            cboFrom.HoverState.BorderColor = Color.FromArgb(0, 164, 239);
            cboFrom.ItemHeight = 30;
            cboFrom.Location = new Point(0, 0);
            cboFrom.Name = "cboFrom";
            cboFrom.ShadowDecoration.BorderRadius = 10;
            cboFrom.ShadowDecoration.Color = Color.FromArgb(0, 164, 239);
            cboFrom.ShadowDecoration.CustomizableEdges = customizableEdges8;
            cboFrom.Size = new Size(280, 36);
            cboFrom.TabIndex = 5;
            cboFrom.TextOffset = new Point(10, 0);
            // 
            // btnSwap
            // 
            btnSwap.Animated = true;
            btnSwap.BackColor = Color.Transparent;
            btnSwap.BorderColor = Color.FromArgb(220, 220, 220);
            btnSwap.BorderThickness = 2;
            btnSwap.Cursor = Cursors.Hand;
            btnSwap.DisabledState.BorderColor = Color.DarkGray;
            btnSwap.DisabledState.CustomBorderColor = Color.DarkGray;
            btnSwap.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnSwap.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnSwap.FillColor = Color.White;
            btnSwap.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnSwap.ForeColor = Color.FromArgb(0, 164, 239);
            btnSwap.HoverState.BorderColor = Color.FromArgb(0, 164, 239);
            btnSwap.HoverState.FillColor = Color.FromArgb(230, 245, 255);
            btnSwap.Image = (Image)resources.GetObject("btnSwap.Image");
            btnSwap.ImageSize = new Size(25, 25);
            btnSwap.Location = new Point(295, 0);
            btnSwap.Name = "btnSwap";
            btnSwap.ShadowDecoration.CustomizableEdges = customizableEdges9;
            btnSwap.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            btnSwap.Size = new Size(40, 40);
            btnSwap.TabIndex = 1;
            btnSwap.Click += BtnSwap_Click;
            // 
            // panelReturnCalendar
            // 
            panelReturnCalendar.BorderRadius = 12;
            panelReturnCalendar.CustomizableEdges = customizableEdges10;
            panelReturnCalendar.FillColor = Color.White;
            panelReturnCalendar.Location = new Point(754, 56);
            panelReturnCalendar.Name = "panelReturnCalendar";
            panelReturnCalendar.ShadowDecoration.CustomizableEdges = customizableEdges11;
            panelReturnCalendar.Size = new Size(350, 331);
            panelReturnCalendar.TabIndex = 0;
            panelReturnCalendar.Visible = false;
            // 
            // cboTo
            // 
            cboTo.Animated = true;
            cboTo.BackColor = Color.Transparent;
            cboTo.BorderColor = Color.FromArgb(220, 220, 220);
            cboTo.BorderRadius = 10;
            cboTo.CustomizableEdges = customizableEdges12;
            cboTo.DrawMode = DrawMode.OwnerDrawFixed;
            cboTo.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTo.FocusedColor = Color.FromArgb(0, 164, 239);
            cboTo.FocusedState.BorderColor = Color.FromArgb(0, 164, 239);
            cboTo.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            cboTo.ForeColor = Color.FromArgb(50, 50, 50);
            cboTo.HoverState.BorderColor = Color.FromArgb(0, 164, 239);
            cboTo.ItemHeight = 30;
            cboTo.Location = new Point(350, 0);
            cboTo.Name = "cboTo";
            cboTo.ShadowDecoration.BorderRadius = 10;
            cboTo.ShadowDecoration.Color = Color.FromArgb(0, 164, 239);
            cboTo.ShadowDecoration.CustomizableEdges = customizableEdges13;
            cboTo.Size = new Size(280, 36);
            cboTo.TabIndex = 6;
            cboTo.TextOffset = new Point(10, 0);
            // 
            // btnSearch
            // 
            btnSearch.Animated = true;
            btnSearch.BackColor = Color.Transparent;
            btnSearch.BorderColor = Color.Gray;
            btnSearch.BorderRadius = 12;
            btnSearch.Cursor = Cursors.Hand;
            btnSearch.CustomizableEdges = customizableEdges14;
            btnSearch.DisabledState.BorderColor = Color.DarkGray;
            btnSearch.DisabledState.CustomBorderColor = Color.DarkGray;
            btnSearch.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnSearch.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnSearch.FillColor = Color.FromArgb(255, 112, 28);
            btnSearch.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnSearch.ForeColor = Color.White;
            btnSearch.HoverState.FillColor = Color.FromArgb(255, 90, 0);
            btnSearch.Image = (Image)resources.GetObject("btnSearch.Image");
            btnSearch.ImageSize = new Size(25, 25);
            btnSearch.Location = new Point(1051, 3);
            btnSearch.Name = "btnSearch";
            btnSearch.ShadowDecoration.BorderRadius = 25;
            btnSearch.ShadowDecoration.Color = Color.FromArgb(255, 112, 28);
            btnSearch.ShadowDecoration.CustomizableEdges = customizableEdges15;
            btnSearch.ShadowDecoration.Depth = 12;
            btnSearch.ShadowDecoration.Enabled = true;
            btnSearch.Size = new Size(53, 47);
            btnSearch.TabIndex = 4;
            btnSearch.TextOffset = new Point(5, 0);
            btnSearch.Click += btnSearch_Click;
            btnSearch.MouseEnter += BtnSearch_MouseEnter;
            btnSearch.MouseLeave += BtnSearch_MouseLeave;
            // 
            // btnStartDate
            // 
            btnStartDate.BorderRadius = 10;
            btnStartDate.CustomizableEdges = customizableEdges16;
            btnStartDate.FillColor = Color.White;
            btnStartDate.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnStartDate.ForeColor = Color.Black;
            btnStartDate.Location = new Point(645, 0);
            btnStartDate.Name = "btnStartDate";
            btnStartDate.ShadowDecoration.CustomizableEdges = customizableEdges17;
            btnStartDate.Size = new Size(178, 50);
            btnStartDate.TabIndex = 7;
            btnStartDate.Text = "Chọn ngày đi";
            btnStartDate.Click += BtnStartDate_Click;
            // 
            // btnReturnDate
            // 
            btnReturnDate.BorderRadius = 10;
            btnReturnDate.CustomizableEdges = customizableEdges18;
            btnReturnDate.Enabled = false;
            btnReturnDate.FillColor = Color.Gainsboro;
            btnReturnDate.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnReturnDate.ForeColor = Color.Gray;
            btnReturnDate.Location = new Point(829, 0);
            btnReturnDate.Name = "btnReturnDate";
            btnReturnDate.ShadowDecoration.CustomizableEdges = customizableEdges19;
            btnReturnDate.Size = new Size(178, 50);
            btnReturnDate.TabIndex = 8;
            btnReturnDate.Text = "Chọn ngày về";
            btnReturnDate.Click += BtnReturnDate_Click;
            // 
            // panelStartCalendar
            // 
            panelStartCalendar.BorderRadius = 12;
            panelStartCalendar.CustomizableEdges = customizableEdges20;
            panelStartCalendar.FillColor = Color.White;
            panelStartCalendar.Location = new Point(368, 56);
            panelStartCalendar.Name = "panelStartCalendar";
            panelStartCalendar.ShadowDecoration.CustomizableEdges = customizableEdges21;
            panelStartCalendar.Size = new Size(350, 331);
            panelStartCalendar.TabIndex = 9;
            panelStartCalendar.Visible = false;
            // 
            // cboSeatClass
            // 
            cboSeatClass.Animated = true;
            cboSeatClass.BackColor = Color.Transparent;
            cboSeatClass.BorderColor = Color.White;
            cboSeatClass.BorderRadius = 10;
            cboSeatClass.CustomizableEdges = customizableEdges22;
            cboSeatClass.DrawMode = DrawMode.OwnerDrawFixed;
            cboSeatClass.DropDownStyle = ComboBoxStyle.DropDownList;
            cboSeatClass.FocusedColor = Color.FromArgb(0, 164, 239);
            cboSeatClass.FocusedState.BorderColor = Color.FromArgb(0, 164, 239);
            cboSeatClass.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            cboSeatClass.ForeColor = Color.Transparent;
            cboSeatClass.HoverState.BorderColor = Color.FromArgb(0, 164, 239);
            cboSeatClass.ItemHeight = 30;
            cboSeatClass.Items.AddRange(new object[] { "Phổ thông", "Phổ thông đặc biệt", "Thương gia", "Hạng nhất" });
            cboSeatClass.Location = new Point(647, 77);
            cboSeatClass.Name = "cboSeatClass";
            cboSeatClass.ShadowDecoration.BorderRadius = 10;
            cboSeatClass.ShadowDecoration.Color = Color.FromArgb(0, 164, 239);
            cboSeatClass.ShadowDecoration.CustomizableEdges = customizableEdges23;
            cboSeatClass.ShadowDecoration.Depth = 8;
            cboSeatClass.Size = new Size(280, 36);
            cboSeatClass.StartIndex = 0;
            cboSeatClass.TabIndex = 6;
            cboSeatClass.TextOffset = new Point(10, 0);
            cboSeatClass.Enter += Combo_Enter;
            cboSeatClass.Leave += Combo_Leave;
            // 
            // btnPassenger
            // 
            btnPassenger.BackColor = Color.Transparent;
            btnPassenger.BorderColor = Color.White;
            btnPassenger.BorderRadius = 10;
            btnPassenger.CustomizableEdges = customizableEdges24;
            btnPassenger.FillColor = Color.Transparent;
            btnPassenger.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnPassenger.ForeColor = Color.White;
            btnPassenger.Location = new Point(962, 77);
            btnPassenger.Name = "btnPassenger";
            btnPassenger.ShadowDecoration.CustomizableEdges = customizableEdges25;
            btnPassenger.Size = new Size(259, 36);
            btnPassenger.TabIndex = 7;
            btnPassenger.Text = "👤 1 Người lớn, 0 Trẻ em, 0 Em bé";
            btnPassenger.Click += BtnPassenger_Click;
            // 
            // flowDeals
            // 
            flowDeals.AutoScroll = true;
            flowDeals.BackColor = Color.Transparent;
            flowDeals.Location = new Point(118, 672);
            flowDeals.Name = "flowDeals";
            flowDeals.Size = new Size(1409, 145);
            flowDeals.TabIndex = 1;
            // 
            // flowStartDays
            // 
            flowStartDays.Location = new Point(0, 0);
            flowStartDays.Name = "flowStartDays";
            flowStartDays.Size = new Size(200, 100);
            flowStartDays.TabIndex = 0;
            // 
            // flowReturnDays
            // 
            flowReturnDays.Location = new Point(0, 0);
            flowReturnDays.Name = "flowReturnDays";
            flowReturnDays.Size = new Size(200, 100);
            flowReturnDays.TabIndex = 0;
            // 
            // UC_FlightSearch
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(pnlMain);
            Name = "UC_FlightSearch";
            Size = new Size(1650, 840);
            Load += UC_FlightSearch_Load;
            pnlMain.ResumeLayout(false);
            pnlSearchContainer.ResumeLayout(false);
            pnlTripType.ResumeLayout(false);
            pnlLabels.ResumeLayout(false);
            pnlLabels.PerformLayout();
            pnlInputs.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        // Event handlers for animations
        private void Input_Enter(object sender, System.EventArgs e)
        {
            if (sender is Guna.UI2.WinForms.Guna2TextBox textBox)
            {
                textBox.ShadowDecoration.Enabled = true;
            }
            else if (sender is Guna.UI2.WinForms.Guna2DateTimePicker dateTimePicker)
            {
                dateTimePicker.ShadowDecoration.Enabled = true;
            }
        }

        private void Input_Leave(object sender, System.EventArgs e)
        {
            if (sender is Guna.UI2.WinForms.Guna2TextBox textBox)
            {
                textBox.ShadowDecoration.Enabled = false;
            }
            else if (sender is Guna.UI2.WinForms.Guna2DateTimePicker dateTimePicker)
            {
                dateTimePicker.ShadowDecoration.Enabled = false;
            }
        }

        private void Combo_Enter(object sender, System.EventArgs e)
        {
            if (sender is Guna.UI2.WinForms.Guna2ComboBox comboBox)
            {
                comboBox.ShadowDecoration.Enabled = true;
            }
        }

        private void Combo_Leave(object sender, System.EventArgs e)
        {
            if (sender is Guna.UI2.WinForms.Guna2ComboBox comboBox)
            {
                comboBox.ShadowDecoration.Enabled = false;
            }
        }

        private void BtnSearch_MouseEnter(object sender, System.EventArgs e)
        {
            // Animation được xử lý bởi HoverState của Guna2Button
        }

        private void BtnSearch_MouseLeave(object sender, System.EventArgs e)
        {
            // Animation được xử lý bởi HoverState của Guna2Button
        }



        private System.Windows.Forms.Panel pnlMain;
        private Guna.UI2.WinForms.Guna2Panel pnlSearchContainer;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlTripType;
        private Guna.UI2.WinForms.Guna2Button btnRoundTrip;
        private System.Windows.Forms.Panel pnlLabels;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Panel pnlInputs;
        private Guna.UI2.WinForms.Guna2ComboBox cboFrom;
        private Guna.UI2.WinForms.Guna2CircleButton btnSwap;
        private Guna.UI2.WinForms.Guna2ComboBox cboTo;
        private Guna.UI2.WinForms.Guna2Button btnSearch;
        private System.Windows.Forms.FlowLayoutPanel flowDeals;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2CustomCheckBox guna2CustomCheckBox1;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
        private Guna2Button btnPassenger;
        private Guna.UI2.WinForms.Guna2ComboBox cboSeatClass;
        private Guna2Panel panelReturnCalendar;
        private Guna2Panel panelStartCalendar;
        private Guna2Button btnStartDate;
        private Guna2Button btnReturnDate;
    }
}