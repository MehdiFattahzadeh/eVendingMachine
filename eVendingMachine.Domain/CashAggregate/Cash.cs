using eVendingMachine.Domain.CashAggregate.Specifications;
using System;

namespace eVendingMachine.Domain
{
    public class Cash : Entity, IAggregateRoot
    {
        private Cash()
        {
            // For EF
        }
        public Cash(decimal price, int number, CashType cashType, Currency currency, int quantity, string code)
        {
            if (quantity < 0) throw new DomainException("Cash Quantity Must be More Than Zero");
            if (price < 0) throw new DomainException("Cash price Must be More Than Zero");
            if (number < 0) throw new DomainException("Cash number Must be More Than Zero");

            Price = price;
            Number = number;
            CashType = cashType;
            Currency = currency;
            Quantity = quantity;
            Code = code;
        }

        public int Quantity { get; private set; }
        public string Code { get; set; }
        public void Add(int quantity)
        {
            if (quantity < 0) throw new DomainException("Cash Quantity Must be More Than Zero");
            Quantity += quantity;
        }

        public void Take(int quantiy)
        {
            if (quantiy < 0) throw new DomainException("Cash Quantity Must be More Than Zero");
            Quantity -= quantiy;
            if (!new CashNegativeQuantitySpecification().IsSatisfiedBy(this)) throw new DomainException("Cash Quantity Cannot be Negative");
        }
        public decimal Price { get; private set; }
        public int Number { get; private set; }
        public CashType CashType { get; private set; }
        public Currency Currency { get; private set; }
        public Guid CurrencyId { get; private set; }
    }
}
