using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioBackEnd.Queries
{
    public class QueryBus : IQueryBus
    {
        private Func<Type, IQueryHandler> _handlersFactory;

        public QueryBus(Func<Type, IQueryHandler> handlersFactory)
        {
            _handlersFactory = handlersFactory;
        }
        public TResult Process<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
        {
            var queryHandler = (IQueryHandler<TQuery, TResult>)_handlersFactory(typeof(TQuery));

            return queryHandler.HandleQuery(query);
        }
        public async Task<TResult> ProcessAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
        {
            var queryHandler = (IQueryHandler<TQuery, TResult>)_handlersFactory(typeof(TQuery));

            return await queryHandler.HandleQueryAsync(query);
        }
    }
}
