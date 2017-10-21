using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using PortfolioBackEnd;
using PortfolioBackEnd.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PortfolioBackEndTest
{
    public class UnitOfWorkTest
    {
        [Fact]
        public async Task UnitOfWork_PersistAllAddedEntities_WhenCommit()
        {
            var contextCreationOption = new DbContextOptionsBuilder<PortfolioDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
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
        public async Task UnitOfWork_AddCreationDate_WhenSaveNewEntity()
        {
            var timeAtTestStart = DateTime.Now;
            var contextCreationOption = new DbContextOptionsBuilder<PortfolioDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
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

        [Fact]
        public async Task UnitOfWork_AddLastModificationDate_WhenSaveExistingEntity()
        {
            var timeAtTestStart = DateTime.Now;
            var contextCreationOption = new DbContextOptionsBuilder<PortfolioDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var context = new PortfolioDbContext(contextCreationOption))
            {
                var modifiedEntity = new TechnologyVersion
                {
                    NickName = "test",
                    MajorBuild = "0",
                    releaseDate = new DateTime(2000, 01, 01)
                };
                context.TechnologiesVersions.Add(modifiedEntity);
                context.SaveChanges();
                context.ChangeTracker.Entries().ToList().ForEach(e => e.State = EntityState.Modified);
                IUnitOfWork unitOfWork = new UnitOfWork(context);

                await unitOfWork.CommitAsync();

                Assert.True(modifiedEntity.LastModificationTime.HasValue
                    && timeAtTestStart < modifiedEntity.LastModificationTime.Value);
            }
        }

        [Fact]
        public async Task UnitOfWork_AddLastModificationDate_WhenDeleteExistingEntity()
        {
            var timeAtTestStart = DateTime.Now;
            var contextCreationOption = new DbContextOptionsBuilder<PortfolioDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var context = new PortfolioDbContext(contextCreationOption))
            {
                var modifiedEntity = new TechnologyVersion
                {
                    NickName = "test",
                    MajorBuild = "0",
                    releaseDate = new DateTime(2000, 01, 01)
                };
                context.TechnologiesVersions.Add(modifiedEntity);
                context.SaveChanges();
                context.ChangeTracker.Entries().ToList().ForEach(e => e.State = EntityState.Deleted);
                IUnitOfWork unitOfWork = new UnitOfWork(context);

                await unitOfWork.CommitAsync();

                Assert.True(modifiedEntity.LastModificationTime.HasValue
                    && timeAtTestStart < modifiedEntity.LastModificationTime.Value);
            }
        }

        [Fact]
        public async Task UnitOfWork_MarkEntityAsDeleted_WhenEntityIsDeleted()
        {
            var timeAtTestStart = DateTime.Now;
            var contextCreationOption = new DbContextOptionsBuilder<PortfolioDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var context = new PortfolioDbContext(contextCreationOption))
            {
                var entityToBeDeleted = new TechnologyVersion
                {
                    NickName = "test",
                    MajorBuild = "0",
                    releaseDate = new DateTime(2000, 01, 01)
                };
                context.TechnologiesVersions.Add(entityToBeDeleted);
                context.SaveChanges();
                context.ChangeTracker.Entries().ToList().ForEach(e => e.State = EntityState.Deleted);

                IUnitOfWork unitOfWork = new UnitOfWork(context);

                await unitOfWork.CommitAsync();

                Assert.True(entityToBeDeleted.IsDeleted);
            }
        }

        [Fact]
        public async Task UnitOfWork_PreserveEntityInDb_WhenEntityIsDeleted()
        {
            var timeAtTestStart = DateTime.Now;
            var contextCreationOption = new DbContextOptionsBuilder<PortfolioDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var context = new PortfolioDbContext(contextCreationOption))
            {
                var entityToBeDeleted = new TechnologyVersion
                {
                    NickName = "test",
                    MajorBuild = "0",
                    releaseDate = new DateTime(2000, 01, 01)
                };
                context.TechnologiesVersions.Add(entityToBeDeleted);
                context.SaveChanges();
                context.ChangeTracker.Entries().ToList().ForEach(e => e.State = EntityState.Deleted);

                IUnitOfWork unitOfWork = new UnitOfWork(context);

                await unitOfWork.CommitAsync();
                
                Assert.Contains(entityToBeDeleted, context.TechnologiesVersions);
            }
        }
    }
}
