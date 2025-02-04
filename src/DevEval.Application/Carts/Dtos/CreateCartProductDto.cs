namespace DevEval.Application.Carts.Dtos
{
    /// <summary>
    /// DTO for the products in the cart.
    /// </summary>
    public class CreateCartProductDto
    {
        /// <summary>
        /// The ID of the product.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// The quantity of the product in the cart.
        /// </summary>
        public int Quantity { get; set; }
    }
}
