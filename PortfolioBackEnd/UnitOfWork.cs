using Microsoft.EntityFrameworkCore;
using PortfolioBackEnd.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioBackEnd
{
    public class UnitOfWork : IUnitOfWork
    {
        private PortfolioDbContext _dbContext;

        public UnitOfWork(PortfolioDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<int> CommitAsync()
        {
            var changeTime = DateTime.Now;

            var addedEntities = _dbContext.ChangeTracker
                .Entries()
                .Where(x => x.State == EntityState.Added)
                .ToList();

            addedEntities.ForEach(entry => ((BaseEntity)entry.Entity).CreationTime = changeTime);

            return _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
