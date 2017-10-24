using PortfolioBackEnd.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioBackEnd.Queries
{
    public interface IGetAllTechnologiesQueryHandler : IQueryHandler<ITechnologyQuery, Technology>
    {
    }
}
