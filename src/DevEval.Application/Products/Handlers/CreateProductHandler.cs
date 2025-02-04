using AutoMapper;
using DevEval.Application.Products.Commands;
using DevEval.Application.Products.Dtos;
using DevEval.Domain.Entities.Product;
using DevEval.Domain.Repositories;
using MediatR;

namespace DevEval.Application.Products.Handlers
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, ProductDto>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public CreateProductHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request);

            var createdProduct = await _repository.AddAsync(product);

            var result = _mapper.Map<ProductDto>(createdProduct);

            return result;
        }
    }
}
