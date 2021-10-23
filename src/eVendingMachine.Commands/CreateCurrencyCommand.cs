using eVendingMachine.Data.EF.Repositories;
using eVendingMachine.Data.EF;
using eVendingMachine.Domain;
using MediatR;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace eVendingMachine.Commands
{

    public class CreateCurrencyCommand : IRequest<Guid>
    {
        public Guid? CurrencyId { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
    }

    public class CreateCurrencyCommanddHandler : IRequestHandler<CreateCurrencyCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrencyRepository _currencyRepository;

        public CreateCurrencyCommanddHandler(IUnitOfWork unitOfWork, ICurrencyRepository currencyRepository)
        {
            _unitOfWork = unitOfWork;
            _currencyRepository = currencyRepository;
        }
        public async Task<Guid> Handle(CreateCurrencyCommand request, CancellationToken cancellationToken)
        {
            if (request.CurrencyId == null)
            {
                var currency = new Currency(request.Name,request.Symbol);
                await _currencyRepository.AddAsync(currency);
                await _unitOfWork.CommitAsync();
                return currency.Id;
            }
            else
            {
                var currency = await _currencyRepository.GetByIdAsync(request.CurrencyId.Value);
                _currencyRepository.Update(currency);
                await _unitOfWork.CommitAsync();
                return currency.Id;
            }
        }
    }

}
