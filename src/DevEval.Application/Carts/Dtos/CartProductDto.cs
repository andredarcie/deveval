namespace DevEval.Application.Carts.Dtos
{
    /// <summary>
    /// DTO for the products in the cart.
    /// </summary>
    public class CartProductDto
    {
        /// <summary>
        /// Gets the product ID.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets the unit price of the product.
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Gets the quantity of the product.
        /// </summary>
        public int Quantity { get; set; }
    }
}
