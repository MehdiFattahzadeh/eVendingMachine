using eVendingMachine.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace eVendingMachine.Data.EF.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {

    }

    public class ProductRepository : EFRepository<Product>, IProductRepository
    {
        public ProductRepository(VendingMachineDbContext dbContext) : base(dbContext)
        {
        }
    }
}
