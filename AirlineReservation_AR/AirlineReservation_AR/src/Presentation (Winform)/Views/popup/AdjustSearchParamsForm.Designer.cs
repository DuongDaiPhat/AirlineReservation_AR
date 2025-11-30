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
            this.pnlMain = new Guna2Panel();
            this.pnlHeader = new Guna2Panel();
            this.lblTitle = new Label();
            this.lblSubtitle = new Label();
            this.btnClose = new Guna2Button();

            this.lblSeatClassTitle = new Label();
            this.cboSeatClass = new Guna2ComboBox();

            this.lblPassengerTitle = new Label();

            // LABEL ADULT
            this.lblAdult = new Label();
            this.txtAdult = new TextBox();
            this.btnAdultMinus = new Guna2Button();
            this.btnAdultPlus = new Guna2Button();

            // LABEL CHILD
            this.lblChild = new Label();
            this.txtChild = new TextBox();
            this.btnChildMinus = new Guna2Button();
            this.btnChildPlus = new Guna2Button();

            // LABEL INFANT
            this.lblInfant = new Label();
            this.txtInfant = new TextBox();
            this.btnInfantMinus = new Guna2Button();
            this.btnInfantPlus = new Guna2Button();

            // BUTTONS
            this.btnCancel = new Guna2Button();
            this.btnDone = new Guna2Button();

            this.SuspendLayout();

            // ================= MAIN PANEL =================
            this.pnlMain.BorderRadius = 16;
            this.pnlMain.FillColor = System.Drawing.Color.White;
            this.pnlMain.ShadowDecoration.Enabled = true;
            this.pnlMain.ShadowDecoration.Depth = 15;
            this.pnlMain.ShadowDecoration.Color = System.Drawing.Color.FromArgb(50, 0, 0, 0);
            this.pnlMain.Size = new System.Drawing.Size(500, 520);
            this.pnlMain.Location = new System.Drawing.Point(10, 10);

            // ================= HEADER PANEL =================
            this.pnlHeader.FillColor = System.Drawing.Color.FromArgb(255, 87, 34);
            this.pnlHeader.BorderRadius = 16;
            this.pnlHeader.CustomBorderThickness = new Padding(0, 0, 0, 16);
            this.pnlHeader.Size = new System.Drawing.Size(500, 100);
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Dock = DockStyle.Top;

            // ================= TITLE =================
            this.lblTitle.Text = "Xin lỗi quý khách!";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(30, 20);
            this.lblTitle.Size = new System.Drawing.Size(380, 30);
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ================= SUBTITLE =================
            this.lblSubtitle.Text = "Vui lòng điều chỉnh lại hạng ghế hoặc số lượng hành khách";
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255, 220);
            this.lblSubtitle.Location = new System.Drawing.Point(30, 55);
            this.lblSubtitle.Size = new System.Drawing.Size(420, 30);
            this.lblSubtitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ================= CLOSE BUTTON =================
            this.btnClose.Text = "✕";
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.FillColor = System.Drawing.Color.Transparent;
            this.btnClose.HoverState.FillColor = System.Drawing.Color.FromArgb(40, 255, 87, 34);
            this.btnClose.Location = new System.Drawing.Point(450, 15);
            this.btnClose.Size = new System.Drawing.Size(35, 35);
            this.btnClose.BorderRadius = 17;
            this.btnClose.Cursor = Cursors.Hand;

            // ================= SEAT CLASS SECTION =================
            this.lblSeatClassTitle.Text = "Hạng ghế";
            this.lblSeatClassTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblSeatClassTitle.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblSeatClassTitle.Location = new System.Drawing.Point(30, 120);
            this.lblSeatClassTitle.Size = new System.Drawing.Size(200, 25);

            this.cboSeatClass.BorderRadius = 8;
            this.cboSeatClass.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cboSeatClass.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.cboSeatClass.Location = new System.Drawing.Point(30, 150);
            this.cboSeatClass.Size = new System.Drawing.Size(440, 40);
            this.cboSeatClass.BorderColor = System.Drawing.Color.FromArgb(213, 218, 223);

            // ================= PASSENGER SECTION =================
            this.lblPassengerTitle.Text = "Số lượng hành khách";
            this.lblPassengerTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblPassengerTitle.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblPassengerTitle.Location = new System.Drawing.Point(30, 210);
            this.lblPassengerTitle.Size = new System.Drawing.Size(250, 25);

            // ================= ADULT ROW =================
            this.lblAdult.Text = "Người lớn (≥12 tuổi)";
            this.lblAdult.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblAdult.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblAdult.Location = new System.Drawing.Point(30, 250);
            this.lblAdult.Size = new System.Drawing.Size(200, 25);

            this.btnAdultMinus.Text = "−";
            this.btnAdultMinus.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnAdultMinus.BorderRadius = 8;
            this.btnAdultMinus.Location = new System.Drawing.Point(320, 245);
            this.btnAdultMinus.Size = new System.Drawing.Size(45, 36);
            this.btnAdultMinus.FillColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.btnAdultMinus.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
            this.btnAdultMinus.HoverState.FillColor = System.Drawing.Color.FromArgb(220, 220, 220);
            this.btnAdultMinus.Cursor = Cursors.Hand;

            this.txtAdult.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.txtAdult.Location = new System.Drawing.Point(375, 245);
            this.txtAdult.Size = new System.Drawing.Size(50, 36);
            this.txtAdult.TextAlign = HorizontalAlignment.Center;
            this.txtAdult.ReadOnly = true;
            this.txtAdult.BorderStyle = BorderStyle.FixedSingle;
            this.txtAdult.BackColor = System.Drawing.Color.White;

            this.btnAdultPlus.Text = "+";
            this.btnAdultPlus.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnAdultPlus.BorderRadius = 8;
            this.btnAdultPlus.Location = new System.Drawing.Point(435, 245);
            this.btnAdultPlus.Size = new System.Drawing.Size(45, 36);
            this.btnAdultPlus.FillColor = System.Drawing.Color.FromArgb(0, 147, 255);
            this.btnAdultPlus.ForeColor = System.Drawing.Color.White;
            this.btnAdultPlus.HoverState.FillColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.btnAdultPlus.Cursor = Cursors.Hand;

            // ================= CHILD ROW =================
            this.lblChild.Text = "Trẻ em (2-11 tuổi)";
            this.lblChild.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblChild.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblChild.Location = new System.Drawing.Point(30, 300);
            this.lblChild.Size = new System.Drawing.Size(200, 25);

            this.btnChildMinus.Text = "−";
            this.btnChildMinus.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnChildMinus.BorderRadius = 8;
            this.btnChildMinus.Location = new System.Drawing.Point(320, 295);
            this.btnChildMinus.Size = new System.Drawing.Size(45, 36);
            this.btnChildMinus.FillColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.btnChildMinus.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
            this.btnChildMinus.HoverState.FillColor = System.Drawing.Color.FromArgb(220, 220, 220);
            this.btnChildMinus.Cursor = Cursors.Hand;

            this.txtChild.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.txtChild.Location = new System.Drawing.Point(375, 295);
            this.txtChild.Size = new System.Drawing.Size(50, 36);
            this.txtChild.TextAlign = HorizontalAlignment.Center;
            this.txtChild.ReadOnly = true;
            this.txtChild.BorderStyle = BorderStyle.FixedSingle;
            this.txtChild.BackColor = System.Drawing.Color.White;

            this.btnChildPlus.Text = "+";
            this.btnChildPlus.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnChildPlus.BorderRadius = 8;
            this.btnChildPlus.Location = new System.Drawing.Point(435, 295);
            this.btnChildPlus.Size = new System.Drawing.Size(45, 36);
            this.btnChildPlus.FillColor = System.Drawing.Color.FromArgb(0, 147, 255);
            this.btnChildPlus.ForeColor = System.Drawing.Color.White;
            this.btnChildPlus.HoverState.FillColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.btnChildPlus.Cursor = Cursors.Hand;

            // ================= INFANT ROW =================
            this.lblInfant.Text = "Em bé (<2 tuổi)";
            this.lblInfant.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblInfant.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblInfant.Location = new System.Drawing.Point(30, 350);
            this.lblInfant.Size = new System.Drawing.Size(200, 25);

            this.btnInfantMinus.Text = "−";
            this.btnInfantMinus.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnInfantMinus.BorderRadius = 8;
            this.btnInfantMinus.Location = new System.Drawing.Point(320, 345);
            this.btnInfantMinus.Size = new System.Drawing.Size(45, 36);
            this.btnInfantMinus.FillColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.btnInfantMinus.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
            this.btnInfantMinus.HoverState.FillColor = System.Drawing.Color.FromArgb(220, 220, 220);
            this.btnInfantMinus.Cursor = Cursors.Hand;

            this.txtInfant.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.txtInfant.Location = new System.Drawing.Point(375, 345);
            this.txtInfant.Size = new System.Drawing.Size(50, 36);
            this.txtInfant.TextAlign = HorizontalAlignment.Center;
            this.txtInfant.ReadOnly = true;
            this.txtInfant.BorderStyle = BorderStyle.FixedSingle;
            this.txtInfant.BackColor = System.Drawing.Color.White;

            this.btnInfantPlus.Text = "+";
            this.btnInfantPlus.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnInfantPlus.BorderRadius = 8;
            this.btnInfantPlus.Location = new System.Drawing.Point(435, 345);
            this.btnInfantPlus.Size = new System.Drawing.Size(45, 36);
            this.btnInfantPlus.FillColor = System.Drawing.Color.FromArgb(0, 147, 255);
            this.btnInfantPlus.ForeColor = System.Drawing.Color.White;
            this.btnInfantPlus.HoverState.FillColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.btnInfantPlus.Cursor = Cursors.Hand;

            // ================= BUTTON HỦY =================
            this.btnCancel.Text = "Hủy";
            this.btnCancel.BorderRadius = 10;
            this.btnCancel.FillColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnCancel.Location = new System.Drawing.Point(30, 430);
            this.btnCancel.Size = new System.Drawing.Size(210, 50);
            this.btnCancel.HoverState.FillColor = System.Drawing.Color.FromArgb(230, 230, 230);
            this.btnCancel.Cursor = Cursors.Hand;

            // ================= BUTTON LƯU =================
            this.btnDone.Text = "Lưu thay đổi";
            this.btnDone.BorderRadius = 10;
            this.btnDone.FillColor = System.Drawing.Color.FromArgb(76, 175, 80);
            this.btnDone.ForeColor = System.Drawing.Color.White;
            this.btnDone.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnDone.Location = new System.Drawing.Point(260, 430);
            this.btnDone.Size = new System.Drawing.Size(210, 50);
            this.btnDone.HoverState.FillColor = System.Drawing.Color.FromArgb(56, 142, 60);
            this.btnDone.Cursor = Cursors.Hand;

            // ================= ADD TO HEADER =================
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblSubtitle);
            this.pnlHeader.Controls.Add(this.btnClose);

            // ================= ADD TO MAIN PANEL =================
            this.pnlMain.Controls.Add(this.pnlHeader);

            this.pnlMain.Controls.Add(this.lblSeatClassTitle);
            this.pnlMain.Controls.Add(this.cboSeatClass);

            this.pnlMain.Controls.Add(this.lblPassengerTitle);

            this.pnlMain.Controls.Add(this.lblAdult);
            this.pnlMain.Controls.Add(this.txtAdult);
            this.pnlMain.Controls.Add(this.btnAdultMinus);
            this.pnlMain.Controls.Add(this.btnAdultPlus);

            this.pnlMain.Controls.Add(this.lblChild);
            this.pnlMain.Controls.Add(this.txtChild);
            this.pnlMain.Controls.Add(this.btnChildMinus);
            this.pnlMain.Controls.Add(this.btnChildPlus);

            this.pnlMain.Controls.Add(this.lblInfant);
            this.pnlMain.Controls.Add(this.txtInfant);
            this.pnlMain.Controls.Add(this.btnInfantMinus);
            this.pnlMain.Controls.Add(this.btnInfantPlus);

            this.pnlMain.Controls.Add(this.btnCancel);
            this.pnlMain.Controls.Add(this.btnDone);

            this.Controls.Add(this.pnlMain);

            // FORM CONFIG
            this.ClientSize = new System.Drawing.Size(520, 540);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = System.Drawing.Color.White;

            this.ResumeLayout(false);
        }
    }
}