using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.Domain.DTOs;

namespace AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.User
{
    public partial class PanelBaggagePassenger : UserControl
    {
        private ServiceOption _serviceOpt;
        public event Action OnServiceChanged;
        public PanelBaggagePassenger()
        {
            InitializeComponent();
        }

        public void BindData(string passengerKey, ServiceOption passengerData, int index)
        {
            _serviceOpt = passengerData;
            indexPassenger.Text = $"{index.ToString()}";
            title.Text = $"Passenger {passengerKey.ToString()}";

            if (passengerData.Baggage.ServiceId == 0)
            {
                radioButtonFree.Checked = true;
            }
            else
            {
                switch (passengerData.Baggage.ServiceId)
                {
                    case 1:
                        radioButton20kg.Checked = true;
                        break;
                    case 2:
                        radioButton10kg.Checked = true;
                        break;
                }
            }

        }

        private void radioButtonFree_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioButtonFree.Checked) return;

            _serviceOpt.Baggage.ServiceId = 0;
            _serviceOpt.Baggage.ServiceName = "0kg Baggage";
            _serviceOpt.Baggage.BasePrice = 0;
            OnServiceChanged?.Invoke();
        }

        private void radioButton10kg_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioButton10kg.Checked) return;
            _serviceOpt.Baggage.ServiceId = 2;
            _serviceOpt.Baggage.ServiceName = "10kg Baggage";
            _serviceOpt.Baggage.BasePrice = 1500000;
            OnServiceChanged?.Invoke();

        }

        private void radioButton20kg_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioButton20kg.Checked) return;
            _serviceOpt.Baggage.ServiceId = 1;
            _serviceOpt.Baggage.ServiceName = "20kg Baggage";
            _serviceOpt.Baggage.BasePrice = 3000000;
            OnServiceChanged?.Invoke();
        }
    }
}
