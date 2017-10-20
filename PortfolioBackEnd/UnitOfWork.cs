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
        public void Commit()
        {
            
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
