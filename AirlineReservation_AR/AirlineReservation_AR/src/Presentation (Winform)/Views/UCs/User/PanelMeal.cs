using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AirlineReservation_AR.src.Domain.DTOs;

namespace AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.User
{
    public partial class PanelMeal : UserControl
    {
        private ServiceOption _serviceOpt;
        public event Action OnServiceChanged;

        public PanelMeal()
        {
            InitializeComponent();
        }


        public void BindData(string passengerKey, ServiceOption passengerData, int index)
        {
            _serviceOpt = passengerData;
            indexPassenger.Text = $"{index.ToString()}";
            title.Text = $"Passenger {passengerKey.ToString()}";

            if (passengerData.Meal.ServiceId == -1)
            {
                radioButtonFree.Checked = true;
            }
            else
            {
                switch (passengerData.Meal.ServiceId)
                {
                    case 6:
                        radioButtonMeal.Checked = true;
                        break;
                }
            }
        }

        private void radioButtonFree_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioButtonFree.Checked) return;

            _serviceOpt.Meal.ServiceId = 0;
            _serviceOpt.Meal.ServiceName = "Do not upgrade";
            _serviceOpt.Meal.BasePrice = 0;
            OnServiceChanged?.Invoke();
        }

        private void radioButtonMeal_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioButtonMeal.Checked) return;
            _serviceOpt.Meal.ServiceId = 6;
            _serviceOpt.Meal.ServiceName = "Upgrade meal service";
            _serviceOpt.Meal.BasePrice = 1200000;
            OnServiceChanged?.Invoke();
        }


    }
}
