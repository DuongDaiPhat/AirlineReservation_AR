using AirlineReservation_AR.src.Domain.DTOs;

using AirlineReservation_AR.src.Infrastructure.DI;
using AirlineReservation_AR.src.Application.Interfaces;
using static AirlineReservation_AR.src.Application.Interfaces.IFlightPricingServiceAdmin;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirlineReservation_AR.src.Presentation__Winform_.Views.Forms.Admin
{
    public partial class AddPromotionForm : Form
    {
        private readonly PromotionDtoAdmin? _existingPromo;

        public AddPromotionForm(PromotionDtoAdmin? promo = null)
        {
            InitializeComponent();
            _existingPromo = promo;
            InitializeCustomStyles();
            SetupValidation();

            if (_existingPromo != null)
            {
                LoadPromotionData();
            }
        }

        private void AddLabel(string text, int x, int y)
        {
            var label = new Label
            {
                Text = text,
                Location = new Point(x, y + 5),
                Size = new Size(160, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 58, 64),
                AutoSize = false
            };
            this.Controls.Add(label);
        }

        private void InitializeCustomStyles()
        {
            // Hover effects for buttons
            btnSave.MouseEnter += (s, e) => btnSave.BackColor = Color.FromArgb(33, 136, 56);
            btnSave.MouseLeave += (s, e) => btnSave.BackColor = Color.FromArgb(40, 167, 69);

            btnCancel.MouseEnter += (s, e) => btnCancel.BackColor = Color.FromArgb(90, 98, 104);
            btnCancel.MouseLeave += (s, e) => btnCancel.BackColor = Color.FromArgb(108, 117, 125);
        }

        private void SetupValidation()
        {
            // Real-time validation
            txtPromoCode.TextChanged += (s, e) =>
            {
                txtPromoCode.BackColor = string.IsNullOrWhiteSpace(txtPromoCode.Text)
                    ? Color.FromArgb(255, 243, 243)
                    : Color.White;
            };

            txtPromoName.TextChanged += (s, e) =>
            {
                txtPromoName.BackColor = string.IsNullOrWhiteSpace(txtPromoName.Text)
                    ? Color.FromArgb(255, 243, 243)
                    : Color.White;
            };

            dtpValidFrom.ValueChanged += (s, e) =>
            {
                if (dtpValidTo.Value <= dtpValidFrom.Value)
                {
                    dtpValidTo.Value = dtpValidFrom.Value.AddDays(1);
                }
            };
        }

        private void LoadPromotionData()
        {
            this.Text = "Edit Promotion";
            lblTitle.Text = "Edit Promotion";
            btnSave.Text = "Save Changes";

            txtPromoCode.Text = _existingPromo.PromoCode;
            txtPromoCode.ReadOnly = true;
            txtPromoCode.BackColor = Color.WhiteSmoke;

            txtPromoName.Text = _existingPromo.PromoName;
            txtDescription.Text = _existingPromo.Description;

            // Handle ComboBox Selection safely
            if (cboDiscountType.Items.Contains(_existingPromo.DiscountType))
            {
                cboDiscountType.SelectedItem = _existingPromo.DiscountType;
            }
            cboDiscountType.Enabled = false; 

            numDiscountValue.Value = Math.Min(Math.Max(numDiscountValue.Minimum, _existingPromo.DiscountValue), numDiscountValue.Maximum);

            if (_existingPromo.MinimumAmount.HasValue)
                numMinAmount.Value = Math.Max(numMinAmount.Minimum, _existingPromo.MinimumAmount.Value);

            if (_existingPromo.MaximumDiscount.HasValue)
                numMaxDiscount.Value = Math.Max(numMaxDiscount.Minimum, _existingPromo.MaximumDiscount.Value);

            if (_existingPromo.UsageLimit.HasValue)
                numUsageLimit.Value = Math.Max(numUsageLimit.Minimum, _existingPromo.UsageLimit.Value);

            if (_existingPromo.UserUsageLimit.HasValue)
                numUserUsageLimit.Value = Math.Max(numUserUsageLimit.Minimum, _existingPromo.UserUsageLimit.Value);

            dtpValidFrom.Value = _existingPromo.ValidFrom;
            try { dtpValidTo.Value = _existingPromo.ValidTo; } catch { dtpValidTo.Value = DateTime.Now.AddDays(1); }
        }

        private void CboDiscountType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDiscountType.SelectedItem?.ToString() == "Percent")
            {
                lblDiscountUnit.Text = "%";
                numDiscountValue.Maximum = 100;
                numDiscountValue.Minimum = 1;
                // Avoid setting Value if not necessary to prevent loops or errors
            }
            else
            {
                lblDiscountUnit.Text = "₫";
                numDiscountValue.Maximum = 10000000;
                numDiscountValue.Minimum = 1000;
            }
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
                return;

            try
            {
                btnSave.Enabled = false;
                btnCancel.Enabled = false;
                btnSave.Text = "Processing...";
                Cursor = Cursors.WaitCursor;

                if (_existingPromo == null)
                {
                    await CreatePromotionAsync();
                }
                else
                {
                    await UpdatePromotionAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error saving promotion:\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                btnSave.Enabled = true;
                btnCancel.Enabled = true;
                btnSave.Text = (_existingPromo == null) ? "Create Promotion" : "Save Changes";
                Cursor = Cursors.Default;
            }
        }

        private async Task CreatePromotionAsync()
        {
            var createDto = new CreatePromotionDtoAdmin
            {
                PromoCode = txtPromoCode.Text.Trim().ToUpper(),
                PromoName = txtPromoName.Text.Trim(),
                Description = string.IsNullOrWhiteSpace(txtDescription.Text) ? null : txtDescription.Text.Trim(),
                DiscountType = cboDiscountType.SelectedItem.ToString(),
                DiscountValue = numDiscountValue.Value,
                MinimumAmount = numMinAmount.Value > 0 ? numMinAmount.Value : (decimal?)null,
                MaximumDiscount = numMaxDiscount.Value > 0 ? numMaxDiscount.Value : (decimal?)null,
                UsageLimit = numUsageLimit.Value > 0 ? (int?)numUsageLimit.Value : null,
                UserUsageLimit = numUserUsageLimit.Value > 0 ? (int?)numUserUsageLimit.Value : null,
                ValidFrom = dtpValidFrom.Value.Date,
                ValidTo = dtpValidTo.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59)
            };

            var response = await DIContainer.PromotionControllerAdmin.CreatePromotion(createDto);

            HandleResponse(response, "created", createDto.PromoCode);
        }

        private async Task UpdatePromotionAsync()
        {
            var updateDto = new UpdatePromotionDtoAdmin
            {
                PromotionId = _existingPromo!.PromotionId,
                PromoName = txtPromoName.Text.Trim(),
                Description = string.IsNullOrWhiteSpace(txtDescription.Text) ? null : txtDescription.Text.Trim(),
                DiscountValue = numDiscountValue.Value,
                MinimumAmount = numMinAmount.Value > 0 ? numMinAmount.Value : (decimal?)null,
                MaximumDiscount = numMaxDiscount.Value > 0 ? numMaxDiscount.Value : (decimal?)null,
                UsageLimit = numUsageLimit.Value > 0 ? (int?)numUsageLimit.Value : null,
                // UserUsageLimit ?? UpdateDto doesn't have it? Let's check Dto again.
                // Step 1199: UpdatePromotionDtoAdmin lines 11-21. No UserUsageLimit!
                // So we cannot update UserUsageLimit. I will skip it.
                ValidFrom = dtpValidFrom.Value.Date,
                ValidTo = dtpValidTo.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59),
                IsActive = _existingPromo.IsActive
            };

            var response = await DIContainer.PromotionControllerAdmin.UpdatePromotion(updateDto);

            HandleResponse(response, "updated", _existingPromo.PromoCode);
        }

        private void HandleResponse<T>(ServiceResponse<T> response, string action, string code)
        {
            if (response.Success)
            {
                MessageBox.Show(
                    $"Promotion {code} successfully {action}!",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(
                    $"{response.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private bool ValidateInputs()
        {
            // Promo Code (Only for Create)
            if (_existingPromo == null)
            {
                if (string.IsNullOrWhiteSpace(txtPromoCode.Text))
                {
                    MessageBox.Show("Please enter promotion code!", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPromoCode.Focus();
                    return false;
                }

                if (txtPromoCode.Text.Trim().Length < 4)
                {
                    MessageBox.Show("Promotion code must be at least 4 characters!", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPromoCode.Focus();
                    return false;
                }
            }

            // Promo Name
            if (string.IsNullOrWhiteSpace(txtPromoName.Text))
            {
                MessageBox.Show("Please enter promotion name!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPromoName.Focus();
                return false;
            }

            // Discount Value
            if (numDiscountValue.Value <= 0)
            {
                MessageBox.Show("Discount value must be greater than 0!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numDiscountValue.Focus();
                return false;
            }

            // Validate percent discount
            // Ensure cboDiscountType is selected or use existing
            string type = cboDiscountType.SelectedItem?.ToString() ?? _existingPromo?.DiscountType;
            
            if (type == "Percent" &&
                (numDiscountValue.Value < 1 || numDiscountValue.Value > 100))
            {
                MessageBox.Show("Percent discount must be between 1-100%!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numDiscountValue.Focus();
                return false;
            }

            // Date validation
            if (dtpValidTo.Value <= dtpValidFrom.Value)
            {
                MessageBox.Show("End date must be after start date!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpValidTo.Focus();
                return false;
            }

            return true;
        }
    }
}
