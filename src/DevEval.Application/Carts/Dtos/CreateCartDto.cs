namespace DevEval.Application.Carts.Dtos
{
    /// <summary>
    /// DTO for creating a new cart.
    /// </summary>
    public class CreateCartDto
    {
        /// <summary>
        /// The ID of the user creating the cart.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// The date when the cart is created.
        /// </summary>
        public string Date { get; set; } = string.Empty;

        /// <summary>
        /// The list of products in the cart.
        /// </summary>
        public List<CreateCartProductDto> Products { get; set; } = new();
    }
}
