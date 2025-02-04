using AutoMapper;
using DevEval.Application.Products.Dtos;
using DevEval.Application.Products.Queries;
using DevEval.Domain.Repositories;
using MediatR;

namespace DevEval.Application.Products.Handlers
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public GetProductByIdHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.Id);

            var result = _mapper.Map<ProductDto>(product);

            return result;
        }
    }
}
