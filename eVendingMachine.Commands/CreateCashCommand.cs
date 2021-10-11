using eVendingMachine.Data.EF;
using eVendingMachine.Data.EF.Repositories;
using eVendingMachine.Domain;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace eVendingMachine.Commands
{
    public class CreateCashCommand : IRequest<Guid>
    {
        public string Code { get; set; }
        public Guid? CasheId { get; set; }
        public decimal Price { get; set; }
        public int Number { get; set; }
        public int CashType { get; set; }
        public Guid CurrencyId { get; set; }
    }

    public class CreateCashCommandHandler : IRequestHandler<CreateCashCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICashRepository _casheRepository;
        private readonly ICurrencyRepository _currencyRepository;

        public CreateCashCommandHandler(IUnitOfWork unitOfWork, ICashRepository casheRepository, ICurrencyRepository currencyRepository)
        {
            _unitOfWork = unitOfWork;
            _casheRepository = casheRepository;
            _currencyRepository = currencyRepository;
        }
        public async Task<Guid> Handle(CreateCashCommand request, CancellationToken cancellationToken)
        {
            if (request.CasheId == null)
            {
                var currency = await _currencyRepository.GetByIdAsync(request.CurrencyId);
                var cash = new Cash(request.Price, request.Number, (CashType)request.CashType, currency, 0, request.Code);
                await _casheRepository.AddAsync(cash);
                await _unitOfWork.CommitAsync();
                return cash.Id;
            }
            else
            {
                var cash = await _casheRepository.GetByIdAsync(request.CasheId.Value);
                _casheRepository.Update(cash);
                await _unitOfWork.CommitAsync();
                return cash.Id;
            }
        }
    }
}
