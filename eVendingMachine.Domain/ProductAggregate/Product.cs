using eVendingMachine.Domain.ProductAggregate.Specification;

namespace eVendingMachine.Domain
{
    public class Product : Entity, IAggregateRoot
    {
        private Product()
        {
            // For EF
        }
        public Product(string name, decimal price, int number)
        {
            SetName(name);
            SetPrice(price);
            Add(number);
        }

        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public int Portion { get; private set; }

        public void SetName(string name)
        {
            Name = name;
        }
        public void SetPrice(decimal price)
        {
            Price = price;
            if (!new ProductNegativePriceSpecification().IsSatisfiedBy(this)) throw new DomainException("Product Price Cannot be Negative");
        }
        public void Add(int number)
        {
            if (number < 0) throw new DomainException("Product Number Must be More Than Zero");
            Portion += number;
            if (!new ProductNegativeNumberrSpecification().IsSatisfiedBy(this)) throw new DomainException("Product Number Cannot be Negative");
        }

        public void Reduce(int number)
        {
            if (number < 0) throw new DomainException("Product Number Must be More Than Zero");
            Portion -= number;
        }
    }
}
