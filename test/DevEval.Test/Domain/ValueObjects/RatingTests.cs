using DevEval.Domain.ValueObjects;

namespace DevEval.Tests.ValueObjects
{
    public class RatingTests
    {
        [Fact]
        public void Constructor_ShouldInitializeProperties_WhenValidArgumentsAreProvided()
        {
            // Arrange
            double rate = 4.5;
            int count = 10;

            // Act
            var rating = new Rating(rate, count);

            // Assert
            Assert.Equal(rate, rating.Rate);
            Assert.Equal(count, rating.Count);
        }

        [Theory]
        [InlineData(-1, 10, "Rate must be between 0 and 5.")]
        [InlineData(6, 10, "Rate must be between 0 and 5.")]
        [InlineData(4, -1, "Count must be greater than or equal to zero.")]
        public void Constructor_ShouldThrowException_WhenInvalidArgumentsAreProvided(double rate, int count, string expectedMessage)
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => new Rating(rate, count));
            Assert.Contains(expectedMessage, exception.Message);
        }

        [Fact]
        public void Empty_ShouldReturnRatingWithDefaultValues()
        {
            // Act
            var emptyRating = Rating.Empty;

            // Assert
            Assert.Equal(0, emptyRating.Rate);
            Assert.Equal(0, emptyRating.Count);
        }

        [Fact]
        public void WithNewVote_ShouldUpdateRateAndCountCorrectly()
        {
            // Arrange
            var rating = new Rating(4.0, 5);
            double newRate = 5.0;

            // Act
            var updatedRating = rating.WithNewVote(newRate);

            // Assert
            Assert.Equal(4.17, updatedRating.Rate, precision: 2); // Precision of 2 decimal places
            Assert.Equal(6, updatedRating.Count);
        }

        [Theory]
        [InlineData(-1, "New rate must be between 0 and 5.")]
        [InlineData(6, "New rate must be between 0 and 5.")]
        public void WithNewVote_ShouldThrowException_WhenNewRateIsInvalid(double newRate, string expectedMessage)
        {
            // Arrange
            var rating = new Rating(4.0, 5);

            // Act & Assert
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => rating.WithNewVote(newRate));
            Assert.Contains(expectedMessage, exception.Message);
        }

        [Fact]
        public void ToString_ShouldReturnFormattedString()
        {
            // Arrange
            var rating = new Rating(4.5, 10);

            // Act
            var result = rating.ToString();

            // Assert
            Assert.Equal("4,5 (10 votes)", result);
        }

        [Fact]
        public void Equals_ShouldReturnTrue_WhenRatingsAreEqual()
        {
            // Arrange
            var rating1 = new Rating(4.0, 5);
            var rating2 = new Rating(4.0, 5);

            // Act & Assert
            Assert.Equal(rating1, rating2);
        }

        [Fact]
        public void Equals_ShouldReturnFalse_WhenRatingsAreNotEqual()
        {
            // Arrange
            var rating1 = new Rating(4.0, 5);
            var rating2 = new Rating(4.5, 5);

            // Act & Assert
            Assert.NotEqual(rating1, rating2);
        }

        [Fact]
        public void GetHashCode_ShouldReturnSameValue_WhenRatingsAreEqual()
        {
            // Arrange
            var rating1 = new Rating(4.0, 5);
            var rating2 = new Rating(4.0, 5);

            // Act & Assert
            Assert.Equal(rating1.GetHashCode(), rating2.GetHashCode());
        }
    }
}