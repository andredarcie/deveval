using DevEval.Domain.Entities.Cart;

namespace DevEval.Test.Domain.Entities
{
    public class CartTests
    {
        [Fact]
        public void Should_Create_Cart_With_Valid_UserId()
        {
            // Arrange
            int userId = 1;
            DateTime date = DateTime.UtcNow;
            var products = new List<CartProduct>();

            // Act
            var cart = new Cart(userId, date, products);

            // Assert
            Assert.Equal(userId, cart.UserId);
            Assert.Equal(date, cart.Date);
            Assert.Empty(cart.Products);
        }

        [Fact]
        public void Should_Throw_Exception_When_Creating_Cart_With_Invalid_UserId()
        {
            // Arrange
            int invalidUserId = 0;
            DateTime date = DateTime.UtcNow;
            var products = new List<CartProduct>();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Cart(invalidUserId, date, products));
        }

        [Fact]
        public void Should_Throw_Exception_When_Creating_Cart_With_Default_Date()
        {
            // Arrange
            int userId = 1;
            DateTime invalidDate = default;
            var products = new List<CartProduct>();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Cart(userId, invalidDate, products));
        }

        [Fact]
        public void Should_Add_Product_To_Cart()
        {
            // Arrange
            var cart = new Cart(1);
            var product = new CartProduct(1, 10.0m, 1);

            // Act
            cart.AddProduct(product);

            // Assert
            Assert.Contains(product, cart.Products);
        }

        [Fact]
        public void Should_Throw_Exception_When_Adding_Null_Product()
        {
            // Arrange
            var cart = new Cart(1);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => cart.AddProduct(null));
        }

        [Fact]
        public void Should_Remove_Product_From_Cart()
        {
            // Arrange
            var cart = new Cart(1);
            var product = new CartProduct(1, 10.0m, 1);
            cart.AddProduct(product);

            // Act
            cart.RemoveProduct(1);

            // Assert
            Assert.DoesNotContain(product, cart.Products);
        }

        [Fact]
        public void Should_Throw_Exception_When_Removing_Nonexistent_Product()
        {
            // Arrange
            var cart = new Cart(1);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => cart.RemoveProduct(999));
        }

        [Fact]
        public void Should_Clear_All_Products_From_Cart()
        {
            // Arrange
            var cart = new Cart(1);
            cart.AddProduct(new CartProduct(1, 10.0m, 1));
            cart.AddProduct(new CartProduct(2, 15.0m, 2));

            // Act
            cart.ClearProducts();

            // Assert
            Assert.Empty(cart.Products);
        }
    }
}