using System;
using System.Threading;
using System.Threading.Tasks;

namespace eVendingMachine.Data.EF
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> CommitAsync(CancellationToken cancellationToken = default);
        bool Commit(CancellationToken cancellationToken = default);
    }

}
