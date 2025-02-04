using AutoMapper;
using DevEval.Application.Sales.Dtos;
using DevEval.Application.Sales.Queries;
using DevEval.Common.Helpers.Pagination;
using DevEval.Domain.Repositories;
using MediatR;

namespace DevEval.Application.Users.Handlers
{
    public class GetSalesHandler : IRequestHandler<GetSalesQuery, PaginatedResult<SaleDto>>
    {
        private readonly ISaleRepository _repository;
        private readonly IMapper _mapper;

        public GetSalesHandler(ISaleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<SaleDto>> Handle(GetSalesQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllAsync(request.Parameters);

            return new PaginatedResult<SaleDto>
            {
                Items = _mapper.Map<List<SaleDto>>(result.Items),
                TotalItems = result.TotalItems,
                CurrentPage = result.CurrentPage,
                TotalPages = result.TotalPages
            };
        }
    }
}
