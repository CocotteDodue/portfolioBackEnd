using PortfolioBackEnd.Entities;

namespace PortfolioBackEnd.Queries
{
    public interface IGetAllTechnologiesQueryHandler : IQueryHandler<ITechnologyQuery, Technology>
    {
    }
}
