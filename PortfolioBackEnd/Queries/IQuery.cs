using PortfolioBackEnd.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioBackEnd.Queries
{
    public interface IQuery<T> where T : BaseEntity
    {
        IQueryable<T> GetQuery();
        IEnumerable<T> RunQuery();
        Task<IEnumerable<T>> RunQueryAsync();
    }
}
