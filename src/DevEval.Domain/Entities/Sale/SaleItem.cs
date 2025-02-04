namespace DevEval.Domain.Entities.Sale
{
    /// <summary>
    /// Represents an item in a sale with its details and metadata.
    /// </summary>
    public class SaleItem
    {
        /// <summary>
        /// Gets the unique identifier of the sale item.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets the product ID of the sale item.
        /// </summary>
        public int ProductId { get; private set; }

        /// <summary>
        /// Gets the quantity of the product in the sale.
        /// </summary>
        public int Quantity { get; private set; }

        /// <summary>
        /// Gets the unit price of the product.
        /// </summary>
        public decimal UnitPrice { get; private set; }

        /// <summary>
        /// Gets the discount applied to the product.
        /// </summary>
        public decimal Discount { get; private set; }

        /// <summary>
        /// Indicates whether the sale item has been cancelled.
        /// </summary>
        public bool IsCancelled { get; private set; }

        /// <summary>
        /// Gets the total price of the sale item, including discount.
        /// </summary>
        public decimal TotalPrice => Quantity * UnitPrice * (1 - Discount);

        /// <summary>
        /// Initializes a new instance of the <see cref="SaleItem"/> class.
        /// </summary>
        /// <param name="productId">The ID of the product.</param>
        /// <param name="quantity">The quantity of the product.</param>
        /// <param name="unitPrice">The unit price of the product.</param>
        /// <param name="discount">The discount applied to the product.</param>
        public SaleItem(int productId, int quantity, decimal unitPrice, decimal discount)
        {
            if (productId <= 0)
                throw new ArgumentOutOfRangeException(nameof(productId), "ProductId must be greater than zero.");

            if (quantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than zero.");

            if (unitPrice <= 0)
                throw new ArgumentOutOfRangeException(nameof(unitPrice), "Unit price must be greater than zero.");

            if (discount < 0 || discount > 1)
                throw new ArgumentOutOfRangeException(nameof(discount), "Discount must be between 0 and 1.");

            Id = Guid.NewGuid();
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Discount = discount;
            IsCancelled = false;
        }

        /// <summary>
        /// Cancels the sale item.
        /// </summary>
        public void CancelItem()
        {
            IsCancelled = true;
        }

        /// <summary>
        /// Updates the quantity of the sale item.
        /// </summary>
        /// <param name="quantity">The new quantity.</param>
        public void UpdateQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than zero.");

            Quantity = quantity;
        }

        /// <summary>
        /// Updates the unit price of the sale item.
        /// </summary>
        /// <param name="unitPrice">The new unit price.</param>
        public void UpdateUnitPrice(decimal unitPrice)
        {
            if (unitPrice <= 0)
                throw new ArgumentOutOfRangeException(nameof(unitPrice), "Unit price must be greater than zero.");

            UnitPrice = unitPrice;
        }

        /// <summary>
        /// Updates the discount applied to the sale item.
        /// </summary>
        /// <param name="discount">The new discount.</param>
        public void UpdateDiscount(decimal discount)
        {
            if (discount < 0 || discount > 1)
                throw new ArgumentOutOfRangeException(nameof(discount), "Discount must be between 0 and 1.");

            Discount = discount;
        }
    }
}