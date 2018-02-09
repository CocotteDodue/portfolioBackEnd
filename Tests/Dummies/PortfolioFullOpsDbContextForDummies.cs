using Microsoft.EntityFrameworkCore;
using Portfolio.DAL.Commands;

namespace PortfolioBackEndTest.Dummies
{
    internal class PortfolioFullOpsDbContextForDummies : PortfolioOperationsDbContext
    {
        internal PortfolioFullOpsDbContextForDummies(DbContextOptions<PortfolioOperationsDbContext> options) 
            : base(options)
        {
        }
        public DbSet<BaseEntityDummy> DummyBase { get; set; }
    }
}
