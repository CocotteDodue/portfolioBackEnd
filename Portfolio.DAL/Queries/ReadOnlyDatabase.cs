using Microsoft.EntityFrameworkCore;
using Portfolio.Contracts.Entities;
using System.Linq;

namespace Portfolio.DAL.Queries
{
    public class ReadOnlyDatabase : IReadOnlyDatabase
    {
        private PortfolioReadOnlyDbContext _dbContext;
        public ReadOnlyDatabase(PortfolioReadOnlyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<TEntity> ReadOnlySet<TEntity>() where TEntity : BaseEntity
        {
            return _dbContext.QuerySet<TEntity>().AsNoTracking();
        }
    }
}
