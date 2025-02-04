using Rebus.Bus;
using DevEval.Application.Sales.Events;
using DevEval.Domain.Entities.Sale;

namespace DevEval.Application.Sales.Services
{
    public class SaleEventPublisher : ISaleEventPublisher
    {
        private readonly IBus _bus;

        public SaleEventPublisher(IBus bus)
        {
            _bus = bus;
        }

        public async Task PublishSaleCreatedAsync(Sale sale)
        {
            var saleCreatedEvent = new SaleCreatedEvent
            {
                SaleId = sale.Id,
                SaleDate = sale.SaleDate,
                TotalAmount = sale.TotalAmount
            };

            await _bus.Publish(saleCreatedEvent);
        }

        public async Task PublishSaleModifiedAsync(Sale sale)
        {
            var saleModifiedEvent = new SaleModifiedEvent
            {
                SaleId = sale.Id,
                SaleDate = sale.SaleDate,
                TotalAmount = sale.TotalAmount
            };

            await _bus.Publish(saleModifiedEvent);
        }

        public async Task PublishSaleCancelledAsync(Sale sale, string reason)
        {
            var saleCancelledEvent = new SaleCancelledEvent
            {
                SaleId = sale.Id,
                Reason = reason
            };

            await _bus.Publish(saleCancelledEvent);
        }

        public async Task PublishItemCancelledAsync(Guid saleId, SaleItem saleItem, string reason)
        {
            var itemCancelledEvent = new ItemCancelledEvent
            {
                ItemId = saleItem.ProductId,
                SaleId = saleId,
                Reason = reason
            };

            await _bus.Publish(itemCancelledEvent);
        }
    }
}