using System.Threading;
using System.Threading.Tasks;

namespace NAF.INFRASTRUCTURE.Interface.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}