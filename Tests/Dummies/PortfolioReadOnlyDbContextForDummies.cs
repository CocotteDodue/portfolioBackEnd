using Microsoft.EntityFrameworkCore;
using Portfolio.DAL.Queries;

namespace PortfolioBackEndTest.Dummies
{
    internal class PortfolioReadOnlyDbContextForDummies : PortfolioReadOnlyDbContext
    {
        internal PortfolioReadOnlyDbContextForDummies(DbContextOptions<PortfolioReadOnlyDbContext> options) 
            : base(options)
        { }

        public DbSet<BaseEntityDummy> DummyBase { get; set; }
    }
}
