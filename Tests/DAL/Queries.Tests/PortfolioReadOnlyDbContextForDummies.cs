using Microsoft.EntityFrameworkCore;
using Portfolio.Tests.Dummies;

namespace Portfolio.DAL.Queries.Dummies
{
    internal class PortfolioReadOnlyDbContextForDummies : PortfolioReadOnlyDbContext
    {
        internal PortfolioReadOnlyDbContextForDummies(DbContextOptions<PortfolioReadOnlyDbContext> options) 
            : base(options)
        { }

        public DbSet<BaseEntityDummy> DummyBase { get; set; }
    }
}
