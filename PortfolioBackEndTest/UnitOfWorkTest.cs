using Microsoft.EntityFrameworkCore;
using PortfolioBackEnd;
using PortfolioBackEnd.Entities;
using System;
using System.Threading.Tasks;
using Xunit;

namespace PortfolioBackEndTest
{
    public class UnitOfWorkTest
    {
        [Fact]
        public async Task UnitOfWork_PersistAllAddedEntities_WhenCommitAsync()
        {
            var contextCreationOption = new DbContextOptionsBuilder<PortfolioDbContext>()
                .UseInMemoryDatabase(new Guid().ToString())
                .Options;

            using (var context = new PortfolioDbContext(contextCreationOption))
            {
                var entityToBeAdded1 = new TechnologyVersion
                {
                    NickName = "test2",
                    MajorBuild = "0",
                    releaseDate = new DateTime(2000, 01, 01)
                };
                var entityToBeAdded2 = new TechnologyVersion
                {
                    NickName = "test2",
                    MajorBuild = "0",
                    releaseDate = new DateTime(2000, 01, 01)
                };

                context.TechnologiesVersions.Add(entityToBeAdded1);
                context.TechnologiesVersions.Add(entityToBeAdded2);
                IUnitOfWork unitOfWork = new UnitOfWork(context);

                await unitOfWork.CommitAsync();
                var numberOfEntityInDb = await context.TechnologiesVersions.CountAsync();

                Assert.Equal(2, numberOfEntityInDb);
            }
        }

        [Fact]
        public async Task UnitOfWork_AddCreationData_WhenSaveNewEntityAsync()
        {
            var timeAtTestStart = DateTime.Now;
            var contextCreationOption = new DbContextOptionsBuilder<PortfolioDbContext>()
                .UseInMemoryDatabase(new Guid().ToString())
                .Options;

            using (var context = new PortfolioDbContext(contextCreationOption))
            {
                var entityToBeAdded = new TechnologyVersion
                {
                        NickName = "test",
                        MajorBuild = "0",
                        releaseDate = new DateTime(2000, 01, 01)
                    };
                context.TechnologiesVersions.Add(entityToBeAdded);
                IUnitOfWork unitOfWork = new UnitOfWork(context);

                await unitOfWork.CommitAsync();

                Assert.True(timeAtTestStart < entityToBeAdded.CreationTime);

            }
        }
    }
}
