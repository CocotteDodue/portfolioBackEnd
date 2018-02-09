using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Contracts.Entities;
using System;

namespace Portfolio.DAL.Commands
{
    public class PortfolioOperationsDbContext : DbContext
    {
        public PortfolioOperationsDbContext(DbContextOptions<PortfolioOperationsDbContext> options) 
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Technology>( e =>
            {
                SetBaseEntityProperties(e);

                e.Property(entity => entity.Name)
                    .IsRequired()
                    .HasMaxLength(56);

                e.ToTable("Technologies");
            });

            modelBuilder.Entity<TechnologyVersion>( e =>
            {
                SetBaseEntityProperties(e);

                e.Property(entity => entity.MajorBuild)
                    .IsRequired();

                e.Property(entity => entity.NickName)
                    .IsRequired()
                    .HasDefaultValue("");

                e.Property(entity => entity.releaseDate)
                    .IsRequired();

                e.HasOne(p => p.Technology)
                    .WithMany(t => t.Versions)
                    .HasForeignKey(p => p.TechnologyId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Version_Technology");

                e.ToTable("TechnologiesVersions");
            });

            base.OnModelCreating(modelBuilder);
        }

        private static void SetBaseEntityProperties<TEntity>(EntityTypeBuilder<TEntity> e)
            where TEntity: BaseEntity
        {
            e.Property(entity => entity.Id)
                                .ValueGeneratedOnAdd();

            e.Property(entity => entity.CreationTime)
                .IsRequired();

            e.Property(entity => entity.IsDeleted)
                .HasDefaultValue(false);

            e.Property(entity => entity.LastModificationTime)
                .HasDefaultValue(null);

            e.HasKey(entity => entity.Id);
        }
    }
}
