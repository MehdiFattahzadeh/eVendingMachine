using System;
using System.Threading;
using System.Threading.Tasks;

namespace eVendingMachine.Data.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VendingMachineDbContext _context;

        public UnitOfWork(VendingMachineDbContext context) => _context = context;

        public async Task<bool> CommitAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false) > 0;
        }

        public bool Commit(CancellationToken cancellationToken = default)
        {
            return _context.SaveChanges() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }

}
