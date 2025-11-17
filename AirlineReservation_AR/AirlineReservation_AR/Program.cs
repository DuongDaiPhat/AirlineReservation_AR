using AirlineReservation_AR.src.AirlineReservation.Presentation__WinForms_.Views.Forms.Common;
using AirlineReservation_AR.src;
using AirlineReservation_AR.src.AirlineReservation.Shared.Utils;
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
            using var db = Connection.GetDbContext();
            Application.Run(new SignInForm());
        }
    }
}