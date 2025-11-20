using System.ComponentModel;

namespace AirlineReservation_AR.src.Presentation__Winform_.Views.Forms.Admin
{
    partial class AddEditFlightForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtFlightCode = new TextBox();
            txtRoute = new TextBox();
            txtAirline = new TextBox();
            dtDeparture = new DateTimePicker();
            txtAircraft = new TextBox();
            numPrice = new NumericUpDown();
            cbStatus = new ComboBox();
            btnSave = new Button();
            btnCancel = new Button();
            ((ISupportInitialize)numPrice).BeginInit();
            SuspendLayout();
            // 
            // txtFlightCode
            // 
            this.txtFlightCode.Location = new Point(30, 30);
            this.txtAirline.Location = new Point(30, 70);
            this.txtRoute.Location = new Point(30, 110);

            // Datetime
            this.dtDeparture.Location = new Point(30, 150);

            // Others
            this.txtAircraft.Location = new Point(30, 190);
            this.numPrice.Location = new Point(30, 230);
            this.cbStatus.Location = new Point(30, 270);

            // Buttons
            this.btnSave.Text = "Lưu";
            this.btnSave.Location = new Point(30, 320);
            this.btnSave.Click += btnSave_Click;

            this.btnCancel.Text = "Hủy";
            this.btnCancel.Location = new Point(120, 320);
            this.btnCancel.Click += btnCancel_Click;

            // Add controls
            this.Controls.Add(this.txtFlightCode);
            this.Controls.Add(this.txtAirline);
            this.Controls.Add(this.txtRoute);
            this.Controls.Add(this.dtDeparture);
            this.Controls.Add(this.txtAircraft);
            this.Controls.Add(this.numPrice);
            this.Controls.Add(this.cbStatus);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);

            // Form
            this.ClientSize = new Size(350, 380);
            this.Name = "AddEditFlightForm";
            this.Text = "Chỉnh sửa chuyến bay";

            ((ISupportInitialize)(this.numPrice)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private TextBox txtFlightCode;
        private TextBox txtRoute;
        private TextBox txtAirline;
        private DateTimePicker dtDeparture;
        private TextBox txtAircraft;
        private NumericUpDown numPrice;
        private ComboBox cbStatus;
        private Button btnSave;
        private Button btnCancel;
    }
}