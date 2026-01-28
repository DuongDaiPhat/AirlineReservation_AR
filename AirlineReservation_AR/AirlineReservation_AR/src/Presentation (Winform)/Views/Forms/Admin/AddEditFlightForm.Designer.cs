using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using AirlineReservation_AR.src.Presentation__Winform_.Theme;

namespace AirlineReservation_AR.src.Presentation__Winform_.Views.Forms.Admin
{
    partial class AddEditFlightForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private Label lblFlightCode;
        private Label lblAirline;
        private Label lblDepartureAirport;
        private Label lblArrivalAirport;
        private Label lblDate;
        private Label lblTime;
        private Label lblAircraft;
        private Label lblPrice;
        private Label lblStatus;

        private TextBox txtFlightCode;
        private ComboBox cboAirline;
        private ComboBox cboDeparture;
        private ComboBox cboArrival;
        private DateTimePicker dtDate;
        private DateTimePicker dtTimeDeparture;
        private DateTimePicker dtTimeArrival;
        private ComboBox cboAircraft;
        private NumericUpDown numPrice;
        private ComboBox cbStatus;
        
        private Button btnSave;
        private Button btnCancel;

        private void InitializeComponent()
        {
            lblFlightCode = new Label();
            lblAirline = new Label();
            lblDepartureAirport = new Label();
            lblArrivalAirport = new Label();
            lblDate = new Label();
            lblTime = new Label();
            lblAircraft = new Label();
            lblPrice = new Label();
            lblStatus = new Label();

            txtFlightCode = new TextBox();
            cboAirline = new ComboBox();
            cboDeparture = new ComboBox();
            cboArrival = new ComboBox();
            dtDate = new DateTimePicker();
            dtTimeDeparture = new DateTimePicker();
            dtTimeArrival = new DateTimePicker();
            cboAircraft = new ComboBox();
            numPrice = new NumericUpDown();
            cbStatus = new ComboBox();

            btnSave = new Button();
            btnCancel = new Button();

            ((ISupportInitialize)numPrice).BeginInit();
            SuspendLayout();

            int startX = 30;
            int labelWidth = 140;
            int controlWidth = 250;
            int spacingY = 45;
            int currentY = 30;

            // Flight Code
            lblFlightCode.Text = "Flight Number:";
            lblFlightCode.Location = new Point(startX, currentY);
            lblFlightCode.AutoSize = true;
            
            txtFlightCode.Location = new Point(startX + labelWidth, currentY - 3);
            txtFlightCode.Size = new Size(controlWidth, 25);
            
            currentY += spacingY;

            // Airline
            lblAirline.Text = "Airline:";
            lblAirline.Location = new Point(startX, currentY);
            lblAirline.AutoSize = true;

            cboAirline.Location = new Point(startX + labelWidth, currentY - 3);
            cboAirline.Size = new Size(controlWidth, 25);
            cboAirline.DropDownStyle = ComboBoxStyle.DropDownList;

            currentY += spacingY;

            // Departure Airport
            lblDepartureAirport.Text = "Departure Airport:";
            lblDepartureAirport.Location = new Point(startX, currentY);
            lblDepartureAirport.AutoSize = true;

            cboDeparture.Location = new Point(startX + labelWidth, currentY - 3);
            cboDeparture.Size = new Size(controlWidth, 25);
            cboDeparture.DropDownStyle = ComboBoxStyle.DropDownList;

            currentY += spacingY;

            // Arrival Airport
            lblArrivalAirport.Text = "Arrival Airport:";
            lblArrivalAirport.Location = new Point(startX, currentY);
            lblArrivalAirport.AutoSize = true;

            cboArrival.Location = new Point(startX + labelWidth, currentY - 3);
            cboArrival.Size = new Size(controlWidth, 25);
            cboArrival.DropDownStyle = ComboBoxStyle.DropDownList;

            currentY += spacingY;
            
            // Aircraft
            lblAircraft.Text = "Aircraft:";
            lblAircraft.Location = new Point(startX, currentY);
            lblAircraft.AutoSize = true;

            cboAircraft.Location = new Point(startX + labelWidth, currentY - 3);
            cboAircraft.Size = new Size(controlWidth, 25);
            cboAircraft.DropDownStyle = ComboBoxStyle.DropDownList;

            currentY += spacingY;

            // Date
            lblDate.Text = "Flight Date:";
            lblDate.Location = new Point(startX, currentY);
            lblDate.AutoSize = true;

            dtDate.Location = new Point(startX + labelWidth, currentY - 3);
            dtDate.Size = new Size(controlWidth, 25);
            dtDate.Format = DateTimePickerFormat.Short;

            currentY += spacingY;

            // Time
            lblTime.Text = "Dep - Arr Time:";
            lblTime.Location = new Point(startX, currentY);
            lblTime.AutoSize = true;

            dtTimeDeparture.Location = new Point(startX + labelWidth, currentY - 3);
            dtTimeDeparture.Size = new Size(110, 25);
            dtTimeDeparture.Format = DateTimePickerFormat.Time;
            dtTimeDeparture.ShowUpDown = true;

            dtTimeArrival.Location = new Point(startX + labelWidth + 120, currentY - 3);
            dtTimeArrival.Size = new Size(110, 25);
            dtTimeArrival.Format = DateTimePickerFormat.Time;
            dtTimeArrival.ShowUpDown = true;

            currentY += spacingY;


            // Price
            lblPrice.Text = "Base Price:";
            lblPrice.Location = new Point(startX, currentY);
            lblPrice.AutoSize = true;

            numPrice.Location = new Point(startX + labelWidth, currentY - 3);
            numPrice.Size = new Size(controlWidth, 25);
            numPrice.Maximum = 100000000;
            numPrice.DecimalPlaces = 0;

            currentY += spacingY;

            // Status
            lblStatus.Text = "Status:";
            lblStatus.Location = new Point(startX, currentY);
            lblStatus.AutoSize = true;

            cbStatus.Location = new Point(startX + labelWidth, currentY - 3);
            cbStatus.Size = new Size(controlWidth, 25);
            cbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cbStatus.Items.AddRange(new string[] { "Available", "Full", "Delayed", "Cancelled", "Completed" });
            cbStatus.SelectedIndex = 0;

            currentY += spacingY + 20;

            // Buttons
            btnSave.Text = "Save Flight";
            btnSave.Location = new Point(startX + 60, currentY);
            btnSave.Size = new Size(140, 40);
            btnSave.BackColor = Color.FromArgb(76, 175, 80); // Should use UIConstants
            btnSave.ForeColor = Color.White;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Click += btnSave_Click;

            btnCancel.Text = "Cancel";
            btnCancel.Location = new Point(startX + 220, currentY);
            btnCancel.Size = new Size(140, 40);
            btnCancel.BackColor = Color.FromArgb(244, 67, 54);
            btnCancel.ForeColor = Color.White;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Click += btnCancel_Click;

            // Add Controls
            Controls.AddRange(new Control[] {
                lblFlightCode, txtFlightCode,
                lblAirline, cboAirline,
                lblDepartureAirport, cboDeparture,
                lblArrivalAirport, cboArrival,
                lblDate, dtDate,
                lblTime, dtTimeDeparture, dtTimeArrival,
                lblAircraft, cboAircraft,
                lblPrice, numPrice,
                lblStatus, cbStatus,
                btnSave, btnCancel
            });

            // Form
            ClientSize = new Size(460, currentY + 70);
            Name = "AddEditFlightForm";
            Text = "Add New Flight";
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            ((ISupportInitialize)(numPrice)).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}