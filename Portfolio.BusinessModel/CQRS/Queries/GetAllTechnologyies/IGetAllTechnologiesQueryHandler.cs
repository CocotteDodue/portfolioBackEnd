using Portfolio.Contracts.Entities;

namespace Portfolio.BusinessModel.Queries
{
    public interface IGetAllTechnologiesQueryHandler : IQueryHandler<IGetAllTechnologyiesQuery, Technology>
    {
    }
}
