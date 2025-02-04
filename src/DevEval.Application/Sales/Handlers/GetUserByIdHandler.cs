using AutoMapper;
using DevEval.Application.Sales.Dtos;
using DevEval.Application.Sales.Queries;
using DevEval.Domain.Repositories;
using MediatR;

namespace DevEval.Application.Sales.Handlers
{
    public class GetSaleByIdHandler : IRequestHandler<GetSaleByIdQuery, SaleDto>
    {
        private readonly ISaleRepository _repository;
        private readonly IMapper _mapper;

        public GetSaleByIdHandler(ISaleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<SaleDto> Handle(GetSaleByIdQuery request, CancellationToken cancellationToken)
        {
            var sale = await _repository.GetByIdAsync(request.Id);

            var result = _mapper.Map<SaleDto>(sale);

            return result;
        }
    }
}
