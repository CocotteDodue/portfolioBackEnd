using Microsoft.EntityFrameworkCore;
using Portfolio.DAL.Queries.Dummies;
using Portfolio.Tests.Dummies;
using System;
using System.Linq;
using Xunit;

namespace Portfolio.DAL.Queries
{
    public class QueryableDatabaseTest
    {
        [Fact]
        public void Database_ExposesReadOnlySet_ForRequestedType()
        {
            var contextCreationOption = new DbContextOptionsBuilder<PortfolioReadOnlyDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var dbContext = new PortfolioReadOnlyDbContextForDummies(contextCreationOption))
            {
                IReadOnlyDatabase queryableDb = new ReadOnlyDatabase(dbContext);

                var set = queryableDb.ReadOnlySet<BaseEntityDummy>();

                var setIsQueryable = set is IQueryable;
                var setIsDbset = set is DbSet<BaseEntityDummy>;
                Assert.True(setIsQueryable && !setIsDbset);
            }
        }
    }
}
