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

        private Label lblFlightCode;
        private Label lblAirline;
        private Label lblRoute;
        private Label lblDeparture;
        private Label lblAircraft;
        private Label lblPrice;
        private Label lblStatus;
        private TextBox txtFlightCode;
        private TextBox txtRoute;
        private TextBox txtAirline;
        private DateTimePicker dtDeparture;
        private TextBox txtAircraft;
        private NumericUpDown numPrice;
        private ComboBox cbStatus;
        private Button btnSave;
        private Button btnCancel;

        private void InitializeComponent()
        {
            // ==== Tạo controls ====
            lblFlightCode = new Label();
            lblAirline = new Label();
            lblRoute = new Label();
            lblDeparture = new Label();
            lblAircraft = new Label();
            lblPrice = new Label();
            lblStatus = new Label();

            txtFlightCode = new TextBox();
            txtAirline = new TextBox();
            txtRoute = new TextBox();
            dtDeparture = new DateTimePicker();
            txtAircraft = new TextBox();
            numPrice = new NumericUpDown();
            cbStatus = new ComboBox();

            btnSave = new Button();
            btnCancel = new Button();

            ((ISupportInitialize)numPrice).BeginInit();
            SuspendLayout();


            int startX = 30;
            int labelWidth = 120;
            int textboxWidth = 220;
            int spacingY = 40;
            int currentY = 30;


            // ========== LABEL + INPUT ==========

            // Flight Code
            lblFlightCode.Text = "Số hiệu chuyến bay:";
            lblFlightCode.Location = new Point(startX, currentY);
            txtFlightCode.Location = new Point(startX + labelWidth, currentY - 3);
            txtFlightCode.Width = textboxWidth;

            currentY += spacingY;

            // Airline
            lblAirline.Text = "Hãng hàng không:";
            lblAirline.Location = new Point(startX, currentY);
            txtAirline.Location = new Point(startX + labelWidth, currentY - 3);
            txtAirline.Width = textboxWidth;

            currentY += spacingY;

            // Route
            lblRoute.Text = "Tuyến bay (VD: SGN-HAN):";
            lblRoute.Location = new Point(startX, currentY);
            txtRoute.Location = new Point(startX + labelWidth, currentY - 3);
            txtRoute.Width = textboxWidth;

            currentY += spacingY;

            // Departure Time
            lblDeparture.Text = "Ngày/Giờ khởi hành:";
            lblDeparture.Location = new Point(startX, currentY);
            dtDeparture.Location = new Point(startX + labelWidth, currentY - 3);
            dtDeparture.Width = textboxWidth;

            currentY += spacingY;

            // Aircraft
            lblAircraft.Text = "Máy bay:";
            lblAircraft.Location = new Point(startX, currentY);
            txtAircraft.Location = new Point(startX + labelWidth, currentY - 3);
            txtAircraft.Width = textboxWidth;

            currentY += spacingY;

            // Price
            lblPrice.Text = "Giá vé:";
            lblPrice.Location = new Point(startX, currentY);
            numPrice.Location = new Point(startX + labelWidth, currentY - 3);
            numPrice.Maximum = 100000000;
            numPrice.DecimalPlaces = 0;
            numPrice.Width = textboxWidth;

            currentY += spacingY;

            // Status
            lblStatus.Text = "Trạng thái:";
            lblStatus.Location = new Point(startX, currentY);
            cbStatus.Location = new Point(startX + labelWidth, currentY - 3);
            cbStatus.Width = textboxWidth;
            cbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cbStatus.Items.AddRange(new string[]
            {
        "Đang bay",
        "Hoãn",
        "Hủy",
        "Đang chuẩn bị"
            });

            currentY += spacingY + 10;


            // ===== Buttons =====
            btnSave.Text = "Lưu";
            btnSave.BackColor = Color.LightGreen;
            btnSave.Location = new Point(startX, currentY);
            btnSave.Width = 120;
            btnSave.Click += btnSave_Click;

            btnCancel.Text = "Hủy";
            btnCancel.BackColor = Color.LightCoral;
            btnCancel.Location = new Point(startX + 140, currentY);
            btnCancel.Width = 120;
            btnCancel.Click += btnCancel_Click;


            // ==== Add Controls ====
            Controls.Add(lblFlightCode);
            Controls.Add(lblAirline);
            Controls.Add(lblRoute);
            Controls.Add(lblDeparture);
            Controls.Add(lblAircraft);
            Controls.Add(lblPrice);
            Controls.Add(lblStatus);

            Controls.Add(txtFlightCode);
            Controls.Add(txtAirline);
            Controls.Add(txtRoute);
            Controls.Add(dtDeparture);
            Controls.Add(txtAircraft);
            Controls.Add(numPrice);
            Controls.Add(cbStatus);

            Controls.Add(btnSave);
            Controls.Add(btnCancel);


            // ==== Form ====
            ClientSize = new Size(430, 430);
            Name = "AddEditFlightForm";
            Text = "Thêm/Chỉnh sửa chuyến bay";

            ((ISupportInitialize)(numPrice)).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }

        #endregion
}