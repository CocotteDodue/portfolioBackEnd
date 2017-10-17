using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
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
