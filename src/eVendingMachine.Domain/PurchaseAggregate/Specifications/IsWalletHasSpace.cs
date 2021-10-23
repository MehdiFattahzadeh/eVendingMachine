namespace eVendingMachine.Domain
{
    public class IsWalletHasSpace : ISpecification<Purchase>
    {
        public bool IsSatisfiedBy(Purchase entity)
        {
            return true;
        }
    }
}
