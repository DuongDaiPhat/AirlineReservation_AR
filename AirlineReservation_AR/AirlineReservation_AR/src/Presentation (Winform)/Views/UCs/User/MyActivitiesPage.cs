using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.User
{
    public partial class MyActivitiesPage : UserControl
    {
        private Guid? _userId;
        private int _currentPage = 1;
        private int _pageSize = 10;
        private int _totalPages = 1;

        public MyActivitiesPage(Guid userId)
        {
            InitializeComponent();
            _userId = userId;
            _currentPage = 1;
            LoadActivities();
        }

        private async void LoadActivities()
        {
            if (_userId == null) return;
            // Lấy tổng số log
            int total = await AirlineReservation_AR.src.AirlineReservation.Infrastructure.Services.AuditLogService.GetByUserAsync(_userId.Value, 1, int.MaxValue).ContinueWith(t => t.Result.Count);
            _totalPages = (int)Math.Ceiling(total / (double)_pageSize);
            lblPageInf.Text = $"Trang {_currentPage}/{_totalPages}";
            previousPageBtn.Enabled = _currentPage > 1;
            nextPageBtn.Enabled = _currentPage < _totalPages;

            // Lấy log trang hiện tại
            var logs = await AirlineReservation_AR.src.AirlineReservation.Infrastructure.Services.AuditLogService.GetByUserAsync(_userId.Value, _currentPage, _pageSize);
            flowPnl.Controls.Clear();
            foreach (var log in logs)
            {
                var activity = new UserActivity();
                activity.SetAuditLog(log);
                flowPnl.Controls.Add(activity);
            }
        }
    }
}
