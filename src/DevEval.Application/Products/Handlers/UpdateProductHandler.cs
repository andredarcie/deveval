using AutoMapper;
using DevEval.Application.Products.Commands;
using DevEval.Application.Products.Dtos;
using DevEval.Domain.Repositories;
using MediatR;

namespace DevEval.Application.Products.Handlers
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, ProductDto>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public UpdateProductHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var existingProduct = await _repository.GetByIdAsync(request.Id);

            if (existingProduct == null) throw new KeyNotFoundException($"Product with ID {request.Id} not found.");

            _mapper.Map(request, existingProduct);

            var updatedProduct = await _repository.UpdateAsync(existingProduct);

            var result = _mapper.Map<ProductDto>(updatedProduct);

            return result;
        }
    }
}
