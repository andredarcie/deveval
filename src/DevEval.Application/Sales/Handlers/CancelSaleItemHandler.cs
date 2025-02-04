using DevEval.Application.Sales.Commands;
using DevEval.Application.Sales.Services;
using DevEval.Domain.Repositories;
using MediatR;

public class CancelSaleItemHandler : IRequestHandler<CancelSaleItemCommand, bool>
{
    private readonly ISaleRepository _repository;
    private readonly ISaleEventPublisher _eventPublisher;

    public CancelSaleItemHandler(ISaleRepository repository, ISaleEventPublisher eventPublisher)
    {
        _repository = repository;
        _eventPublisher = eventPublisher;
    }

    public async Task<bool> Handle(CancelSaleItemCommand request, CancellationToken cancellationToken)
    {
        var sale = await _repository.GetByIdAsync(request.SaleId);
        if (sale == null)
        {
            throw new KeyNotFoundException($"Sale with ID {request.SaleId} not found.");
        }

        var saleItem = sale.Items.FirstOrDefault(i => i.Id == request.ItemId);
        if (saleItem == null)
        {
            throw new KeyNotFoundException($"Item with ID {request.ItemId} not found in sale {request.SaleId}.");
        }

        saleItem.CancelItem();

        await _repository.UpdateAsync(sale);

        await _eventPublisher.PublishItemCancelledAsync(request.SaleId, saleItem, request.Reason);

        return true;
    }
}