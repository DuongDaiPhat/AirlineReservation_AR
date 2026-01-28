namespace AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.Admin
{
    partial class FlightManagementControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
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
            panelFilters = new Panel();
            btnAddFlight = new Guna.UI2.WinForms.Guna2Button();
            cboDestination = new Guna.UI2.WinForms.Guna2ComboBox();
            btnSearch = new Guna.UI2.WinForms.Guna2Button();
            btnRefresh = new Guna.UI2.WinForms.Guna2Button();
            cboStatus = new Guna.UI2.WinForms.Guna2ComboBox();
            guna2DateTimePicker1 = new Guna.UI2.WinForms.Guna2DateTimePicker();
            cboAirline = new Guna.UI2.WinForms.Guna2ComboBox();
            lblDestination = new Label();
            lblStatus = new Label();
            lblFlightDate = new Label();
            lblAirline = new Label();
            txtFlightNo = new Guna.UI2.WinForms.Guna2TextBox();
            lblFlightNo = new Label();
            dgvFlights = new DataGridView();
            colFlightCode = new DataGridViewTextBoxColumn();
            colAirline = new DataGridViewTextBoxColumn();
            colRoute = new DataGridViewTextBoxColumn();
            colDate = new DataGridViewTextBoxColumn();
            colDuration = new DataGridViewTextBoxColumn();
            colDeparture = new DataGridViewTextBoxColumn();
            colAircraft = new DataGridViewTextBoxColumn();
            colPrice = new DataGridViewTextBoxColumn();
            colSeats = new DataGridViewTextBoxColumn();
            colStatus = new DataGridViewTextBoxColumn();
            colActions = new DataGridViewTextBoxColumn();
            panel1 = new Panel();
            paginationControl1 = new PaginationControl();
            panel2 = new Panel();
            panelFilters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvFlights).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panelFilters
            // 
            panelFilters.Controls.Add(btnAddFlight);
            panelFilters.Controls.Add(cboDestination);
            panelFilters.Controls.Add(btnSearch);
            panelFilters.Controls.Add(btnRefresh);
            panelFilters.Controls.Add(cboStatus);
            panelFilters.Controls.Add(guna2DateTimePicker1);
            panelFilters.Controls.Add(cboAirline);
            panelFilters.Controls.Add(lblDestination);
            panelFilters.Controls.Add(lblStatus);
            panelFilters.Controls.Add(lblFlightDate);
            panelFilters.Controls.Add(lblAirline);
            panelFilters.Controls.Add(txtFlightNo);
            panelFilters.Controls.Add(lblFlightNo);
            panelFilters.Dock = DockStyle.Top;
            panelFilters.Location = new Point(15, 15);
            panelFilters.Margin = new Padding(0);
            panelFilters.Name = "panelFilters";
            panelFilters.Size = new Size(1010, 161);
            panelFilters.TabIndex = 0;
            // 
            // btnAddFlight
            // 
            btnAddFlight.BorderRadius = 8;
            btnAddFlight.CustomizableEdges = customizableEdges1;
            btnAddFlight.DisabledState.BorderColor = Color.DarkGray;
            btnAddFlight.DisabledState.CustomBorderColor = Color.DarkGray;
            btnAddFlight.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnAddFlight.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnAddFlight.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAddFlight.ForeColor = Color.White;
            btnAddFlight.Location = new Point(805, 111);
            btnAddFlight.Margin = new Padding(0);
            btnAddFlight.Name = "btnAddFlight";
            btnAddFlight.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnAddFlight.Size = new Size(125, 40);
            btnAddFlight.TabIndex = 11;
            btnAddFlight.Text = "+ New Flight";
            btnAddFlight.Click += btnAddFlight_Click;
            // 
            // cboDestination
            // 
            cboDestination.BackColor = Color.Transparent;
            cboDestination.BorderRadius = 8;
            cboDestination.CustomizableEdges = customizableEdges3;
            cboDestination.DrawMode = DrawMode.OwnerDrawFixed;
            cboDestination.DropDownStyle = ComboBoxStyle.DropDownList;
            cboDestination.FocusedColor = Color.FromArgb(94, 148, 255);
            cboDestination.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            cboDestination.Font = new Font("Segoe UI", 10F);
            cboDestination.ForeColor = Color.FromArgb(68, 88, 112);
            cboDestination.ItemHeight = 30;
            cboDestination.Location = new Point(20, 115);
            cboDestination.Name = "cboDestination";
            cboDestination.ShadowDecoration.CustomizableEdges = customizableEdges4;
            cboDestination.Size = new Size(200, 36);
            cboDestination.TabIndex = 10;
            // 
            // btnSearch
            // 
            btnSearch.BorderRadius = 8;
            btnSearch.CustomizableEdges = customizableEdges5;
            btnSearch.DisabledState.BorderColor = Color.DarkGray;
            btnSearch.DisabledState.CustomBorderColor = Color.DarkGray;
            btnSearch.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnSearch.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnSearch.FillColor = Color.FromArgb(33, 150, 243);
            btnSearch.Font = new Font("Segoe UI", 9F);
            btnSearch.ForeColor = Color.White;
            btnSearch.Location = new Point(810, 40);
            btnSearch.Margin = new Padding(0);
            btnSearch.Name = "btnSearch";
            btnSearch.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnSearch.Size = new Size(120, 35);
            btnSearch.TabIndex = 9;
            btnSearch.Text = "🔍 Search";
            btnSearch.Click += BtnSearch_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.BorderRadius = 8;
            btnRefresh.CustomizableEdges = customizableEdges1;
            btnRefresh.FillColor = Color.Gray;
            btnRefresh.Font = new Font("Segoe UI", 9F);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(940, 40);
            btnRefresh.Margin = new Padding(0);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.ShadowDecoration.CustomizableEdges = customizableEdges7;
            btnRefresh.Size = new Size(80, 35);
            btnRefresh.TabIndex = 12;
            btnRefresh.Text = "Refresh";
            btnRefresh.Click += BtnRefresh_Click;
            // 
            // cboStatus
            // 
            cboStatus.BackColor = Color.Transparent;
            cboStatus.BorderRadius = 8;
            cboStatus.CustomizableEdges = customizableEdges8;
            cboStatus.DrawMode = DrawMode.OwnerDrawFixed;
            cboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cboStatus.FocusedColor = Color.FromArgb(94, 148, 255);
            cboStatus.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            cboStatus.Font = new Font("Segoe UI", 10F);
            cboStatus.ForeColor = Color.FromArgb(68, 88, 112);
            cboStatus.ItemHeight = 30;
            cboStatus.Location = new Point(640, 40);
            cboStatus.Margin = new Padding(0);
            cboStatus.Name = "cboStatus";
            cboStatus.ShadowDecoration.CustomizableEdges = customizableEdges9;
            cboStatus.Size = new Size(150, 36);
            cboStatus.TabIndex = 8;
            // 
            // guna2DateTimePicker1
            // 
            guna2DateTimePicker1.BorderRadius = 8;
            guna2DateTimePicker1.Checked = true;
            guna2DateTimePicker1.CustomizableEdges = customizableEdges10;
            guna2DateTimePicker1.FillColor = Color.White;
            guna2DateTimePicker1.Font = new Font("Segoe UI", 9F);
            guna2DateTimePicker1.Format = DateTimePickerFormat.Short;
            guna2DateTimePicker1.Location = new Point(440, 43);
            guna2DateTimePicker1.Margin = new Padding(0);
            guna2DateTimePicker1.MaxDate = new DateTime(9998, 12, 31, 0, 0, 0, 0);
            guna2DateTimePicker1.MinDate = new DateTime(1753, 1, 1, 0, 0, 0, 0);
            guna2DateTimePicker1.Name = "guna2DateTimePicker1";
            guna2DateTimePicker1.ShadowDecoration.CustomizableEdges = customizableEdges11;
            guna2DateTimePicker1.Size = new Size(180, 30);
            guna2DateTimePicker1.TabIndex = 7;
            guna2DateTimePicker1.Value = new DateTime(2025, 11, 20, 13, 12, 27, 30);
            // 
            // cboAirline
            // 
            cboAirline.BackColor = Color.Transparent;
            cboAirline.BorderRadius = 8;
            cboAirline.CustomizableEdges = customizableEdges12;
            cboAirline.DrawMode = DrawMode.OwnerDrawFixed;
            cboAirline.DropDownStyle = ComboBoxStyle.DropDownList;
            cboAirline.FocusedColor = Color.FromArgb(94, 148, 255);
            cboAirline.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            cboAirline.Font = new Font("Segoe UI", 10F);
            cboAirline.ForeColor = Color.FromArgb(68, 88, 112);
            cboAirline.ItemHeight = 30;
            cboAirline.Location = new Point(240, 42);
            cboAirline.Margin = new Padding(0);
            cboAirline.Name = "cboAirline";
            cboAirline.ShadowDecoration.CustomizableEdges = customizableEdges13;
            cboAirline.Size = new Size(180, 36);
            cboAirline.TabIndex = 6;
            // 
            // lblDestination
            // 
            lblDestination.AutoSize = true;
            lblDestination.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDestination.Location = new Point(20, 85);
            lblDestination.Name = "lblDestination";
            lblDestination.Size = new Size(87, 15);
            lblDestination.TabIndex = 5;
            lblDestination.Text = "Arrival Airport";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblStatus.Location = new Point(640, 15);
            lblStatus.Margin = new Padding(0);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(42, 15);
            lblStatus.TabIndex = 4;
            lblStatus.Text = "Status";
            // 
            // lblFlightDate
            // 
            lblFlightDate.AutoSize = true;
            lblFlightDate.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFlightDate.Location = new Point(440, 15);
            lblFlightDate.Margin = new Padding(0);
            lblFlightDate.Name = "lblFlightDate";
            lblFlightDate.Size = new Size(34, 15);
            lblFlightDate.TabIndex = 3;
            lblFlightDate.Text = "Date";
            // 
            // lblAirline
            // 
            lblAirline.AutoSize = true;
            lblAirline.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblAirline.Location = new Point(240, 15);
            lblAirline.Name = "lblAirline";
            lblAirline.Size = new Size(43, 15);
            lblAirline.TabIndex = 2;
            lblAirline.Text = "Airline";
            // 
            // txtFlightNo
            // 
            txtFlightNo.BorderRadius = 8;
            txtFlightNo.CustomizableEdges = customizableEdges14;
            txtFlightNo.DefaultText = "";
            txtFlightNo.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtFlightNo.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtFlightNo.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtFlightNo.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtFlightNo.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtFlightNo.Font = new Font("Segoe UI", 9F);
            txtFlightNo.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtFlightNo.Location = new Point(20, 44);
            txtFlightNo.Margin = new Padding(0);
            txtFlightNo.Name = "txtFlightNo";
            txtFlightNo.PlaceholderText = "Eg: VN210";
            txtFlightNo.SelectedText = "";
            txtFlightNo.ShadowDecoration.CustomizableEdges = customizableEdges15;
            txtFlightNo.Size = new Size(200, 30);
            txtFlightNo.TabIndex = 1;
            txtFlightNo.KeyDown += TxtFlightNo_KeyDown;
            // 
            // lblFlightNo
            // 
            lblFlightNo.AutoSize = true;
            lblFlightNo.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFlightNo.Location = new Point(20, 15);
            lblFlightNo.Margin = new Padding(0);
            lblFlightNo.Name = "lblFlightNo";
            lblFlightNo.Size = new Size(57, 15);
            lblFlightNo.TabIndex = 0;
            lblFlightNo.Text = "Flight No";
            // 
            // dgvFlights
            // 
            dgvFlights.AllowUserToAddRows = false;
            dgvFlights.AllowUserToDeleteRows = false;
            dgvFlights.BackgroundColor = Color.White;
            dgvFlights.BorderStyle = BorderStyle.None;
            dgvFlights.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvFlights.Columns.AddRange(new DataGridViewColumn[] { colFlightCode, colAirline, colRoute, colDate, colDuration, colDeparture, colAircraft, colPrice, colSeats, colStatus, colActions });
            dgvFlights.Dock = DockStyle.Fill;
            dgvFlights.GridColor = Color.FromArgb(224, 224, 224);
            dgvFlights.Location = new Point(0, 0);
            dgvFlights.Margin = new Padding(0);
            dgvFlights.MultiSelect = false;
            dgvFlights.Name = "dgvFlights";
            dgvFlights.ReadOnly = true;
            dgvFlights.RowHeadersVisible = false;
            dgvFlights.RowHeadersWidth = 51;
            dgvFlights.RowTemplate.Height = 50;
            dgvFlights.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvFlights.Size = new Size(1010, 459);
            dgvFlights.TabIndex = 1;
            dgvFlights.CellClick += DgvFlights_CellClick;
            // 
            // colFlightCode
            // 
            colFlightCode.FillWeight = 176.981171F;
            colFlightCode.HeaderText = "FLIGHT CODE";
            colFlightCode.MinimumWidth = 6;
            colFlightCode.Name = "colFlightCode";
            colFlightCode.ReadOnly = true;
            colFlightCode.Width = 120;
            // 
            // colAirline
            // 
            colAirline.FillWeight = 820.8122F;
            colAirline.HeaderText = "AIRLINE";
            colAirline.MinimumWidth = 6;
            colAirline.Name = "colAirline";
            colAirline.ReadOnly = true;
            colAirline.Width = 150;
            // 
            // colRoute
            // 
            colRoute.FillWeight = 11.3562918F;
            colRoute.HeaderText = "ROUTE";
            colRoute.MinimumWidth = 6;
            colRoute.Name = "colRoute";
            colRoute.ReadOnly = true;
            colRoute.Width = 120;
            // 
            // colDate
            // 
            colDate.FillWeight = 11.3562918F;
            colDate.HeaderText = "DATE";
            colDate.MinimumWidth = 6;
            colDate.Name = "colDate";
            colDate.ReadOnly = true;
            colDate.Width = 120;
            // 
            // colDuration
            // 
            colDuration.FillWeight = 11.3562918F;
            colDuration.HeaderText = "DURATION";
            colDuration.MinimumWidth = 6;
            colDuration.Name = "colDuration";
            colDuration.ReadOnly = true;
            colDuration.Width = 125;
            // 
            // colDeparture
            // 
            colDeparture.FillWeight = 11.3562918F;
            colDeparture.HeaderText = "DEPARTURE";
            colDeparture.MinimumWidth = 6;
            colDeparture.Name = "colDeparture";
            colDeparture.ReadOnly = true;
            colDeparture.Width = 125;
            // 
            // colAircraft
            // 
            colAircraft.FillWeight = 11.3562918F;
            colAircraft.HeaderText = "AIRCRAFT";
            colAircraft.MinimumWidth = 6;
            colAircraft.Name = "colAircraft";
            colAircraft.ReadOnly = true;
            colAircraft.Width = 125;
            // 
            // colPrice
            // 
            colPrice.FillWeight = 11.3562918F;
            colPrice.HeaderText = "BASE PRICE";
            colPrice.MinimumWidth = 6;
            colPrice.Name = "colPrice";
            colPrice.ReadOnly = true;
            colPrice.Width = 120;
            // 
            // colSeats
            // 
            colSeats.FillWeight = 11.3562918F;
            colSeats.HeaderText = "AVAIL. SEATS";
            colSeats.MinimumWidth = 6;
            colSeats.Name = "colSeats";
            colSeats.ReadOnly = true;
            colSeats.Width = 125;
            // 
            // colStatus
            // 
            colStatus.FillWeight = 11.3562918F;
            colStatus.HeaderText = "STATUS";
            colStatus.MinimumWidth = 6;
            colStatus.Name = "colStatus";
            colStatus.ReadOnly = true;
            colStatus.Width = 125;
            // 
            // colActions
            // 
            colActions.FillWeight = 11.3562918F;
            colActions.HeaderText = "ACTIONS";
            colActions.MinimumWidth = 6;
            colActions.Name = "colActions";
            colActions.ReadOnly = true;
            colActions.Width = 180;
            // 
            // panel1
            // 
            panel1.Controls.Add(paginationControl1);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(15, 635);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1010, 50);
            panel1.TabIndex = 2;
            // 
            // paginationControl1
            // 
            paginationControl1.ActiveBorderColor = Color.FromArgb(255, 105, 180);
            paginationControl1.ActiveButtonColor = Color.FromArgb(255, 105, 180);
            paginationControl1.ActiveTextColor = Color.White;
            paginationControl1.BackColor = Color.White;
            paginationControl1.Dock = DockStyle.Fill;
            paginationControl1.HoverButtonColor = Color.FromArgb(255, 200, 220);
            paginationControl1.InactiveBorderColor = Color.FromArgb(220, 220, 220);
            paginationControl1.InactiveButtonColor = Color.White;
            paginationControl1.InactiveTextColor = Color.FromArgb(120, 120, 120);
            paginationControl1.Location = new Point(0, 0);
            paginationControl1.Margin = new Padding(0);
            paginationControl1.Name = "paginationControl1";
            paginationControl1.Size = new Size(1010, 50);
            paginationControl1.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Controls.Add(dgvFlights);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(15, 176);
            panel2.Margin = new Padding(0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1010, 459);
            panel2.TabIndex = 3;
            // 
            // FlightManagementControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(panelFilters);
            Margin = new Padding(0);
            Name = "FlightManagementControl";
            Padding = new Padding(15);
            Size = new Size(1040, 700);
            panelFilters.ResumeLayout(false);
            panelFilters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvFlights).EndInit();
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelFilters;
        private Label lblFlightNo;
        private Guna.UI2.WinForms.Guna2TextBox txtFlightNo;
        private Label lblAirline;
        private Guna.UI2.WinForms.Guna2ComboBox cboAirline;
        private Label lblDestination;
        private Label lblStatus;
        private Label lblFlightDate;
        private Guna.UI2.WinForms.Guna2DateTimePicker guna2DateTimePicker1;
        private Guna.UI2.WinForms.Guna2ComboBox cboStatus;
        private Guna.UI2.WinForms.Guna2Button btnSearch;
        private Guna.UI2.WinForms.Guna2ComboBox cboDestination;
        private DataGridView dgvFlights;
        private DataGridViewTextBoxColumn colFlightCode;
        private DataGridViewTextBoxColumn colAirline;
        private DataGridViewTextBoxColumn colRoute;
        private DataGridViewTextBoxColumn colDate;
        private DataGridViewTextBoxColumn colDuration;
        private DataGridViewTextBoxColumn colDeparture;
        private DataGridViewTextBoxColumn colAircraft;
        private DataGridViewTextBoxColumn colPrice;
        private DataGridViewTextBoxColumn colSeats;
        private DataGridViewTextBoxColumn colStatus;
        private DataGridViewTextBoxColumn colActions;
        private Guna.UI2.WinForms.Guna2Button btnAddFlight;
        private Guna.UI2.WinForms.Guna2Button btnRefresh;
        private Panel panel1;
        private PaginationControl paginationControl1;
        private Panel panel2;
    }
}
