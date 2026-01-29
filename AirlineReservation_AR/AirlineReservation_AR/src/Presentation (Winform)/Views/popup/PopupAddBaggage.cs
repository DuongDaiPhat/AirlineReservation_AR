using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AirlineReservation_AR.src.Domain.DTOs;
using AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.User;

namespace AirlineReservation_AR.src.Presentation__Winform_.Views.popup
{
    public partial class PopupAddBaggage : Form
    {
        private Dictionary<string, ServiceOption> servicePassengers;
        public event Action<Dictionary<string, ServiceOption>> OnServicesChanged;

        private Dictionary<string, ServiceOption> servicePassengersReturn;
        public event Action<Dictionary<string, ServiceOption>> OnServicesReturnChanged;

        private bool isRoundTrip = false;
        public PopupAddBaggage(Dictionary<string, ServiceOption> _servicePassengers, Dictionary<string, ServiceOption> _servicePassengersReturn, bool isRoundTrip)
        {
            servicePassengers = _servicePassengers;
            servicePassengersReturn = _servicePassengersReturn;
            InitializeComponent();
            if (isRoundTrip == false) {
                guna2Panel2.Enabled = false;
            }
            generatePassengerPanels();
            updateSummary();
        }
        private void PopupAddBaggage_Load(object sender, EventArgs e)
        {

        }

        private void guna2GradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelPassenger1_Load(object sender, EventArgs e)
        {

        }
        public void generatePassengerPanels()
        {
            var data = GetActiveServices();

            if (data == null || data.Count == 0)
            {
                MessageBox.Show("No passengers available.");
                return;
            }

            rightBody.Controls.Clear();

            var title = new TitleCardBaggage();
            title.Margin = new Padding(15, 3, 3, 3);
            rightBody.Controls.Add(title);

            int index = 1;
            foreach (var item in data)
            {
                var panel = new PanelBaggagePassenger();
                panel.BindData(item.Key, item.Value, index++);
                panel.Margin = new Padding(15, 20, 3, 3);
                panel.OnServiceChanged += updateSummary;
                rightBody.Controls.Add(panel);
            }
        }


        public void generateMealPanels()
        {
            var data = GetActiveServices();

            if (data == null || data.Count == 0)
            {
                MessageBox.Show("No passengers available.");
                return;
            }

            rightBody.Controls.Clear();

            var titleCardMeal = new TitleMealCard
            {
                Margin = new Padding(15, 3, 3, 3)
            };
            rightBody.Controls.Add(titleCardMeal);

            int displayIndex = 1;

            foreach (var item in data)
            {
                var panelMeal = new PanelMeal();
                panelMeal.BindData(item.Key, item.Value, displayIndex++);
                panelMeal.Margin = new Padding(15, 20, 3, 3);
                panelMeal.OnServiceChanged += updateSummary;

                rightBody.Controls.Add(panelMeal);
            }
        }


        public void generatePriorityPanels()
        {
            var data = GetActiveServices();

            if (data == null || data.Count == 0)
            {
                MessageBox.Show("No passengers available.");
                return;
            }

            rightBody.Controls.Clear();

            var titleCardPriority = new TitleEntertaimentCard
            {
                Margin = new Padding(15, 3, 3, 3)
            };
            rightBody.Controls.Add(titleCardPriority);

            int displayIndex = 1;

            foreach (var item in data)
            {
                var panelPriority = new PanelWifi();
                panelPriority.BindData(item.Key, item.Value, displayIndex++);
                panelPriority.Margin = new Padding(15, 20, 3, 3);
                panelPriority.OnServiceChanged += updateSummary;

                rightBody.Controls.Add(panelPriority);
            }
        }


        private void pictureBox6_Click(object sender, EventArgs e)
        {
            OnServicesChanged?.Invoke(servicePassengers);

            if (servicePassengersReturn != null)
                OnServicesReturnChanged?.Invoke(servicePassengersReturn);


            this.Close();
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            OnServicesChanged?.Invoke(servicePassengers);

            if (servicePassengersReturn != null)
                OnServicesReturnChanged?.Invoke(servicePassengersReturn);

            this.Close();
        }



        public void updateSummary()
        {
            decimal outbound =
                servicePassengers?.Values.Sum(x => x.totalPrice) ?? 0;

            decimal inbound =
                servicePassengersReturn?.Values.Sum(x => x.totalPrice) ?? 0;

            lblTotalPrice.Text = $"Total: {(outbound + inbound):N0}";
        }

        private void BaggageService_Click(object sender, EventArgs e)
        {
            generatePassengerPanels();
        }

        private void MealService_Click(object sender, EventArgs e)
        {
            generateMealPanels();
        }

        private void PriorityService_Click(object sender, EventArgs e)
        {
            generatePriorityPanels();
        }

        private void OnewayClick(object sender, EventArgs e)
        {
            isRoundTrip = false;
            generatePassengerPanels();
            OneWay.ForeColor = Color.FromArgb(37, 99, 235);
            OneWay.FillColor = Color.White;
            RoundTrip.ForeColor = Color.DimGray;
            RoundTrip.FillColor = Color.FromArgb(224, 224, 224);
        }

        private void RoundTripClick(object sender, EventArgs e)
        {
            isRoundTrip = true;
            generatePassengerPanels();
            OneWay.ForeColor = Color.DimGray;
            OneWay.FillColor = Color.FromArgb(224, 224, 224);
            RoundTrip.ForeColor = Color.FromArgb(37, 99, 235);
            RoundTrip.FillColor = Color.White; 
        }

        private Dictionary<string, ServiceOption> GetActiveServices()
        {
            return isRoundTrip
                ? servicePassengersReturn
                : servicePassengers;
        }


    }
}
