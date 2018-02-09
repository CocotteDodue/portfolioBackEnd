using System.Threading.Tasks;

namespace Portfolio.DAL.Commands
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
    }
}
