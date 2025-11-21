namespace AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.User
{
    partial class SummaryBookingControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private Panel pnlRoot;
        private Label lblHeaderFlight;
        private Panel pnlFlightBox;
        private Label lblRoute;
        private Label lblAirline;
        private Label lblTime;
        private Label lblDates;
        private Label lblSummaryHeader;
        private Panel pnlSummaryBox;
        private FlowLayoutPanel flowPriceList;
        private Label lblTotalTitle;
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlRoot = new Panel();
            label1 = new Label();
            pricingVAT = new Label();
            pnlFlightBox = new Panel();
            lblHeaderFlight = new Label();
            lblRoute = new Label();
            lblTime = new Label();
            lblAirline = new Label();
            lblDates = new Label();
            lblSummaryHeader = new Label();
            pnlSummaryBox = new Panel();
            flowPriceList = new FlowLayoutPanel();
            lblTotalTitle = new Label();
            lblTotalPrice = new Label();
            pnlRoot.SuspendLayout();
            pnlFlightBox.SuspendLayout();
            pnlSummaryBox.SuspendLayout();
            SuspendLayout();
            // 
            // pnlRoot
            // 
            pnlRoot.BackColor = Color.White;
            pnlRoot.Controls.Add(label1);
            pnlRoot.Controls.Add(pricingVAT);
            pnlRoot.Controls.Add(pnlFlightBox);
            pnlRoot.Controls.Add(lblSummaryHeader);
            pnlRoot.Controls.Add(pnlSummaryBox);
            pnlRoot.Controls.Add(lblTotalTitle);
            pnlRoot.Controls.Add(lblTotalPrice);
            pnlRoot.Dock = DockStyle.Fill;
            pnlRoot.Location = new Point(0, 0);
            pnlRoot.Name = "pnlRoot";
            pnlRoot.Padding = new Padding(10);
            pnlRoot.Size = new Size(380, 540);
            pnlRoot.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label1.Location = new Point(13, 362);
            label1.Name = "label1";
            label1.Size = new Size(119, 21);
            label1.TabIndex = 6;
            label1.Text = "Giá trước thuê";
            // 
            // pricingVAT
            // 
            pricingVAT.AutoSize = true;
            pricingVAT.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold | FontStyle.Strikeout, GraphicsUnit.Point, 0);
            pricingVAT.ForeColor = SystemColors.ButtonShadow;
            pricingVAT.Location = new Point(138, 359);
            pricingVAT.Name = "pricingVAT";
            pricingVAT.Size = new Size(78, 25);
            pricingVAT.TabIndex = 7;
            pricingVAT.Text = "000000";
            // 
            // pnlFlightBox
            // 
            pnlFlightBox.BackColor = Color.WhiteSmoke;
            pnlFlightBox.BorderStyle = BorderStyle.FixedSingle;
            pnlFlightBox.Controls.Add(lblHeaderFlight);
            pnlFlightBox.Controls.Add(lblRoute);
            pnlFlightBox.Controls.Add(lblTime);
            pnlFlightBox.Controls.Add(lblAirline);
            pnlFlightBox.Controls.Add(lblDates);
            pnlFlightBox.Location = new Point(0, 0);
            pnlFlightBox.Name = "pnlFlightBox";
            pnlFlightBox.Padding = new Padding(10);
            pnlFlightBox.Size = new Size(350, 160);
            pnlFlightBox.TabIndex = 1;
            // 
            // lblHeaderFlight
            // 
            lblHeaderFlight.AutoSize = true;
            lblHeaderFlight.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblHeaderFlight.Location = new Point(87, 1);
            lblHeaderFlight.Name = "lblHeaderFlight";
            lblHeaderFlight.Size = new Size(185, 25);
            lblHeaderFlight.TabIndex = 0;
            lblHeaderFlight.Text = "Tóm tắt chuyến bay";
            // 
            // lblRoute
            // 
            lblRoute.AutoSize = true;
            lblRoute.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblRoute.Location = new Point(5, 5);
            lblRoute.Name = "lblRoute";
            lblRoute.Size = new Size(0, 21);
            lblRoute.TabIndex = 0;
            // 
            // lblTime
            // 
            lblTime.AutoSize = true;
            lblTime.Font = new Font("Segoe UI", 10F);
            lblTime.Location = new Point(5, 35);
            lblTime.Name = "lblTime";
            lblTime.Size = new Size(0, 19);
            lblTime.TabIndex = 1;
            // 
            // lblAirline
            // 
            lblAirline.AutoSize = true;
            lblAirline.Font = new Font("Segoe UI", 10F);
            lblAirline.Location = new Point(5, 65);
            lblAirline.Name = "lblAirline";
            lblAirline.Size = new Size(0, 19);
            lblAirline.TabIndex = 2;
            // 
            // lblDates
            // 
            lblDates.AutoSize = true;
            lblDates.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
            lblDates.ForeColor = Color.Gray;
            lblDates.Location = new Point(5, 95);
            lblDates.Name = "lblDates";
            lblDates.Size = new Size(0, 19);
            lblDates.TabIndex = 3;
            // 
            // lblSummaryHeader
            // 
            lblSummaryHeader.AutoSize = true;
            lblSummaryHeader.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblSummaryHeader.Location = new Point(10, 180);
            lblSummaryHeader.Name = "lblSummaryHeader";
            lblSummaryHeader.Size = new Size(79, 25);
            lblSummaryHeader.TabIndex = 2;
            lblSummaryHeader.Text = "Tóm tắt";
            // 
            // pnlSummaryBox
            // 
            pnlSummaryBox.BackColor = Color.WhiteSmoke;
            pnlSummaryBox.BorderStyle = BorderStyle.FixedSingle;
            pnlSummaryBox.Controls.Add(flowPriceList);
            pnlSummaryBox.Location = new Point(0, 0);
            pnlSummaryBox.Name = "pnlSummaryBox";
            pnlSummaryBox.Padding = new Padding(10);
            pnlSummaryBox.Size = new Size(350, 220);
            pnlSummaryBox.TabIndex = 3;
            // 
            // flowPriceList
            // 
            flowPriceList.AutoSize = true;
            flowPriceList.Dock = DockStyle.Top;
            flowPriceList.FlowDirection = FlowDirection.TopDown;
            flowPriceList.Location = new Point(10, 10);
            flowPriceList.Name = "flowPriceList";
            flowPriceList.Size = new Size(328, 0);
            flowPriceList.TabIndex = 0;
            flowPriceList.WrapContents = false;
            // 
            // lblTotalTitle
            // 
            lblTotalTitle.AutoSize = true;
            lblTotalTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTotalTitle.Location = new Point(10, 410);
            lblTotalTitle.Name = "lblTotalTitle";
            lblTotalTitle.Size = new Size(93, 21);
            lblTotalTitle.TabIndex = 4;
            lblTotalTitle.Text = "Giá bạn trả";
            // 
            // lblTotalPrice
            // 
            lblTotalPrice.AutoSize = true;
            lblTotalPrice.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTotalPrice.ForeColor = Color.OrangeRed;
            lblTotalPrice.Location = new Point(120, 408);
            lblTotalPrice.Name = "lblTotalPrice";
            lblTotalPrice.Size = new Size(78, 25);
            lblTotalPrice.TabIndex = 5;
            lblTotalPrice.Text = "000000";
            // 
            // SummaryBookingControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlRoot);
            Name = "SummaryBookingControl";
            Size = new Size(380, 540);
            pnlRoot.ResumeLayout(false);
            pnlRoot.PerformLayout();
            pnlFlightBox.ResumeLayout(false);
            pnlFlightBox.PerformLayout();
            pnlSummaryBox.ResumeLayout(false);
            pnlSummaryBox.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Label pricingVAT;
    }
}
