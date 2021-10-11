using eVendingMachine.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eVendingMachine.Data.EF.Repositories
{
    public interface IPurchaseRepository : IGenericRepository<Purchase>
    {
        //void UpdateCashes(Purchase p, Cash cash);
        //void UpdateCashes(Purchase p, List<CashIO> cash);
    }

    public class PurchaseRepository : EFRepository<Purchase>, IPurchaseRepository
    {
        private readonly VendingMachineDbContext _dbContext;

        public PurchaseRepository(VendingMachineDbContext dbContext) : base(dbContext) => _dbContext = dbContext;

        public override Task<Purchase> GetByIdAsync(Guid id)
        {
            return _dbContext.Purchases
                             //.Include(x => x.CashesIO)
                             .FirstOrDefaultAsync(x => x.Id == id);

        }
        //public void UpdateCashes(Purchase p, Cash cash)
        //{
        //    _dbContext.CashIOs.AddAsync(new CashIO { Cash = cash, CashId = cash.Id, Purchase = p, PurchaseId = p.Id, InOut = InOut.In });

        //}
        //public void UpdateCashes(Purchase p, List<CashIO> cash)
        //{
        //    _dbContext.CashIOs.AddRange(cash);

        //}
    }
}
