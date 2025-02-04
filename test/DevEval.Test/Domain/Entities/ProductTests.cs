using DevEval.Domain.Entities.Product;
using DevEval.Domain.ValueObjects;

namespace DevEval.Test.Domain.Entities
{
    public class ProductTests
    {
        [Fact]
        public void Should_Create_Product_With_Valid_Values()
        {
            // Arrange
            string title = "Test Product";
            decimal price = 99.99m;
            string description = "This is a test product.";
            string image = "https://example.com/image.png";
            string category = "Electronics";

            // Act
            var product = new Product(title, price, description, image, category);

            // Assert
            Assert.Equal(title, product.Title);
            Assert.Equal(price, product.Price);
            Assert.Equal(description, product.Description);
            Assert.Equal(image, product.Image);
            Assert.Equal(category, product.Category);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Should_Throw_Exception_When_Creating_Product_With_Invalid_Title(string invalidTitle)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Product(invalidTitle, 99.99m, "Description", "https://image.com", "Category"));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public void Should_Throw_Exception_When_Creating_Product_With_Invalid_Price(decimal invalidPrice)
        {
            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new Product("Test", invalidPrice, "Description", "https://image.com", "Category"));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Should_Throw_Exception_When_Creating_Product_With_Invalid_Description(string invalidDescription)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Product("Test", 99.99m, invalidDescription, "https://image.com", "Category"));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Should_Throw_Exception_When_Creating_Product_With_Invalid_Image(string invalidImage)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Product("Test", 99.99m, "Description", invalidImage, "Category"));
        }

        [Fact]
        public void Should_Update_Title_Correctly()
        {
            // Arrange
            var product = new Product("Old Title", 99.99m, "Description", "https://image.com");
            string newTitle = "New Title";

            // Act
            product.UpdateTitle(newTitle);

            // Assert
            Assert.Equal(newTitle, product.Title);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Should_Throw_Exception_When_Updating_Title_With_Invalid_Value(string invalidTitle)
        {
            // Arrange
            var product = new Product("Old Title", 99.99m, "Description", "https://image.com");

            // Act & Assert
            Assert.Throws<ArgumentException>(() => product.UpdateTitle(invalidTitle));
        }

        [Fact]
        public void Should_Update_Price_Correctly()
        {
            // Arrange
            var product = new Product("Test", 99.99m, "Description", "https://image.com");
            decimal newPrice = 149.99m;

            // Act
            product.UpdatePrice(newPrice);

            // Assert
            Assert.Equal(newPrice, product.Price);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-20)]
        public void Should_Throw_Exception_When_Updating_Price_With_Invalid_Value(decimal invalidPrice)
        {
            // Arrange
            var product = new Product("Test", 99.99m, "Description", "https://image.com");

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => product.UpdatePrice(invalidPrice));
        }

        [Fact]
        public void Should_Update_Description_Correctly()
        {
            // Arrange
            var product = new Product("Test", 99.99m, "Old Description", "https://image.com");
            string newDescription = "New product description.";

            // Act
            product.UpdateDescription(newDescription);

            // Assert
            Assert.Equal(newDescription, product.Description);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Should_Throw_Exception_When_Updating_Description_With_Invalid_Value(string invalidDescription)
        {
            // Arrange
            var product = new Product("Test", 99.99m, "Old Description", "https://image.com");

            // Act & Assert
            Assert.Throws<ArgumentException>(() => product.UpdateDescription(invalidDescription));
        }

        [Fact]
        public void Should_Update_Category_Correctly()
        {
            // Arrange
            var product = new Product("Test", 99.99m, "Description", "https://image.com");
            string newCategory = "Home Appliances";

            // Act
            product.UpdateCategory(newCategory);

            // Assert
            Assert.Equal(newCategory, product.Category);
        }

        [Fact]
        public void Should_Update_Image_Correctly()
        {
            // Arrange
            var product = new Product("Test", 99.99m, "Description", "https://oldimage.com");
            string newImage = "https://newimage.com";

            // Act
            product.UpdateImage(newImage);

            // Assert
            Assert.Equal(newImage, product.Image);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Should_Throw_Exception_When_Updating_Image_With_Invalid_Value(string invalidImage)
        {
            // Arrange
            var product = new Product("Test", 99.99m, "Description", "https://image.com");

            // Act & Assert
            Assert.Throws<ArgumentException>(() => product.UpdateImage(invalidImage));
        }

        [Fact]
        public void Should_Update_Rating_Correctly()
        {
            // Arrange
            var product = new Product("Test", 99.99m, "Description", "https://image.com");
            var newRating = new Rating(4.5, 100);

            // Act
            product.UpdateRating(newRating);

            // Assert
            Assert.Equal(newRating, product.Rating);
        }
    }
}