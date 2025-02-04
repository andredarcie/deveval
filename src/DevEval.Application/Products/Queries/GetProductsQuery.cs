using DevEval.Application.Products.Dtos;
using DevEval.Common.Helpers.Pagination;
using MediatR;

namespace DevEval.Application.Products.Queries
{
    public class GetProductsQuery : IRequest<PaginatedResult<ProductDto>>
    {
        public PaginationParameters Parameters { get; set; }

        public GetProductsQuery(PaginationParameters parameters)
        {
            Parameters = parameters;
        }
    }
}
