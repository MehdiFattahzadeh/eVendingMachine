using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace eVendingMachine.Domain.Tests.Unit
{
    public class PurchaseTests
    {
        List<Cash> _cashes;
        Product _tea;
        public PurchaseTests()
        {
            var euroCurrency = new Currency("Euro", "€");
            _cashes = new List<Cash>
            {
                new Cash(0.1M, 10, CashType.Coin,euroCurrency,1,""),
                new Cash(0.2M, 20, CashType.Coin,euroCurrency,1,""),
                new Cash(0.5M, 50, CashType.Coin, euroCurrency,1,""),
                new Cash(1.0M, 1, CashType.Coin, euroCurrency,1,""),
            };

            _tea = new Product("Tea", 1.30M, 10);
        }

        [Fact]
        public void Purchase_Product_Add_Test()
        {
            var purchase = new Purchase();
            purchase.InsertCash(_cashes.FirstOrDefault(x => x.Number == 1));
            purchase.InsertCash(_cashes.FirstOrDefault(x => x.Number == 50));
            purchase.SetProduct(_tea);
            Assert.True(purchase.TotalOutCash == 0.20M);
        }

        [Fact]
        public void Total_In_Purchase_Test()
        {
            var purchase = new Purchase();
            purchase.InsertCash(_cashes.FirstOrDefault(x => x.Number == 1));
            purchase.InsertCash(_cashes.FirstOrDefault(x => x.Number == 50));
            Assert.True(purchase.TotalInCash == 1.5M);
            purchase.InsertCash(_cashes.FirstOrDefault(x => x.Number == 10));
            Assert.True(purchase.TotalInCash == 1.6M);
        }

        [Fact]
        public void Puchase_Should_Generate_New_Id()
        {
            var purchase = new Purchase();
            Assert.True(purchase.Id.GetType() == typeof(Guid));
        }

        [Fact]
        public void Puchase_Add_Coin_Test()
        {
            var purchase = new Purchase();
            Assert.True(purchase.State == PurchaseState.New);
            purchase.InsertCash(_cashes[0]);
            purchase.InsertCash(_cashes[1]);
        }

    }
}
