using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
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
    public partial class UserActivity : UserControl
    {
        public UserActivity()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Đổ dữ liệu AuditLog vào UserActivity (thân thiện UI)
        /// </summary>
        public void SetAuditLog(AuditLog log)
        {
            // Chọn icon
            operationIcon.Image = GetIconForAuditLog(log);

            // Nội dung hành động
            var opText = AirlineReservation_AR.src.AirlineReservation.Infrastructure.Services.AuditLogService.MapOperationToFriendlyText(log.Operation);
            var tableText = AirlineReservation_AR.src.AirlineReservation.Infrastructure.Services.AuditLogService.MapTableNameToFriendlyText(log.TableName);
            var userName = log.User?.FullName ?? "Người dùng";
            var content = $"<b>{userName}</b> {opText} {tableText}";
            if (!string.IsNullOrEmpty(log.RecordId) && log.RecordId != "N/A" && opText != "register" && opText != "login")
                content += $" (ID: {log.RecordId})";

            // Nếu có old/new value
            if (!string.IsNullOrEmpty(log.OldValues) || !string.IsNullOrEmpty(log.NewValues))
            {
                content += "<br><span style='font-size:9pt;'>";
                if (!string.IsNullOrEmpty(log.OldValues))
                    content += $"<b>Trước:</b> {log.OldValues} ";
                if (!string.IsNullOrEmpty(log.NewValues))
                    content += $"<b>Sau:</b> {log.NewValues}";
                content += "</span>";
            }

            Operation.Text = System.Text.RegularExpressions.Regex.Replace(content, "<.*?>", string.Empty); // Nếu không dùng HtmlLabel

            // Thời gian
            time.Text = log.Timestamp.ToLocalTime().ToString("dd/MM/yyyy HH:mm:ss");
        }

        private Image GetIconForAuditLog(AirlineReservation_AR.src.AirlineReservation.Domain.Entities.AuditLog log)
        {
            var op = log.Operation?.ToLower() ?? "";
            // Liên quan tiền bạc
            if (op.Contains("payment") || op.Contains("pay") || op.Contains("wallet") || log.TableName == "Payments" || log.TableName == "PaymentHistory")
                return AirlineReservation_AR.Properties.Resources.wallet;
            // Liên quan user
            if (op.Contains("user") || op.Contains("login") || op.Contains("logout") || op.Contains("register") || log.TableName == "Users")
                return AirlineReservation_AR.Properties.Resources.user;
            // Liên quan máy bay
            if (log.TableName == "Flights" || log.TableName == "BookingFlights" || log.TableName == "FlightPricing")
                return AirlineReservation_AR.Properties.Resources.take_off;
            // Mặc định
            return AirlineReservation_AR.Properties.Resources.task;
        }
    }
}
