using AutoMapper;
using DevEval.Application.Carts.Commands;
using DevEval.Application.Carts.Dtos;
using DevEval.Application.Carts.Profiles;
using DevEval.Domain.Entities.Cart;

namespace DevEval.Test.Application.Carts.Profiles
{
    public class CartProfileTests
    {
        private readonly IMapper _mapper;

        public CartProfileTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CartProfile>();
            });
            _mapper = configuration.CreateMapper();
        }

        [Fact]
        public void Configuration_ShouldBeValid()
        {
            // Assert
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        [Fact]
        public void Map_Cart_To_CartDto_ShouldWork_WithValidData()
        {
            // Arrange
            var cart = new Cart(1, DateTime.UtcNow, new List<CartProduct>
            {
                new CartProduct(1, 10.5m, 2),
                new CartProduct(2, 5.0m, 3)
            });

            // Act
            var cartDto = _mapper.Map<CartDto>(cart);

            // Assert
            Assert.Equal(cart.UserId, cartDto.UserId);
            Assert.Equal(cart.Date.ToString("yyyy-MM-dd"), cartDto.Date);
            Assert.Equal(cart.Products.Count, cartDto.Products.Count);
            Assert.All(cartDto.Products, dto =>
            {
                var product = cart.Products.First(p => p.ProductId == dto.ProductId);
                Assert.Equal(product.ProductId, dto.ProductId);
                Assert.Equal(product.UnitPrice, dto.UnitPrice);
                Assert.Equal(product.Quantity, dto.Quantity);
            });
        }

        [Fact]
        public void Map_CartProductDto_To_CartProduct_ShouldWork()
        {
            // Arrange
            var productDto = new CartProductDto
            {
                ProductId = 1,
                UnitPrice = 10.5m,
                Quantity = 2
            };

            // Act
            var product = _mapper.Map<CartProduct>(productDto);

            // Assert
            Assert.Equal(productDto.ProductId, product.ProductId);
            Assert.Equal(productDto.UnitPrice, product.UnitPrice);
            Assert.Equal(productDto.Quantity, product.Quantity);
        }

        [Fact]
        public void Map_CartProduct_To_CartProductDto_ShouldWork()
        {
            // Arrange
            var product = new CartProduct(1, 10.5m, 2);

            // Act
            var productDto = _mapper.Map<CartProductDto>(product);

            // Assert
            Assert.Equal(product.ProductId, productDto.ProductId);
            Assert.Equal(product.UnitPrice, productDto.UnitPrice);
            Assert.Equal(product.Quantity, productDto.Quantity);
        }
    }
}