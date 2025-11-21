using AirlineReservation_AR.src.Presentation__Winform_.Helpers;

namespace AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.User
{
    partial class UC_FlightSearchResult
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private FlowLayoutPanel flowDayTabs;
        private FlowLayoutPanel flowResults;
        private FlowLayoutPanel flowFilters;
        private Panel pnlLeftFilters;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_FlightSearchResult));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            flowDayTabs = new FlowLayoutPanel();
            flowResults = new FlowLayoutPanel();
            pnlFlowResuilt = new Guna.UI2.WinForms.Guna2Panel();
            flowFlightCards = new BetterFlowLayoutPanel();
            flowFilters = new FlowLayoutPanel();
            pnlLeftFilters = new Panel();
            lblFilter = new Label();
            sqlCommandBuilder1 = new Microsoft.Data.SqlClient.SqlCommandBuilder();
            flowResults.SuspendLayout();
            pnlFlowResuilt.SuspendLayout();
            pnlLeftFilters.SuspendLayout();
            SuspendLayout();
            // 
            // flowDayTabs
            // 
            flowDayTabs.Dock = DockStyle.Top;
            flowDayTabs.Location = new Point(249, 0);
            flowDayTabs.Name = "flowDayTabs";
            flowDayTabs.Padding = new Padding(10);
            flowDayTabs.Size = new Size(1191, 90);
            flowDayTabs.TabIndex = 2;
            // 
            // flowResults
            // 
            flowResults.AutoScroll = true;
            flowResults.BackgroundImage = (Image)resources.GetObject("flowResults.BackgroundImage");
            flowResults.BackgroundImageLayout = ImageLayout.Center;
            flowResults.Controls.Add(pnlFlowResuilt);
            flowResults.Dock = DockStyle.Fill;
            flowResults.Location = new Point(249, 90);
            flowResults.Name = "flowResults";
            flowResults.Padding = new Padding(20, 10, 20, 10);
            flowResults.Size = new Size(1191, 750);
            flowResults.TabIndex = 0;
            // 
            // pnlFlowResuilt
            // 
            pnlFlowResuilt.BackColor = Color.Transparent;
            pnlFlowResuilt.BorderRadius = 16;
            pnlFlowResuilt.Controls.Add(flowFlightCards);
            pnlFlowResuilt.CustomizableEdges = customizableEdges1;
            pnlFlowResuilt.Location = new Point(23, 13);
            pnlFlowResuilt.Name = "pnlFlowResuilt";
            pnlFlowResuilt.ShadowDecoration.BorderRadius = 16;
            pnlFlowResuilt.ShadowDecoration.Color = Color.FromArgb(0, 0, 0);
            pnlFlowResuilt.ShadowDecoration.CustomizableEdges = customizableEdges2;
            pnlFlowResuilt.ShadowDecoration.Depth = 20;
            pnlFlowResuilt.ShadowDecoration.Enabled = true;
            pnlFlowResuilt.ShadowDecoration.Shadow = new Padding(0, 0, 8, 8);
            pnlFlowResuilt.Size = new Size(1129, 708);
            pnlFlowResuilt.TabIndex = 1;
            // 
            // flowFlightCards
            // 
            flowFlightCards.AutoScroll = true;
            flowFlightCards.BackColor = Color.Transparent;
            flowFlightCards.Dock = DockStyle.Fill;
            flowFlightCards.FlowDirection = FlowDirection.TopDown;
            flowFlightCards.Location = new Point(0, 0);
            flowFlightCards.Name = "flowFlightCards";
            flowFlightCards.Padding = new Padding(20);
            flowFlightCards.Size = new Size(1129, 708);
            flowFlightCards.TabIndex = 0;
            flowFlightCards.WrapContents = false;
            // 
            // flowFilters
            // 
            flowFilters.AutoScroll = true;
            flowFilters.Location = new Point(10, 10);
            flowFilters.Name = "flowFilters";
            flowFilters.Size = new Size(233, 820);
            flowFilters.TabIndex = 0;
            // 
            // pnlLeftFilters
            // 
            pnlLeftFilters.BackColor = Color.White;
            pnlLeftFilters.Controls.Add(flowFilters);
            pnlLeftFilters.Controls.Add(lblFilter);
            pnlLeftFilters.Dock = DockStyle.Left;
            pnlLeftFilters.Location = new Point(0, 0);
            pnlLeftFilters.Name = "pnlLeftFilters";
            pnlLeftFilters.Padding = new Padding(10);
            pnlLeftFilters.Size = new Size(249, 840);
            pnlLeftFilters.TabIndex = 3;
            // 
            // lblFilter
            // 
            lblFilter.Location = new Point(0, 0);
            lblFilter.Name = "lblFilter";
            lblFilter.Size = new Size(100, 23);
            lblFilter.TabIndex = 1;
            // 
            // UC_FlightSearchResult
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 250);
            Controls.Add(flowResults);
            Controls.Add(flowDayTabs);
            Controls.Add(pnlLeftFilters);
            Name = "UC_FlightSearchResult";
            Size = new Size(1440, 840);
            Load += UC_FlightSearchResult_Load;
            flowResults.ResumeLayout(false);
            pnlFlowResuilt.ResumeLayout(false);
            pnlLeftFilters.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Label lblFilter;
        private Guna.UI2.WinForms.Guna2Panel pnlFlowResuilt;
        private BetterFlowLayoutPanel flowFlightCards;
        private Microsoft.Data.SqlClient.SqlCommandBuilder sqlCommandBuilder1;
    }
}
