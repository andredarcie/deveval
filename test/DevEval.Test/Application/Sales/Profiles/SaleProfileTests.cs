using AutoMapper;
using DevEval.Application.Sales.Commands;
using DevEval.Application.Sales.Dtos;
using DevEval.Application.Sales.Profiles;
using DevEval.Domain.Entities.Sale;

namespace DevEval.Test.Application.Sales.Profiles
{
    public class SaleProfileTests
    {
        private readonly IMapper _mapper;

        public SaleProfileTests()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<SaleProfile>());
            _mapper = configuration.CreateMapper();
        }

        [Fact]
        public void Configuration_ShouldBeValid()
        {
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        [Fact]
        public void Map_CreateSaleCommand_To_Sale_ShouldWork_WithValidData()
        {
            // Arrange
            var command = new CreateSaleCommand
            {
                SaleNumber = "S12345",
                SaleDate = DateTime.UtcNow,
                CustomerId = Guid.NewGuid(),
                CustomerName = "John Doe",
                BranchId = Guid.NewGuid(),
                BranchName = "Main Branch",
                Items = new List<SaleItemDto>
                {
                    new SaleItemDto
                    {
                        ProductId = 1,
                        Quantity = 2,
                        UnitPrice = 100.0m,
                        Discount = 0.1m, // Valid discount
                        TotalPrice = 190.0m
                    }
                }
            };

            // Act
            var sale = _mapper.Map<Sale>(command);

            // Assert
            Assert.Equal(command.SaleNumber, sale.SaleNumber);
            Assert.Equal(command.SaleDate, sale.SaleDate);
            Assert.Equal(command.CustomerId, sale.CustomerId);
            Assert.Equal(command.CustomerName, sale.CustomerName);
            Assert.Equal(command.BranchId, sale.BranchId);
            Assert.Equal(command.BranchName, sale.BranchName);
            Assert.Single(sale.Items);
            var item = sale.Items.First();
            Assert.Equal(command.Items[0].ProductId, item.ProductId);
            Assert.Equal(command.Items[0].Quantity, item.Quantity);
            Assert.Equal(command.Items[0].UnitPrice, item.UnitPrice);
            Assert.False(sale.IsCancelled);
        }


        [Fact]
        public void Map_CreateSaleCommand_To_Sale_ShouldWork_WithDefaultDate()
        {
            // Arrange
            var command = new CreateSaleCommand
            {
                SaleNumber = "S12345",
                SaleDate = default,
                CustomerId = Guid.NewGuid(),
                CustomerName = "John Doe",
                BranchId = Guid.NewGuid(),
                BranchName = "Main Branch",
                Items = new List<SaleItemDto>()
            };

            // Act
            var sale = _mapper.Map<Sale>(command);

            // Assert
            Assert.Equal(command.SaleNumber, sale.SaleNumber);
            Assert.NotEqual(default, sale.SaleDate); // SaleDate should be set to DateTime.UtcNow
        }

        [Fact]
        public void Map_Sale_To_SaleDto_ShouldWork_WithValidData()
        {
            // Arrange
            var sale = new Sale("S12345", Guid.NewGuid(), "John Doe", Guid.NewGuid(), "Main Branch");
            sale.AddItem(1, 2, 100.0m);

            // Act
            var saleDto = _mapper.Map<SaleDto>(sale);

            // Assert
            Assert.NotNull(saleDto);
            Assert.Equal(sale.SaleNumber, saleDto.SaleNumber);
            Assert.Equal(sale.TotalAmount, saleDto.TotalAmount);
            Assert.Equal(sale.Items.Count, saleDto.Items.Count);

            // Assert Sale Items
            var saleItem = sale.Items.First();
            var saleItemDto = saleDto.Items.First();

            Assert.Equal(saleItem.ProductId, saleItemDto.ProductId);
            Assert.Equal(saleItem.Quantity, saleItemDto.Quantity);
            Assert.Equal(saleItem.UnitPrice, saleItemDto.UnitPrice);
            Assert.Equal(saleItem.TotalPrice, saleItemDto.TotalPrice);
        }

        [Fact]
        public void Map_Sale_To_SaleDto_ShouldWork_WithEmptyItems()
        {
            // Arrange
            var sale = new Sale("S12345", Guid.NewGuid(), "John Doe", Guid.NewGuid(), "Main Branch");

            // Act
            var saleDto = _mapper.Map<SaleDto>(sale);

            // Assert
            Assert.Empty(saleDto.Items);
        }

        [Fact]
        public void Map_SaleItem_To_SaleItemDto_ShouldWork()
        {
            // Arrange
            var saleItem = new SaleItem(1, 2, 100.0m, 0.1m);

            // Act
            var saleItemDto = _mapper.Map<SaleItemDto>(saleItem);

            // Assert
            Assert.Equal(saleItem.ProductId, saleItemDto.ProductId);
            Assert.Equal(saleItem.Quantity, saleItemDto.Quantity);
            Assert.Equal(saleItem.UnitPrice, saleItemDto.UnitPrice);
            Assert.Equal(saleItem.Discount, saleItemDto.Discount);
            Assert.Equal(saleItem.TotalPrice, saleItemDto.TotalPrice);
        }

        [Fact]
        public void Map_SaleItemDto_To_SaleItem_ShouldWork()
        {
            // Arrange
            var saleItemDto = new SaleItemDto
            {
                ProductId = 1,
                Quantity = 2,
                UnitPrice = 100.0m,
                Discount = 0.1m, // Valid discount
                TotalPrice = 190.0m
            };

            // Act
            var saleItem = _mapper.Map<SaleItem>(saleItemDto);

            // Assert
            Assert.Equal(saleItemDto.ProductId, saleItem.ProductId);
            Assert.Equal(saleItemDto.Quantity, saleItem.Quantity);
            Assert.Equal(saleItemDto.UnitPrice, saleItem.UnitPrice);
            Assert.Equal(saleItemDto.Discount, saleItem.Discount);
        }
    }
}