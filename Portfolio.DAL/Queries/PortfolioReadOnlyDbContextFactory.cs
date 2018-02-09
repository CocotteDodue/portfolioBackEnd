using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Portfolio.DAL.Queries
{
    public class PortfolioReadOnlyDbContextFactory : IDesignTimeDbContextFactory<PortfolioReadOnlyDbContext>
    {
        public PortfolioReadOnlyDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PortfolioReadOnlyDbContext>();
            optionsBuilder.UseSqlServer(@"Server=(local)\\sqlexpress;Database=PortfolioBackEnd;Trusted_Connection=True;");

            return new PortfolioReadOnlyDbContext(optionsBuilder.Options);

        }
    }
}
