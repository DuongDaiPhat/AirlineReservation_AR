using AirlineReservation_AR.src.Domain.DTOs;
using AirlineReservation_AR.src.Infrastructure.DI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirlineReservation_AR.src.Presentation__Winform_.Views.Forms.Admin
{
    public partial class AddPromotionForm : Form
    {
        public AddPromotionForm()
        {
            InitializeComponent();
            InitializeCustomStyles();
            SetupValidation();
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

        private void CboDiscountType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDiscountType.SelectedItem?.ToString() == "Percent")
            {
                lblDiscountUnit.Text = "%";
                numDiscountValue.Maximum = 100;
                numDiscountValue.Minimum = 1;
                numDiscountValue.Value = Math.Min(numDiscountValue.Value, 100);
            }
            else
            {
                lblDiscountUnit.Text = "₫";
                numDiscountValue.Maximum = 10000000;
                numDiscountValue.Minimum = 1000;
                numDiscountValue.Value = Math.Max(numDiscountValue.Value, 1000);
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
                btnSave.Text = "⏳ Đang xử lý...";
                Cursor = Cursors.WaitCursor;

                // Tạo DTO
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

                // Call API
                var response = await DIContainer.PromotionControllerAdmin.CreatePromotion(createDto);

                if (response.Success)
                {
                    MessageBox.Show(
                        $"✅ {response.Message}\n\nCode: {createDto.PromoCode}\nName: {createDto.PromoName}",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(
                        $"❌ {response.Message}",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"❌ Error creating promotion:\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                btnSave.Enabled = true;
                btnCancel.Enabled = true;
                btnSave.Text = "💾 Create Promotion";
                Cursor = Cursors.Default;
            }
        }

        private bool ValidateInputs()
        {
            // Promo Code
            if (string.IsNullOrWhiteSpace(txtPromoCode.Text))
            {
                MessageBox.Show("⚠️ Please enter promotional code!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPromoCode.Focus();
                return false;
            }

            if (txtPromoCode.Text.Trim().Length < 4)
            {
                MessageBox.Show("⚠️ Promotional code must have at least 4 characters!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPromoCode.Focus();
                return false;
            }

            // Promo Name
            if (string.IsNullOrWhiteSpace(txtPromoName.Text))
            {
                MessageBox.Show("⚠️ Please enter promotion name!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPromoName.Focus();
                return false;
            }

            // Discount Value
            if (numDiscountValue.Value <= 0)
            {
                MessageBox.Show("⚠️ Discount value must be greater than 0!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numDiscountValue.Focus();
                return false;
            }

            // Validate percent discount
            if (cboDiscountType.SelectedItem?.ToString() == "Percent" &&
                (numDiscountValue.Value < 1 || numDiscountValue.Value > 100))
            {
                MessageBox.Show("⚠️ Percent discount value must be from 1-100%!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numDiscountValue.Focus();
                return false;
            }

            // Date validation
            if (dtpValidTo.Value <= dtpValidFrom.Value)
            {
                MessageBox.Show("⚠️ End date must be after start date!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpValidTo.Focus();
                return false;
            }

            // Validate MaxDiscount for Percent type
            if (cboDiscountType.SelectedItem?.ToString() == "Percent" &&
                numMaxDiscount.Value <= 0 && numMinAmount.Value > 0)
            {
                var result = MessageBox.Show(
                    "⚠️ You have not set a maximum discount limit for percent promotion.\n\nThis may cause a loss if the order value is high.\n\nDo you want to continue?",
                    "Warning",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                {
                    numMaxDiscount.Focus();
                    return false;
                }
            }

            return true;
        }
    }
}
