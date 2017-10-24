using Microsoft.EntityFrameworkCore;
using PortfolioBackEnd;
using PortfolioBackEndTest.Dummies;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PortfolioBackEndTest
{
    public class UnitOfWorkTest
    {
        [Fact]
        public async Task UnitOfWork_PersistsAllAddedEntities_WhenCommit()
        {
            var contextCreationOption = new DbContextOptionsBuilder<PortfolioOperationsDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var dbContext = new PortfolioFullOpsDbContextForDummies(contextCreationOption))
            {
                var entityToBeAdded1 = new BaseEntityDummy();
                var entityToBeAdded2 = new BaseEntityDummy();

                dbContext.Add(entityToBeAdded1);
                dbContext.Add(entityToBeAdded2);
                IUnitOfWork unitOfWork = new UnitOfWork(dbContext);

                await unitOfWork.CommitAsync();
                var numberOfEntityInDb = await dbContext.Set<BaseEntityDummy>().CountAsync();

                Assert.Equal(2, numberOfEntityInDb);
            }
        }

        [Fact]
        public async Task UnitOfWork_AddsCreationDate_WhenSaveNewEntity()
        {
            var timeAtTestStart = DateTime.Now;
            var contextCreationOption = new DbContextOptionsBuilder<PortfolioOperationsDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var dbContext = new PortfolioFullOpsDbContextForDummies(contextCreationOption))
            {
                var entityToBeAdded = new BaseEntityDummy();
                dbContext.Add(entityToBeAdded);
                IUnitOfWork unitOfWork = new UnitOfWork(dbContext);

                await unitOfWork.CommitAsync();

                Assert.True(timeAtTestStart < entityToBeAdded.CreationTime);
            }
        }

        [Fact]
        public async Task UnitOfWork_AddsLastModificationDate_WhenSaveExistingEntity()
        {
            var timeAtTestStart = DateTime.Now;
            var contextCreationOption = new DbContextOptionsBuilder<PortfolioOperationsDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var dbContext = new PortfolioFullOpsDbContextForDummies(contextCreationOption))
            {
                var modifiedEntity = new BaseEntityDummy();
                dbContext.Add(modifiedEntity);
                dbContext.SaveChanges();
                dbContext.ChangeTracker.Entries().ToList().ForEach(e => e.State = EntityState.Modified);
                IUnitOfWork unitOfWork = new UnitOfWork(dbContext);

                await unitOfWork.CommitAsync();

                Assert.True(modifiedEntity.LastModificationTime.HasValue
                    && timeAtTestStart < modifiedEntity.LastModificationTime.Value);
            }
        }

        [Fact]
        public async Task UnitOfWork_AddsLastModificationDate_WhenDeleteExistingEntity()
        {
            var timeAtTestStart = DateTime.Now;
            var contextCreationOption = new DbContextOptionsBuilder<PortfolioOperationsDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var dbContext = new PortfolioFullOpsDbContextForDummies(contextCreationOption))
            {
                var modifiedEntity = new BaseEntityDummy();
                dbContext.Add(modifiedEntity);
                dbContext.SaveChanges();
                dbContext.ChangeTracker.Entries().ToList().ForEach(e => e.State = EntityState.Deleted);
                IUnitOfWork unitOfWork = new UnitOfWork(dbContext);

                await unitOfWork.CommitAsync();

                Assert.True(modifiedEntity.LastModificationTime.HasValue
                    && timeAtTestStart < modifiedEntity.LastModificationTime.Value);
            }
        }

        [Fact]
        public async Task UnitOfWork_MarksEntityAsDeleted_WhenEntityIsDeleted()
        {
            var timeAtTestStart = DateTime.Now;
            var contextCreationOption = new DbContextOptionsBuilder<PortfolioOperationsDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var dbContext = new PortfolioFullOpsDbContextForDummies(contextCreationOption))
            {
                var entityToBeDeleted = new BaseEntityDummy();
                dbContext.Add(entityToBeDeleted);
                dbContext.SaveChanges();
                dbContext.ChangeTracker.Entries().ToList().ForEach(e => e.State = EntityState.Deleted);

                IUnitOfWork unitOfWork = new UnitOfWork(dbContext);

                await unitOfWork.CommitAsync();

                Assert.True(entityToBeDeleted.IsDeleted);
            }
        }

        [Fact]
        public async Task UnitOfWork_PreservesEntityInDb_WhenEntityIsDeleted()
        {
            var timeAtTestStart = DateTime.Now;
            var contextCreationOption = new DbContextOptionsBuilder<PortfolioOperationsDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var dbContext = new PortfolioFullOpsDbContextForDummies(contextCreationOption))
            {
                var entityToBeDeleted = new BaseEntityDummy();
                dbContext.Add(entityToBeDeleted);
                dbContext.SaveChanges();
                dbContext.ChangeTracker.Entries().ToList().ForEach(e => e.State = EntityState.Deleted);

                IUnitOfWork unitOfWork = new UnitOfWork(dbContext);

                await unitOfWork.CommitAsync();
                
                Assert.Contains(entityToBeDeleted, dbContext.Set<BaseEntityDummy>());
            }
        }
    }
}
