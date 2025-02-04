using DevEval.Domain.Entities.Sale;

namespace DevEval.Application.Sales.Services
{
    public interface ISaleEventPublisher
    {
        Task PublishItemCancelledAsync(Guid saleId, SaleItem saleItem, string reason);
        Task PublishSaleCancelledAsync(Sale sale, string reason);
        Task PublishSaleCreatedAsync(Sale sale);
        Task PublishSaleModifiedAsync(Sale sale);
    }
}