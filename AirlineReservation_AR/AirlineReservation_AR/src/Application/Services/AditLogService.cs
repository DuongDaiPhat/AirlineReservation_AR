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
            string? ipAddress = null)
        {
            try
            {
                // Validate dữ liệu đầu vào
                if (string.IsNullOrWhiteSpace(tableName))
                {
                    Console.WriteLine("Lỗi: tableName không được trống");
                    return;
                }

                if (string.IsNullOrWhiteSpace(operation))
                {
                    Console.WriteLine("Lỗi: operation không được trống");
                    return;
                }

                // Lấy thông tin User Agent
                string computerName = Environment.MachineName;
                string osVersion = Environment.OSVersion.ToString();
                string userAgent = $"Machine: {computerName} | OS: {osVersion}";

                // Chuyển đổi object sang JSON string
                string oldValuesJson = null;
                string newValuesJson = null;

                try
                {
                    oldValuesJson = oldValues != null ? JsonSerializer.Serialize(oldValues) : null;
                    newValuesJson = newValues != null ? JsonSerializer.Serialize(newValues) : null;
                }
                catch (Exception jsonEx)
                {
                    Console.WriteLine($"Lỗi serialize JSON: {jsonEx.Message}");
                    oldValuesJson = oldValues?.ToString();
                    newValuesJson = newValues?.ToString();
                }

                // Tạo đối tượng AuditLog
                var auditLog = new AuditLog
                {
                    UserId = userId,
                    TableName = tableName,
                    Operation = operation,
                    RecordId = recordId ?? "N/A",
                    OldValues = oldValuesJson,
                    NewValues = newValuesJson,
                    Timestamp = DateTime.UtcNow,
                    IpAddress = ipAddress ?? "",
                    UserAgent = userAgent
                };

                // TẠO DbContext RIÊNG cho audit log để tránh xung đột transaction
                using (var auditDbContext = DIContainer.CreateDb())
                {
                    auditDbContext.AuditLogs.Add(auditLog);
                    int savedCount = await auditDbContext.SaveChangesAsync();

                    if (savedCount > 0)
                    {
                        Console.WriteLine($"AuditLog lưu thành công: {tableName} - {operation} - {recordId}");
                    }
                    else
                    {
                        Console.WriteLine("Cảnh báo: AuditLog không được lưu (savedCount = 0)");
                    }
                }
            }
            catch (DbUpdateException dbEx)
            {
                string errorMsg = $"Lỗi Database:\n" +
                    $"Message: {dbEx.Message}\n" +
                    $"Inner Exception: {dbEx.InnerException?.Message}\n" +
                    $"Table: {tableName}, Operation: {operation}, RecordId: {recordId}\n\n" +
                    $"Entries in error:\n";

                if (dbEx.Entries != null)
                {
                    foreach (var entry in dbEx.Entries)
                    {
                        errorMsg += $"- Entity: {entry.Entity.GetType().Name}, State: {entry.State}\n";
                    }
                }

                AnnouncementForm form = new AnnouncementForm();
                form.SetAnnouncement("Lỗi lưu Database - Audit Log", errorMsg, false, null);
                form.Show();
                form.Focus();

                Console.WriteLine($"DbUpdateException: {errorMsg}");
            }
            catch (Exception ex)
            {
                string errorMsg = $"Lỗi hệ thống:\n" +
                    $"Type: {ex.GetType().Name}\n" +
                    $"Message: {ex.Message}\n" +
                    $"StackTrace: {ex.StackTrace}";

                AnnouncementForm form = new AnnouncementForm();
                form.SetAnnouncement("Lỗi hệ thống - Audit Log", errorMsg, false, null);
                form.Show();
                form.Focus();

                Console.WriteLine($"Exception: {errorMsg}");
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
        /// Lưu log không async (fire-and-forget) - dùng khi đã có transaction mở
        /// </summary>
        public static void LogSimpleAction(
            Guid? userId,
            string tableName,
            string operation,
            string recordId = null,
            string ipAddress = null)
        {
            try
            {
                Console.WriteLine($"[AuditLog] Bắt đầu LogSimpleAction: {tableName} - {operation} - {recordId}");

                // Validate dữ liệu đầu vào
                if (string.IsNullOrWhiteSpace(tableName))
                {
                    Console.WriteLine("[AuditLog] Lỗi: tableName không được trống");
                    return;
                }

                if (string.IsNullOrWhiteSpace(operation))
                {
                    Console.WriteLine("[AuditLog] Lỗi: operation không được trống");
                    return;
                }

                // Lấy thông tin User Agent
                string computerName = Environment.MachineName;
                string osVersion = Environment.OSVersion.ToString();
                string userAgent = $"Machine: {computerName} | OS: {osVersion}";

                // Tạo đối tượng AuditLog
                var auditLog = new AuditLog
                {
                    UserId = userId,
                    TableName = tableName,
                    Operation = operation,
                    RecordId = recordId ?? "N/A",
                    OldValues = null,
                    NewValues = null,
                    Timestamp = DateTime.UtcNow,
                    IpAddress = ipAddress ?? "",
                    UserAgent = userAgent
                };

                Console.WriteLine($"[AuditLog] Tạo AuditLog object thành công");

                // TẠO DbContext RIÊNG cho audit log
                using (var auditDbContext = DIContainer.CreateDb())
                {
                    Console.WriteLine($"[AuditLog] DbContext được tạo");

                    // Kiểm tra xem DbSet có được register không
                    var dbSet = auditDbContext.AuditLogs;
                    if (dbSet == null)
                    {
                        Console.WriteLine("[AuditLog] Lỗi: AuditLogs DbSet không được register trong DbContext");
                        return;
                    }

                    Console.WriteLine($"[AuditLog] DbSet<AuditLog> tồn tại");

                    auditDbContext.AuditLogs.Add(auditLog);
                    Console.WriteLine($"[AuditLog] AuditLog được add vào DbContext");

                    int savedCount = auditDbContext.SaveChanges();
                    Console.WriteLine($"[AuditLog] SaveChanges() trả về: {savedCount}");

                    if (savedCount > 0)
                    {
                        Console.WriteLine($"✓ [AuditLog] Lưu thành công: {tableName} - {operation} - {recordId}");
                    }
                    else
                    {
                        Console.WriteLine("[AuditLog] Cảnh báo: SaveChanges trả về 0");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[AuditLog] Lỗi: {ex.GetType().Name}");
                Console.WriteLine($"[AuditLog] Message: {ex.Message}");
                Console.WriteLine($"[AuditLog] StackTrace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"[AuditLog] InnerException: {ex.InnerException.Message}");
                }
            }
        }

        /// <summary>
        /// Lấy log theo ID
        /// </summary>
        public static async Task<AuditLog> GetByIdAsync(long logId)
        {
            using (var db = DIContainer.CreateDb())
            {
                return await db.AuditLogs
                    .Include(a => a.User)
                    .FirstOrDefaultAsync(a => a.LogId == logId);
            }
        }

        /// <summary>
        /// Lấy tất cả audit logs (sắp xếp theo thời gian giảm dần)
        /// </summary>
        public static async Task<List<AuditLog>> GetAllAsync(int pageNumber = 1, int pageSize = 100)
        {
            using (var db = DIContainer.CreateDb())
            {
                return await db.AuditLogs
                    .OrderByDescending(a => a.Timestamp)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }
        }

        /// <summary>
        /// Lấy logs của một user cụ thể
        /// </summary>
        public static async Task<List<AuditLog>> GetByUserAsync(Guid userId, int pageNumber = 1, int pageSize = 50)
        {
            using (var db = DIContainer.CreateDb())
            {
                return await db.AuditLogs
                    .Where(a => a.UserId == userId)
                    .OrderByDescending(a => a.Timestamp)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }
        }

        /// <summary>
        /// Lấy logs của một bảng cụ thể
        /// </summary>
        public static async Task<List<AuditLog>> GetByTableAsync(string tableName, int pageNumber = 1, int pageSize = 50)
        {
            using (var db = DIContainer.CreateDb())
            {
                return await db.AuditLogs
                    .Where(a => a.TableName == tableName)
                    .OrderByDescending(a => a.Timestamp)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }
        }

        /// <summary>
        /// Lấy logs của một bản ghi cụ thể
        /// </summary>
        public static async Task<List<AuditLog>> GetByRecordAsync(string tableName, string recordId)
        {
            using (var db = DIContainer.CreateDb())
            {
                return await db.AuditLogs
                    .Where(a => a.TableName == tableName && a.RecordId == recordId)
                    .OrderByDescending(a => a.Timestamp)
                    .ToListAsync();
            }
        }

        /// <summary>
        /// Lấy logs trong khoảng thời gian
        /// </summary>
        public static async Task<List<AuditLog>> GetByDateRangeAsync(DateTime from, DateTime to, int pageNumber = 1, int pageSize = 50)
        {
            using (var db = DIContainer.CreateDb())
            {
                return await db.AuditLogs
                    .Where(a => a.Timestamp >= from && a.Timestamp <= to)
                    .OrderByDescending(a => a.Timestamp)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }
        }

        /// <summary>
        /// Lấy logs theo loại hành động
        /// </summary>
        public static async Task<List<AuditLog>> GetByOperationAsync(string operation, int pageNumber = 1, int pageSize = 50)
        {
            using (var db = DIContainer.CreateDb())
            {
                return await db.AuditLogs
                    .Where(a => a.Operation == operation)
                    .OrderByDescending(a => a.Timestamp)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }
        }

        /// <summary>
        /// Đếm tổng số audit logs
        /// </summary>
        public static async Task<int> CountAsync()
        {
            using (var db = DIContainer.CreateDb())
            {
                return await db.AuditLogs.CountAsync();
            }
        }

        /// <summary>
        /// Xóa audit logs cũ (dựa trên ngày)
        /// </summary>
        public static async Task<int> DeleteOldLogsAsync(DateTime beforeDate)
        {
            using (var db = DIContainer.CreateDb())
            {
                var logsToDelete = await db.AuditLogs
                    .Where(a => a.Timestamp < beforeDate)
                    .ToListAsync();

                db.AuditLogs.RemoveRange(logsToDelete);
                return await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Kiểm tra kết nối database và bảng AuditLog
        /// </summary>
        public static async Task<(bool Success, string Message)> TestConnectionAsync()
        {
            try
            {
                using (var db = DIContainer.CreateDb())
                {
                    // Kiểm tra kết nối
                    await db.Database.OpenConnectionAsync();
                    await db.Database.CloseConnectionAsync();
                    
                    // Kiểm tra bảng AuditLog
                    int count = await db.AuditLogs.CountAsync();
                    
                    return (true, $"✓ Kết nối database thành công. Số logs hiện có: {count}");
                }
            }
            catch (Exception ex)
            {
                return (false, $"✗ Lỗi kết nối: {ex.Message}\n{ex.InnerException?.Message}");
            }
        }

        /// <summary>
        /// Tạo log test để kiểm tra
        /// </summary>
        public static async Task<(bool Success, string Message)> TestLogCreationAsync()
        {
            try
            {
                var testLog = new AuditLog
                {
                    UserId = Guid.NewGuid(),
                    TableName = "TestTable",
                    Operation = "test",
                    RecordId = "test-1",
                    OldValues = null,
                    NewValues = null,
                    Timestamp = DateTime.UtcNow,
                    IpAddress = "",
                    UserAgent = "Test Agent"
                };

                using (var db = DIContainer.CreateDb())
                {
                    db.AuditLogs.Add(testLog);
                    int result = await db.SaveChangesAsync();
                    
                    if (result > 0)
                    {
                        // Xóa log test
                        var createdLog = await db.AuditLogs
                            .Where(a => a.TableName == "TestTable" && a.RecordId == "test-1")
                            .FirstOrDefaultAsync();
                        
                        if (createdLog != null)
                        {
                            db.AuditLogs.Remove(createdLog);
                            await db.SaveChangesAsync();
                        }
                        
                        return (true, "Lưu audit log test thành công");
                    }
                    else
                    {
                        return (false, "Không thể lưu audit log (savedCount = 0)");
                    }
                }
            }
            catch (DbUpdateException dbEx)
            {
                string msg = $"Lỗi Database:\n{dbEx.Message}\n";
                if (dbEx.InnerException != null)
                    msg += $"Inner: {dbEx.InnerException.Message}";
                return (false, msg);
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi: {ex.GetType().Name}\n{ex.Message}");
            }
        }
    }
}
