using Microsoft.EntityFrameworkCore;
using Portfolio.Contracts.Entities;

namespace Portfolio.DAL.Commands
{
    public class OperationDatabase : IOperationsDatabase
    {
        PortfolioOperationsDbContext _dbContext;

        public OperationDatabase(PortfolioOperationsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public DbSet<TEntity> DbSet<TEntity>() where TEntity : BaseEntity
        {
            return _dbContext.Set<TEntity>();
        }
    }
}
