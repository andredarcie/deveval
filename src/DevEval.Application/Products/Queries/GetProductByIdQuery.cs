using DevEval.Application.Products.Dtos;
using MediatR;

namespace DevEval.Application.Products.Queries
{
    public class GetProductByIdQuery : IRequest<ProductDto>
    {
        public int Id { get; set; }

        public GetProductByIdQuery(int id)
        {
            Id = id;
        }
    }
}
