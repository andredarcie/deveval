using AutoMapper;
using DevEval.Application.Products.Dtos;
using DevEval.Application.Products.Queries;
using DevEval.Common.Helpers.Pagination;
using DevEval.Domain.Repositories;
using MediatR;

namespace DevEval.Application.Products.Handlers
{
    public class GetProductsByCategoryHandler : IRequestHandler<GetProductsByCategoryQuery, PaginatedResult<ProductDto>>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public GetProductsByCategoryHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<ProductDto>> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
        {
            var products = await _repository.GetProductsByCategoryAsync(request.Category, request.Parameters);

            return new PaginatedResult<ProductDto>
            {
                Items = _mapper.Map<List<ProductDto>>(products.Items),
                TotalItems = products.TotalItems,
                CurrentPage = products.CurrentPage,
                TotalPages = products.TotalPages
            };
        }
    }
}
