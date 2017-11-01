using Microsoft.EntityFrameworkCore;
using PortfolioBackEnd.Entities;

namespace PortfolioBackEnd
{
    public interface IOperationsDatabase
    {
        DbSet<TEntity> DbSet<TEntity>() where TEntity : BaseEntity;  
    }
}
