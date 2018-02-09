using System.Threading.Tasks;

namespace Portfolio.BusinessModel.Queries
{
    public interface IQueryBus
    {
        TResult Process<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>;
        Task<TResult> ProcessAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>;
    }
}
