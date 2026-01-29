using AirlineReservation_AR.src;
using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Services;
using AirlineReservation_AR.src.AirlineReservation.Presentation__WinForms_.Views.Forms.Common;
using AirlineReservation_AR.src.AirlineReservation.Shared.Utils;
using AirlineReservation_AR.src.Infrastructure.DI;
using AirlineReservation_AR.src.Presentation__Winform_.Helpers;
using AirlineReservation_AR.src.Presentation__Winform_.Views.Forms.User;
using AirlineReservation_AR.src.Shared.Helper;
using AirlineReservation_AR.src.Shared.Utils;
using AR_Winform.Presentation.Forms;
using System;
using System.IO;
using System.Windows.Forms;

namespace AirlineReservation_AR
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            
            // Check if already licensed
            if (TrialManager.IsLicensed())
            {
                // Licensed - run normally
                DIContainer.Init();
                var signInForm = new SignInForm();
                TrialManager.SetupTrialTimer(signInForm); // Will show [Licensed] in title
                Application.Run(signInForm);
                TrialManager.Dispose();
                return;
            }
            
            // Trial mode
            TrialManager.StartTrialSession();
            
            // Show trial welcome message with option to enter license
            var result = MessageBox.Show(
                $"Welcome to the Trial Version!\n\n" +
                $"You have {TrialManager.TRIAL_MINUTES} minutes to explore.\n" +
                $"Remaining time will be shown in the window title.\n\n" +
                $"Do you have a license key? Click YES to enter it now.",
                "Trial Version",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information);
            
            if (result == DialogResult.Yes)
            {
                if (TrialManager.ShowLicenseInputDialog())
                {
                    MessageBox.Show(
                        "License activated successfully!\nThank you for your purchase.",
                        "License Activated",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            
            DIContainer.Init();
            
            // Create and setup main form with trial timer
            var signInFormTrial = new SignInForm();
            TrialManager.SetupTrialTimer(signInFormTrial);
            
            Application.Run(signInFormTrial);
            
            // Cleanup
            TrialManager.Dispose();
        }
    }
}
