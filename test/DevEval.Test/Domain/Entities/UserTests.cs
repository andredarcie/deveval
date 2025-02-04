using DevEval.Domain.Entities.User;
using DevEval.Domain.Enums;
using DevEval.Domain.ValueObjects;

namespace DevEval.Test.Domain.Entities
{
    public class UserTests
    {
        [Fact]
        public void Constructor_ShouldInitializeProperties_WhenValidParametersProvided()
        {
            // Arrange
            string email = "test@example.com";
            string username = "testuser";
            string password = "securepassword";

            // Act
            var user = new User(email, username, password, UserRole.Customer);

            // Assert
            Assert.Equal(email, user.Email);
            Assert.Equal(username, user.Username);
            Assert.Equal(password, user.Password);
            Assert.Equal(UserRole.Customer, user.Role);
            Assert.Equal(UserStatus.Active, user.Status); // Default status should be Active
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenEmailIsInvalid()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentException>(() => new User(string.Empty, "username", "password", UserRole.Customer));
            Assert.Throws<ArgumentException>(() => new User("", "username", "password", UserRole.Customer));
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenUsernameIsInvalid()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentException>(() => new User("test@example.com", string.Empty, "password", UserRole.Customer));
            Assert.Throws<ArgumentException>(() => new User("test@example.com", "", "password", UserRole.Customer));
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenPasswordIsInvalid()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentException>(() => new User("test@example.com", "username", string.Empty, UserRole.Customer));
            Assert.Throws<ArgumentException>(() => new User("test@example.com", "username", "", UserRole.Customer));
        }

        [Fact]
        public void UpdateName_ShouldUpdateName_WhenValidNameProvided()
        {
            // Arrange
            var user = new User("test@example.com", "testuser", "securepassword", UserRole.Customer);
            var name = new Name("John", "Doe");

            // Act
            user.UpdateName(name);

            // Assert
            Assert.Equal(name, user.Name);
        }

        [Fact]
        public void UpdateAddress_ShouldUpdateAddress_WhenValidAddressProvided()
        {
            // Arrange
            var user = new User("test@example.com", "testuser", "securepassword", UserRole.Customer);
            var geolocation = new Geolocation(12.34, 56.78);
            var address = new Address("City", "Street", 123, "12345-678", geolocation);

            // Act
            user.UpdateAddress(address);

            // Assert
            Assert.Equal(address, user.Address);
        }


        [Fact]
        public void UpdatePhone_ShouldThrowException_WhenPhoneIsInvalid()
        {
            // Arrange
            var user = new User("test@example.com", "testuser", "securepassword", UserRole.Customer);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => user.UpdatePhone(string.Empty));
            Assert.Throws<ArgumentException>(() => user.UpdatePhone(""));
        }

        [Fact]
        public void UpdateRole_ShouldUpdateRole_WhenValidRoleProvided()
        {
            // Arrange
            var user = new User("test@example.com", "testuser", "securepassword", UserRole.Customer);

            // Act
            user.UpdateRole(UserRole.Manager);

            // Assert
            Assert.Equal(UserRole.Manager, user.Role);
        }

        [Fact]
        public void UpdateStatus_ShouldUpdateStatus_WhenValidStatusProvided()
        {
            // Arrange
            var user = new User("test@example.com", "testuser", "securepassword", UserRole.Customer);

            // Act
            user.UpdateStatus(UserStatus.Suspended);

            // Assert
            Assert.Equal(UserStatus.Suspended, user.Status);
        }
    }
}