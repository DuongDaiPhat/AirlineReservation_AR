using AirlineReservation_AR.src.Domain.DTOs;
using AirlineReservation_AR.src.Infrastructure.DI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirlineReservation_AR.src.Presentation__Winform_.Views.Forms.Admin
{
    public partial class EditPricingForm : Form
    {
        private readonly FlightPricingDtoAdmin _pricing;

        public EditPricingForm(FlightPricingDtoAdmin pricing)
        {
            InitializeComponent();
            _pricing = pricing;
            LoadData();
            RegisterEvents();
        }

        private void LoadData()
        {
            lblFlightNumber.Text = $"{_pricing.FlightNumber} ({_pricing.AirlineName})";
            lblRoute.Text = _pricing.Route;
            lblSeatClass.Text = _pricing.SeatClass;
            lblCurrentPrice.Text = $"{_pricing.Price:N0} VND";
            
            decimal initialVal = _pricing.Price;
            if (initialVal < numNewPrice.Minimum) initialVal = numNewPrice.Minimum;
            if (initialVal > numNewPrice.Maximum) initialVal = numNewPrice.Maximum;
            
            numNewPrice.Value = initialVal;
        }

        private void RegisterEvents()
        {
            btnSave.Click += async (s, e) => await SaveAsync();
            btnCancel.Click += (s, e) => this.Close();
        }

        private async Task SaveAsync()
        {
            try
            {
                btnSave.Enabled = false;
                btnSave.Text = "Saving...";

                var dto = new UpdateFlightPricingDtoAdmin
                {
                    PricingId = _pricing.PricingId,
                    Price = numNewPrice.Value
                };

                var response = await DIContainer.PricingControllerAdmin.UpdatePricing(dto);

                if (response.Success)
                {
                    MessageBox.Show("Pricing updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                     MessageBox.Show(response.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSave.Enabled = true;
                btnSave.Text = "Save Changes";
            }
        }
    }
}
