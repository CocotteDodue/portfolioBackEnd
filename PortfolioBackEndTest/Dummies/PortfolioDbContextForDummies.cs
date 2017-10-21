using PortfolioBackEnd;
using Microsoft.EntityFrameworkCore;

namespace PortfolioBackEndTest.Dummies
{
    internal class PortfolioDbContextForDummies : PortfolioDbContext
    {
        public PortfolioDbContextForDummies(DbContextOptions<PortfolioDbContext> options) : base(options)
        {
        }
        public DbSet<BaseEntityDummy> DummyBase { get; set; }
    }
}
