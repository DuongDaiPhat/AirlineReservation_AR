using AirlineReservation_AR.src;
using AirlineReservation_AR.src.AirlineReservation.Presentation__WinForms_.Views.Forms.Common;
using AirlineReservation_AR.src.AirlineReservation.Shared.Utils;
using AirlineReservation_AR.src.Infrastructure.DI;
using AirlineReservation_AR.src.Presentation__Winform_.Helpers;
using AirlineReservation_AR.src.Presentation__Winform_.Views.Forms.User;
using AR_Winform.Presentation.Forms;
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
            DIContainer.Init();
            Application.Run(new SignInForm());
        }
    }
}