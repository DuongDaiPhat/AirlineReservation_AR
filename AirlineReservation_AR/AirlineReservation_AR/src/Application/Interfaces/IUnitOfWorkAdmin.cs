using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Application.Interfaces
{
    public interface IUnitOfWorkAdmin : IDisposable
    {
        IFlightPricingRepositoryAdmin FlightPricings { get; }
        IPromotionRepositoryAdmin Promotions { get; }

        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
