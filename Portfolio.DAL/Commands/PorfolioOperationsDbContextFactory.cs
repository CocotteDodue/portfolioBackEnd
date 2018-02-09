using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Portfolio.DAL.Commands
{
    public class PorfolioOperationsDbContextFactory : IDesignTimeDbContextFactory<PortfolioOperationsDbContext>
    {
        public PortfolioOperationsDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PortfolioOperationsDbContext>();
            optionsBuilder.UseSqlServer(@"Server=(local)\sqlexpress;Database=PortfolioBackEnd;Trusted_Connection=True;");

            return new PortfolioOperationsDbContext(optionsBuilder.Options);
        }
    }
}
