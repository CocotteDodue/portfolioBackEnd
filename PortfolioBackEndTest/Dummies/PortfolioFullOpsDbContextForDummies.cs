using PortfolioBackEnd;
using Microsoft.EntityFrameworkCore;

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
