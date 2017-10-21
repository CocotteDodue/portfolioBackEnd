using PortfolioBackEnd.Entities;
using System.Linq;

namespace PortfolioBackEnd
{
    public interface IReadOnlyDatabase
    {
        IQueryable<TEntity> ReadOnlySet<TEntity>() where TEntity : BaseEntity;
    }
}
