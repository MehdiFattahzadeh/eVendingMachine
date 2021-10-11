using System;
using System.Collections.Generic;
using System.Text;

namespace eVendingMachine.Domain.ProductAggregate.Specification
{
    class ProductNegativeNumberrSpecification : ISpecification<Product>
    {
        public bool IsSatisfiedBy(Product entity)
        {
            return entity.Portion > 0;
        }
    }
}
