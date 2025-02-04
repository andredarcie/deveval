using DevEval.Domain.ValueObjects;

namespace DevEval.Tests.ValueObjects
{
    public class AddressTests
    {
        [Fact]
        public void Constructor_ShouldCreateAddress_WhenValidArgumentsProvided()
        {
            // Arrange
            var city = "Sample City";
            var street = "Sample Street";
            var number = 123;
            var zipCode = "12345-678";
            var geolocation = new Geolocation(12.34, 56.78);

            // Act
            var address = new Address(city, street, number, zipCode, geolocation);

            // Assert
            Assert.Equal(city, address.City);
            Assert.Equal(street, address.Street);
            Assert.Equal(number, address.Number);
            Assert.Equal(zipCode, address.ZipCode);
            Assert.Equal(geolocation, address.Geolocation);
        }

        [Theory]
        [InlineData(null, "Street", 1, "12345", "City cannot be null or empty.")]
        [InlineData("", "Street", 1, "12345", "City cannot be null or empty.")]
        [InlineData("City", null, 1, "12345", "Street cannot be null or empty.")]
        [InlineData("City", "", 1, "12345", "Street cannot be null or empty.")]
        [InlineData("City", "Street", 0, "12345", "Number must be greater than zero.")]
        [InlineData("City", "Street", -1, "12345", "Number must be greater than zero.")]
        [InlineData("City", "Street", 1, null, "ZIP code cannot be null or empty.")]
        [InlineData("City", "Street", 1, "", "ZIP code cannot be null or empty.")]
        public void Constructor_ShouldThrowException_WhenInvalidArgumentsProvided(
            string city,
            string street,
            int number,
            string zipCode,
            string expectedMessage)
        {
            // Arrange
            var geolocation = new Geolocation(12.34, 56.78);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                new Address(city, street, number, zipCode, geolocation));

            Assert.Contains(expectedMessage, exception.Message);
        }

        [Fact]
        public void Empty_ShouldReturnDefaultAddress()
        {
            // Act
            var emptyAddress = Address.Empty;

            // Assert
            Assert.Equal("N/A", emptyAddress.City);
            Assert.Equal("N/A", emptyAddress.Street);
            Assert.Equal(1, emptyAddress.Number);
            Assert.Equal("N/A", emptyAddress.ZipCode);
            Assert.Equal(Geolocation.Empty, emptyAddress.Geolocation);
        }

        [Fact]
        public void ToString_ShouldReturnFormattedAddress()
        {
            // Arrange
            var city = "Sample City";
            var street = "Sample Street";
            var number = 123;
            var zipCode = "12345-678";
            var geolocation = new Geolocation(12.34, 56.78);
            var address = new Address(city, street, number, zipCode, geolocation);

            // Act
            var result = address.ToString();

            // Assert
            Assert.Equal("Sample Street, 123, Sample City - 12345-678 (Geolocation: 12,34, 56,78)", result);
        }

        [Fact]
        public void Equals_ShouldReturnTrue_WhenAddressesAreEqual()
        {
            // Arrange
            var geolocation = new Geolocation(12.34, 56.78);
            var address1 = new Address("City", "Street", 123, "12345", geolocation);
            var address2 = new Address("City", "Street", 123, "12345", geolocation);

            // Act
            var areEqual = address1.Equals(address2);

            // Assert
            Assert.True(areEqual);
        }

        [Fact]
        public void Equals_ShouldReturnFalse_WhenAddressesAreDifferent()
        {
            // Arrange
            var geolocation1 = new Geolocation(12.34, 56.78);
            var geolocation2 = new Geolocation(34.56, 78.90);
            var address1 = new Address("City1", "Street1", 123, "12345", geolocation1);
            var address2 = new Address("City2", "Street2", 456, "67890", geolocation2);

            // Act
            var areEqual = address1.Equals(address2);

            // Assert
            Assert.False(areEqual);
        }
    }
}