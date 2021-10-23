using eVendingMachine.Data.EF;
using eVendingMachine.Data.EF.Repositories;
using eVendingMachine.Domain;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace eVendingMachine.Commands
{
    public class InsertCasheToMachinCommand : IRequest<Guid>
    {
        public Guid? PurchaseId { get; set; }
        public Guid CashId { get; set; }
    }

    public class InsertCasheToMachinCommandHandler : IRequestHandler<InsertCasheToMachinCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICashRepository _casheRepository;
        private readonly IPurchaseRepository _purchaseRepository;

        public InsertCasheToMachinCommandHandler(IUnitOfWork unitOfWork, ICashRepository casheRepository, IPurchaseRepository purchaseRepository)
        {
            _unitOfWork = unitOfWork;
            _casheRepository = casheRepository;
            _purchaseRepository = purchaseRepository;
        }
        public async Task<Guid> Handle(InsertCasheToMachinCommand request, CancellationToken cancellationToken)
        {
            var cashe = await _casheRepository.GetByIdAsync(request.CashId);
            cashe.Add(1);
            _casheRepository.Update(cashe);

            Purchase purchase;
            if (request.PurchaseId == null)
            {
                purchase = new Purchase();
                purchase.InsertCash(cashe);
                await _purchaseRepository.AddAsync(purchase);
                await _unitOfWork.CommitAsync();
            }
            else
            {
                purchase = await _purchaseRepository.GetByIdAsync(request.PurchaseId.Value);
                purchase.InsertCash(cashe);
                _purchaseRepository.Update(purchase);
                await _unitOfWork.CommitAsync();
            }
            return purchase.Id;
        }
    }
}
