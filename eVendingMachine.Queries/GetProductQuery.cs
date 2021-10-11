using eVendingMachine.Data.EF;
using eVendingMachine.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace eVendingMachine.Queries
{
    public class GetProductQuery : IRequest<List<ProductDto>>
    {

    }
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, List<ProductDto>>
    {
        private readonly VendingMachineDbContext _vendingMachineDbContext;

        public GetProductQueryHandler(VendingMachineDbContext vendingMachineDbContext)
            => _vendingMachineDbContext = vendingMachineDbContext;

        public async Task<List<ProductDto>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            return await _vendingMachineDbContext.Products
                                                 .Select(x => new ProductDto { Id = x.Id, Price = x.Price, Name = x.Name })
                                                 .ToListAsync();
        }
    }
}
