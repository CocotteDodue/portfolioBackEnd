using Microsoft.EntityFrameworkCore;
using PortfolioBackEnd.Entities;

namespace PortfolioBackEnd
{
    public class PortfolioOperationsDbContext : DbContext
    {
        public PortfolioOperationsDbContext(DbContextOptions<PortfolioOperationsDbContext> options) 
            : base(options)
        { }

        public DbSet<Technology> Technologies { get; set; }
        public DbSet<TechnologyVersion> TechnologiesVersions { get; set; }
    }
}
