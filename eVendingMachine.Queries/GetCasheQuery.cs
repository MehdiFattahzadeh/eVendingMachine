using eVendingMachine.Data.EF;
using eVendingMachine.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace eVendingMachine.Queries
{

    public class GetCashQuery : IRequest<List<CashDto>>
    {

    }
    public class GetCasheQueryHandler : IRequestHandler<GetCashQuery, List<CashDto>>
    {
        private readonly VendingMachineDbContext _vendingMachineDbContext;

        public GetCasheQueryHandler(VendingMachineDbContext vendingMachineDbContext) => _vendingMachineDbContext = vendingMachineDbContext;

        public async Task<List<CashDto>> Handle(GetCashQuery request, CancellationToken cancellationToken)
        {
            return await _vendingMachineDbContext.Cashes.Include(c => c.Currency)
                                                 .Select(x => new CashDto { Id = x.Id, Name = $" {x.Price} {x.Currency.Name}", Price = x.Price })
                                                 .ToListAsync();
        }
    }
}
