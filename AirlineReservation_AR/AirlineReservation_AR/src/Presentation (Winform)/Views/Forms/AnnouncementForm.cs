using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using AirlineReservation_AR.Properties;

namespace AirlineReservation.src.AirlineReservation.Presentation__WinForms_.Views.Forms.Common
{
    public partial class AnnouncementForm : Form
    {
        private Form _nextForm;
        private bool _isSuccess = false;
        public AnnouncementForm()
        {
            InitializeComponent();
        }

        public void SetAnnouncement(string title, string content, bool isSuccess, Form newForm = null)
        {
            if (this.titleLabel != null)
                this.titleLabel.Text = title ?? string.Empty;

            if (this.subtitleLabel != null)
                this.subtitleLabel.Text = content ?? string.Empty;


            _isSuccess = isSuccess;
            _nextForm = newForm;
            UpdateImage();
            completeBtn.Visible = true;
        }

        private void UpdateImage()
        {
            if (pictureBox1 == null) return;

            try
            {
                if (_isSuccess)
                {

                    pictureBox1.Image = Resources.check;
                }
                else
                {
                    pictureBox1.Image = Resources.fail;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading image: {ex.Message}");
            }
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            // Nếu có form cần chuyển đến
            if (_nextForm != null)
            {
                _nextForm.Show();
            }
        }

        private void completeBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            _nextForm?.Show();
        }
    }
}
