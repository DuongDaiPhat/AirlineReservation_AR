using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;

namespace AirlineReservation_AR.src.AirlineReservation.Domain.Services
{
    public interface IAuditLogService
    {
        Task<AuditLog?> GetByIdAsync(long logId);
        Task<IEnumerable<AuditLog>> GetAllAsync();
        Task<IEnumerable<AuditLog>> GetByUserAsync(Guid userId);
        Task<IEnumerable<AuditLog>> GetByTableAsync(string tableName);
        Task<IEnumerable<AuditLog>> GetByRecordAsync(string tableName, string recordId);
        Task<IEnumerable<AuditLog>> GetByDateRangeAsync(DateTime from, DateTime to);

        Task<AuditLog> CreateAsync(AuditLog log);
    }
}
