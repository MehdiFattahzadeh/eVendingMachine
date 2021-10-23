using eVendingMachine.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eVendingMachine.Data.EF.Repositories
{
    public interface ICashRepository : IGenericRepository<Cash>
    {
        Task<List<Cash>> GetAvailableCash();
    }

    public class CashRepository : EFRepository<Cash>, ICashRepository
    {
        private readonly VendingMachineDbContext _dbContext;

        public CashRepository(VendingMachineDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Cash>> GetAvailableCash()
        {
            return await _dbContext.Cashes.Include(x => x.Currency).Where(x => x.Number > 0).ToListAsync();
        }
    }
}
