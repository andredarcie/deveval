using DevEval.Application.Carts.Dtos;
using DevEval.Common.Helpers.Pagination;
using MediatR;

namespace DevEval.Application.Carts.Queries
{
    public class GetCartsQuery : IRequest<PaginatedResult<CartDto>>
    {
        public PaginationParameters Parameters { get; set; }

        public GetCartsQuery(PaginationParameters parameters)
        {
            Parameters = parameters;
        }
    }
}
