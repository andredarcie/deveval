using AutoMapper;
using DevEval.Application.Products.Commands;
using DevEval.Application.Products.Dtos;
using DevEval.Application.Products.Profiles;
using DevEval.Domain.Entities.Product;
using DevEval.Domain.ValueObjects;

namespace DevEval.Test.Application.Products.Profiles
{
    public class ProductProfileTests
    {
        private readonly IMapper _mapper;

        public ProductProfileTests()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<ProductProfile>());
            _mapper = configuration.CreateMapper();
        }

        [Fact]
        public void Configuration_ShouldBeValid()
        {
            // Validate the AutoMapper configuration
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        [Fact]
        public void Map_CreateProductCommand_To_Product_ShouldWork_WithValidData()
        {
            // Arrange
            var command = new CreateProductCommand
            {
                Title = "Test Product",
                Price = 99.99m,
                Description = "Test Description",
                Category = "Test Category",
                Image = "test-image-url",
                Rating = new RatingDto { Rate = 4.5, Count = 10 }
            };

            // Act
            var product = _mapper.Map<Product>(command);

            // Assert
            Assert.NotNull(product);
            Assert.Equal(command.Title, product.Title);
            Assert.Equal(command.Price, product.Price);
            Assert.Equal(command.Description, product.Description);
            Assert.Equal(command.Category, product.Category);
            Assert.Equal(command.Image, product.Image);
            Assert.NotNull(product.Rating);
            Assert.Equal(command.Rating.Rate, product.Rating.Rate);
            Assert.Equal(command.Rating.Count, product.Rating.Count);
        }

        [Fact]
        public void Map_CreateProductCommand_To_Product_ShouldWork_WithNullRating()
        {
            // Arrange
            var command = new CreateProductCommand
            {
                Title = "Test Product",
                Price = 99.99m,
                Description = "Test Description",
                Category = "Test Category",
                Image = "test-image-url",
                Rating = null
            };

            // Act
            var product = _mapper.Map<Product>(command);

            // Assert
            Assert.NotNull(product);
            Assert.Equal(command.Title, product.Title);
            Assert.Equal(command.Price, product.Price);
            Assert.Equal(command.Description, product.Description);
            Assert.Equal(command.Category, product.Category);
            Assert.Equal(command.Image, product.Image);
            Assert.NotNull(product.Rating);
            Assert.Equal(Rating.Empty.Rate, product.Rating.Rate);
            Assert.Equal(Rating.Empty.Count, product.Rating.Count);
        }

        [Fact]
        public void Map_Product_To_ProductDto_ShouldWork_WithValidData()
        {
            // Arrange
            var product = new Product("Test Product", 99.99m, "Test Description", "test-image-url", "Test Category")
            {
                Rating = new Rating(4.5, 10)
            };

            // Act
            var productDto = _mapper.Map<ProductDto>(product);

            // Assert
            Assert.NotNull(productDto);
            Assert.Equal(product.Title, productDto.Title);
            Assert.Equal(product.Price, productDto.Price);
            Assert.Equal(product.Description, productDto.Description);
            Assert.Equal(product.Category, productDto.Category);
            Assert.Equal(product.Image, productDto.Image);
            Assert.NotNull(productDto.Rating);
            Assert.Equal(product.Rating.Rate, productDto.Rating.Rate);
            Assert.Equal(product.Rating.Count, productDto.Rating.Count);
        }

        [Fact]
        public void Map_Product_To_ProductDto_ShouldWork_WithNullRating()
        {
            // Arrange
            var product = new Product("Test Product", 99.99m, "Test Description", "test-image-url", "Test Category");

            // Act
            var productDto = _mapper.Map<ProductDto>(product);

            // Assert
            Assert.NotNull(productDto);
            Assert.Equal(product.Title, productDto.Title);
            Assert.Equal(product.Price, productDto.Price);
            Assert.Equal(product.Description, productDto.Description);
            Assert.Equal(product.Category, productDto.Category);
            Assert.Equal(product.Image, productDto.Image);
            Assert.Null(productDto.Rating);
        }

        [Fact]
        public void Map_UpdateProductCommand_To_Product_ShouldWork_WithValidData()
        {
            // Arrange
            var command = new UpdateProductCommand
            {
                Title = "Updated Product",
                Price = 149.99m,
                Description = "Updated Description",
                Category = "Updated Category",
                Image = "updated-image-url",
                Rating = new RatingDto { Rate = 4.8, Count = 20 }
            };

            var product = new Product("Test Product", 99.99m, "Test Description", "test-image-url", "Test Category")
            {
                Rating = new Rating(4.5, 10)
            };

            // Act
            _mapper.Map(command, product);

            // Assert
            Assert.NotNull(product);
            Assert.Equal(command.Title, product.Title);
            Assert.Equal(command.Price, product.Price);
            Assert.Equal(command.Description, product.Description);
            Assert.Equal(command.Category, product.Category);
            Assert.Equal(command.Image, product.Image);
            Assert.NotNull(product.Rating);
            Assert.Equal(command.Rating.Rate, product.Rating.Rate);
            Assert.Equal(command.Rating.Count, product.Rating.Count);
        }

        [Fact]
        public void Map_UpdateProductCommand_To_Product_ShouldWork_WithNullRating()
        {
            // Arrange
            var command = new UpdateProductCommand
            {
                Title = "Updated Product",
                Price = 149.99m,
                Description = "Updated Description",
                Category = "Updated Category",
                Image = "updated-image-url",
                Rating = null
            };

            var product = new Product("Test Product", 99.99m, "Test Description", "test-image-url", "Test Category")
            {
                Rating = new Rating(4.5, 10)
            };

            // Act
            _mapper.Map(command, product);

            // Assert
            Assert.NotNull(product);
            Assert.Equal(command.Title, product.Title);
            Assert.Equal(command.Price, product.Price);
            Assert.Equal(command.Description, product.Description);
            Assert.Equal(command.Category, product.Category);
            Assert.Equal(command.Image, product.Image);
            Assert.NotNull(product.Rating);
            Assert.Equal(Rating.Empty.Rate, product.Rating.Rate);
            Assert.Equal(Rating.Empty.Count, product.Rating.Count);
        }
    }
}