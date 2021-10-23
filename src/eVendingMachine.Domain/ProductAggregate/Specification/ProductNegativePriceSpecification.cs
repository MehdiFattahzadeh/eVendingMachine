using System;
using System.Collections.Generic;
using System.Text;

namespace eVendingMachine.Domain.ProductAggregate.Specification
{
    class ProductNegativePriceSpecification : ISpecification<Product>
    {
        public bool IsSatisfiedBy(Product entity)
        {
            return entity.Price > 0;
        }
    }
}
