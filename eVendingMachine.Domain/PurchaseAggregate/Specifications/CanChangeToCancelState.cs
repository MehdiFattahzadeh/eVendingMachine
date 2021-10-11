using System;
using System.Collections.Generic;
using System.Text;

namespace eVendingMachine.Domain.PurchaseAggregate.Specifications
{
    class CanChangeToCancelState : ISpecification<Purchase>
    {
        public bool IsSatisfiedBy(Purchase entity)
        {
            return entity.State == PurchaseState.New;
        }
    }
}
