using Microsoft.EntityFrameworkCore;
using Portfolio.Contracts.Entities;

namespace Portfolio.DAL.Commands
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
