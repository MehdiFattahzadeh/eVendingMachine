using eVendingMachine.Domain;
using eVendingMachine.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace eVendingMachine.DomainServices.Tests.Unit
{
    public class CashChangerServiceTests
    {
        List<Cash> _cashes;
        public CashChangerServiceTests()
        {
            var euroCurrency = new Currency("Euro", "€");
            _cashes = new List<Cash>
            {
                new Cash(0.1M, 10, CashType.Coin,euroCurrency,1,""),
                new Cash(0.2M, 20, CashType.Coin,euroCurrency,1,""),
                new Cash(0.5M, 50, CashType.Coin, euroCurrency,1,""),
                new Cash(1.0M, 1, CashType.Coin, euroCurrency,1,""),
            };

        }

        [Theory]
        [InlineData(1.8)]
        [InlineData(1.5)]
        [InlineData(2.8)]
        public void CashChanger_Should_Calculate_ReturnsCahes(decimal total)
        {
            ICashChanger cashChanger = new CashChanger();
            var changedCashes = cashChanger.CalculateOutCashes(_cashes, total);
            Assert.True(changedCashes.Sum(x => x.Price) == total);
        }
    }
}
