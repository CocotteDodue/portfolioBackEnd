using System.Threading.Tasks;

namespace PortfolioBackEnd
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
    }
}
