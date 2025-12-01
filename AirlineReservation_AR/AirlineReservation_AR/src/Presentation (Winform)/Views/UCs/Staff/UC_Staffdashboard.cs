using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.Staff
{
    public partial class UC_Staffdashboard : UserControl
    {
        private readonly System.Windows.Forms.Timer _sidebarTimer;
        private int _expandedWidth;
        private int _collapsedWidth = 46;    // nhỏ hơn → thu gọn nhiều hơn, thu gọn còn 46px
        private int _targetWidth;
        private const int Step = 10;    // bước nhỏ → chuyển động mượt
        private const int Interval = 10;    // tick nhanh hơn → cảm giác “nhanh” hơn
        private UserControl _currentContent;

        public UC_Staffdashboard()
        {
            InitializeComponent();

            _expandedWidth = dashboardPnl.Width;
            _targetWidth = _expandedWidth;

            _sidebarTimer = new System.Windows.Forms.Timer { Interval = Interval };
            _sidebarTimer.Tick += SidebarTimer_Tick;


            ShowContent(new UC_Dashboard());

            contentPnl.Resize += contentPnl_Resize;
        }

        // Hiển thị nội dung tương ứng khi load
        private void ShowContent(UserControl uc)
        {
            if (_currentContent != null)
            {
                contentPnl.Controls.Remove(_currentContent);
                _currentContent.Dispose();
            }

            _currentContent = uc;

            _currentContent.Dock = DockStyle.None;          // RẤT QUAN TRỌNG: KHÔNG Dock Fill
            _currentContent.Anchor = AnchorStyles.Top;      // chỉ bám phía trên
            _currentContent.Top = 0;                        // tuỳ bạn muốn cách top bao nhiêu

            contentPnl.Controls.Add(_currentContent);
            CenterCurrentContent();                         // căn giữa lần đầu
        }

        private void contentPnl_Resize(object sender, EventArgs e)
        {
            CenterCurrentContent();
        }

        //  Căn giữa nội dung hiện tại trong contentPnl
        private void CenterCurrentContent()
        {
            if (_currentContent == null) return;

            int x = (contentPnl.ClientSize.Width - _currentContent.Width) / 2;
            if (x < 0) x = 0;

            // giữ nguyên Top, chỉ căn giữa theo chiều ngang
            _currentContent.Left = x;
        }

        // Mở/đóng sidebar với hiệu ứng mượt
        public void ToggleSidebar()
        {
            if (_sidebarTimer.Enabled) return;

            bool isCollapsed = dashboardPnl.Width <= _collapsedWidth + 2;
            _targetWidth = isCollapsed ? _expandedWidth : _collapsedWidth;

            _sidebarTimer.Start();
        }

        // Xử lý sự kiện Tick của Timer để thu/phóng sidebar
        private void SidebarTimer_Tick(object? sender, EventArgs e)
        {
            int cur = dashboardPnl.Width;
            int diff = _targetWidth - cur;

            if (Math.Abs(diff) <= Step)
            {
                dashboardPnl.Width = _targetWidth;
                UpdateContentPadding();     // Cập nhật padding khi hoàn thành
                _sidebarTimer.Stop();
                return;
            }

            dashboardPnl.Width += Math.Sign(diff) * Step;
            UpdateContentPadding();     // Cập nhật padding trong quá trình thu/phóng
        }

        // Cập nhật padding của contentPnl để giữ mép trái cố định
        private void UpdateContentPadding()
        {
            // Giữ mép trái nội dung cố định như khi sidebar full
            int leftPadding = Math.Max(0, _expandedWidth - dashboardPnl.Width);
            contentPnl.Padding = new Padding(leftPadding, 0, 0, 0);
        }

        private void SetActiveButton(Guna.UI2.WinForms.Guna2Button activeBtn)
        {
            // nếu sử dụng button thường thì đổi type cho đúng
            dashboarBtn.Checked = (dashboarBtn == activeBtn);
            checkinBtn.Checked = (checkinBtn == activeBtn);
            progressBtn.Checked = (progressBtn == activeBtn);
            reportBtn.Checked = (reportBtn == activeBtn);
        }

        private void dashboarBtn_Click(object sender, EventArgs e)
        {
            SetActiveButton(dashboarBtn);
            ShowContent(new UC_Dashboard());
        }

        private void checkinBtn_Click(object sender, EventArgs e)
        {
            SetActiveButton(checkinBtn);
            ShowContent(new UC_CheckIn());
        }

        private void progressBtn_Click(object sender, EventArgs e)
        {
            SetActiveButton(progressBtn);
            ShowContent(new UC_Progress());
        }

        private void reportBtn_Click(object sender, EventArgs e)
        {
            SetActiveButton(reportBtn);
            ShowContent(new UC_Report());
        }
    }
}
