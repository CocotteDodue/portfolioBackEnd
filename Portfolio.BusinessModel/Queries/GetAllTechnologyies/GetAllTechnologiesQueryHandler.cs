using Portfolio.Contracts.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.BusinessModel.Queries
{
    public class GetAllTechnologiesQueryHandler : IGetAllTechnologiesQueryHandler
    {
        public IQueryable<Technology> GetQuery()
        {
            throw new NotImplementedException();
        }

        public Technology HandleQuery(IGetAllTechnologyiesQuery query)
        {
            throw new NotImplementedException();
        }

        public Task<Technology> HandleQueryAsync(IGetAllTechnologyiesQuery query)
        {
            throw new NotImplementedException();
        }
    }
}
