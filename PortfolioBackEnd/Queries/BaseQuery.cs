using PortfolioBackEnd.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PortfolioBackEnd.Queries
{
    //public abstract class BaseQuery<TEntity> : IQueryProcessor<TEntity> where TEntity : BaseEntity
    //{
    //    protected IReadOnlyDatabase _db;

    //    public BaseQuery(IReadOnlyDatabase queryablePortfolioDb)
    //    {
    //        _db = queryablePortfolioDb;
    //    }

    //    public virtual IEnumerable<TEntity> RunQuery() => GetQuery().ToList();

    //    public virtual async Task<IEnumerable<TEntity>> RunQueryAsync() => await GetQuery().ToListAsync();

    //    public virtual IQueryable<TEntity> GetQuery()
    //    {
    //        return _db.ReadOnlySet<TEntity>()
    //            .Where(entity => !entity.IsDeleted);
    //    }
    //}
}
