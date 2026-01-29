using AirlineReservation_AR.src.Domain.DTOs;
using AirlineReservation_AR.src.Infrastructure.DI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirlineReservation_AR.src.Presentation__Winform_.Views.Forms.Admin
{
    public partial class AddEditFlightForm : Form
    {
        private bool _isEditMode;
        private FlightListDtoAdmin _flight;

        public AddEditFlightForm()
        {
            InitializeComponent();
            _isEditMode = false;
            this.Load += AddEditFlightForm_Load;
        }

        public AddEditFlightForm(FlightListDtoAdmin flight)
        {
            InitializeComponent();
            _flight = flight;
            _isEditMode = true;
            this.Text = "Edit Flight";
            this.Load += AddEditFlightForm_Load;
        }

        private async void AddEditFlightForm_Load(object sender, EventArgs e)
        {
            try
            {
                await LoadDropdownData();
                
                if (_isEditMode)
                {
                    LoadFlightData();
                }
                else
                {
                    // Set defaults for Add Mode
                    dtDate.Value = DateTime.Today.AddDays(1);
                    dtTimeDeparture.Value = DateTime.Today.AddHours(8); // 08:00
                    dtTimeArrival.Value = DateTime.Today.AddHours(10); // 10:00
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadFlightData()
        {
            if (_flight == null) return;

            txtFlightCode.Text = _flight.FlightCode;
            cboAirline.SelectedValue = _flight.AirlineId;
            cboDeparture.SelectedValue = _flight.DepartureAirportId;
            cboArrival.SelectedValue = _flight.ArrivalAirportId;
            cboAircraft.SelectedValue = _flight.AircraftId;
            dtDate.Value = _flight.FlightDate;
            dtTimeDeparture.Value = DateTime.Today.Add(_flight.DepartureTime);
            dtTimeArrival.Value = DateTime.Today.Add(_flight.ArrivalTime);
            numPrice.Value = _flight.BasePrice;
            cbStatus.SelectedItem = _flight.Status;

            // Disable immutable fields if needed, or allow all?
            // User requirement said "Don't edit Route/Airline". 
            // I'll disable them to be safe and match backend logic (which only updates Aircraft/Date/Price)
            // But wait, I updated backend to support AircraftId update.
            // I did NOT update backend to support AirlineId / Route (Airports) update.
            // So these must be disabled.
            
            cboAirline.Enabled = false;
            cboDeparture.Enabled = false;
            cboArrival.Enabled = false;
            txtFlightCode.Enabled = true; // Backend updates FlightNumber
        }

        private async Task LoadDropdownData()
        {
            // Airlines
            var airlines = await DIContainer.LookupService.GetAirlinesAsync();
            cboAirline.DataSource = airlines;
            cboAirline.DisplayMember = "DisplayName";
            cboAirline.ValueMember = "AirlineId";

            // Airports (Load once for both departure and arrival)
            var airports = await DIContainer.LookupService.GetAirportsAsync();
            
            // Clone list for second combo box binding
            var depAirports = new List<AirportSelectDto>(airports);
            var arrAirports = new List<AirportSelectDto>(airports);

            cboDeparture.DataSource = depAirports;
            cboDeparture.DisplayMember = "DisplayName";
            cboDeparture.ValueMember = "AirportId";

            cboArrival.DataSource = arrAirports;
            cboArrival.DisplayMember = "DisplayName";
            cboArrival.ValueMember = "AirportId";

            // Aircrafts
            var aircrafts = await DIContainer.LookupService.GetAircraftsAsync();
            cboAircraft.DataSource = aircrafts;
            cboAircraft.DisplayMember = "Name"; // AircraftSelectDto property
            cboAircraft.ValueMember = "Id"; // AircraftSelectDto property
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            try
            {
                btnSave.Enabled = false;
                btnSave.Text = "Processing...";
                Cursor = Cursors.WaitCursor;

                bool result = false;

                if (_isEditMode)
                {
                    // Update
                    var updateDto = new FlightListDtoAdmin
                    {
                        FlightId = _flight.FlightId,
                        FlightCode = txtFlightCode.Text.Trim().ToUpper(),
                        // Airline/Route unchanged in backend logic, but needed for DTO consistency?
                        // Backend only reads FlightCode, Date, Time, Price, Status, AircraftId from DTO.
                        // So we pass those.
                        AircraftId = (int)cboAircraft.SelectedValue,
                        FlightDate = dtDate.Value.Date,
                        DepartureTime = dtTimeDeparture.Value.TimeOfDay,
                        ArrivalTime = dtTimeArrival.Value.TimeOfDay,
                        BasePrice = numPrice.Value,
                        Status = cbStatus.SelectedItem?.ToString() ?? "Available",
                        TotalSeats = _flight?.TotalSeats ?? 0 // Needed to prevent 'seats > 0' validation error from entity
                    };

                    result = await DIContainer.FlightControllerAdmin.UpdateFlightAsync(updateDto);
                }
                else
                {
                    // Create
                    var dto = new CreateFlightDtoAdmin
                    {
                        FlightCode = txtFlightCode.Text.Trim().ToUpper(),
                        AirlineId = (int)cboAirline.SelectedValue,
                        DepartureAirportId = (int)cboDeparture.SelectedValue,
                        ArrivalAirportId = (int)cboArrival.SelectedValue,
                        AircraftId = (int)cboAircraft.SelectedValue,
                        FlightDate = dtDate.Value.Date,
                        DepartureTime = dtTimeDeparture.Value.TimeOfDay,
                        ArrivalTime = dtTimeArrival.Value.TimeOfDay,
                        BasePrice = numPrice.Value,
                        Status = cbStatus.SelectedItem?.ToString() ?? "Available"
                    };

                    result = await DIContainer.FlightControllerAdmin.CreateFlightAsync(dto);
                }

                if (result)
                {
                    MessageBox.Show(_isEditMode ? "Flight updated successfully!" : "Flight created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(_isEditMode ? "Failed to update flight." : "Failed to create flight.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving flight: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSave.Enabled = true;
                btnSave.Text = "Save Flight";
                Cursor = Cursors.Default;
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtFlightCode.Text))
            {
                ShowError("Please enter Flight Number.");
                txtFlightCode.Focus();
                return false;
            }

            if (cboAirline.SelectedValue == null)
            {
                ShowError("Please select an Airline.");
                cboAirline.Focus();
                return false;
            }

            if (cboDeparture.SelectedValue == null || cboArrival.SelectedValue == null)
            {
                ShowError("Please select Airports.");
                return false;
            }

            if ((int)cboDeparture.SelectedValue == (int)cboArrival.SelectedValue)
            {
                ShowError("Departure and Arrival airports cannot be the same.");
                cboArrival.Focus();
                return false;
            }

            if (cboAircraft.SelectedValue == null)
            {
                ShowError("Please select an Aircraft.");
                cboAircraft.Focus();
                return false;
            }

            if (dtDate.Value.Date < DateTime.Today)
            {
                ShowError("Flight date cannot be in the past.");
                dtDate.Focus();
                return false;
            }
            
            if (numPrice.Value <= 0)
            {
                ShowError("Base Price must be greater than 0.");
                numPrice.Focus();
                return false;
            }
            
            // Check departure vs arrival time
            if (dtTimeDeparture.Value.TimeOfDay >= dtTimeArrival.Value.TimeOfDay)
            {
                 ShowError("Arrival time must be after Departure time (Same-day flights only).");
                 dtTimeArrival.Focus();
                 return false;
            }

            return true;
        }

        private void ShowError(string msg)
        {
            MessageBox.Show(msg, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
