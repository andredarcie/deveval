namespace DevEval.Application.Carts.Dtos
{
    /// <summary>
    /// DTO for returning cart information.
    /// </summary>
    public class CartDto
    {
        /// <summary>
        /// The ID of the cart.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The ID of the user who owns the cart.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// The date when the cart was created.
        /// </summary>
        public string Date { get; set; } = string.Empty;

        /// <summary>
        /// The list of products in the cart.
        /// </summary>
        public List<CartProductDto> Products { get; set; } = new List<CartProductDto>();
    }
}
