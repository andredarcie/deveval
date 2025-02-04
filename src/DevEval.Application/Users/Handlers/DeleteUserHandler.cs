using DevEval.Application.Users.Commands;
using DevEval.Domain.Repositories;
using MediatR;

namespace DevEval.Application.Users.Handlers
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUserRepository _repository;

        public DeleteUserHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(request.Id);
        }
    }
}