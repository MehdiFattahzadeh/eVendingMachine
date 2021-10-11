using System;
using Xunit;

namespace eVendingMachine.Domain.Tests.Unit
{
    public class ProductTests
    {
        public ProductTests()
        {

        }

        [Fact]
        public void Product_Number_Should_Update_After_AddMethod()
        {
            Product espresso = new Product("Espresso", 1.80M, 20); ;
            espresso.Add(5);
            Assert.True(espresso.Portion == 25);
        }

        [Fact]
        public void Product_Number_Should_Reduce_After_ReduceMethod()
        {
            Product espresso = new Product("Espresso", 1.80M, 20); ;
            espresso.Reduce(5);
            Assert.True(espresso.Portion == 15);
        }

        [Fact]
        public void Should_Throw_DomainException_When_AddNumber_IS_LessThan_Zero()
        {
            Product espresso = new Product("Espresso", 1.80M, 20);
            Action act = () => espresso.Add(-5);
            Assert.Throws<DomainException>(act);
        }

        [Fact]
        public void Should_Throw_DomainException_When_ReduceNumber_IS_LessThan_Zero()
        {
            Product espresso = new Product("Espresso", 1.80M, 20);
            Action act = () => espresso.Reduce(-5);
            Assert.Throws<DomainException>(act);
        }
    }
}
