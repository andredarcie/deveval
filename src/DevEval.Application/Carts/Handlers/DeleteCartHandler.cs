using DevEval.Application.Carts.Commands;
using DevEval.Domain.Repositories;
using MediatR;

namespace DevEval.Application.Carts.Handlers
{
    public class DeleteCartHandler : IRequestHandler<DeleteCartCommand>
    {
        private readonly ICartRepository _repository;

        public DeleteCartHandler(ICartRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteCartCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(request.Id);
        }
    }
}