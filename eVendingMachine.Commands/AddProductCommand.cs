using eVendingMachine.Data.EF;
using eVendingMachine.Data.EF.Repositories;
using eVendingMachine.Domain;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace eVendingMachine.Commands
{
    public class AddProductCommand : IRequest<Guid>
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Number { get; set; }
    }

    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;

        public AddProductCommandHandler(IUnitOfWork unitOfWork, IProductRepository productRepository)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
        }
        public async Task<Guid> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            Product product;
            if (request.Id == null)
            {
                product = new Product(request.Name, request.Price, request.Number);
            }
            else
            {
                product = await _productRepository.GetByIdAsync(request.Id.Value);
                product.SetName(request.Name);
                product.SetPrice(request.Price);
            }
            await _productRepository.AddAsync(product);
            await _unitOfWork.CommitAsync();
            return product.Id;
        }
    }
}
