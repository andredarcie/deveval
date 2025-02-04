using MediatR;

namespace DevEval.Application.Sales.Commands
{
    public class DeleteSaleCommand : IRequest
    {
        public DeleteSaleCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
