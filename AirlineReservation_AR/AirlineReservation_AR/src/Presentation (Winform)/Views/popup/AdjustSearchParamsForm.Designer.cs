using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace AirlineReservation_AR.src.Presentation__Winform_.Views.popup
{
    partial class AdjustSearchParamsForm
    {
        private System.ComponentModel.IContainer components = null;

        private Guna2Panel pnlMain;
        private Guna2Panel pnlHeader;
        private Label lblTitle;
        private Label lblSubtitle;
        private Guna2Button btnClose;

        private Label lblSeatClassTitle;
        private Guna2ComboBox cboSeatClass;

        private Label lblPassengerTitle;
        private Label lblAdult;
        private Label lblChild;
        private Label lblInfant;

        private TextBox txtAdult;
        private TextBox txtChild;
        private TextBox txtInfant;

        private Guna2Button btnAdultMinus;
        private Guna2Button btnAdultPlus;

        private Guna2Button btnChildMinus;
        private Guna2Button btnChildPlus;

        private Guna2Button btnInfantMinus;
        private Guna2Button btnInfantPlus;

        private Guna2Button btnCancel;
        private Guna2Button btnDone;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges23 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges24 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
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
            pnlMain = new Guna2Panel();
            pnlHeader = new Guna2Panel();
            lblTitle = new Label();
            lblSubtitle = new Label();
            btnClose = new Guna2Button();
            lblSeatClassTitle = new Label();
            cboSeatClass = new Guna2ComboBox();
            lblPassengerTitle = new Label();
            lblAdult = new Label();
            txtAdult = new TextBox();
            btnAdultMinus = new Guna2Button();
            btnAdultPlus = new Guna2Button();
            lblChild = new Label();
            txtChild = new TextBox();
            btnChildMinus = new Guna2Button();
            btnChildPlus = new Guna2Button();
            lblInfant = new Label();
            txtInfant = new TextBox();
            btnInfantMinus = new Guna2Button();
            btnInfantPlus = new Guna2Button();
            btnCancel = new Guna2Button();
            btnDone = new Guna2Button();
            pnlMain.SuspendLayout();
            pnlHeader.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMain
            // 
            pnlMain.BackColor = Color.Transparent;
            pnlMain.BorderRadius = 16;
            pnlMain.Controls.Add(pnlHeader);
            pnlMain.Controls.Add(lblSeatClassTitle);
            pnlMain.Controls.Add(cboSeatClass);
            pnlMain.Controls.Add(lblPassengerTitle);
            pnlMain.Controls.Add(lblAdult);
            pnlMain.Controls.Add(txtAdult);
            pnlMain.Controls.Add(btnAdultMinus);
            pnlMain.Controls.Add(btnAdultPlus);
            pnlMain.Controls.Add(lblChild);
            pnlMain.Controls.Add(txtChild);
            pnlMain.Controls.Add(btnChildMinus);
            pnlMain.Controls.Add(btnChildPlus);
            pnlMain.Controls.Add(lblInfant);
            pnlMain.Controls.Add(txtInfant);
            pnlMain.Controls.Add(btnInfantMinus);
            pnlMain.Controls.Add(btnInfantPlus);
            pnlMain.Controls.Add(btnCancel);
            pnlMain.Controls.Add(btnDone);
            pnlMain.CustomizableEdges = customizableEdges23;
            pnlMain.FillColor = Color.White;
            pnlMain.Location = new Point(10, 10);
            pnlMain.Name = "pnlMain";
            pnlMain.ShadowDecoration.Color = Color.FromArgb(50, 0, 0, 0);
            pnlMain.ShadowDecoration.CustomizableEdges = customizableEdges24;
            pnlMain.ShadowDecoration.Depth = 15;
            pnlMain.ShadowDecoration.Enabled = true;
            pnlMain.Size = new Size(500, 520);
            pnlMain.TabIndex = 0;
            // 
            // pnlHeader
            // 
            pnlHeader.BorderRadius = 16;
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(lblSubtitle);
            pnlHeader.Controls.Add(btnClose);
            pnlHeader.CustomBorderThickness = new Padding(0, 0, 0, 16);
            pnlHeader.CustomizableEdges = customizableEdges3;
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.FillColor = Color.FromArgb(255, 87, 34);
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.ShadowDecoration.CustomizableEdges = customizableEdges4;
            pnlHeader.Size = new Size(500, 100);
            pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(30, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(380, 30);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Sorry, dear customer!";
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblSubtitle
            // 
            lblSubtitle.Font = new Font("Segoe UI", 10F);
            lblSubtitle.ForeColor = Color.FromArgb(255, 255, 220);
            lblSubtitle.Location = new Point(30, 55);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(420, 30);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Please adjust the seat class or number of passengers.";
            lblSubtitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnClose
            // 
            btnClose.BorderRadius = 17;
            btnClose.Cursor = Cursors.Hand;
            btnClose.CustomizableEdges = customizableEdges1;
            btnClose.FillColor = Color.Transparent;
            btnClose.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnClose.ForeColor = Color.White;
            btnClose.HoverState.FillColor = Color.FromArgb(40, 255, 87, 34);
            btnClose.Location = new Point(450, 15);
            btnClose.Name = "btnClose";
            btnClose.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnClose.Size = new Size(35, 35);
            btnClose.TabIndex = 2;
            btnClose.Text = "✕";
            // 
            // lblSeatClassTitle
            // 
            lblSeatClassTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblSeatClassTitle.ForeColor = Color.FromArgb(64, 64, 64);
            lblSeatClassTitle.Location = new Point(30, 120);
            lblSeatClassTitle.Name = "lblSeatClassTitle";
            lblSeatClassTitle.Size = new Size(200, 25);
            lblSeatClassTitle.TabIndex = 1;
            lblSeatClassTitle.Text = "Seat Class";
            // 
            // cboSeatClass
            // 
            cboSeatClass.BackColor = Color.Transparent;
            cboSeatClass.BorderColor = Color.FromArgb(213, 218, 223);
            cboSeatClass.BorderRadius = 8;
            cboSeatClass.CustomizableEdges = customizableEdges5;
            cboSeatClass.DrawMode = DrawMode.OwnerDrawFixed;
            cboSeatClass.DropDownStyle = ComboBoxStyle.DropDownList;
            cboSeatClass.FocusedColor = Color.Empty;
            cboSeatClass.Font = new Font("Segoe UI", 11F);
            cboSeatClass.ForeColor = Color.FromArgb(64, 64, 64);
            cboSeatClass.ItemHeight = 30;
            cboSeatClass.Location = new Point(30, 150);
            cboSeatClass.Name = "cboSeatClass";
            cboSeatClass.ShadowDecoration.CustomizableEdges = customizableEdges6;
            cboSeatClass.Size = new Size(440, 36);
            cboSeatClass.TabIndex = 2;
            // 
            // lblPassengerTitle
            // 
            lblPassengerTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblPassengerTitle.ForeColor = Color.FromArgb(64, 64, 64);
            lblPassengerTitle.Location = new Point(30, 210);
            lblPassengerTitle.Name = "lblPassengerTitle";
            lblPassengerTitle.Size = new Size(250, 25);
            lblPassengerTitle.TabIndex = 3;
            lblPassengerTitle.Text = "Number of passengers";
            // 
            // lblAdult
            // 
            lblAdult.Font = new Font("Segoe UI", 10F);
            lblAdult.ForeColor = Color.FromArgb(64, 64, 64);
            lblAdult.Location = new Point(30, 250);
            lblAdult.Name = "lblAdult";
            lblAdult.Size = new Size(200, 25);
            lblAdult.TabIndex = 4;
            lblAdult.Text = "Adult (≥12 ages)";
            // 
            // txtAdult
            // 
            txtAdult.BackColor = Color.White;
            txtAdult.BorderStyle = BorderStyle.FixedSingle;
            txtAdult.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            txtAdult.Location = new Point(375, 245);
            txtAdult.Name = "txtAdult";
            txtAdult.ReadOnly = true;
            txtAdult.Size = new Size(50, 29);
            txtAdult.TabIndex = 5;
            txtAdult.TextAlign = HorizontalAlignment.Center;
            // 
            // btnAdultMinus
            // 
            btnAdultMinus.BorderRadius = 8;
            btnAdultMinus.Cursor = Cursors.Hand;
            btnAdultMinus.CustomizableEdges = customizableEdges7;
            btnAdultMinus.FillColor = Color.FromArgb(240, 240, 240);
            btnAdultMinus.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnAdultMinus.ForeColor = Color.FromArgb(100, 100, 100);
            btnAdultMinus.HoverState.FillColor = Color.FromArgb(220, 220, 220);
            btnAdultMinus.Location = new Point(320, 245);
            btnAdultMinus.Name = "btnAdultMinus";
            btnAdultMinus.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnAdultMinus.Size = new Size(45, 36);
            btnAdultMinus.TabIndex = 6;
            btnAdultMinus.Text = "−";
            // 
            // btnAdultPlus
            // 
            btnAdultPlus.BorderRadius = 8;
            btnAdultPlus.Cursor = Cursors.Hand;
            btnAdultPlus.CustomizableEdges = customizableEdges9;
            btnAdultPlus.FillColor = Color.FromArgb(0, 147, 255);
            btnAdultPlus.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnAdultPlus.ForeColor = Color.White;
            btnAdultPlus.HoverState.FillColor = Color.FromArgb(0, 120, 215);
            btnAdultPlus.Location = new Point(435, 245);
            btnAdultPlus.Name = "btnAdultPlus";
            btnAdultPlus.ShadowDecoration.CustomizableEdges = customizableEdges10;
            btnAdultPlus.Size = new Size(45, 36);
            btnAdultPlus.TabIndex = 7;
            btnAdultPlus.Text = "+";
            // 
            // lblChild
            // 
            lblChild.Font = new Font("Segoe UI", 10F);
            lblChild.ForeColor = Color.FromArgb(64, 64, 64);
            lblChild.Location = new Point(30, 300);
            lblChild.Name = "lblChild";
            lblChild.Size = new Size(200, 25);
            lblChild.TabIndex = 8;
            lblChild.Text = "Child (2-11 ages)";
            // 
            // txtChild
            // 
            txtChild.BackColor = Color.White;
            txtChild.BorderStyle = BorderStyle.FixedSingle;
            txtChild.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            txtChild.Location = new Point(375, 295);
            txtChild.Name = "txtChild";
            txtChild.ReadOnly = true;
            txtChild.Size = new Size(50, 29);
            txtChild.TabIndex = 9;
            txtChild.TextAlign = HorizontalAlignment.Center;
            // 
            // btnChildMinus
            // 
            btnChildMinus.BorderRadius = 8;
            btnChildMinus.Cursor = Cursors.Hand;
            btnChildMinus.CustomizableEdges = customizableEdges11;
            btnChildMinus.FillColor = Color.FromArgb(240, 240, 240);
            btnChildMinus.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnChildMinus.ForeColor = Color.FromArgb(100, 100, 100);
            btnChildMinus.HoverState.FillColor = Color.FromArgb(220, 220, 220);
            btnChildMinus.Location = new Point(320, 295);
            btnChildMinus.Name = "btnChildMinus";
            btnChildMinus.ShadowDecoration.CustomizableEdges = customizableEdges12;
            btnChildMinus.Size = new Size(45, 36);
            btnChildMinus.TabIndex = 10;
            btnChildMinus.Text = "−";
            // 
            // btnChildPlus
            // 
            btnChildPlus.BorderRadius = 8;
            btnChildPlus.Cursor = Cursors.Hand;
            btnChildPlus.CustomizableEdges = customizableEdges13;
            btnChildPlus.FillColor = Color.FromArgb(0, 147, 255);
            btnChildPlus.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnChildPlus.ForeColor = Color.White;
            btnChildPlus.HoverState.FillColor = Color.FromArgb(0, 120, 215);
            btnChildPlus.Location = new Point(435, 295);
            btnChildPlus.Name = "btnChildPlus";
            btnChildPlus.ShadowDecoration.CustomizableEdges = customizableEdges14;
            btnChildPlus.Size = new Size(45, 36);
            btnChildPlus.TabIndex = 11;
            btnChildPlus.Text = "+";
            // 
            // lblInfant
            // 
            lblInfant.Font = new Font("Segoe UI", 10F);
            lblInfant.ForeColor = Color.FromArgb(64, 64, 64);
            lblInfant.Location = new Point(30, 350);
            lblInfant.Name = "lblInfant";
            lblInfant.Size = new Size(200, 25);
            lblInfant.TabIndex = 12;
            lblInfant.Text = "Infant (<2 ages)";
            // 
            // txtInfant
            // 
            txtInfant.BackColor = Color.White;
            txtInfant.BorderStyle = BorderStyle.FixedSingle;
            txtInfant.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            txtInfant.Location = new Point(375, 345);
            txtInfant.Name = "txtInfant";
            txtInfant.ReadOnly = true;
            txtInfant.Size = new Size(50, 29);
            txtInfant.TabIndex = 13;
            txtInfant.TextAlign = HorizontalAlignment.Center;
            // 
            // btnInfantMinus
            // 
            btnInfantMinus.BorderRadius = 8;
            btnInfantMinus.Cursor = Cursors.Hand;
            btnInfantMinus.CustomizableEdges = customizableEdges15;
            btnInfantMinus.FillColor = Color.FromArgb(240, 240, 240);
            btnInfantMinus.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnInfantMinus.ForeColor = Color.FromArgb(100, 100, 100);
            btnInfantMinus.HoverState.FillColor = Color.FromArgb(220, 220, 220);
            btnInfantMinus.Location = new Point(320, 345);
            btnInfantMinus.Name = "btnInfantMinus";
            btnInfantMinus.ShadowDecoration.CustomizableEdges = customizableEdges16;
            btnInfantMinus.Size = new Size(45, 36);
            btnInfantMinus.TabIndex = 14;
            btnInfantMinus.Text = "−";
            // 
            // btnInfantPlus
            // 
            btnInfantPlus.BorderRadius = 8;
            btnInfantPlus.Cursor = Cursors.Hand;
            btnInfantPlus.CustomizableEdges = customizableEdges17;
            btnInfantPlus.FillColor = Color.FromArgb(0, 147, 255);
            btnInfantPlus.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnInfantPlus.ForeColor = Color.White;
            btnInfantPlus.HoverState.FillColor = Color.FromArgb(0, 120, 215);
            btnInfantPlus.Location = new Point(435, 345);
            btnInfantPlus.Name = "btnInfantPlus";
            btnInfantPlus.ShadowDecoration.CustomizableEdges = customizableEdges18;
            btnInfantPlus.Size = new Size(45, 36);
            btnInfantPlus.TabIndex = 15;
            btnInfantPlus.Text = "+";
            // 
            // btnCancel
            // 
            btnCancel.BorderRadius = 10;
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.CustomizableEdges = customizableEdges19;
            btnCancel.FillColor = Color.FromArgb(245, 245, 245);
            btnCancel.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnCancel.ForeColor = Color.FromArgb(100, 100, 100);
            btnCancel.HoverState.FillColor = Color.FromArgb(230, 230, 230);
            btnCancel.Location = new Point(30, 430);
            btnCancel.Name = "btnCancel";
            btnCancel.ShadowDecoration.CustomizableEdges = customizableEdges20;
            btnCancel.Size = new Size(210, 50);
            btnCancel.TabIndex = 16;
            btnCancel.Text = "Cancel";
            // 
            // btnDone
            // 
            btnDone.BorderRadius = 10;
            btnDone.Cursor = Cursors.Hand;
            btnDone.CustomizableEdges = customizableEdges21;
            btnDone.FillColor = Color.FromArgb(76, 175, 80);
            btnDone.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnDone.ForeColor = Color.White;
            btnDone.HoverState.FillColor = Color.FromArgb(56, 142, 60);
            btnDone.Location = new Point(260, 430);
            btnDone.Name = "btnDone";
            btnDone.ShadowDecoration.CustomizableEdges = customizableEdges22;
            btnDone.Size = new Size(210, 50);
            btnDone.TabIndex = 17;
            btnDone.Text = "Save Changes";
            // 
            // AdjustSearchParamsForm
            // 
            BackColor = Color.White;
            ClientSize = new Size(520, 540);
            Controls.Add(pnlMain);
            FormBorderStyle = FormBorderStyle.None;
            Name = "AdjustSearchParamsForm";
            StartPosition = FormStartPosition.CenterParent;
            pnlMain.ResumeLayout(false);
            pnlMain.PerformLayout();
            pnlHeader.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}