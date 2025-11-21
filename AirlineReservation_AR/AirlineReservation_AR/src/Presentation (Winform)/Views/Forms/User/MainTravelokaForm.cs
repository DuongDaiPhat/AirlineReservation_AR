using AirlineReservation_AR.src.Domain.DTOs;
using AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.User;

namespace AirlineReservation_AR
{
    public partial class MainTravelokaForm : Form
    {
        public MainTravelokaForm()
        {
            InitializeComponent();

        }

        private void LoadResultPage(FlightSearchParams p)
        {
            var resultUC = new UC_FlightSearchResult(p);
            resultUC.Dock = DockStyle.Fill;

            bodyPanel.Controls.Clear();
            bodyPanel.Controls.Add(resultUC);
        }

        public void SwitchScreen(UserControl next)
        {
            bodyPanel.Controls.Clear();
            next.Dock = DockStyle.Fill;
            bodyPanel.Controls.Add(next);
        }

        private void MainTravelokaForm_Load(object sender, EventArgs e)
        {
            ucFlightSearch.OnSearchSubmit += LoadResultPage;
        }
    }
}
