namespace DevEval.Application.Sales.Events
{
    public class SaleModifiedEvent
    {
        public Guid SaleId { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
