using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.Domain.DTOs;
using AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.User;
using AirlineReservation_AR.src.AirlineReservation.Presentation__WinForms_.Views.Forms.Common;

namespace AirlineReservation_AR.src.Presentation__Winform_.Views.popup
{
    public partial class ComfirmFlights : Form
    {

        public event Action OnComfirm;
        public event Action OnCancel;
        public ComfirmFlights()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public void setData(FlightResultDTO flight, FlightResultDTO flightReturn, int sl, bool isReturn)
        {
 

            FlightNumber.Text = flight.FlightCode.ToString();
            flightDetailCard1.setData(flight);
            seat.Text = flight.SelectedSeatClassName.ToString();
            human.Text = sl.ToString() + "người";
            hl.Text = (7 * sl).ToString("N0") + " KG";
            pricesOnPerson.Text = flight.Price.ToString("N0") + " VND";
            TotalPrices.Text = (flight.Price * sl).ToString("N0") + " VND";
            if (isReturn)
            {
                var cardReturn = new FlightDetailCard();
                cardReturn.setData(flightReturn);
                cardReturn.Margin = new Padding(20, 3, 3, 3);
                cardReturn.Dock = DockStyle.None;
                returnCode.Text = flightReturn.FlightCode.ToString();
                betterFlowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
                betterFlowLayoutPanel1.Controls.Add(cardReturn);

                betterFlowLayoutPanel1.Controls.SetChildIndex(
                                cardReturn,
                                betterFlowLayoutPanel1.Controls.IndexOf(PanelReturn) + 1
                );


            }
            else
            {
                PanelReturn.Visible = false;
            }
        }

        private void ComfirmFlights_Load(object sender, EventArgs e)
        {
            this.MaximumSize = new Size(
            this.Width,
            Screen.PrimaryScreen.WorkingArea.Height - 50);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            IntPtr region = CreateRoundRectRgn(
                0, 0, this.Width, this.Height, 20, 20);

            this.Region = Region.FromHrgn(region);
        }

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            AnnouncementForm announcementForm = new AnnouncementForm();
            announcementForm.SetAnnouncement("Bạn có chắc muốn hủy chọn chuyến bay này", "Xin vui lòng xác nhận hủy hoặc tắt thông báo này để tiếp tục", false, null);
            announcementForm.setClose();
            announcementForm.OnComplete += () =>
            {

                this.Hide();


                OnCancel?.Invoke();
            };
            announcementForm.Show();

        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            OnComfirm?.Invoke();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            AnnouncementForm announcementForm = new AnnouncementForm();
            announcementForm.SetAnnouncement("Bạn có chắc muốn hủy chọn chuyến bay này", "Xin vui lòng xác nhận hủy hoặc tắt thông báo này để tiếp tục", false, null);
            announcementForm.setClose();
            announcementForm.OnComplete += () =>
            {

                this.Hide();


                OnCancel?.Invoke();
            };
            announcementForm.Show();
        }
    }

}

