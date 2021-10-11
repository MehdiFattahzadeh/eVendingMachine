using eVendingMachine.Domain;

namespace eVendingMachine.Data.EF.Repositories
{
    public interface ICurrencyRepository : IGenericRepository<Currency>
    {

    }
    public class CurrencyRepository : EFRepository<Currency>, ICurrencyRepository
    {
        public CurrencyRepository(VendingMachineDbContext dbContext) : base(dbContext)
        {
        }
    }
}
