using DevEval.Domain.ValueObjects;

namespace DevEval.Tests.ValueObjects
{
    public class GeolocationTests
    {
        [Fact]
        public void Constructor_ShouldCreateGeolocation_WhenValidArgumentsProvided()
        {
            // Arrange
            var latitude = 12.34;
            var longitude = 56.78;

            // Act
            var geolocation = new Geolocation(latitude, longitude);

            // Assert
            Assert.Equal(latitude, geolocation.Lat);
            Assert.Equal(longitude, geolocation.Long);
        }

        [Theory]
        [InlineData(-91, 0, "Latitude must be between -90 and 90.")]
        [InlineData(91, 0, "Latitude must be between -90 and 90.")]
        [InlineData(0, -181, "Longitude must be between -180 and 180.")]
        [InlineData(0, 181, "Longitude must be between -180 and 180.")]
        public void Constructor_ShouldThrowException_WhenInvalidArgumentsProvided(
            double latitude,
            double longitude,
            string expectedMessage)
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                new Geolocation(latitude, longitude));

            Assert.Contains(expectedMessage, exception.Message);
        }

        [Fact]
        public void Empty_ShouldReturnDefaultGeolocation()
        {
            // Act
            var emptyGeolocation = Geolocation.Empty;

            // Assert
            Assert.Equal(0, emptyGeolocation.Lat);
            Assert.Equal(0, emptyGeolocation.Long);
        }

        [Fact]
        public void ToString_ShouldReturnFormattedGeolocation()
        {
            // Arrange
            var latitude = 12.34;
            var longitude = 56.78;
            var geolocation = new Geolocation(latitude, longitude);

            // Act
            var result = geolocation.ToString();

            // Assert
            Assert.Equal("Latitude: 12,34, Longitude: 56,78", result);
        }

        [Fact]
        public void Equals_ShouldReturnTrue_WhenGeolocationsAreEqual()
        {
            // Arrange
            var geolocation1 = new Geolocation(12.34, 56.78);
            var geolocation2 = new Geolocation(12.34, 56.78);

            // Act
            var areEqual = geolocation1.Equals(geolocation2);

            // Assert
            Assert.True(areEqual);
        }

        [Fact]
        public void Equals_ShouldReturnFalse_WhenGeolocationsAreDifferent()
        {
            // Arrange
            var geolocation1 = new Geolocation(12.34, 56.78);
            var geolocation2 = new Geolocation(34.56, 78.90);

            // Act
            var areEqual = geolocation1.Equals(geolocation2);

            // Assert
            Assert.False(areEqual);
        }

        [Fact]
        public void GetHashCode_ShouldReturnSameHashCode_WhenGeolocationsAreEqual()
        {
            // Arrange
            var geolocation1 = new Geolocation(12.34, 56.78);
            var geolocation2 = new Geolocation(12.34, 56.78);

            // Act
            var hashCode1 = geolocation1.GetHashCode();
            var hashCode2 = geolocation2.GetHashCode();

            // Assert
            Assert.Equal(hashCode1, hashCode2);
        }

        [Fact]
        public void GetHashCode_ShouldReturnDifferentHashCodes_WhenGeolocationsAreDifferent()
        {
            // Arrange
            var geolocation1 = new Geolocation(12.34, 56.78);
            var geolocation2 = new Geolocation(34.56, 78.90);

            // Act
            var hashCode1 = geolocation1.GetHashCode();
            var hashCode2 = geolocation2.GetHashCode();

            // Assert
            Assert.NotEqual(hashCode1, hashCode2);
        }
    }
}