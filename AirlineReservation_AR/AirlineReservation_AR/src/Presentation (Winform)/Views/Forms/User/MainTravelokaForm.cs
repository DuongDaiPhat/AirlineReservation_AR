using AirlineReservation_AR.src.Domain.DTOs;
using AirlineReservation_AR.src.Infrastructure.DI;
using AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.User;

namespace AirlineReservation_AR
{
    public partial class MainTravelokaForm : Form
    {
        public MainTravelokaForm()
        {
            InitializeComponent();

            
        }
        private void MainTravelokaForm_Load(object sender, EventArgs e)
        {
            //ucFlightSearch.OnSearchSubmit += LoadResultPage;
            ucHeader.MyTicketClick += LoadUserDashboard;
            ucHeader.BookingClick += LoadSearchPage;
            ucHeader.HomeClick += LoadHomePage;
            ucHeader.PromotionClick += LoadPromotionPage;

            // Mặc định load trang tìm kiếm
            LoadSearchPage();
        }

        private void LoadResultPage(FlightSearchParams p)
        {
            var resultUC = new UC_FlightSearchResult(p);
            resultUC.Dock = DockStyle.Fill;

            bodyPanel.Controls.Clear();
            bodyPanel.Controls.Add(resultUC);
        }

        private void LoadHomePage()
        {
            var homeForm = new MainTravelokaForm();
            homeForm.Show();
            this.Close(); 
        }

        private void LoadUserDashboard(UserDTO user)
        {
            var dashboard = new UserDashboard(user);
            DashBoardInit(dashboard);
            SwitchScreen(dashboard);
        }

        private void LoadSearchPage()
        {
            var search = new UC_FlightSearch();
            search.OnSearchSubmit += LoadResultPage; 
            SwitchScreen(search);
        }

        private void LoadPromotionPage()
        {
            var promoUC = new UC_Promotion();
            SwitchScreen(promoUC);   
        }


        public void SwitchScreen(UserControl next)
        {
            bodyPanel.Controls.Clear();
            next.Dock = DockStyle.Fill;
            bodyPanel.Controls.Add(next);
        }
        private void LoadUserDashboardWithMyBookings(UserDTO user)
        {
            var dashboard = new UserDashboard(user);
            DashBoardInit(dashboard);
            dashboard.LoadMyBookingPage();
            SwitchScreen(dashboard);
        }
        private void LoadUserDashboardWithMyPurchaseList(UserDTO user)
        {
            var dashboard = new UserDashboard(user);
            DashBoardInit(dashboard);
            dashboard.LoadMyPurchaseList();
            SwitchScreen(dashboard);

        }
        private void LoadUserDashboardWithAccountModified(UserDTO user)
        {
            var dashboard = new UserDashboard(user);
            DashBoardInit(dashboard);
            dashboard.LoadMyAccountModify();
            SwitchScreen(dashboard);
        }
        private void UpdatedUI()
        {
            //ucHeader.LoadUI();
        }
        private void HandleLogout()
        {
            var result = MessageBox.Show(
                "Are your sure want to log out?",
                "Confirm Logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.No) return;

            DIContainer.SetCurrentUser(null);

            PerformLogout();
        }
        private void PerformLogout()
        {
            Application.Restart();

            Environment.Exit(0);
        }

        private void DashBoardInit(UserDashboard dashboard)
        {
            dashboard.UpdatedUIRequest += UpdatedUI;
            dashboard.LogoutRequest += HandleLogout;
        }
    }
}
