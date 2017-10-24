using PortfolioBackEnd.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioBackEnd.Queries
{
    public class GetAllTechnologiesQueryHandler : IGetAllTechnologiesQueryHandler
    {
        public IQueryable<Technology> GetQuery()
        {
            throw new NotImplementedException();
        }

        public Technology HandleQuery(ITechnologyQuery query)
        {
            throw new NotImplementedException();
        }

        public Task<Technology> HandleQueryAsync(ITechnologyQuery query)
        {
            throw new NotImplementedException();
        }
    }
}
