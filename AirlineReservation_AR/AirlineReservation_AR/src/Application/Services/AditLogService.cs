using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using AirlineReservation_AR.src.Infrastructure.DI;
using AirlineReservation_AR.src.AirlineReservation.Presentation__WinForms_.Views.Forms.Common;

namespace AirlineReservation_AR.src.AirlineReservation.Infrastructure.Services
{
    /// <summary>
    /// Static service để lưu audit log của các hành động user trên hệ thống
    /// </summary>
    public static class AuditLogService
    {
        private static AirlineReservationDbContext _dbContext;

        /// <summary>
        /// Khởi tạo DbContext cho service
        /// </summary>
        public static void Initialize(AirlineReservationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Lưu hành động vào audit log
        /// </summary>
        /// <param name="userId">ID của user thực hiện hành động</param>
        /// <param name="tableName">Tên bảng bị ảnh hưởng</param>
        /// <param name="operation">Loại hành động (CREATE, UPDATE, DELETE, LOGIN, LOGOUT, etc)</param>
        /// <param name="recordId">ID của bản ghi</param>
        /// <param name="oldValues">Giá trị cũ (trước khi cập nhật)</param>
        /// <param name="newValues">Giá trị mới (sau khi cập nhật)</param>
        /// <param name="ipAddress">Địa chỉ IP của user</param>
        public static async Task LogActionAsync(
            Guid? userId,
            string tableName,
            string operation,
            string recordId,
            object oldValues = null,
            object newValues = null,
            string ipAddress = null)
        {
            try
            {
                if (_dbContext == null)
                {
                    AnnouncementForm announcementForm1 = new AnnouncementForm();
                    announcementForm1.SetAnnouncement("AuditLogService chưa được khởi tạo", $"Thiếu Initialize", false, null);
                    announcementForm1.Show();
                    return;
                }

                // Lấy thông tin User Agent
                string computerName = Environment.MachineName;
                string osVersion = Environment.OSVersion.ToString();
                string userAgent = $"Machine: {computerName} | OS: {osVersion}";

                // Chuyển đổi object sang JSON string
                string oldValuesJson = oldValues != null ? JsonSerializer.Serialize(oldValues) : null;
                string newValuesJson = newValues != null ? JsonSerializer.Serialize(newValues) : null;

                // Tạo đối tượng AuditLog
                var auditLog = new AuditLog
                {
                    UserId = userId,
                    TableName = tableName,
                    Operation = operation,
                    RecordId = recordId,
                    OldValues = oldValuesJson,
                    NewValues = newValuesJson,
                    Timestamp = DateTime.UtcNow,
                    IpAddress = ipAddress,
                    UserAgent = userAgent
                };

                // Thêm vào database
                _dbContext.AuditLogs.Add(auditLog);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lưu AuditLog: {ex.Message}");
                // Log lỗi ra file hoặc console, không throw exception để không ảnh hưởng đến flow chính
            }
        }

        /// <summary>
        /// Lưu log với method đơn giản hơn (không cần oldValues, newValues)
        /// </summary>
        public static async Task LogSimpleActionAsync(
            Guid? userId,
            string tableName,
            string operation,
            string recordId = null,
            string ipAddress = null)
        {
            await LogActionAsync(userId, tableName, operation, recordId, null, null, ipAddress);
        }

        /// <summary>
        /// Lưu log cần oldValues, newValues
        /// </summary>

        public static async Task LogActionAsync(
            Guid? userId,
            string tableName,
            string operation,
            string recordId = null,
            string oldValue = null,
            string newValue = null,
            string ipAddress = null)
        {
            await LogActionAsync(userId, tableName, operation, recordId, oldValue, newValue, ipAddress);
        }
        /// <summary>
        /// Lấy log theo ID
        /// </summary>
        public static async Task<AuditLog> GetByIdAsync(long logId)
        {
            if (_dbContext == null) return null;

            return await _dbContext.AuditLogs
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.LogId == logId);
        }

        /// <summary>
        /// Lấy tất cả audit logs (sắp xếp theo thời gian giảm dần)
        /// </summary>
        public static async Task<List<AuditLog>> GetAllAsync(int pageNumber = 1, int pageSize = 100)
        {
            if (_dbContext == null) return new List<AuditLog>();

            return await _dbContext.AuditLogs
                .OrderByDescending(a => a.Timestamp)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        /// <summary>
        /// Lấy logs của một user cụ thể
        /// </summary>
        public static async Task<List<AuditLog>> GetByUserAsync(Guid userId, int pageNumber = 1, int pageSize = 50)
        {
            if (_dbContext == null) return new List<AuditLog>();

            return await _dbContext.AuditLogs
                .Where(a => a.UserId == userId)
                .OrderByDescending(a => a.Timestamp)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        /// <summary>
        /// Lấy logs của một bảng cụ thể
        /// </summary>
        public static async Task<List<AuditLog>> GetByTableAsync(string tableName, int pageNumber = 1, int pageSize = 50)
        {
            if (_dbContext == null) return new List<AuditLog>();

            return await _dbContext.AuditLogs
                .Where(a => a.TableName == tableName)
                .OrderByDescending(a => a.Timestamp)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        /// <summary>
        /// Lấy logs của một bản ghi cụ thể
        /// </summary>
        public static async Task<List<AuditLog>> GetByRecordAsync(string tableName, string recordId)
        {
            if (_dbContext == null) return new List<AuditLog>();

            return await _dbContext.AuditLogs
                .Where(a => a.TableName == tableName && a.RecordId == recordId)
                .OrderByDescending(a => a.Timestamp)
                .ToListAsync();
        }

        /// <summary>
        /// Lấy logs trong khoảng thời gian
        /// </summary>
        public static async Task<List<AuditLog>> GetByDateRangeAsync(DateTime from, DateTime to, int pageNumber = 1, int pageSize = 50)
        {
            if (_dbContext == null) return new List<AuditLog>();

            return await _dbContext.AuditLogs
                .Where(a => a.Timestamp >= from && a.Timestamp <= to)
                .OrderByDescending(a => a.Timestamp)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        /// <summary>
        /// Lấy logs theo loại hành động
        /// </summary>
        public static async Task<List<AuditLog>> GetByOperationAsync(string operation, int pageNumber = 1, int pageSize = 50)
        {
            if (_dbContext == null) return new List<AuditLog>();

            return await _dbContext.AuditLogs
                .Where(a => a.Operation == operation)
                .OrderByDescending(a => a.Timestamp)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        /// <summary>
        /// Đếm tổng số audit logs
        /// </summary>
        public static async Task<int> CountAsync()
        {
            if (_dbContext == null) return 0;

            return await _dbContext.AuditLogs.CountAsync();
        }

        /// <summary>
        /// Xóa audit logs cũ (dựa trên ngày)
        /// </summary>
        public static async Task<int> DeleteOldLogsAsync(DateTime beforeDate)
        {
            if (_dbContext == null) return 0;

            var logsToDelete = await _dbContext.AuditLogs
                .Where(a => a.Timestamp < beforeDate)
                .ToListAsync();

            _dbContext.AuditLogs.RemoveRange(logsToDelete);
            return await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Chuyển đổi tên bảng kỹ thuật thành tên thân thiện tiếng Việt
        /// </summary>
        public static string GetTableDisplayName(string tableName)
        {
            if (string.IsNullOrWhiteSpace(tableName))
                return "Không xác định";

            var tableNameMapping = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "BookingFlights", "Chuyến bay đặt" },
                { "BookingServices", "Dịch vụ đặt" },
                { "Flights", "Chuyến bay" },
                { "FlightPricing", "Giá chuyến bay" },
                { "Passengers", "Hành khách" },
                { "Tickets", "Vé máy bay" },
                { "Payments", "Thanh toán" },
                { "PaymentHistory", "Lịch sử thanh toán" },
                { "Users", "Người dùng" },
                { "Bookings", "Đặt chuyến" },
                { "Promotions", "Khuyến mãi" },
                { "Airlines", "Hãng hàng không" },
                { "Airports", "Sân bay" },
                { "Aircraft", "Máy bay" },
                { "SeatClasses", "Hạng ghế" },
                { "Services", "Dịch vụ" }
            };

            return tableNameMapping.ContainsKey(tableName)
                ? tableNameMapping[tableName]
                : tableName;
        }

        /// <summary>
        /// Chuyển đổi loại hành động kỹ thuật thành mô tả thân thiện tiếng Việt
        /// </summary>
        public static string GetOperationDisplayName(string operation)
        {
            if (string.IsNullOrWhiteSpace(operation))
                return "Không xác định";

            var operationMapping = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "create", "Tạo mới" },
                { "update", "Cập nhật" },
                { "delete", "Xóa" },
                { "login", "Đăng nhập" },
                { "logout", "Đăng xuất" },
                { "register", "Đăng ký" },
                { "apply", "Áp dụng" },
                { "calculate", "Tính toán" },
                { "process", "Xử lý" },
                { "complete", "Hoàn thành" },
                { "cancel", "Hủy" },
                { "refund", "Hoàn tiền" },
                { "checkin", "Check-in" },
                { "checkout", "Check-out" }
            };

            return operationMapping.ContainsKey(operation)
                ? operationMapping[operation]
                : operation;
        }

        /// <summary>
        /// Format timestamp thành chuỗi thân thiện với người dùng (Tiếng Việt)
        /// </summary>
        public static string GetFormattedTimestamp(DateTime timestamp)
        {
            var vietnamTime = TimeZoneInfo.ConvertTime(timestamp, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
            
            var now = DateTime.Now;
            var timespan = now - vietnamTime;

            if (timespan.TotalSeconds < 60)
                return "Vừa xong";
            
            if (timespan.TotalMinutes < 60)
                return $"{(int)timespan.TotalMinutes} phút trước";
            
            if (timespan.TotalHours < 24)
                return $"{(int)timespan.TotalHours} giờ trước";
            
            if (timespan.TotalDays < 7)
                return $"{(int)timespan.TotalDays} ngày trước";
            
            if (timespan.TotalDays < 30)
                return $"{(int)timespan.TotalDays / 7} tuần trước";
            
            if (timespan.TotalDays < 365)
                return $"{(int)timespan.TotalDays / 30} tháng trước";
            
            return vietnamTime.ToString("dd/MM/yyyy HH:mm:ss");
        }

        /// <summary>
        /// Format timestamp thành chuỗi đầy đủ (Tiếng Việt)
        /// </summary>
        public static string GetFormattedTimestampFull(DateTime timestamp)
        {
            var vietnamTime = TimeZoneInfo.ConvertTime(timestamp, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
            return vietnamTime.ToString("dd/MM/yyyy HH:mm:ss");
        }

        /// <summary>
        /// Lấy tên người dùng từ ID (tạm thời trả về ID nếu không tìm thấy user)
        /// </summary>
        public static string GetUserDisplayName(Guid? userId)
        {
            if (!userId.HasValue || userId == Guid.Empty)
                return "Hệ thống";

            try
            {
                if (_dbContext == null)
                    return userId.ToString();

                var user = _dbContext.Users.FirstOrDefault(u => u.UserId == userId);
                if (user != null)
                    return !string.IsNullOrWhiteSpace(user.FullName) ? user.FullName : user.Email;
            }
            catch { }

            return userId.ToString()[..8];
        }

        /// <summary>
        /// Trích xuất thông tin máy tính từ UserAgent
        /// </summary>
        public static Dictionary<string, string> ParseUserAgent(string userAgent)
        {
            var result = new Dictionary<string, string>
            {
                { "MachineName", "Không xác định" },
                { "OSVersion", "Không xác định" }
            };

            if (string.IsNullOrWhiteSpace(userAgent))
                return result;

            try
            {
                var parts = userAgent.Split('|');
                foreach (var part in parts)
                {
                    var kv = part.Trim().Split(':');
                    if (kv.Length == 2)
                    {
                        result[kv[0].Trim()] = kv[1].Trim();
                    }
                }
            }
            catch { }

            return result;
        }

        /// <summary>
        /// Tạo DTO hiển thị cho UI từ AuditLog (thân thiện, dễ đọc)
        /// </summary>
        public class AuditLogDisplayDTO
        {
            public long LogId { get; set; }
            public string UserName { get; set; }
            public string TableDisplayName { get; set; }
            public string OperationDisplayName { get; set; }
            public string RecordId { get; set; }
            public string OldValues { get; set; }
            public string NewValues { get; set; }
            public string Timestamp { get; set; }
            public string TimestampFull { get; set; }
            public string IpAddress { get; set; }
            public string MachineName { get; set; }
            public string OSVersion { get; set; }
            public string Summary { get; set; }
        }

        /// <summary>
        /// Chuyển đổi AuditLog thành DTO hiển thị
        /// </summary>
        public static AuditLogDisplayDTO ConvertToDisplayDTO(AuditLog log)
        {
            if (log == null)
                return null;

            var userAgentInfo = ParseUserAgent(log.UserAgent);

            var displayDTO = new AuditLogDisplayDTO
            {
                LogId = log.LogId,
                UserName = GetUserDisplayName(log.UserId),
                TableDisplayName = GetTableDisplayName(log.TableName),
                OperationDisplayName = GetOperationDisplayName(log.Operation),
                RecordId = log.RecordId ?? "N/A",
                OldValues = log.OldValues ?? "Không có",
                NewValues = log.NewValues ?? "Không có",
                Timestamp = GetFormattedTimestamp(log.Timestamp),
                TimestampFull = GetFormattedTimestampFull(log.Timestamp),
                IpAddress = log.IpAddress ?? "N/A",
                MachineName = userAgentInfo.ContainsKey("Machine") ? userAgentInfo["Machine"] : "Không xác định",
                OSVersion = userAgentInfo.ContainsKey("OS") ? userAgentInfo["OS"] : "Không xác định",
                Summary = $"{GetOperationDisplayName(log.Operation)} {GetTableDisplayName(log.TableName)} (ID: {log.RecordId})"
            };

            return displayDTO;
        }

        /// <summary>
        /// Chuyển đổi danh sách AuditLog thành danh sách DTO hiển thị
        /// </summary>
        public static List<AuditLogDisplayDTO> ConvertToDisplayDTOs(List<AuditLog> logs)
        {
            return logs?.Select(ConvertToDisplayDTO).Where(x => x != null).ToList() ?? new List<AuditLogDisplayDTO>();
        }

        /// <summary>
        /// Lấy tất cả logs và chuyển đổi thành DTO hiển thị
        /// </summary>
        public static async Task<List<AuditLogDisplayDTO>> GetAllDisplayAsync(int pageNumber = 1, int pageSize = 100)
        {
            var logs = await GetAllAsync(pageNumber, pageSize);
            return ConvertToDisplayDTOs(logs);
        }

        /// <summary>
        /// Lấy logs của user cụ thể và chuyển đổi thành DTO hiển thị
        /// </summary>
        public static async Task<List<AuditLogDisplayDTO>> GetByUserDisplayAsync(Guid userId, int pageNumber = 1, int pageSize = 50)
        {
            var logs = await GetByUserAsync(userId, pageNumber, pageSize);
            return ConvertToDisplayDTOs(logs);
        }

        /// <summary>
        /// Lấy logs của bảng cụ thể và chuyển đổi thành DTO hiển thị
        /// </summary>
        public static async Task<List<AuditLogDisplayDTO>> GetByTableDisplayAsync(string tableName, int pageNumber = 1, int pageSize = 50)
        {
            var logs = await GetByTableAsync(tableName, pageNumber, pageSize);
            return ConvertToDisplayDTOs(logs);
        }

        /// <summary>
        /// Lấy logs của bản ghi cụ thể và chuyển đổi thành DTO hiển thị
        /// </summary>
        public static async Task<List<AuditLogDisplayDTO>> GetByRecordDisplayAsync(string tableName, string recordId)
        {
            var logs = await GetByRecordAsync(tableName, recordId);
            return ConvertToDisplayDTOs(logs);
        }

        /// <summary>
        /// Lấy logs trong khoảng thời gian và chuyển đổi thành DTO hiển thị
        /// </summary>
        public static async Task<List<AuditLogDisplayDTO>> GetByDateRangeDisplayAsync(DateTime from, DateTime to, int pageNumber = 1, int pageSize = 50)
        {
            var logs = await GetByDateRangeAsync(from, to, pageNumber, pageSize);
            return ConvertToDisplayDTOs(logs);
        }

        /// <summary>
        /// Lấy logs theo loại hành động và chuyển đổi thành DTO hiển thị
        /// </summary>
        public static async Task<List<AuditLogDisplayDTO>> GetByOperationDisplayAsync(string operation, int pageNumber = 1, int pageSize = 50)
        {
            var logs = await GetByOperationAsync(operation, pageNumber, pageSize);
            return ConvertToDisplayDTOs(logs);
        }
    }
}
