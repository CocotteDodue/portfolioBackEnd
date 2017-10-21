using Microsoft.EntityFrameworkCore;
using PortfolioBackEnd;
using PortfolioBackEnd.Entities;
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
        public async Task UnitOfWork_PersistAllAddedEntities_WhenCommit()
        {
            var contextCreationOption = new DbContextOptionsBuilder<PortfolioDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var context = new PortfolioDbContextForDummies(contextCreationOption))
            {
                var entityToBeAdded1 = new BaseEntityDummy();
                var entityToBeAdded2 = new BaseEntityDummy();

                context.Add(entityToBeAdded1);
                context.Add(entityToBeAdded2);
                IUnitOfWork unitOfWork = new UnitOfWork(context);

                await unitOfWork.CommitAsync();
                var numberOfEntityInDb = await context.Set<BaseEntityDummy>().CountAsync();

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

            using (var context = new PortfolioDbContextForDummies(contextCreationOption))
            {
                var entityToBeAdded = new BaseEntityDummy();
                context.Add(entityToBeAdded);
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

            using (var context = new PortfolioDbContextForDummies(contextCreationOption))
            {
                var modifiedEntity = new BaseEntityDummy();
                context.Add(modifiedEntity);
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

            using (var context = new PortfolioDbContextForDummies(contextCreationOption))
            {
                var modifiedEntity = new BaseEntityDummy();
                context.Add(modifiedEntity);
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

            using (var context = new PortfolioDbContextForDummies(contextCreationOption))
            {
                var entityToBeDeleted = new BaseEntityDummy();
                context.Add(entityToBeDeleted);
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

            using (var context = new PortfolioDbContextForDummies(contextCreationOption))
            {
                var entityToBeDeleted = new BaseEntityDummy();
                context.Add(entityToBeDeleted);
                context.SaveChanges();
                context.ChangeTracker.Entries().ToList().ForEach(e => e.State = EntityState.Deleted);

                IUnitOfWork unitOfWork = new UnitOfWork(context);

                await unitOfWork.CommitAsync();
                
                Assert.Contains(entityToBeDeleted, context.Set<BaseEntityDummy>());
            }
        }
    }
}
