namespace AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.User
{
    partial class UCUserTransaction
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            flowLayoutPanel1 = new FlowLayoutPanel();
            fpnlTransactionHolder = new FlowLayoutPanel();
            guna2HtmlLabel3 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlNoTransaction = new Guna.UI2.WinForms.Guna2Panel();
            guna2HtmlLabel8 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2HtmlLabel7 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            flowLayoutPanel1.SuspendLayout();
            pnlNoTransaction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)guna2PictureBox1).BeginInit();
            SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(pnlNoTransaction);
            flowLayoutPanel1.Controls.Add(fpnlTransactionHolder);
            flowLayoutPanel1.Location = new Point(0, 39);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(856, 961);
            flowLayoutPanel1.TabIndex = 5;
            // 
            // fpnlTransactionHolder
            // 
            fpnlTransactionHolder.Location = new Point(3, 171);
            fpnlTransactionHolder.Name = "fpnlTransactionHolder";
            fpnlTransactionHolder.Size = new Size(853, 827);
            fpnlTransactionHolder.TabIndex = 1;
            // 
            // guna2HtmlLabel3
            // 
            guna2HtmlLabel3.BackColor = Color.Transparent;
            guna2HtmlLabel3.Dock = DockStyle.Top;
            guna2HtmlLabel3.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            guna2HtmlLabel3.Location = new Point(0, 0);
            guna2HtmlLabel3.Name = "guna2HtmlLabel3";
            guna2HtmlLabel3.Size = new Size(856, 33);
            guna2HtmlLabel3.TabIndex = 6;
            guna2HtmlLabel3.Text = "Transaction History";
            // 
            // pnlNoTransaction
            // 
            pnlNoTransaction.BackColor = Color.White;
            pnlNoTransaction.Controls.Add(guna2HtmlLabel8);
            pnlNoTransaction.Controls.Add(guna2HtmlLabel7);
            pnlNoTransaction.Controls.Add(guna2PictureBox1);
            pnlNoTransaction.CustomizableEdges = customizableEdges3;
            pnlNoTransaction.Dock = DockStyle.Top;
            pnlNoTransaction.Location = new Point(3, 3);
            pnlNoTransaction.Name = "pnlNoTransaction";
            pnlNoTransaction.ShadowDecoration.CustomizableEdges = customizableEdges4;
            pnlNoTransaction.Size = new Size(853, 162);
            pnlNoTransaction.TabIndex = 12;
            // 
            // guna2HtmlLabel8
            // 
            guna2HtmlLabel8.BackColor = Color.Transparent;
            guna2HtmlLabel8.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            guna2HtmlLabel8.ForeColor = SystemColors.ControlDark;
            guna2HtmlLabel8.Location = new Point(167, 65);
            guna2HtmlLabel8.Name = "guna2HtmlLabel8";
            guna2HtmlLabel8.Size = new Size(604, 27);
            guna2HtmlLabel8.TabIndex = 3;
            guna2HtmlLabel8.Text = "You have made no transaction yet. Let's get one on the Home page now";
            // 
            // guna2HtmlLabel7
            // 
            guna2HtmlLabel7.BackColor = Color.Transparent;
            guna2HtmlLabel7.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            guna2HtmlLabel7.Location = new Point(167, 32);
            guna2HtmlLabel7.Name = "guna2HtmlLabel7";
            guna2HtmlLabel7.Size = new Size(185, 27);
            guna2HtmlLabel7.TabIndex = 2;
            guna2HtmlLabel7.Text = "No transaction found";
            // 
            // guna2PictureBox1
            // 
            guna2PictureBox1.CustomizableEdges = customizableEdges1;
            guna2PictureBox1.Image = Properties.Resources.no_ticket;
            guna2PictureBox1.ImageRotate = 0F;
            guna2PictureBox1.Location = new Point(22, 19);
            guna2PictureBox1.Name = "guna2PictureBox1";
            guna2PictureBox1.ShadowDecoration.CustomizableEdges = customizableEdges2;
            guna2PictureBox1.Size = new Size(129, 118);
            guna2PictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            guna2PictureBox1.TabIndex = 1;
            guna2PictureBox1.TabStop = false;
            // 
            // UCUserTransaction
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(guna2HtmlLabel3);
            Controls.Add(flowLayoutPanel1);
            Name = "UCUserTransaction";
            Size = new Size(856, 1000);
            flowLayoutPanel1.ResumeLayout(false);
            pnlNoTransaction.ResumeLayout(false);
            pnlNoTransaction.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)guna2PictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FlowLayoutPanel flowLayoutPanel1;
        private FlowLayoutPanel fpnlTransactionHolder;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel3;
        private Guna.UI2.WinForms.Guna2Panel pnlNoTransaction;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel8;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel7;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
    }
}
