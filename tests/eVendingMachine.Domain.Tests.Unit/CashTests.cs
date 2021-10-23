using System;
using Xunit;

namespace eVendingMachine.Domain.Tests.Unit
{
    public class CashTests
    {
        [Fact]
        public void Cash_Quantity_Should_Increase_After_AddMethod()
        {
            Cash cash = new Cash(1, 1, CashType.Note, new Currency("dollar", "$"), 1,"C1");
            cash.Add(2);
            Assert.True(cash.Quantity == 3);
        }

        [Fact]
        public void Cash_Quantity_Should_Decrease_After_TakeMethod()
        {
            Cash cash = new Cash(1, 5, CashType.Note, new Currency("dollar", "$"), 1, "C5");
            cash.Add(5);
            cash.Take(2);
            Assert.True(cash.Quantity == 4);
        }

        [Fact]
        public void Should_Throw_DomainException_When_AddQuantity_IS_LessThan_Zero()
        {
            Cash cash = new Cash(1, 5, CashType.Note, new Currency("dollar", "$"), 1,"C5");
            Action act = () => { cash.Add(-5); };
            Assert.Throws<DomainException>(act);
        }

        [Fact]
        public void Should_Throw_DomainException_When_TakeQuantity_IS_LessThan_Zero()
        {
            Cash cash = new Cash(1, 5, CashType.Note, new Currency("dollar", "$"), 1,"D1");
            Action act = () => { cash.Take(-5); };
            Assert.Throws<DomainException>(act);
        }

        [Fact]
        public void Should_Throw_DomainException_When_Quantity_IS_Negative()
        {
            Cash cash = new Cash(1, 5, CashType.Note, new Currency("dollar", "$"), 1,"D1");
            Action act = () => { cash.Take(4); };
            Assert.Throws<DomainException>(act);
        }
    }
}
