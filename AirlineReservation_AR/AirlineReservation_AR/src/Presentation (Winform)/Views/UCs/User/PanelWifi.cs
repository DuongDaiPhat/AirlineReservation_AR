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
using System.Windows.Forms.DataVisualization.Charting;

namespace AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.User
{
    public partial class PanelWifi : UserControl
    {

        private ServiceOption _serviceOpt;
        public event Action OnServiceChanged;
        public PanelWifi()
        {
            InitializeComponent();
        }

        private void PanelWifi_Load(object sender, EventArgs e)
        {

        }

        public void BindData(string passengerKey, ServiceOption passengerData, int index)
        {
            _serviceOpt = passengerData;
            indexPassenger.Text = $"{index.ToString()}";
            title.Text = $"Passenger {passengerKey.ToString()}";

            if (passengerData.Priority.ServiceId == 0)
            {
                radioButtonFree.Checked = true;
            }
            else
            {
                switch (passengerData.Priority.ServiceId)
                {
                    case 9:
                        radioButtonWifi.Checked = true;
                        break;
                }
            }
        }

        private void radioButtonFree_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioButtonFree.Checked) return;
            _serviceOpt.Priority.ServiceId = 0;
            _serviceOpt.Priority.ServiceName = "Do not use wifi";
            _serviceOpt.Priority.BasePrice = 0;
            OnServiceChanged?.Invoke();

        }

        private void radioButtonWifi_CheckedChanged(object sender, EventArgs e)
        {
            if(!radioButtonWifi.Checked) return;
            _serviceOpt.Priority.ServiceId = 9;
            _serviceOpt.Priority.ServiceName = "In-flight WiFi pass";
            _serviceOpt.Priority.BasePrice = 800000;
            OnServiceChanged?.Invoke();
        }
    }
}
