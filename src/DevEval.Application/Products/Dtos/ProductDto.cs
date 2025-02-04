namespace DevEval.Application.Products.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public RatingDto Rating { get; set; } = new RatingDto();
    }
}
