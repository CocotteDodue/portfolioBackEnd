using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.BusinessModel.Queries
{
    // Necessary for Reflexion to work and enable genericity (QueryBus processing method)
    public interface IQueryHandler
    { }

    public interface IQueryHandler<TQuery, TResult> : IQueryHandler where TQuery : IQuery<TResult>
    {
        IQueryable<TResult> GetQuery();
        TResult HandleQuery(TQuery query);
        Task<TResult> HandleQueryAsync(TQuery query);
    }
}
