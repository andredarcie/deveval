namespace DevEval.Domain.Entities.Cart
{
    /// <summary>
    /// Represents a product in the shopping cart with its quantity.
    /// </summary>
    public class CartProduct
    {
        /// <summary>
        /// Gets the unique identifier of the cart product.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Gets the product ID.
        /// </summary>
        public int ProductId { get; private set; }

        /// <summary>
        /// Gets the unit price of the product.
        /// </summary>
        public decimal UnitPrice { get; private set; }

        /// <summary>
        /// Gets the quantity of the product.
        /// </summary>
        public int Quantity { get; private set; }

        /// <summary>
        /// Gets the total price of the product (UnitPrice * Quantity).
        /// </summary>
        public decimal TotalPrice => UnitPrice * Quantity;

        /// <summary>
        /// Initializes a new instance of the <see cref="CartProduct"/> class.
        /// </summary>
        /// <param name="productId">The product ID.</param>
        /// <param name="unitPrice">The unit price of the product.</param>
        /// <param name="quantity">The quantity of the product.</param>
        public CartProduct(int productId, decimal unitPrice, int quantity)
        {
            if (productId <= 0)
                throw new ArgumentOutOfRangeException(nameof(productId), "ProductId must be greater than zero.");

            if (unitPrice <= 0)
                throw new ArgumentOutOfRangeException(nameof(unitPrice), "UnitPrice must be greater than zero.");

            if (quantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than zero.");

            ProductId = productId;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }

        /// <summary>
        /// Updates the quantity of the product.
        /// </summary>
        /// <param name="quantity">The new quantity.</param>
        public void UpdateQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than zero.");

            Quantity = quantity;
        }

        /// <summary>
        /// Updates the unit price of the product.
        /// </summary>
        /// <param name="unitPrice">The new unit price.</param>
        public void UpdateUnitPrice(decimal unitPrice)
        {
            if (unitPrice <= 0)
                throw new ArgumentOutOfRangeException(nameof(unitPrice), "UnitPrice must be greater than zero.");

            UnitPrice = unitPrice;
        }
    }
}