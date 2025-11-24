using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Context;
using AirlineReservation_AR.src.Application.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Application.Services
{
    public class UnitOfWorkAdmin : IUnitOfWorkAdmin
    {
        private readonly AirlineReservationDbContext _context;
        private IDbContextTransaction? _transaction;

        public IFlightPricingRepositoryAdmin FlightPricings { get; }
        public IPromotionRepositoryAdmin Promotions { get; }

        public UnitOfWorkAdmin(
            AirlineReservationDbContext context,
            IFlightPricingRepositoryAdmin flightPricingRepository,
            IPromotionRepositoryAdmin promotionRepository)
        {
            _context = context;
            FlightPricings = flightPricingRepository;
            Promotions = promotionRepository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                if (_transaction != null)
                {
                    await _transaction.CommitAsync();
                }
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
            finally
            {
                if (_transaction != null)
                {
                    await _transaction.DisposeAsync();
                    _transaction = null;
                }
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }
    }
}
