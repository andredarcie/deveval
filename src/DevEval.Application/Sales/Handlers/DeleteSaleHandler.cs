using DevEval.Application.Sales.Commands;
using DevEval.Application.Sales.Services;
using DevEval.Domain.Repositories;
using MediatR;

namespace DevEval.Application.Sales.Handlers
{
    public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand>
    {
        private readonly ISaleRepository _repository;
        private readonly ISaleEventPublisher _eventPublisher;

        public DeleteSaleHandler(ISaleRepository repository, ISaleEventPublisher eventPublisher)
        {
            _repository = repository;
            _eventPublisher = eventPublisher;
        }

        public async Task Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _repository.GetByIdAsync(request.Id);
            if (sale == null)
            {
                throw new KeyNotFoundException($"Sale with ID {request.Id} not found.");
            }

            await _eventPublisher.PublishSaleCancelledAsync(sale, "Cancelled by user");

            await _repository.DeleteAsync(request.Id);
        }
    }
}