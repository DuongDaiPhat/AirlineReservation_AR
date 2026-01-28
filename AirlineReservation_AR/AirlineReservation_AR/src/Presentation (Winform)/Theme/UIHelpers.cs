using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace AirlineReservation_AR.src.Presentation__Winform_.Theme
{
    /// <summary>
    /// UI Helper methods for consistent styling across Admin dashboard
    /// </summary>
    public static class UIHelpers
    {
        #region Button Styling

        /// <summary>
        /// Apply primary button style (filled blue)
        /// </summary>
        public static void ApplyPrimaryStyle(Guna2Button button)
        {
            button.FillColor = UIConstants.PrimaryActive;
            button.ForeColor = UIConstants.TextLight;
            button.Font = UIConstants.FontButton;
            button.BorderRadius = UIConstants.RadiusButton;
            button.Animated = false; // Disable for performance
        }

        /// <summary>
        /// Apply secondary button style (outlined)
        /// </summary>
        public static void ApplySecondaryStyle(Guna2Button button)
        {
            button.FillColor = Color.Transparent;
            button.ForeColor = UIConstants.PrimaryActive;
            button.BorderColor = UIConstants.PrimaryActive;
            button.BorderThickness = 1;
            button.Font = UIConstants.FontButton;
            button.BorderRadius = UIConstants.RadiusButton;
            button.Animated = false;
        }

        /// <summary>
        /// Apply danger button style (red)
        /// </summary>
        public static void ApplyDangerStyle(Guna2Button button)
        {
            button.FillColor = UIConstants.Danger;
            button.ForeColor = UIConstants.TextLight;
            button.Font = UIConstants.FontButton;
            button.BorderRadius = UIConstants.RadiusButton;
            button.Animated = false;
        }

        /// <summary>
        /// Apply success button style (green)
        /// </summary>
        public static void ApplySuccessStyle(Guna2Button button)
        {
            button.FillColor = UIConstants.Success;
            button.ForeColor = UIConstants.TextLight;
            button.Font = UIConstants.FontButton;
            button.BorderRadius = UIConstants.RadiusButton;
            button.Animated = false;
        }

        /// <summary>
        /// Apply sidebar menu button style
        /// </summary>
        public static void ApplySidebarButtonStyle(Guna2Button button, bool isActive = false)
        {
            button.FillColor = isActive ? UIConstants.PrimaryActive : UIConstants.SidebarBg;
            button.ForeColor = UIConstants.TextLight;
            button.Font = UIConstants.FontMenu;
            button.BorderRadius = 0;
            button.ImageAlign = HorizontalAlignment.Left;
            button.TextAlign = HorizontalAlignment.Left;
            button.Animated = false;
        }

        #endregion

        #region Panel Styling

        /// <summary>
        /// Apply card panel style (white background with shadow)
        /// </summary>
        public static void ApplyCardStyle(Panel panel)
        {
            panel.BackColor = UIConstants.CardBg;
            panel.Padding = new Padding(UIConstants.PaddingStandard);
        }

        /// <summary>
        /// Apply card style to Guna2Panel
        /// </summary>
        public static void ApplyCardStyle(Guna2Panel panel)
        {
            panel.FillColor = UIConstants.CardBg;
            panel.BorderRadius = UIConstants.RadiusCard;
            panel.ShadowDecoration.Enabled = true;
            panel.ShadowDecoration.Depth = 10;
            panel.ShadowDecoration.Color = Color.FromArgb(20, 0, 0, 0);
        }

        /// <summary>
        /// Apply content area style (light gray background)
        /// </summary>
        public static void ApplyContentAreaStyle(Panel panel)
        {
            panel.BackColor = UIConstants.ContentBg;
            panel.Padding = new Padding(UIConstants.PaddingLarge);
        }

        /// <summary>
        /// Apply sidebar style
        /// </summary>
        public static void ApplySidebarStyle(Panel panel)
        {
            panel.BackColor = UIConstants.SidebarBg;
            panel.Width = UIConstants.SidebarExpandedWidth;
        }

        #endregion

        #region DataGridView Styling

        /// <summary>
        /// Apply standard DataGridView styling for Admin
        /// </summary>
        public static void ApplyDataGridStyle(DataGridView dgv)
        {
            // General settings
            dgv.BackgroundColor = UIConstants.CardBg;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = UIConstants.Divider;
            dgv.RowHeadersVisible = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Header style
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = UIConstants.ContentBg;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = UIConstants.TextPrimary;
            dgv.ColumnHeadersDefaultCellStyle.Font = UIConstants.FontButton;
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = UIConstants.ContentBg;
            dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = UIConstants.TextPrimary;
            dgv.ColumnHeadersHeight = 45;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            // Row style
            dgv.DefaultCellStyle.BackColor = UIConstants.CardBg;
            dgv.DefaultCellStyle.ForeColor = UIConstants.TextPrimary;
            dgv.DefaultCellStyle.Font = UIConstants.FontGrid;
            dgv.DefaultCellStyle.SelectionBackColor = UIConstants.PrimaryLight;
            dgv.DefaultCellStyle.SelectionForeColor = UIConstants.TextPrimary;
            dgv.DefaultCellStyle.Padding = new Padding(8, 4, 8, 4);
            dgv.RowTemplate.Height = 40;

            // Alternating row style
            dgv.AlternatingRowsDefaultCellStyle.BackColor = UIConstants.ContentBg;
        }

        #endregion

        #region TextBox Styling

        /// <summary>
        /// Apply standard input style
        /// </summary>
        public static void ApplyInputStyle(Guna2TextBox textBox)
        {
            textBox.BorderRadius = UIConstants.RadiusInput;
            textBox.BorderColor = UIConstants.Border;
            textBox.FocusedState.BorderColor = UIConstants.PrimaryActive;
            textBox.Font = UIConstants.FontBody;
            textBox.ForeColor = UIConstants.TextPrimary;
            textBox.PlaceholderForeColor = UIConstants.TextSecondary;
        }

        /// <summary>
        /// Apply search input style (with icon)
        /// </summary>
        public static void ApplySearchStyle(Guna2TextBox textBox)
        {
            ApplyInputStyle(textBox);
            textBox.BorderRadius = UIConstants.RadiusLarge;
            textBox.IconLeft = Properties.Resources.search_white ?? null;
            textBox.IconLeftSize = new Size(18, 18);
        }

        #endregion

        #region ComboBox Styling

        /// <summary>
        /// Apply standard combobox style
        /// </summary>
        public static void ApplyComboBoxStyle(Guna2ComboBox comboBox)
        {
            comboBox.BorderRadius = UIConstants.RadiusInput;
            comboBox.BorderColor = UIConstants.Border;
            comboBox.FocusedState.BorderColor = UIConstants.PrimaryActive;
            comboBox.Font = UIConstants.FontBody;
            comboBox.ForeColor = UIConstants.TextPrimary;
            comboBox.ItemHeight = 30;
        }

        #endregion

        #region Label Styling

        /// <summary>
        /// Apply page title style
        /// </summary>
        public static void ApplyTitleStyle(Label label)
        {
            label.Font = UIConstants.FontTitle;
            label.ForeColor = UIConstants.TextPrimary;
        }

        /// <summary>
        /// Apply section header style
        /// </summary>
        public static void ApplyHeaderStyle(Label label)
        {
            label.Font = UIConstants.FontHeader;
            label.ForeColor = UIConstants.TextPrimary;
        }

        /// <summary>
        /// Apply body text style
        /// </summary>
        public static void ApplyBodyStyle(Label label)
        {
            label.Font = UIConstants.FontBody;
            label.ForeColor = UIConstants.TextSecondary;
        }

        #endregion

        #region Status Badge

        /// <summary>
        /// Get status color based on status string
        /// </summary>
        public static Color GetStatusColor(string status)
        {
            return status?.ToLower() switch
            {
                "confirmed" or "completed" or "active" or "available" => UIConstants.Success,
                "pending" or "processing" => UIConstants.Warning,
                "cancelled" or "failed" or "inactive" => UIConstants.Danger,
                "refunded" => UIConstants.Info,
                _ => UIConstants.TextSecondary
            };
        }

        /// <summary>
        /// Get status background color
        /// </summary>
        public static Color GetStatusBgColor(string status)
        {
            return status?.ToLower() switch
            {
                "confirmed" or "completed" or "active" or "available" => UIConstants.SuccessLight,
                "pending" or "processing" => UIConstants.WarningLight,
                "cancelled" or "failed" or "inactive" => UIConstants.DangerLight,
                "refunded" => UIConstants.InfoLight,
                _ => UIConstants.ContentBg
            };
        }

        #endregion

        #region Animation Helpers

        /// <summary>
        /// Animate sidebar expansion/collapse
        /// </summary>
        public static void AnimateSidebar(Panel sidebar, bool expand, System.Windows.Forms.Timer timer)
        {
            int targetWidth = expand ? UIConstants.SidebarExpandedWidth : UIConstants.SidebarCollapsedWidth;
            
            timer.Interval = UIConstants.AnimationInterval;
            timer.Tick += (s, e) =>
            {
                if (expand)
                {
                    sidebar.Width += UIConstants.AnimationStep;
                    if (sidebar.Width >= targetWidth)
                    {
                        sidebar.Width = targetWidth;
                        timer.Stop();
                    }
                }
                else
                {
                    sidebar.Width -= UIConstants.AnimationStep;
                    if (sidebar.Width <= targetWidth)
                    {
                        sidebar.Width = targetWidth;
                        timer.Stop();
                    }
                }
            };
            timer.Start();
        }

        #endregion

        #region Form Helpers

        /// <summary>
        /// Enable double buffering for smoother UI
        /// </summary>
        public static void EnableDoubleBuffering(Control control)
        {
            typeof(Control).GetProperty("DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance)
                ?.SetValue(control, true, null);
        }

        /// <summary>
        /// Apply standard form styling
        /// </summary>
        public static void ApplyFormStyle(Form form)
        {
            form.BackColor = UIConstants.ContentBg;
            form.Font = UIConstants.FontBody;
            EnableDoubleBuffering(form);
        }

        #endregion
    }
}
