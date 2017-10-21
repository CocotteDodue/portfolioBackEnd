using Microsoft.EntityFrameworkCore;
using Moq;
using PortfolioBackEnd;
using PortfolioBackEnd.Queries;
using PortfolioBackEndTest.Dummies;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PortfolioBackEndTest
{
    public class QueryBaseTest
    {
        [Fact]
        public void Query_ReadsOnlyDatabaseAccess_AtCreation()
        {
            var fakeContext = new Mock<PortfolioReadOnlyDbContext>(new DbContextOptions<PortfolioReadOnlyDbContext>());
            IReadOnlyDatabase queryableDatabase = new ReadOnlyDatabase(fakeContext.Object);

            IQuery<BaseEntityDummy> dummyQuery = new QueryBaseDummy<BaseEntityDummy>(queryableDatabase);

            Assert.NotNull(dummyQuery);
        }

        [Fact]
        public void Query_ProvidesQueryable_WhenRequested()
        {
            var fakeQueryableDatabase = new Mock<IReadOnlyDatabase>();
            fakeQueryableDatabase
                .Setup(fakeDb => fakeDb.ReadOnlySet<BaseEntityDummy>())
                .Returns(Enumerable.Empty<BaseEntityDummy>().AsQueryable());

            IQuery<BaseEntityDummy> dummyQuery = new QueryBaseDummy<BaseEntityDummy>(fakeQueryableDatabase.Object);
            var dbQuery = dummyQuery.GetQuery();

            Assert.IsAssignableFrom<IQueryable>(dbQuery);
        }

        [Fact]
        public void Query_IgnoresDeletedEntities_DefaultBehavior()
        {
            var contextCreationOption = new DbContextOptionsBuilder<PortfolioOperationsDbContext>()
                   .UseInMemoryDatabase(Guid.NewGuid().ToString())
                   .Options;

            using (var dbContext = new PortfolioFullOpsDbContextForDummies(contextCreationOption))
            {
                var liveEntity = new BaseEntityDummy();
                var deletedEntity = new BaseEntityDummy() { IsDeleted = true };

                var entities = new List<BaseEntityDummy>(){
                    liveEntity,
                    deletedEntity
                };

                var fakeQueryableDatabase = new Mock<IReadOnlyDatabase>();
                fakeQueryableDatabase
                    .Setup(db => db.ReadOnlySet<BaseEntityDummy>())
                    .Returns(entities.AsQueryable());

                IQuery<BaseEntityDummy> dummyQuery = new QueryBaseDummy<BaseEntityDummy>(fakeQueryableDatabase.Object);
                var result = dummyQuery.RunQuery();

                Assert.Single(result);
            }
        }
    }
}
