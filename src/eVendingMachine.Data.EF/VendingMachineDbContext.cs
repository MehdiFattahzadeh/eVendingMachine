using eVendingMachine.Domain;
using Microsoft.EntityFrameworkCore;

namespace eVendingMachine.Data.EF
{
    public class VendingMachineDbContext : DbContext
    {
        public VendingMachineDbContext(DbContextOptions<VendingMachineDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Cash> Cashes { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Currency> Currencies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
