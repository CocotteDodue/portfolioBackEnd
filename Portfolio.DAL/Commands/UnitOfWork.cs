using Microsoft.EntityFrameworkCore;
using Portfolio.Contracts.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.DAL.Commands
{
    internal class UnitOfWork : IUnitOfWork
    {
        private PortfolioOperationsDbContext _dbContext;

        public UnitOfWork(PortfolioOperationsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<int> CommitAsync()
        {
            var transactionTime = DateTime.Now;

            SetCreationTime(transactionTime);
            SetLastModificationTime(transactionTime);
            HandleDeleteEntities();

            // EF Core provide atomic SaveChange (if provider supports transaction)
            return _dbContext.SaveChangesAsync();
        }

        private void HandleDeleteEntities()
        {
            var entityToBeDeleted = _dbContext.ChangeTracker
                            .Entries()
                            .Where(x => x.State == EntityState.Deleted)
                            .ToList();
            entityToBeDeleted.ForEach(entry =>
            {
                ((BaseEntity)entry.Entity).IsDeleted = true;
                entry.State = EntityState.Modified;
            });
        }

        private void SetLastModificationTime(DateTime transactionTime)
        {
            var modifiedEntities = _dbContext.ChangeTracker
                .Entries()
                .Where(x => x.State == EntityState.Modified
                            || x.State == EntityState.Deleted)
                .ToList();
            modifiedEntities.ForEach(entry => ((BaseEntity)entry.Entity).LastModificationTime = transactionTime);
        }

        private void SetCreationTime(DateTime transactionTime)
        {
            var addedEntities = _dbContext.ChangeTracker
                .Entries()
                .Where(x => x.State == EntityState.Added)
                .ToList();
            addedEntities.ForEach(entry => ((BaseEntity)entry.Entity).CreationTime = transactionTime);
        }
    }
}
