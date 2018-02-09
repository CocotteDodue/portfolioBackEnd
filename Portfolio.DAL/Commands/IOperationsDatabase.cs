using Microsoft.EntityFrameworkCore;
using Portfolio.Contracts.Entities;

namespace Portfolio.DAL.Commands
{
    public interface IOperationsDatabase
    {
        DbSet<TEntity> DbSet<TEntity>() where TEntity : BaseEntity;  
    }
}
