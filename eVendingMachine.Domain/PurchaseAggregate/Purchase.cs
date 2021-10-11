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
            //CashesIO = new List<CashIO>();
            State = PurchaseState.New;
            DateTime = DateTime.Now;
        }
        //public ICollection<CashIO> CashesIO { get; private set; }
        public Product Product { get; private set; }
        public Guid? ProductId { get; private set; }
        public DateTime DateTime { get; private set; }
        public PurchaseState State { get; private set; }
        public decimal TotalInCash { get; private set; }
        public decimal TotalOutCash { get; private set; }
        public void InsertCash(Cash cash)
        {
            if (!new IsWalletHasSpace().IsSatisfiedBy(this)) throw new Exception();
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
            if (!new CanChangeToCancelState().IsSatisfiedBy(this)) throw new Exception();
            State = PurchaseState.Canceled;
        }

    }
}
