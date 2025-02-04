using DevEval.Application.Carts.Dtos;
using MediatR;

namespace DevEval.Application.Carts.Queries
{
    public class GetCartByIdQuery : IRequest<CartDto>
    {
        public int Id { get; set; }

        public GetCartByIdQuery(int id)
        {
            Id = id;
        }
    }
}
