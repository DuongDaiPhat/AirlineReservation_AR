namespace AirlineReservation_AR.src.Presentation__Winform_.Views.Forms.Admin
{
    partial class EditPricingForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblFlightNumber;
        private System.Windows.Forms.Label lblRoute;
        private System.Windows.Forms.Label lblSeatClass;
        private System.Windows.Forms.NumericUpDown numNewPrice;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblCurrentPrice;

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
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 450);
            this.Text = "Edit Pricing";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Header
            var pnlHeader = new System.Windows.Forms.Panel();
            pnlHeader.Size = new System.Drawing.Size(400, 70);
            pnlHeader.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;

            lblTitle = new System.Windows.Forms.Label();
            lblTitle.Text = "Edit Flight Price";
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            lblTitle.ForeColor = System.Drawing.Color.White;
            lblTitle.Location = new System.Drawing.Point(20, 20);
            lblTitle.AutoSize = true;
            pnlHeader.Controls.Add(lblTitle);
            this.Controls.Add(pnlHeader);

            int y = 90;
            int xLabel = 30;
            int xVal = 130;

            // Flight Info
            var lblF = new System.Windows.Forms.Label();
            lblF.Text = "Flight:";
            lblF.Location = new System.Drawing.Point(xLabel, y);
            lblF.Font = new System.Drawing.Font("Segoe UI", 10);
            this.Controls.Add(lblF);

            lblFlightNumber = new System.Windows.Forms.Label();
            lblFlightNumber.Location = new System.Drawing.Point(xVal, y);
            lblFlightNumber.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            lblFlightNumber.AutoSize = true;
            this.Controls.Add(lblFlightNumber);

            y += 40;
            var lblR = new System.Windows.Forms.Label();
            lblR.Text = "Route:";
            lblR.Location = new System.Drawing.Point(xLabel, y);
             lblR.Font = new System.Drawing.Font("Segoe UI", 10);
            this.Controls.Add(lblR);

            lblRoute = new System.Windows.Forms.Label();
            lblRoute.Location = new System.Drawing.Point(xVal, y);
            lblRoute.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            lblRoute.AutoSize = true;
            this.Controls.Add(lblRoute);

            y += 40;
            var lblS = new System.Windows.Forms.Label();
            lblS.Text = "Seat Class:";
            lblS.Location = new System.Drawing.Point(xLabel, y);
             lblS.Font = new System.Drawing.Font("Segoe UI", 10);
            this.Controls.Add(lblS);

            lblSeatClass = new System.Windows.Forms.Label();
            lblSeatClass.Location = new System.Drawing.Point(xVal, y);
            lblSeatClass.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            lblSeatClass.AutoSize = true;
            this.Controls.Add(lblSeatClass);

            y += 40;
            var lblP = new System.Windows.Forms.Label();
            lblP.Text = "Current Price:";
            lblP.Location = new System.Drawing.Point(xLabel, y);
             lblP.Font = new System.Drawing.Font("Segoe UI", 10);
            this.Controls.Add(lblP);

            lblCurrentPrice = new System.Windows.Forms.Label();
            lblCurrentPrice.Location = new System.Drawing.Point(xVal, y);
            lblCurrentPrice.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Strikeout);
            lblCurrentPrice.AutoSize = true;
            this.Controls.Add(lblCurrentPrice);

            y += 50;
            var lblNew = new System.Windows.Forms.Label();
            lblNew.Text = "New Price (VND):";
            lblNew.Location = new System.Drawing.Point(xLabel, y);
            lblNew.AutoSize = true;
             lblNew.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            this.Controls.Add(lblNew);

            y += 25;
            numNewPrice = new System.Windows.Forms.NumericUpDown();
            numNewPrice.Location = new System.Drawing.Point(xLabel, y);
            numNewPrice.Size = new System.Drawing.Size(340, 30);
            numNewPrice.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
            numNewPrice.Minimum = new decimal(new int[] { 1000, 0, 0, 0 });
            numNewPrice.Increment = new decimal(new int[] { 50000, 0, 0, 0 });
            numNewPrice.Font = new System.Drawing.Font("Segoe UI", 12);
            this.Controls.Add(numNewPrice);

            // Buttons
            btnSave = new System.Windows.Forms.Button();
            btnSave.Text = "Save Changes";
            btnSave.Location = new System.Drawing.Point(xLabel, 380);
            btnSave.Size = new System.Drawing.Size(160, 40);
            btnSave.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            btnSave.ForeColor = System.Drawing.Color.White;
            btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnSave.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            this.Controls.Add(btnSave);

            btnCancel = new System.Windows.Forms.Button();
            btnCancel.Text = "Cancel";
            btnCancel.Location = new System.Drawing.Point(210, 380);
            btnCancel.Size = new System.Drawing.Size(160, 40);
            btnCancel.BackColor = System.Drawing.Color.FromArgb(149, 165, 166);
            btnCancel.ForeColor = System.Drawing.Color.White;
            btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnCancel.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            this.Controls.Add(btnCancel);
        }
    }
}
