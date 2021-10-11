using eVendingMachine.Data.EF;
using eVendingMachine.Data.EF.Repositories;
using eVendingMachine.Domain.Service;
using eVendingMachine.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace eVendingMachine.Commands
{
    public class PickProductCommand : IRequest<List<CashDto>>
    {
        public Guid? PurchaseId { get; set; }
        public Guid ProductId { get; set; }
    }

    public class PickProductCommandCommandHandler : IRequestHandler<PickProductCommand, List<CashDto>>
    {
        private readonly ICashChanger _cashChanger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICashRepository _casheRepository;

        public PickProductCommandCommandHandler(ICashChanger cashChanger, IUnitOfWork unitOfWork, IPurchaseRepository purchaseRepository, IProductRepository productRepository, ICashRepository casheRepository)
        {
            _cashChanger = cashChanger;
            _unitOfWork = unitOfWork;
            _purchaseRepository = purchaseRepository;
            _productRepository = productRepository;
            _casheRepository = casheRepository;
        }
        public async Task<List<CashDto>> Handle(PickProductCommand request, CancellationToken cancellationToken)
        {
            var availableCashes = await _casheRepository.GetAvailableCash();

            var product = await _productRepository.GetByIdAsync(request.ProductId);
            var purchase = await _purchaseRepository.GetByIdAsync(request.PurchaseId.Value);

            purchase.SetProduct(product);
            _purchaseRepository.Update(purchase);

            var changecash = _cashChanger.CalculateOutCashes(availableCashes, purchase.TotalOutCash);
            foreach (var cash in changecash)
            {
                cash.Take(1);
                _casheRepository.Update(cash);
            }

            await _unitOfWork.CommitAsync();
            var result = changecash.Select(x => new CashDto { Id = x.Id, Name = $"{x.Currency.Name} {x.Number}", Price = x.Price });
            return result.ToList();
        }
    }
}
