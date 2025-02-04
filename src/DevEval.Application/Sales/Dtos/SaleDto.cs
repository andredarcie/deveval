namespace DevEval.Application.Sales.Dtos
{
    public class SaleDto
    {
        public Guid Id { get; set; }
        public string SaleNumber { get; set; } = string.Empty;
        public DateTime SaleDate { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsCancelled { get; set; }
        public List<SaleItemDto> Items { get; set; } = new List<SaleItemDto>();
    }
}
