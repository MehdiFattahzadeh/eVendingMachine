using eVendingMachine.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace eVendingMachine.ConsoleApp
{
    public class CurrentOrder
    {
        public Guid? Id { get; set; }
        public decimal TotalInputCash { get; set; }

    }
    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new HostBuilder()
                              .ConfigureServices((hostContext, services) =>
                              {
                                  services.AddHttpClient();
                                  services.AddTransient<CasheService>();
                                  services.AddTransient<ProductService>();
                              }).UseConsoleLifetime();

            var host = builder.Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                Console.ForegroundColor = ConsoleColor.White;

                var currentOrder = new CurrentOrder();
                char key = 'x';
                while (true)
                {
                    try
                    {

                        if (key == '4') break;

                        switch (key)
                        {
                            case 'x':
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Clear();
                                Console.WriteLine($"[TotalInput {currentOrder.TotalInputCash}]");
                                Console.WriteLine("-------------------");
                                Console.WriteLine("[1] : Insert Coin");
                                Console.WriteLine("[2] : Pick Product");
                                Console.WriteLine("[3] : Cancel");
                                Console.WriteLine("[4] : Exit");
                                Console.WriteLine("-------------------");

                                key = Console.ReadKey().KeyChar;
                                break;
                            case '1':

                                while (true)
                                {
                                    Console.Clear();
                                    Console.WriteLine($"[TotalInput {currentOrder.TotalInputCash}]");
                                    Console.WriteLine("-------------------");
                                    Console.WriteLine("[x] : Back");
                                    Console.WriteLine("-------------------");
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    var coinService = services.GetRequiredService<CasheService>();
                                    var coins = await coinService.GetCoins();
                                    for (int i = 0; i < coins.Count; i++)
                                    {
                                        Console.WriteLine($"[{i}]  {coins[i].Name} ");
                                    }
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.WriteLine("Insert Coin to Machine");
                                    key = Console.ReadKey().KeyChar;
                                    if (key == 'x') break;

                                    var selectedNumber = int.Parse(key.ToString());
                                    currentOrder.Id = await coinService.InsertCash(new InsertCasheToMachinCommand
                                    {
                                        CashId = coins[selectedNumber].Id,
                                        PurchaseId = currentOrder.Id,
                                    });
                                    currentOrder.TotalInputCash += coins[selectedNumber].Price;

                                }
                                break;
                            case '2':
                                while (true)
                                {
                                    Console.Clear();
                                    Console.WriteLine($"[TotalInput {currentOrder.TotalInputCash}]");
                                    Console.WriteLine("-------------------");
                                    Console.WriteLine("[x] : Back");
                                    Console.WriteLine("-------------------");
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    var productService = services.GetRequiredService<ProductService>();
                                    var products = await productService.GetProducts();
                                    for (int i = 0; i < products.Count; i++)
                                    {
                                        Console.WriteLine($"[{i}]  {products[i].Name} ({products[i].Price} $) ");
                                    }
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.WriteLine("Pick a Product");
                                    key = Console.ReadKey().KeyChar;
                                    if (key == 'x') break;

                                    var selectedNumber = int.Parse(key.ToString());
                                    var returnCashes = await productService.BuyProducts(new PickProductCommand
                                    {
                                        ProductId = products[selectedNumber].Id,
                                        PurchaseId = currentOrder.Id
                                    });

                                    Console.Clear();
                                    Console.WriteLine($"[TotalInput {currentOrder.TotalInputCash}]");
                                    Console.WriteLine("-------------------");
                                    Console.WriteLine("[x] : Back");
                                    Console.WriteLine("-------------------");
                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.WriteLine("Thank you !");
                                    Console.WriteLine("-----------------");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.Write("Your Return Cash [ ");
                                    foreach (var cash in returnCashes)
                                    {
                                        Console.Write($"{cash.Price} ");
                                    }
                                    Console.WriteLine(" ]");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    currentOrder = new CurrentOrder();
                                    key = Console.ReadKey().KeyChar;
                                }
                                break;
                            case '3':
                                Console.Clear();
                                Console.WriteLine("[3] : Cancel");
                                Console.WriteLine("[x] : Back");
                                Console.WriteLine("-------------------");
                                key = Console.ReadKey().KeyChar;
                                break;
                            case '4':
                                break;
                            default:
                                break;
                        }
                    }
                    catch (ApiException ex)
                    {
                        Console.Clear();
                        Console.WriteLine("[3] : Cancel");
                        Console.WriteLine("[x] : Back");
                        Console.WriteLine("-------------------");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine(ex.Message);
                        key = Console.ReadKey().KeyChar;
                    }
                    catch (Exception ex)
                    {
                        Console.Clear();
                        Console.WriteLine("[3] : Cancel");
                        Console.WriteLine("[x] : Back");
                        Console.WriteLine("-------------------");
                       
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Error Occured");
                        key = Console.ReadKey().KeyChar;
                    }
                }


            }

            //return 0;
        }



    }
}
