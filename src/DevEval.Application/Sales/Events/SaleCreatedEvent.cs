namespace DevEval.Application.Sales.Events
{
    public class SaleCreatedEvent
    {
        public Guid SaleId { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
