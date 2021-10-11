using System;
using System.Collections.Generic;
using System.Text;

namespace eVendingMachine.Domain.CashAggregate.Specifications
{
    public class CashNegativeQuantitySpecification : ISpecification<Cash>
    {
        public bool IsSatisfiedBy(Cash entity)
        {
            return entity.Quantity >= 0;
        }
    }
}
