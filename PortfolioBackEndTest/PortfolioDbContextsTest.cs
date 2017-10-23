using Microsoft.EntityFrameworkCore;
using PortfolioBackEnd;
using PortfolioBackEndTest.Dummies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace PortfolioBackEndTest
{
    public class PortfolioDbContextsTest
    {
        [Fact]
        public void ReadOnlyContext_ThrowInvalidOperationException_WhenSavingChanges()
        {
            var contextCreationOption = new DbContextOptionsBuilder<PortfolioReadOnlyDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var dbContext = new PortfolioReadOnlyDbContextForDummies(contextCreationOption))
            {
                var ex = Assert.Throws<InvalidOperationException>(() => dbContext.SaveChanges());
            }
        }

        [Fact]
        public void ReadOnlyContext_ThrowInvalidOperationException_WhenRequestForSet()
        {
            var contextCreationOption = new DbContextOptionsBuilder<PortfolioReadOnlyDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var dbContext = new PortfolioReadOnlyDbContextForDummies(contextCreationOption))
            {
                var ex = Assert.Throws<InvalidOperationException>(() => dbContext.Set<BaseEntityDummy>());
            }
        }

        [Fact]
        public void ReadOnlyContext_ProvideIQueryableEntities_WhenRequestForSet()
        {
            var contextCreationOption = new DbContextOptionsBuilder<PortfolioReadOnlyDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var dbContext = new PortfolioReadOnlyDbContextForDummies(contextCreationOption))
            {
                IReadOnlyDatabase queryableDb = new ReadOnlyDatabase(dbContext);

                var set = queryableDb.ReadOnlySet<BaseEntityDummy>();
                
                Assert.IsAssignableFrom<IQueryable>(set);
            }
        }

        [Fact]
        public void ReadOnlyContext_DoNotTrackChange_OnAllQueries()
        {
            var contextCreationOption = new DbContextOptionsBuilder<PortfolioReadOnlyDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var dbContext = new PortfolioReadOnlyDbContext(contextCreationOption))
            {
                Assert.True(QueryTrackingBehavior.NoTracking == dbContext.ChangeTracker.QueryTrackingBehavior);
            }
        }
    }
}
