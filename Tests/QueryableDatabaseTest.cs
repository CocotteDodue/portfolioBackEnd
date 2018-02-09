using Microsoft.EntityFrameworkCore;
using Portfolio.DAL.Queries;
using PortfolioBackEndTest.Dummies;
using System;
using System.Linq;
using Xunit;

namespace PortfolioBackEndTest
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
