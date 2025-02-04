namespace DevEval.Application.Sales.Events
{
    public class ItemCancelledEvent
    {
        public required int ItemId { get; set; }
        public required Guid SaleId { get; set; }
        public required string Reason { get; set; }
    }
}
