namespace eVendingMachine.Domain
{
    public class PurchasaseCannotChangeToNewState : ISpecification<Purchase>
    {
        public bool IsSatisfiedBy(Purchase entity)
        {
            return entity.State != PurchaseState.New;
        }
    }

    public class PurchasaseInsufficientAmount : ISpecification<Purchase>
    {
        public bool IsSatisfiedBy(Purchase entity)
        {
            return entity.TotalOutCash > 0;
        }
    }
}
