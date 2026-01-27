using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.AirlineReservation.Domain.Services;
using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AirlineReservation_AR.src.AirlineReservation.Infrastructure.Services
{
    public class AuditLogService : IAuditLogService
    {
        private readonly AirlineReservationDbContext _db;
        public static Guid? CurrentUserID { get; set; }

        public AuditLogService(AirlineReservationDbContext context)
        {
            _db = context;
        }

        public static async Task LogActionAsync(string tableName, string operation, string recordId, object oldValues = null, object newValues = null)
        {
            try
            {
                // 1. Lấy thông tin môi trường
                string computerName = Environment.MachineName;
                string osVersion = Environment.OSVersion.ToString();
                string userAgent = $"Machine: {computerName} | OS: {osVersion}";

                // 2. Khởi tạo đối tượng Log (Theo cấu trúc bảng của bạn)
                using (var db = new YourDataContext()) // Thay bằng tên DataContext của bạn
                {
                    var log = new AuditLog
                    {
                        // LogID là bigint Identity nên không cần gán
                        UserId = CurrentUserID,
                        TableName = tableName,
                        Operation = operation,
                        RecordId = recordId,
                        OldValues = oldValues != null ? oldValues.SerializeObject(oldValues) : null,

                        NewValues = newValues != null ? newValues?.SerializeObject(newValues) : null,
                        Timestamp = DateTime.Now,
                        IpAddress = ipAddress,
                        UserAgent = userAgent
                    };

                    db.AuditLogs.InsertOnSubmit(log);

                    // Sử dụng Task.Run để SubmitChanges không chặn luồng chính
                    await Task.Run(() => db.SubmitChanges());
                }
            }
            catch (Exception ex)
            {
                // Log lỗi ra file text cục bộ nếu không lưu được vào DB
                Console.WriteLine("Audit Log Error: " + ex.Message);
            }
        }
        public async Task<AuditLog?> GetByIdAsync(long logId)
        {
            return await _db.AuditLogs
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.LogId == logId);
        }

        public async Task<IEnumerable<AuditLog>> GetAllAsync()
        {
            return await _db.AuditLogs
                .OrderByDescending(a => a.Timestamp)
                .ToListAsync();
        }

        public async Task<IEnumerable<AuditLog>> GetByUserAsync(Guid userId)
        {
            return await _db.AuditLogs
                .Where(a => a.UserId == userId)
                .OrderByDescending(a => a.Timestamp)
                .ToListAsync();
        }

        public async Task<IEnumerable<AuditLog>> GetByTableAsync(string tableName)
        {
            return await _db.AuditLogs
                .Where(a => a.TableName == tableName)
                .OrderByDescending(a => a.Timestamp)
                .ToListAsync();
        }

        public async Task<IEnumerable<AuditLog>> GetByRecordAsync(string tableName, string recordId)
        {
            return await _db.AuditLogs
                .Where(a => a.TableName == tableName && a.RecordId == recordId)
                .OrderByDescending(a => a.Timestamp)
                .ToListAsync();
        }

        public async Task<IEnumerable<AuditLog>> GetByDateRangeAsync(DateTime from, DateTime to)
        {
            return await _db.AuditLogs
                .Where(a => a.Timestamp >= from && a.Timestamp <= to)
                .OrderByDescending(a => a.Timestamp)
                .ToListAsync();
        }

        public async Task<AuditLog> CreateAsync(AuditLog log)
        {
            await _db.AuditLogs.AddAsync(log);
            await _db.SaveChangesAsync();
            return log;
        }
    }
}
