namespace DevEval.Application.Sales.Events
{
    public class SaleCancelledEvent
    {
        public required Guid SaleId { get; set; }
        public required string Reason { get; set; }
    }
}
