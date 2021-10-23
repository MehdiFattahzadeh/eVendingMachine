using eVendingMachine.Data.EF;
using eVendingMachine.Data.EF.Repositories;
using eVendingMachine.Domain.Service;
using eVendingMachine.Dto;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace eVendingMachine.Commands
{
    public class CancelPurchaseCommand : IRequest<Guid>
    {
        public Guid? PurchaseId { get; set; }
    }
    public class CancelPurchaseCommandHandler : IRequestHandler<CancelPurchaseCommand, Guid>
    {
        private readonly ICashChanger _cashChanger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly ICashRepository _casheRepository;

        public CancelPurchaseCommandHandler(ICashChanger cashChanger, IUnitOfWork unitOfWork, IPurchaseRepository purchaseRepository,ICashRepository casheRepository)
        {
            _cashChanger = cashChanger;
            _unitOfWork = unitOfWork;
            _purchaseRepository = purchaseRepository;
            _casheRepository = casheRepository;
        }
        public async Task<Guid> Handle(CancelPurchaseCommand request, CancellationToken cancellationToken)
        {
            var availableCashes = await _casheRepository.GetAvailableCash();
            var purchase = await _purchaseRepository.GetByIdAsync(request.PurchaseId.Value);
            purchase.Cancel();
            _purchaseRepository.Update(purchase);
            var changecash = _cashChanger.CalculateOutCashes(availableCashes, purchase.TotalOutCash);
            foreach (var cash in changecash)
            {
                cash.Take(1);
                _casheRepository.Update(cash);
            }
            await _unitOfWork.CommitAsync();
            var result = changecash.Select(x => new CashDto { Id = x.Id, Name = $"{x.Currency.Name} {x.Number}" });
            return purchase.Id;
        }
    }

}
