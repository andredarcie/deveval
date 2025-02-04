using MediatR;

namespace DevEval.Application.Carts.Commands
{
    public class DeleteCartCommand : IRequest
    {
        public DeleteCartCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
