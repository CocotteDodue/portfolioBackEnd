using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PortfolioBackEnd.Entities;
using Microsoft.EntityFrameworkCore;

namespace PortfolioBackEnd
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
            return _dbContext.QuerySet<TEntity>();
        }
    }
}
