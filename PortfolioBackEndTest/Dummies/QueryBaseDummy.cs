using PortfolioBackEnd.Queries;
using System;
using System.Linq;
using PortfolioBackEnd;

namespace PortfolioBackEndTest.Dummies
{
    internal class QueryBaseDummy<TEntity> : BaseQuery<BaseEntityDummy>, IQuery<BaseEntityDummy> where TEntity : BaseEntityDummy
    {
        public QueryBaseDummy(IReadOnlyDatabase queryablePortfolioDb) : base(queryablePortfolioDb) {}
    }
}
