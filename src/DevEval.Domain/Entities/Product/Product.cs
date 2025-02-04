using DevEval.Domain.ValueObjects;

namespace DevEval.Domain.Entities.Product
{
    /// <summary>
    /// Represents a product with its details and metadata.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Gets the unique identifier of the product.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Gets the title of the product.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Gets the price of the product.
        /// </summary>
        public decimal Price { get; private set; }

        /// <summary>
        /// Gets the description of the product.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets the category of the product.
        /// </summary>
        public string Category { get; private set; }

        /// <summary>
        /// Gets the URL of the product image.
        /// </summary>
        public string Image { get; private set; }

        /// <summary>
        /// Gets the rating information for the product.
        /// </summary>
        public Rating? Rating { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Product"/> class.
        /// </summary>
        /// <param name="title">The title of the product.</param>
        /// <param name="price">The price of the product.</param>
        /// <param name="description">The description of the product.</param>
        /// <param name="image">The URL of the product image.</param>
        /// <param name="category">The category of the product.</param>
        public Product(string title, decimal price, string description, string image, string? category = null)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be null or empty.", nameof(title));

            if (price <= 0)
                throw new ArgumentOutOfRangeException(nameof(price), "Price must be greater than zero.");

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description cannot be null or empty.", nameof(description));

            if (string.IsNullOrWhiteSpace(image))
                throw new ArgumentException("Image URL cannot be null or empty.", nameof(image));

            Title = title;
            Price = price;
            Description = description;
            Image = image;
            Category = category ?? string.Empty;
        }

        /// <summary>
        /// Updates the title of the product.
        /// </summary>
        /// <param name="title">The new title.</param>
        public void UpdateTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be null or empty.", nameof(title));

            Title = title;
        }

        /// <summary>
        /// Updates the price of the product.
        /// </summary>
        /// <param name="price">The new price.</param>
        public void UpdatePrice(decimal price)
        {
            if (price <= 0)
                throw new ArgumentOutOfRangeException(nameof(price), "Price must be greater than zero.");

            Price = price;
        }

        /// <summary>
        /// Updates the description of the product.
        /// </summary>
        /// <param name="description">The new description.</param>
        public void UpdateDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description cannot be null or empty.", nameof(description));

            Description = description;
        }

        /// <summary>
        /// Updates the category of the product.
        /// </summary>
        /// <param name="category">The new category.</param>
        public void UpdateCategory(string category)
        {
            Category = category;
        }

        /// <summary>
        /// Updates the image URL of the product.
        /// </summary>
        /// <param name="image">The new image URL.</param>
        public void UpdateImage(string image)
        {
            if (string.IsNullOrWhiteSpace(image))
                throw new ArgumentException("Image URL cannot be null or empty.", nameof(image));

            Image = image;
        }

        /// <summary>
        /// Updates the rating of the product.
        /// </summary>
        /// <param name="rating">The new rating.</param>
        public void UpdateRating(Rating rating)
        {
            Rating = rating;
        }
    }
}