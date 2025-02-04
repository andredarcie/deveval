using DevEval.Domain.Entities.Sale;

namespace DevEval.Test.Domain.Entities
{
    public class SaleItemTests
    {
        [Fact]
        public void Should_Create_SaleItem_With_Valid_Values()
        {
            // Arrange
            int productId = 1;
            int quantity = 3;
            decimal unitPrice = 100.0m;
            decimal discount = 0.10m;

            // Act
            var saleItem = new SaleItem(productId, quantity, unitPrice, discount);

            // Assert
            Assert.Equal(productId, saleItem.ProductId);
            Assert.Equal(quantity, saleItem.Quantity);
            Assert.Equal(unitPrice, saleItem.UnitPrice);
            Assert.Equal(discount, saleItem.Discount);
            Assert.False(saleItem.IsCancelled);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Should_Throw_Exception_When_Creating_SaleItem_With_Invalid_ProductId(int invalidProductId)
        {
            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new SaleItem(invalidProductId, 3, 100.0m, 0.10m));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void Should_Throw_Exception_When_Creating_SaleItem_With_Invalid_Quantity(int invalidQuantity)
        {
            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new SaleItem(1, invalidQuantity, 100.0m, 0.10m));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-50)]
        public void Should_Throw_Exception_When_Creating_SaleItem_With_Invalid_UnitPrice(decimal invalidUnitPrice)
        {
            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new SaleItem(1, 3, invalidUnitPrice, 0.10m));
        }

        [Theory]
        [InlineData(-0.1)]
        [InlineData(1.1)]
        public void Should_Throw_Exception_When_Creating_SaleItem_With_Invalid_Discount(decimal invalidDiscount)
        {
            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new SaleItem(1, 3, 100.0m, invalidDiscount));
        }

        [Fact]
        public void Should_Calculate_TotalPrice_Correctly()
        {
            // Arrange
            var saleItem = new SaleItem(1, 2, 50.0m, 0.10m);

            // Act
            decimal totalPrice = saleItem.TotalPrice;

            // Assert
            Assert.Equal(90.0m, totalPrice);
        }

        [Fact]
        public void Should_Cancel_SaleItem()
        {
            // Arrange
            var saleItem = new SaleItem(1, 2, 50.0m, 0.10m);

            // Act
            saleItem.CancelItem();

            // Assert
            Assert.True(saleItem.IsCancelled);
        }

        [Fact]
        public void Should_Update_Quantity_Correctly()
        {
            // Arrange
            var saleItem = new SaleItem(1, 2, 50.0m, 0.10m);
            int newQuantity = 5;

            // Act
            saleItem.UpdateQuantity(newQuantity);

            // Assert
            Assert.Equal(newQuantity, saleItem.Quantity);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Should_Throw_Exception_When_Updating_Quantity_With_Invalid_Value(int invalidQuantity)
        {
            // Arrange
            var saleItem = new SaleItem(1, 2, 50.0m, 0.10m);

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => saleItem.UpdateQuantity(invalidQuantity));
        }

        [Fact]
        public void Should_Update_UnitPrice_Correctly()
        {
            // Arrange
            var saleItem = new SaleItem(1, 2, 50.0m, 0.10m);
            decimal newPrice = 80.0m;

            // Act
            saleItem.UpdateUnitPrice(newPrice);

            // Assert
            Assert.Equal(newPrice, saleItem.UnitPrice);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-20)]
        public void Should_Throw_Exception_When_Updating_UnitPrice_With_Invalid_Value(decimal invalidPrice)
        {
            // Arrange
            var saleItem = new SaleItem(1, 2, 50.0m, 0.10m);

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => saleItem.UpdateUnitPrice(invalidPrice));
        }

        [Fact]
        public void Should_Update_Discount_Correctly()
        {
            // Arrange
            var saleItem = new SaleItem(1, 2, 50.0m, 0.10m);
            decimal newDiscount = 0.20m;

            // Act
            saleItem.UpdateDiscount(newDiscount);

            // Assert
            Assert.Equal(newDiscount, saleItem.Discount);
        }

        [Theory]
        [InlineData(-0.1)]
        [InlineData(1.1)]
        public void Should_Throw_Exception_When_Updating_Discount_With_Invalid_Value(decimal invalidDiscount)
        {
            // Arrange
            var saleItem = new SaleItem(1, 2, 50.0m, 0.10m);

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => saleItem.UpdateDiscount(invalidDiscount));
        }
    }
}