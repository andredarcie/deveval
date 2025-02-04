using DevEval.Application.Sales.Dtos;
using MediatR;

namespace DevEval.Application.Sales.Queries
{
    public class GetSaleByIdQuery : IRequest<SaleDto>
    {
        public Guid Id { get; set; }

        public GetSaleByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}