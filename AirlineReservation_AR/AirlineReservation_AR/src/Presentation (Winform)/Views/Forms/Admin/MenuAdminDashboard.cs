using AirlineReservation_AR.src.AirlineReservation.Presentation__Winform_.Views.UCs.Admin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirlineReservation_AR.src.AirlineReservation.Presentation__Winform_.Views.Forms.Admin
{
    public partial class MenuAdminDashboard : Form
    {
        private List<Control> _defaultPanelMain = new List<Control>();
        private UserAccountManagementControl _userControlAccount;
        public event Action<bool> SidebarStateChanged;

        public MenuAdminDashboard()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            _userControlAccount = new UserAccountManagementControl();
            adminDasboardControlUsers.Tag = _userControlAccount;

            SetDoubleBuffered(flowLayoutPanelMenu);
            SetDoubleBuffered(adminDasboardControlDatVe_ThanhToan);
            SetDoubleBuffered(adminDasboardControlGiaVe_KhuyenMai);
            SetDoubleBuffered(adminDasboardControlUsers);
            SetDoubleBuffered(adminDasboardControlSetting);
            RegisterClickEvents(this);
        }
        private void SetDoubleBuffered(Control control)
        {
            typeof(Control).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.SetProperty |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.NonPublic,
                null, control, new object[] { true });
        }
        private void RegisterClickEvents(Control parent)
        {
            foreach (Control ctl in parent.Controls)
            {
                // Gắn sự kiện Click cho control này
                ctl.MouseClick += (s, e) => this.OnMouseClick(e);

                // Gắn cho cấp con nữa (nếu có)
                if (ctl.HasChildren)
                {
                    RegisterClickEvents(ctl);
                }
            }

            // Gắn cho chính UserControl
            this.MouseClick += (s, e) =>
            {
                Console.WriteLine("MouseClicked on adminDasboard");
            };
        }
        bool menuExpand = true;
        private void pictureBoxMenu_Click(object sender, EventArgs e)
        {
            MenuTime.Start();

        }

        private void MenuTime_Tick(object sender, EventArgs e)
        {
            if (menuExpand)
            {
                flowLayoutPanelMenu.Width -= 10;
                adminDasboardControlDatVe_ThanhToan.Location = new Point(adminDasboardControlDatVe_ThanhToan.Location.X + 5, adminDasboardControlDatVe_ThanhToan.Location.Y);
                adminDasboardControlGiaVe_KhuyenMai.Location = new Point(adminDasboardControlGiaVe_KhuyenMai.Location.X + 10, adminDasboardControlGiaVe_KhuyenMai.Location.Y);
                adminDasboardControlUsers.Location = new Point(adminDasboardControlUsers.Location.X + 5, adminDasboardControlUsers.Location.Y);
                adminDasboardControlSetting.Location = new Point(adminDasboardControlSetting.Location.X + 10, adminDasboardControlSetting.Location.Y);
                if (flowLayoutPanelMenu.Width <= 60)
                {
                    MenuTime.Stop();
                    menuExpand = false;
                    SidebarStateChanged?.Invoke(false);
                }
            }
            else
            {
                flowLayoutPanelMenu.Width += 10;
                adminDasboardControlDatVe_ThanhToan.Location = new Point(adminDasboardControlDatVe_ThanhToan.Location.X - 5, adminDasboardControlDatVe_ThanhToan.Location.Y);
                adminDasboardControlGiaVe_KhuyenMai.Location = new Point(adminDasboardControlGiaVe_KhuyenMai.Location.X - 10, adminDasboardControlGiaVe_KhuyenMai.Location.Y);
                adminDasboardControlUsers.Location = new Point(adminDasboardControlUsers.Location.X - 5, adminDasboardControlUsers.Location.Y);
                adminDasboardControlSetting.Location = new Point(adminDasboardControlSetting.Location.X - 10, adminDasboardControlSetting.Location.Y);
                if (flowLayoutPanelMenu.Width >= 240)
                {
                    MenuTime.Stop();
                    menuExpand = true;
                    SidebarStateChanged?.Invoke(true);
                }
            }
        }

        private void SetActiveButton(Guna.UI2.WinForms.Guna2Button activeButton)
        {
            var buttons = new[]
            {
                btnDashboard,
                btnQuanLyChuyenBay,
                btnDatVe_thanhToan,
                btnGiaVe_khuyenMai,
                btnBaoCao_thongKe,
                btnKhachHang,
                btnCaiDat
            };

            foreach (var btn in buttons)
            {
                btn.FillColor = Color.Black;
                btn.ForeColor = Color.White;
            }

            activeButton.FillColor = Color.FromArgb(0, 102, 203);
            activeButton.ForeColor = Color.White;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Guna.UI2.WinForms.Guna2Button button = sender as Guna.UI2.WinForms.Guna2Button;

            if (button != null)
            {
                SetActiveButton(button);

                //if (button == btnDashboard)
                //{
                //    ShowBigControl("Dashboard Tổng quan", Properties.Resources.Dashboard);
                //    panelMain.Visible = true;
                //    panelMain.BringToFront();
                //}
                if (button == btnDashboard)
                {
                    ShowBigControl("Dashboard Tổng quan", Properties.Resources.booking);
                    RestoreDefaultDashboard();
                }
                else if (button == btnKhachHang)
                {
                    ShowBigControl(adminDasboardControlUsers.title, adminDasboardControlUsers.dashboardImage);
                    ShowUserControl(_userControlAccount);
                }
                else if (button.Tag is adminDasboard small)
                {
                    panelMain.Visible = false;
                    ShowBigControl(small.title, small.dashboardImage);
                }
                if (flowLayoutPanelMenu.Width > 60)
                    MenuTime.Start();
            }
            else
            {
                return;
            }
        }
        private void ShowBigControl(string title, Image image)
        {
            panelTopMain.Controls.Clear();

            var big = new AdminDashboardControlHight();
            big.SetCardData(title, image);
            big.Dock = DockStyle.Fill;

            panelTopMain.Controls.Add(big);
        }

        private void adminDasboardControlSmall_click(object sender, EventArgs e)
        {
            if (sender is adminDasboard small)
            {
                AdminDashboardControlHight header = new AdminDashboardControlHight();
                header.SetCardData(small.title, small.dashboardImage);

                panelTopMain.Controls.Clear();

                panelTopMain.Controls.Add(header);

                if (small == adminDasboardControlUsers)
                {
                    ShowUserControl(_userControlAccount);
                    if (flowLayoutPanelMenu.Width > 60)
                        MenuTime.Start();
                    btnKhachHang.FillColor = Color.FromArgb(0, 102, 203);
                }
            }
        }

        private void MenuAdminDashboard_Load(object sender, EventArgs e)
        {
            foreach (Control ctrl in panelMain.Controls)
            {
                _defaultPanelMain.Add(ctrl);
            }

            btnDashboard.Tag = null;
            btnQuanLyChuyenBay.Tag = adminDasboardControlQuanLyChuyenBay;
            btnDatVe_thanhToan.Tag = adminDasboardControlDatVe_ThanhToan;
            btnGiaVe_khuyenMai.Tag = adminDasboardControlGiaVe_KhuyenMai;
            btnBaoCao_thongKe.Tag = adminDasboardControlBaoCao_ThongKe;
            btnKhachHang.Tag = adminDasboardControlUsers;
            btnCaiDat.Tag = adminDasboardControlSetting;

            _userControlAccount = new UserAccountManagementControl();
            _userControlAccount.Dock = DockStyle.Fill;
        }
        private void ShowUserControl(UserControl ctrl)
        {
            panelMain.Controls.Clear();
            ctrl.Dock = DockStyle.Fill;
            panelMain.Controls.Add(ctrl);
            panelMain.Visible = true;
            ctrl.BringToFront();
        }
        private void RestoreDefaultDashboard()
        {
            panelMain.Controls.Clear();

            foreach (Control ctrl in _defaultPanelMain)
            {
                panelMain.Controls.Add(ctrl);
            }

            panelMain.Visible = true;
            panelMain.BringToFront();
        }
    }
}
