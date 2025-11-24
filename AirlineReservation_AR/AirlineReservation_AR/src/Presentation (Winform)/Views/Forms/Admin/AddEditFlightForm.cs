using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.Domain.DTOs;
using AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.Admin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public enum FormMode
{
    Add,
    Edit
}

namespace AirlineReservation_AR.src.Presentation__Winform_.Views.Forms.Admin
{
    public partial class AddEditFlightForm : Form
    {
        private readonly FormMode _mode;
        private readonly FlightListDtoAdmin _flight;
        public AddEditFlightForm(FormMode mode, FlightListDtoAdmin flight = null)
        {
            InitializeComponent();
            _mode = mode;
            _flight = flight;

            if (_mode == FormMode.Edit && _flight != null)
            {
                LoadFlightData();
            }
        }
        private void LoadFlightData()
        {
            txtRoute.Text = _flight.FlightCode;
            txtAirline.Text = _flight.Airline;
            txtRoute.Text = _flight.Route;

            //dtDeparture.Value = _flight.Departure;
            //txtAircraft.Text = _flight.Aircraft;
            //numPrice.Value = _flight.Price;
            //cbStatus.Text = _flight.Status;

            // ❌ Không cho sửa các trường này
            txtRoute.Enabled = false;
            txtAirline.Enabled = false;
            txtRoute.Enabled = false;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (_mode == FormMode.Edit)
                {
                    //_flight.DepartureTime = dtDeparture.Value;
                    //_flight.Aircraft = txtAircraft.Text;
                    //_flight.Price = numPrice.Value;
                    _flight.Status = cbStatus.Text;
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
