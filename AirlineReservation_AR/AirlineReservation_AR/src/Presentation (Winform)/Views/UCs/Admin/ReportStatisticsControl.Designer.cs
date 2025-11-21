namespace AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.Admin
{
    partial class ReportStatisticsControl
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
            ListViewItem listViewItem9 = new ListViewItem("STT");
            ListViewItem listViewItem10 = new ListViewItem("Tuyến bay");
            ListViewItem listViewItem11 = new ListViewItem("Số booking");
            ListViewItem listViewItem12 = new ListViewItem("Doanh thu");
            ListViewItem listViewItem13 = new ListViewItem("STT");
            ListViewItem listViewItem14 = new ListViewItem("Khách hàng");
            ListViewItem listViewItem15 = new ListViewItem("Email");
            ListViewItem listViewItem16 = new ListViewItem("Tổng chi");
            pnlFilters = new Panel();
            flpStats = new FlowLayoutPanel();
            pnlCharts = new Panel();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            cboReportType = new Guna.UI2.WinForms.Guna2ComboBox();
            dtpFromDate = new Guna.UI2.WinForms.Guna2DateTimePicker();
            btnFilter = new Guna.UI2.WinForms.Guna2Button();
            guna2DateTimePicker1 = new Guna.UI2.WinForms.Guna2DateTimePicker();
            btnExport = new Guna.UI2.WinForms.Guna2Button();
            pnlChart1 = new Panel();
            label4 = new Label();
            pnlChart1Content = new Panel();
            pnlChart2 = new Panel();
            label5 = new Label();
            lvTopRoutes = new ListView();
            pnlChart3 = new Panel();
            label6 = new Label();
            listView1 = new ListView();
            pnlChart4 = new Panel();
            pnlSummary = new Panel();
            label7 = new Label();
            pnlFilters.SuspendLayout();
            pnlCharts.SuspendLayout();
            pnlChart1.SuspendLayout();
            pnlChart2.SuspendLayout();
            pnlChart3.SuspendLayout();
            pnlChart4.SuspendLayout();
            SuspendLayout();
            // 
            // pnlFilters
            // 
            pnlFilters.Controls.Add(btnExport);
            pnlFilters.Controls.Add(guna2DateTimePicker1);
            pnlFilters.Controls.Add(btnFilter);
            pnlFilters.Controls.Add(dtpFromDate);
            pnlFilters.Controls.Add(cboReportType);
            pnlFilters.Controls.Add(label3);
            pnlFilters.Controls.Add(label2);
            pnlFilters.Controls.Add(label1);
            pnlFilters.Dock = DockStyle.Top;
            pnlFilters.Location = new Point(0, 0);
            pnlFilters.Margin = new Padding(0);
            pnlFilters.Name = "pnlFilters";
            pnlFilters.Padding = new Padding(15);
            pnlFilters.Size = new Size(1040, 80);
            pnlFilters.TabIndex = 0;
            // 
            // flpStats
            // 
            flpStats.BackColor = Color.WhiteSmoke;
            flpStats.Dock = DockStyle.Top;
            flpStats.Location = new Point(0, 80);
            flpStats.Margin = new Padding(0);
            flpStats.Name = "flpStats";
            flpStats.Padding = new Padding(15);
            flpStats.Size = new Size(1040, 120);
            flpStats.TabIndex = 1;
            // 
            // pnlCharts
            // 
            pnlCharts.BackColor = Color.FromArgb(248, 249, 250);
            pnlCharts.Controls.Add(pnlChart4);
            pnlCharts.Controls.Add(pnlChart3);
            pnlCharts.Controls.Add(pnlChart2);
            pnlCharts.Controls.Add(pnlChart1);
            pnlCharts.Dock = DockStyle.Fill;
            pnlCharts.Location = new Point(0, 200);
            pnlCharts.Margin = new Padding(0);
            pnlCharts.Name = "pnlCharts";
            pnlCharts.Padding = new Padding(15);
            pnlCharts.Size = new Size(1040, 450);
            pnlCharts.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(20, 15);
            label1.Margin = new Padding(0);
            label1.Name = "label1";
            label1.Size = new Size(74, 15);
            label1.TabIndex = 0;
            label1.Text = "Loại báo cáo";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(220, 15);
            label2.Margin = new Padding(0);
            label2.Name = "label2";
            label2.Size = new Size(51, 15);
            label2.TabIndex = 1;
            label2.Text = "Từ ngày";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(390, 15);
            label3.Margin = new Padding(0);
            label3.Name = "label3";
            label3.Size = new Size(57, 15);
            label3.TabIndex = 2;
            label3.Text = "Đến ngày";
            // 
            // cboReportType
            // 
            cboReportType.BackColor = Color.Transparent;
            cboReportType.BorderRadius = 10;
            cboReportType.CustomizableEdges = customizableEdges11;
            cboReportType.DrawMode = DrawMode.OwnerDrawFixed;
            cboReportType.DropDownStyle = ComboBoxStyle.DropDownList;
            cboReportType.FocusedColor = Color.FromArgb(94, 148, 255);
            cboReportType.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            cboReportType.Font = new Font("Segoe UI", 10F);
            cboReportType.ForeColor = Color.FromArgb(68, 88, 112);
            cboReportType.ItemHeight = 30;
            cboReportType.Location = new Point(20, 40);
            cboReportType.Margin = new Padding(0);
            cboReportType.Name = "cboReportType";
            cboReportType.ShadowDecoration.CustomizableEdges = customizableEdges12;
            cboReportType.Size = new Size(180, 36);
            cboReportType.TabIndex = 3;
            // 
            // dtpFromDate
            // 
            dtpFromDate.BorderRadius = 5;
            dtpFromDate.Checked = true;
            dtpFromDate.CustomizableEdges = customizableEdges13;
            dtpFromDate.FillColor = Color.White;
            dtpFromDate.Font = new Font("Segoe UI", 9F);
            dtpFromDate.Format = DateTimePickerFormat.Short;
            dtpFromDate.Location = new Point(220, 40);
            dtpFromDate.Margin = new Padding(0);
            dtpFromDate.MaxDate = new DateTime(9998, 12, 31, 0, 0, 0, 0);
            dtpFromDate.MinDate = new DateTime(1753, 1, 1, 0, 0, 0, 0);
            dtpFromDate.Name = "dtpFromDate";
            dtpFromDate.ShadowDecoration.CustomizableEdges = customizableEdges14;
            dtpFromDate.Size = new Size(150, 30);
            dtpFromDate.TabIndex = 4;
            dtpFromDate.Value = new DateTime(2025, 11, 20, 3, 48, 24, 342);
            // 
            // btnFilter
            // 
            btnFilter.BackColor = Color.FromArgb(52, 152, 219);
            btnFilter.BorderRadius = 5;
            btnFilter.CustomizableEdges = customizableEdges15;
            btnFilter.DisabledState.BorderColor = Color.DarkGray;
            btnFilter.DisabledState.CustomBorderColor = Color.DarkGray;
            btnFilter.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnFilter.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnFilter.FillColor = Color.Empty;
            btnFilter.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnFilter.ForeColor = Color.White;
            btnFilter.Location = new Point(560, 38);
            btnFilter.Margin = new Padding(0);
            btnFilter.Name = "btnFilter";
            btnFilter.ShadowDecoration.CustomizableEdges = customizableEdges16;
            btnFilter.Size = new Size(120, 35);
            btnFilter.TabIndex = 5;
            btnFilter.Text = "🔍 Lọc";
            // 
            // guna2DateTimePicker1
            // 
            guna2DateTimePicker1.BorderRadius = 5;
            guna2DateTimePicker1.Checked = true;
            guna2DateTimePicker1.CustomizableEdges = customizableEdges17;
            guna2DateTimePicker1.FillColor = Color.White;
            guna2DateTimePicker1.Font = new Font("Segoe UI", 9F);
            guna2DateTimePicker1.Format = DateTimePickerFormat.Short;
            guna2DateTimePicker1.Location = new Point(390, 40);
            guna2DateTimePicker1.Margin = new Padding(0);
            guna2DateTimePicker1.MaxDate = new DateTime(9998, 12, 31, 0, 0, 0, 0);
            guna2DateTimePicker1.MinDate = new DateTime(1753, 1, 1, 0, 0, 0, 0);
            guna2DateTimePicker1.Name = "guna2DateTimePicker1";
            guna2DateTimePicker1.ShadowDecoration.CustomizableEdges = customizableEdges18;
            guna2DateTimePicker1.Size = new Size(150, 30);
            guna2DateTimePicker1.TabIndex = 6;
            guna2DateTimePicker1.Value = new DateTime(2025, 11, 20, 3, 48, 24, 342);
            // 
            // btnExport
            // 
            btnExport.BackColor = Color.FromArgb(40, 167, 69);
            btnExport.BorderRadius = 5;
            btnExport.CustomizableEdges = customizableEdges19;
            btnExport.DisabledState.BorderColor = Color.DarkGray;
            btnExport.DisabledState.CustomBorderColor = Color.DarkGray;
            btnExport.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnExport.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnExport.FillColor = Color.Empty;
            btnExport.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnExport.ForeColor = Color.White;
            btnExport.Location = new Point(750, 38);
            btnExport.Margin = new Padding(0);
            btnExport.Name = "btnExport";
            btnExport.ShadowDecoration.CustomizableEdges = customizableEdges20;
            btnExport.Size = new Size(140, 35);
            btnExport.TabIndex = 7;
            btnExport.Text = "📥 Xuất Excel";
            // 
            // pnlChart1
            // 
            pnlChart1.BorderStyle = BorderStyle.FixedSingle;
            pnlChart1.Controls.Add(pnlChart1Content);
            pnlChart1.Controls.Add(label4);
            pnlChart1.Location = new Point(15, 15);
            pnlChart1.Margin = new Padding(0);
            pnlChart1.Name = "pnlChart1";
            pnlChart1.Size = new Size(490, 200);
            pnlChart1.TabIndex = 0;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(15, 15);
            label4.Margin = new Padding(0);
            label4.Name = "label4";
            label4.Size = new Size(164, 20);
            label4.TabIndex = 0;
            label4.Text = "📊 Biểu đồ doanh thu";
            // 
            // pnlChart1Content
            // 
            pnlChart1Content.Location = new Point(15, 45);
            pnlChart1Content.Margin = new Padding(0);
            pnlChart1Content.Name = "pnlChart1Content";
            pnlChart1Content.Size = new Size(460, 140);
            pnlChart1Content.TabIndex = 1;
            // 
            // pnlChart2
            // 
            pnlChart2.BorderStyle = BorderStyle.FixedSingle;
            pnlChart2.Controls.Add(lvTopRoutes);
            pnlChart2.Controls.Add(label5);
            pnlChart2.Location = new Point(520, 15);
            pnlChart2.Margin = new Padding(0);
            pnlChart2.Name = "pnlChart2";
            pnlChart2.Size = new Size(490, 200);
            pnlChart2.TabIndex = 2;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(15, 15);
            label5.Margin = new Padding(0);
            label5.Name = "label5";
            label5.Size = new Size(134, 20);
            label5.TabIndex = 0;
            label5.Text = "📈 Top tuyến bay";
            // 
            // lvTopRoutes
            // 
            lvTopRoutes.FullRowSelect = true;
            lvTopRoutes.GridLines = true;
            lvTopRoutes.Items.AddRange(new ListViewItem[] { listViewItem9, listViewItem10, listViewItem11, listViewItem12 });
            lvTopRoutes.Location = new Point(15, 45);
            lvTopRoutes.Margin = new Padding(0);
            lvTopRoutes.Name = "lvTopRoutes";
            lvTopRoutes.Size = new Size(460, 140);
            lvTopRoutes.TabIndex = 1;
            lvTopRoutes.UseCompatibleStateImageBehavior = false;
            lvTopRoutes.View = View.Details;
            // 
            // pnlChart3
            // 
            pnlChart3.BorderStyle = BorderStyle.FixedSingle;
            pnlChart3.Controls.Add(listView1);
            pnlChart3.Controls.Add(label6);
            pnlChart3.Location = new Point(15, 230);
            pnlChart3.Margin = new Padding(0);
            pnlChart3.Name = "pnlChart3";
            pnlChart3.Size = new Size(490, 200);
            pnlChart3.TabIndex = 2;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(15, 15);
            label6.Margin = new Padding(0);
            label6.Name = "label6";
            label6.Size = new Size(137, 20);
            label6.TabIndex = 0;
            label6.Text = "⭐ Top khách hàng";
            // 
            // listView1
            // 
            listView1.FullRowSelect = true;
            listView1.GridLines = true;
            listView1.Items.AddRange(new ListViewItem[] { listViewItem13, listViewItem14, listViewItem15, listViewItem16 });
            listView1.Location = new Point(15, 45);
            listView1.Margin = new Padding(0);
            listView1.Name = "listView1";
            listView1.Size = new Size(460, 140);
            listView1.TabIndex = 2;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            // 
            // pnlChart4
            // 
            pnlChart4.BorderStyle = BorderStyle.FixedSingle;
            pnlChart4.Controls.Add(pnlSummary);
            pnlChart4.Controls.Add(label7);
            pnlChart4.Location = new Point(520, 230);
            pnlChart4.Margin = new Padding(0);
            pnlChart4.Name = "pnlChart4";
            pnlChart4.Size = new Size(490, 200);
            pnlChart4.TabIndex = 2;
            // 
            // pnlSummary
            // 
            pnlSummary.Location = new Point(15, 45);
            pnlSummary.Margin = new Padding(0);
            pnlSummary.Name = "pnlSummary";
            pnlSummary.Size = new Size(460, 140);
            pnlSummary.TabIndex = 1;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.Location = new Point(15, 15);
            label7.Margin = new Padding(0);
            label7.Name = "label7";
            label7.Size = new Size(168, 20);
            label7.TabIndex = 0;
            label7.Text = "🎯 Thống kê tổng hợp";
            // 
            // ReportStatisticsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(pnlCharts);
            Controls.Add(flpStats);
            Controls.Add(pnlFilters);
            Margin = new Padding(0);
            Name = "ReportStatisticsControl";
            Size = new Size(1040, 650);
            pnlFilters.ResumeLayout(false);
            pnlFilters.PerformLayout();
            pnlCharts.ResumeLayout(false);
            pnlChart1.ResumeLayout(false);
            pnlChart1.PerformLayout();
            pnlChart2.ResumeLayout(false);
            pnlChart2.PerformLayout();
            pnlChart3.ResumeLayout(false);
            pnlChart3.PerformLayout();
            pnlChart4.ResumeLayout(false);
            pnlChart4.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlFilters;
        private FlowLayoutPanel flpStats;
        private Panel pnlCharts;
        private Label label3;
        private Label label2;
        private Label label1;
        private Guna.UI2.WinForms.Guna2Button btnFilter;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpFromDate;
        private Guna.UI2.WinForms.Guna2ComboBox cboReportType;
        private Guna.UI2.WinForms.Guna2DateTimePicker guna2DateTimePicker1;
        private Guna.UI2.WinForms.Guna2Button btnExport;
        private Panel pnlChart1;
        private Label label4;
        private Panel pnlChart1Content;
        private Panel pnlChart2;
        private Label label5;
        private ListView lvTopRoutes;
        private Panel pnlChart3;
        private Label label6;
        private ListView listView1;
        private Panel pnlChart4;
        private Panel pnlSummary;
        private Label label7;
    }
}
