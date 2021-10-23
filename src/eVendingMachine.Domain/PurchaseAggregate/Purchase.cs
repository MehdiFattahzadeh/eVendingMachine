using eVendingMachine.Domain.PurchaseAggregate.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eVendingMachine.Domain
{
    public class Purchase : Entity, IAggregateRoot
    {
        public Purchase()
        {
            State = PurchaseState.New;
            DateTime = DateTime.Now;
        }
        public Product Product { get; private set; }
        public Guid? ProductId { get; private set; }
        public DateTime DateTime { get; private set; }
        public PurchaseState State { get; private set; }
        public decimal TotalInCash { get; private set; }
        public decimal TotalOutCash { get; private set; }
        public void InsertCash(Cash cash)
        {
            if (!new IsWalletHasSpace().IsSatisfiedBy(this)) throw new DomainException("Wallet Cannot Store Cashs");
            if (cash == null) throw new ArgumentNullException(nameof(cash));

            TotalInCash += cash.Price;
        }

        public void SetProduct(Product product)
        {
            Product = product ?? throw new ArgumentNullException(nameof(product));
            ProductId = product.Id;
            TotalOutCash = TotalInCash - product.Price;
            if (!new PurchasaseInsufficientAmount().IsSatisfiedBy(this))
                throw new DomainException("Insufficient amount");
        }

        public void Cancel()
        {
            if (!new CanChangeToCancelState().IsSatisfiedBy(this)) throw new DomainException("Purchase Cannot be Cancel"); 
            State = PurchaseState.Canceled;
        }

    }
}
