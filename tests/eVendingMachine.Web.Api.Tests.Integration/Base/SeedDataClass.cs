using eVendingMachine.Data.EF;
using eVendingMachine.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eVendingMachine.Web.Api.Tests.Integration
{
    public static class DefaultEntities
    {
        public static Currency Currency1 = new Currency("Dollar", "$");
        public static Cash Cash_10_cent = new Cash(0.10M, 10, CashType.Coin, Currency1, 100,"");
        public static Cash Cash_20_cent = new Cash(0.20M, 20, CashType.Coin, Currency1, 100, "");
        public static Cash Cash_50_cent = new Cash(0.50M, 50, CashType.Coin, Currency1, 100, "");
        public static Cash Cash_1_Euro = new Cash(1.0M, 1, CashType.Coin, Currency1, 100, "");
        public static Product product_Tea = new Product("Tea", 1.3M, 10);
        public static Product product_Espresso = new Product("Espresso", 1.8M, 20);
        public static Product product_Jiuce = new Product("Jiuce", 1.8M, 20);
        public static Product product_Chicken_Soup = new Product("Chicken_Soup", 1.8M, 15);

        public static Purchase purchase1 = new Purchase();
        public static Purchase purchase2 = new Purchase();
        public static Purchase purchase3 = new Purchase();
    }

    public class SeedDataClass : ISeedDataClass
    {
        private readonly VendingMachineDbContext _vendingMachineDbContext;

        public SeedDataClass(VendingMachineDbContext vendingMachineDbContext)
        {
            _vendingMachineDbContext = vendingMachineDbContext;
        }

        public void Init()
        {
            _vendingMachineDbContext.Currencies.Add(DefaultEntities.Currency1);
            _vendingMachineDbContext.Cashes.Add(DefaultEntities.Cash_10_cent);
            _vendingMachineDbContext.Cashes.Add(DefaultEntities.Cash_20_cent);
            _vendingMachineDbContext.Cashes.Add(DefaultEntities.Cash_50_cent);
            _vendingMachineDbContext.Cashes.Add(DefaultEntities.Cash_1_Euro);
            _vendingMachineDbContext.Products.Add(DefaultEntities.product_Tea);
            _vendingMachineDbContext.Products.Add(DefaultEntities.product_Espresso);
            _vendingMachineDbContext.Products.Add(DefaultEntities.product_Jiuce);
            _vendingMachineDbContext.Products.Add(DefaultEntities.product_Chicken_Soup);

            DefaultEntities.purchase1.InsertCash(DefaultEntities.Cash_50_cent);
            DefaultEntities.purchase1.InsertCash(DefaultEntities.Cash_1_Euro);


            DefaultEntities.purchase2.InsertCash(DefaultEntities.Cash_1_Euro);
            DefaultEntities.purchase2.InsertCash(DefaultEntities.Cash_1_Euro);
            DefaultEntities.purchase3.InsertCash(DefaultEntities.Cash_10_cent);

            _vendingMachineDbContext.Purchases.Add(DefaultEntities.purchase1);
            _vendingMachineDbContext.Purchases.Add(DefaultEntities.purchase2);
            _vendingMachineDbContext.Purchases.Add(DefaultEntities.purchase3);
            _vendingMachineDbContext.SaveChanges();
        }
}
}
