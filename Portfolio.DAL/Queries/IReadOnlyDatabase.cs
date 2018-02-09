using Portfolio.Contracts.Entities;
using System.Linq;

namespace Portfolio.DAL.Queries
{
    public interface IReadOnlyDatabase
    {
        IQueryable<TEntity> ReadOnlySet<TEntity>() where TEntity : BaseEntity;
    }
}
