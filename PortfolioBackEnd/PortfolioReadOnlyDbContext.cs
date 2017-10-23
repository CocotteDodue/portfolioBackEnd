using Microsoft.EntityFrameworkCore;
using PortfolioBackEnd.Entities;
using System;
using System.Linq;

namespace PortfolioBackEnd
{
    public class PortfolioReadOnlyDbContext : DbContext
    {
        public PortfolioReadOnlyDbContext(DbContextOptions<PortfolioReadOnlyDbContext> options) 
            : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public override int SaveChanges()
        {
            throw new InvalidOperationException("ReadOnly Context");
        }
        
        public IQueryable<TEntity> QuerySet<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public override DbSet<TEntity> Set<TEntity>()
        {
            throw new InvalidOperationException("ReadOnly Context, Full DbSet unavailable");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Needed because of no exposed DbSet<T> property
            modelBuilder.Entity<Technology>();
            modelBuilder.Entity<TechnologyVersion>();
        }
    }
}
