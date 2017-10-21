using Microsoft.EntityFrameworkCore;
using Moq;
using PortfolioBackEnd;
using PortfolioBackEnd.Entities;
using PortfolioBackEndTest.Dummies;
using System;
using System.Collections.Generic;
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

                var result = queryableDb.ReadOnlySet<BaseEntityDummy>();

                Assert.IsAssignableFrom<IQueryable>(result);
            }
        }
    }
}
