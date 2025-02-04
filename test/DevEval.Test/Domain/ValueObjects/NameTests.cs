using DevEval.Domain.ValueObjects;

namespace DevEval.Tests.ValueObjects
{
    public class NameTests
    {
        [Fact]
        public void Constructor_ShouldCreateName_WhenValidArgumentsProvided()
        {
            // Arrange
            var firstName = "John";
            var lastName = "Doe";

            // Act
            var name = new Name(firstName, lastName);

            // Assert
            Assert.Equal(firstName, name.FirstName);
            Assert.Equal(lastName, name.LastName);
            Assert.Equal("John Doe", name.FullName);
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenFirstNameIsEmpty()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new Name("", "Doe"));
            Assert.Contains("First name cannot be empty.", exception.Message);
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenLastNameIsEmpty()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new Name("John", ""));
            Assert.Contains("Last name cannot be empty.", exception.Message);
        }

        [Theory]
        [InlineData("J", "Doe", "First name must be between 2 and 50 characters.")]
        [InlineData("John", "D", "Last name must be between 2 and 50 characters.")]
        [InlineData("JJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJ", "Doe", "First name must be between 2 and 50 characters.")]
        [InlineData("John", "DDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDD", "Last name must be between 2 and 50 characters.")]
        public void Constructor_ShouldThrowException_WhenNameLengthIsInvalid(string firstName, string lastName, string expectedMessage)
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new Name(firstName, lastName));
            Assert.Contains(expectedMessage, exception.Message);
        }


        [Theory]
        [InlineData("John123", "Doe", "First name contains invalid characters.")]
        [InlineData("John", "Doe@!", "Last name contains invalid characters.")]
        public void Constructor_ShouldThrowException_WhenNameContainsInvalidCharacters(string firstName, string lastName, string expectedMessage)
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new Name(firstName, lastName));
            Assert.Contains(expectedMessage, exception.Message);
        }

        [Fact]
        public void ToString_ShouldReturnFullName()
        {
            // Arrange
            var name = new Name("John", "Doe");

            // Act
            var result = name.ToString();

            // Assert
            Assert.Equal("John Doe", result);
        }

        [Fact]
        public void Equals_ShouldReturnTrue_WhenNamesAreEqual()
        {
            // Arrange
            var name1 = new Name("John", "Doe");
            var name2 = new Name("John", "Doe");

            // Act
            var areEqual = name1.Equals(name2);

            // Assert
            Assert.True(areEqual);
        }

        [Fact]
        public void Equals_ShouldReturnFalse_WhenNamesAreDifferent()
        {
            // Arrange
            var name1 = new Name("John", "Doe");
            var name2 = new Name("Jane", "Smith");

            // Act
            var areEqual = name1.Equals(name2);

            // Assert
            Assert.False(areEqual);
        }

        [Fact]
        public void GetHashCode_ShouldReturnSameHashCode_WhenNamesAreEqual()
        {
            // Arrange
            var name1 = new Name("John", "Doe");
            var name2 = new Name("John", "Doe");

            // Act
            var hashCode1 = name1.GetHashCode();
            var hashCode2 = name2.GetHashCode();

            // Assert
            Assert.Equal(hashCode1, hashCode2);
        }

        [Fact]
        public void GetHashCode_ShouldReturnDifferentHashCodes_WhenNamesAreDifferent()
        {
            // Arrange
            var name1 = new Name("John", "Doe");
            var name2 = new Name("Jane", "Smith");

            // Act
            var hashCode1 = name1.GetHashCode();
            var hashCode2 = name2.GetHashCode();

            // Assert
            Assert.NotEqual(hashCode1, hashCode2);
        }
    }
}