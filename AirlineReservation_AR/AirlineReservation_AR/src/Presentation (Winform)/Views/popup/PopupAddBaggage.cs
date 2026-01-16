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

        public PopupAddBaggage(Dictionary<string, ServiceOption> _servicePassengers)
        {
            servicePassengers = _servicePassengers;
            InitializeComponent();
            generatePassengerPanels(servicePassengers);
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
        public void generatePassengerPanels(Dictionary<string, ServiceOption> servicePassengers)
        {
            if (servicePassengers == null || servicePassengers.Count == 0)
            {
                MessageBox.Show("No passengers available.");
                return;
            }
            rightBody.Controls.Clear();
            TitleCardBaggage titleCardBaggage = new TitleCardBaggage();
            titleCardBaggage.Margin = new Padding(15, 3, 3, 3);
            rightBody.Controls.Add(titleCardBaggage);
            int displayIndex = 1;


            foreach (var item in servicePassengers)
            {
                PanelBaggagePassenger panelPassenger = new PanelBaggagePassenger();
                panelPassenger.BindData(item.Key, item.Value, displayIndex++);
                panelPassenger.Margin = new Padding(15, 20, 3, 3);
                panelPassenger.OnServiceChanged += () =>
                {
                    updateSummary();

                };
                rightBody.Controls.Add(panelPassenger);
            }
        }

        public void generateMealPanels(Dictionary<string, ServiceOption> servicePassengers)
        {
            if (servicePassengers == null || servicePassengers.Count == 0)
            {
                MessageBox.Show("No passengers available.");
                return;
            }
            rightBody.Controls.Clear();
            TitleMealCard titleCardMeal = new TitleMealCard();
            titleCardMeal.Margin = new Padding(15, 3, 3, 3);
            rightBody.Controls.Add(titleCardMeal);
            int displayIndex = 1;

            
            foreach (var item in servicePassengers)
            {
                PanelMeal panelMeal = new PanelMeal();
                panelMeal.BindData(item.Key, item.Value, displayIndex++);
                panelMeal.Margin = new Padding(15, 20, 3, 3);
                panelMeal.OnServiceChanged += () =>
                {
                    updateSummary();

                };
                rightBody.Controls.Add(panelMeal);
            }
        }

        public void generatePriorityPanels(Dictionary<string, ServiceOption> servicePassengers)
        {
            if (servicePassengers == null || servicePassengers.Count == 0)
            {
                MessageBox.Show("No passengers available.");
                return;
            }
            rightBody.Controls.Clear();
            TitleEntertaimentCard titleCardPriority = new TitleEntertaimentCard();
            titleCardPriority.Margin = new Padding(15, 3, 3, 3);
            rightBody.Controls.Add(titleCardPriority);
            int displayIndex = 1;
            foreach (var item in servicePassengers)
            {
                PanelWifi panelPriority = new PanelWifi();
                panelPriority.BindData(item.Key, item.Value, displayIndex++);
                panelPriority.Margin = new Padding(15, 20, 3, 3);
                panelPriority.OnServiceChanged += () =>
                {
                    updateSummary();
                };
                rightBody.Controls.Add(panelPriority);
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            OnServicesChanged?.Invoke(servicePassengers);

            this.Close();
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            OnServicesChanged?.Invoke(servicePassengers);

            this.Close();
        }



        public void updateSummary()
        {
            lblTotalPrice.Text = $"Total: {servicePassengers.Values.Sum(s => s.totalPrice).ToString("N0")}";
        }

        private void BaggageService_Click(object sender, EventArgs e)
        {
            generatePassengerPanels(servicePassengers);
        }

        private void MealService_Click(object sender, EventArgs e)
        {
           generateMealPanels(servicePassengers);
        }

        private void PriorityService_Click(object sender, EventArgs e)
        {
            generatePriorityPanels(servicePassengers);
        }
    }
}
