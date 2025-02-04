using DevEval.Domain.Entities.Cart;

namespace DevEval.Test.Domain.Entities
{
    public class CartProductTests
    {
        [Fact]
        public void Should_Create_CartProduct_With_Valid_Values()
        {
            // Arrange
            int productId = 1;
            decimal unitPrice = 10.5m;
            int quantity = 3;

            // Act
            var product = new CartProduct(productId, unitPrice, quantity);

            // Assert
            Assert.Equal(productId, product.ProductId);
            Assert.Equal(unitPrice, product.UnitPrice);
            Assert.Equal(quantity, product.Quantity);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Should_Throw_Exception_When_Creating_CartProduct_With_Invalid_ProductId(int invalidProductId)
        {
            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new CartProduct(invalidProductId, 10.0m, 1));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public void Should_Throw_Exception_When_Creating_CartProduct_With_Invalid_UnitPrice(decimal invalidUnitPrice)
        {
            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new CartProduct(1, invalidUnitPrice, 1));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void Should_Throw_Exception_When_Creating_CartProduct_With_Invalid_Quantity(int invalidQuantity)
        {
            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new CartProduct(1, 10.0m, invalidQuantity));
        }

        [Fact]
        public void Should_Calculate_TotalPrice_Correctly()
        {
            // Arrange
            var product = new CartProduct(1, 20.0m, 2);

            // Act
            decimal totalPrice = product.TotalPrice;

            // Assert
            Assert.Equal(40.0m, totalPrice);
        }

        [Fact]
        public void Should_Update_Quantity_Correctly()
        {
            // Arrange
            var product = new CartProduct(1, 10.0m, 2);
            int newQuantity = 5;

            // Act
            product.UpdateQuantity(newQuantity);

            // Assert
            Assert.Equal(newQuantity, product.Quantity);
        }

        [Fact]
        public void Should_Throw_Exception_When_Updating_Quantity_With_Invalid_Value()
        {
            // Arrange
            var product = new CartProduct(1, 10.0m, 2);

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => product.UpdateQuantity(0));
            Assert.Throws<ArgumentOutOfRangeException>(() => product.UpdateQuantity(-1));
        }

        [Fact]
        public void Should_Update_UnitPrice_Correctly()
        {
            // Arrange
            var product = new CartProduct(1, 10.0m, 2);
            decimal newPrice = 15.5m;

            // Act
            product.UpdateUnitPrice(newPrice);

            // Assert
            Assert.Equal(newPrice, product.UnitPrice);
        }

        [Fact]
        public void Should_Throw_Exception_When_Updating_UnitPrice_With_Invalid_Value()
        {
            // Arrange
            var product = new CartProduct(1, 10.0m, 2);

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => product.UpdateUnitPrice(0));
            Assert.Throws<ArgumentOutOfRangeException>(() => product.UpdateUnitPrice(-5.0m));
        }
    }
}