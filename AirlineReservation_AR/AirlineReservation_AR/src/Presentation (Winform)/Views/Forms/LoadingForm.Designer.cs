using AirlineReservation_AR.Properties;
namespace AirlineReservation_AR.src.AirlineReservation.Presentation__WinForms_.Views.Forms.Common
{
    partial class LoadingForm
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
            components = new System.ComponentModel.Container();
            statusLabel = new Label();
            panel1 = new Panel();
            rotate = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // statusLabel
            // 
            statusLabel.Font = new Font("Fz Poppins SemBd", 14.25F, FontStyle.Bold);
            statusLabel.Location = new Point(0, 150);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(300, 30);
            statusLabel.TabIndex = 3;
            statusLabel.Text = "Loading ...";
            statusLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            panel1.Location = new Point(105, 39);
            panel1.Name = "panel1";
            panel1.Size = new Size(90, 90);
            panel1.TabIndex = 7;
            // 
            // rotate
            // 
            rotate.Tick += rotate_Tick;
            // 
            // LoadingForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(300, 200);
            Controls.Add(statusLabel);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "LoadingForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "LoadingForm";
            ResumeLayout(false);
        }

        #endregion
        private Label statusLabel;
        private Panel panel1;
        private System.Windows.Forms.Timer rotate;
    }
}