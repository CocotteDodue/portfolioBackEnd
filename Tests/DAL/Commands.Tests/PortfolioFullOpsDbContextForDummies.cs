using Microsoft.EntityFrameworkCore;
using Portfolio.Tests.Dummies;

namespace Portfolio.DAL.Commands.Dummies
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
