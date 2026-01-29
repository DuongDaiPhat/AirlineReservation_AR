using Microsoft.Win32;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace AirlineReservation_AR.src.Shared.Utils
{
    /// <summary>
    /// Manages trial license with session-based time limit and license activation
    /// </summary>
    public static class TrialManager
    {
        // Trial duration in minutes
        public const int TRIAL_MINUTES = 15;
        
        // Registry paths
        private const string REG_KEY = @"SOFTWARE\AirlineReservation";
        private const string REG_LICENSE = "LicenseKey";
        
        // License secret (change this for your app)
        private const string LICENSE_SECRET = "AR2024-SECRET-KEY";
        
        // Session start time
        private static DateTime _sessionStartTime;
        private static System.Windows.Forms.Timer? _trialTimer;
        private static Form? _mainForm;
        private static bool _isLicensed = false;
        
        /// <summary>
        /// Check if app is licensed (call this first)
        /// </summary>
        public static bool IsLicensed()
        {
            try
            {
                using var key = Registry.CurrentUser.OpenSubKey(REG_KEY);
                if (key != null)
                {
                    var storedLicense = key.GetValue(REG_LICENSE) as string;
                    if (!string.IsNullOrEmpty(storedLicense) && ValidateLicenseKey(storedLicense))
                    {
                        _isLicensed = true;
                        return true;
                    }
                }
            }
            catch { }
            
            return false;
        }
        
        /// <summary>
        /// Initialize trial session - call this at app startup
        /// </summary>
        public static void StartTrialSession()
        {
            _sessionStartTime = DateTime.Now;
        }
        
        /// <summary>
        /// Setup auto-close timer on main form
        /// </summary>
        public static void SetupTrialTimer(Form mainForm)
        {
            _mainForm = mainForm;
            
            // Skip timer if licensed
            if (_isLicensed) return;
            
            // Timer checks every 30 seconds
            _trialTimer = new System.Windows.Forms.Timer();
            _trialTimer.Interval = 30 * 1000; // 30 seconds
            _trialTimer.Tick += TrialTimer_Tick;
            _trialTimer.Start();
            
            // Show remaining time in title
            UpdateFormTitle();
        }
        
        private static void TrialTimer_Tick(object? sender, EventArgs e)
        {
            if (_isLicensed)
            {
                _trialTimer?.Stop();
                return;
            }
            
            if (IsSessionExpired())
            {
                _trialTimer?.Stop();
                ShowExpiredMessageWithLicenseOption();
            }
            else
            {
                UpdateFormTitle();
                
                // Warning at 2 minutes remaining
                int remaining = GetRemainingMinutes();
                if (remaining == 2)
                {
                    MessageBox.Show(
                        $"Warning: {remaining} minutes remaining!\nPlease save your work.",
                        "Trial Warning",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
        }
        
        /// <summary>
        /// Check if trial session has expired
        /// </summary>
        public static bool IsSessionExpired()
        {
            if (_isLicensed) return false;
            var elapsed = DateTime.Now - _sessionStartTime;
            return elapsed.TotalMinutes >= TRIAL_MINUTES;
        }
        
        /// <summary>
        /// Get remaining minutes in trial
        /// </summary>
        public static int GetRemainingMinutes()
        {
            var elapsed = DateTime.Now - _sessionStartTime;
            var remaining = TRIAL_MINUTES - (int)elapsed.TotalMinutes;
            return Math.Max(0, remaining);
        }
        
        /// <summary>
        /// Get remaining time as formatted string
        /// </summary>
        public static string GetRemainingTimeString()
        {
            var elapsed = DateTime.Now - _sessionStartTime;
            var remaining = TimeSpan.FromMinutes(TRIAL_MINUTES) - elapsed;
            
            if (remaining.TotalSeconds <= 0)
                return "00:00";
                
            return $"{(int)remaining.TotalMinutes:D2}:{remaining.Seconds:D2}";
        }
        
        private static void UpdateFormTitle()
        {
            if (_mainForm != null && !_mainForm.IsDisposed)
            {
                var baseTitle = _mainForm.Text.Split(new[] { " [" }, StringSplitOptions.None)[0];
                
                if (_isLicensed)
                {
                    _mainForm.Text = $"{baseTitle} [Licensed]";
                }
                else
                {
                    var remaining = GetRemainingTimeString();
                    _mainForm.Text = $"{baseTitle} [Trial: {remaining}]";
                }
            }
        }
        
        private static void ShowExpiredMessageWithLicenseOption()
        {
            var result = MessageBox.Show(
                "Your 15-minute trial session has ended!\n\n" +
                "Click YES to enter a license key.\n" +
                "Click NO to exit the application.",
                "Trial Expired",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Stop);
            
            if (result == DialogResult.Yes)
            {
                if (ShowLicenseInputDialog())
                {
                    // License activated - restart timer check
                    _isLicensed = true;
                    UpdateFormTitle();
                    MessageBox.Show(
                        "License activated successfully!\nThank you for your purchase.",
                        "License Activated",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }
            }
            
            System.Windows.Forms.Application.Exit();
        }
        
        /// <summary>
        /// Show license input dialog
        /// </summary>
        public static bool ShowLicenseInputDialog()
        {
            using var form = new Form();
            form.Text = "Enter License Key";
            form.Size = new System.Drawing.Size(400, 150);
            form.StartPosition = FormStartPosition.CenterScreen;
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.MaximizeBox = false;
            form.MinimizeBox = false;
            
            var label = new Label();
            label.Text = "Please enter your license key:";
            label.Location = new System.Drawing.Point(20, 20);
            label.AutoSize = true;
            
            var textBox = new TextBox();
            textBox.Location = new System.Drawing.Point(20, 45);
            textBox.Size = new System.Drawing.Size(340, 25);
            
            var btnActivate = new Button();
            btnActivate.Text = "Activate";
            btnActivate.Location = new System.Drawing.Point(180, 80);
            btnActivate.Size = new System.Drawing.Size(80, 30);
            btnActivate.DialogResult = DialogResult.OK;
            
            var btnCancel = new Button();
            btnCancel.Text = "Cancel";
            btnCancel.Location = new System.Drawing.Point(280, 80);
            btnCancel.Size = new System.Drawing.Size(80, 30);
            btnCancel.DialogResult = DialogResult.Cancel;
            
            form.Controls.AddRange(new Control[] { label, textBox, btnActivate, btnCancel });
            form.AcceptButton = btnActivate;
            form.CancelButton = btnCancel;
            
            if (form.ShowDialog() == DialogResult.OK)
            {
                string licenseKey = textBox.Text.Trim().ToUpper();
                
                if (ValidateLicenseKey(licenseKey))
                {
                    SaveLicenseKey(licenseKey);
                    return true;
                }
                else
                {
                    MessageBox.Show(
                        "Invalid license key!\nPlease check and try again.",
                        "Invalid Key",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            
            return false;
        }
        
        /// <summary>
        /// Validate license key format: AR-XXXX-XXXX-XXXX
        /// </summary>
        private static bool ValidateLicenseKey(string key)
        {
            if (string.IsNullOrEmpty(key)) return false;
            
            // Simple format check: AR-XXXX-XXXX-XXXX
            key = key.ToUpper().Trim();
            
            // Check format
            if (!key.StartsWith("AR-")) return false;
            
            var parts = key.Split('-');
            if (parts.Length != 4) return false;
            
            // Verify checksum (last 4 chars should be hash of first parts)
            var baseKey = $"{parts[0]}-{parts[1]}-{parts[2]}";
            var expectedChecksum = GenerateChecksum(baseKey);
            
            return parts[3] == expectedChecksum;
        }
        
        /// <summary>
        /// Generate a simple checksum for license validation
        /// </summary>
        private static string GenerateChecksum(string input)
        {
            var combined = input + LICENSE_SECRET;
            using var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(combined));
            // Take first 4 chars of hex hash
            return BitConverter.ToString(hash).Replace("-", "").Substring(0, 4);
        }
        
        /// <summary>
        /// Generate a valid license key (for admin use)
        /// </summary>
        public static string GenerateLicenseKey()
        {
            var random = new Random();
            var part1 = random.Next(1000, 9999).ToString();
            var part2 = random.Next(1000, 9999).ToString();
            var baseKey = $"AR-{part1}-{part2}";
            var checksum = GenerateChecksum(baseKey);
            return $"{baseKey}-{checksum}";
        }
        
        private static void SaveLicenseKey(string key)
        {
            try
            {
                using var regKey = Registry.CurrentUser.CreateSubKey(REG_KEY);
                regKey?.SetValue(REG_LICENSE, key);
            }
            catch { }
        }
        
        /// <summary>
        /// Cleanup timer resources
        /// </summary>
        public static void Dispose()
        {
            _trialTimer?.Stop();
            _trialTimer?.Dispose();
            _trialTimer = null;
        }
    }
}
