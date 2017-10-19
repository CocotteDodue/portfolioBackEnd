using Microsoft.EntityFrameworkCore;
using PortfolioBackEnd.Entities;

namespace PortfolioBackEnd
{
    public class PortfolioDbContext : DbContext
    {
        public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options) : base(options)
        {
        }

        public DbSet<Technology> Technologies { get; set; }
        public DbSet<TechnologyVersion> TechnologiesVersions { get; set; }
    }
}
