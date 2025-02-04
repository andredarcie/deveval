using AutoMapper;
using DevEval.Application.Users.Commands;
using DevEval.Application.Users.Dtos;
using DevEval.Application.Users.Profiles;
using DevEval.Domain.Entities.User;
using DevEval.Domain.Enums;
using DevEval.Domain.ValueObjects;

namespace DevEval.Test.Application.Users.Profiles
{
    public class UserProfileTests
    {
        private readonly IMapper _mapper;

        public UserProfileTests()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<UserProfile>());
            _mapper = configuration.CreateMapper();
        }

        [Fact]
        public void Configuration_ShouldBeValid()
        {
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        [Fact]
        public void Map_CreateUserCommand_To_User_ShouldWork_WithValidData()
        {
            var command = new CreateUserCommand
            {
                Email = "test@example.com",
                Username = "testuser",
                Password = "password123",
                Name = new NameDto { FirstName = "John", LastName = "Doe" },
                Address = new AddressDto
                {
                    City = "City",
                    Street = "Street",
                    Number = 123,
                    ZipCode = "12345",
                    Geolocation = new GeolocationDto { Lat = 10.5, Long = 20.5 }
                },
                Phone = "+123456789",
                Status = UserStatus.Active,
                Role = UserRole.Customer
            };

            var user = _mapper.Map<User>(command);

            Assert.Equal(command.Email, user.Email);
            Assert.Equal(command.Username, user.Username);
            Assert.Equal(command.Password, user.Password);
            Assert.Equal(command.Name.FirstName, user.Name?.FirstName);
            Assert.Equal(command.Name.LastName, user.Name?.LastName);
            Assert.Equal(command.Address.City, user.Address?.City);
            Assert.Equal(command.Address.Street, user.Address?.Street);
            Assert.Equal(command.Address.Number, user.Address?.Number);
            Assert.Equal(command.Address.ZipCode, user.Address?.ZipCode);
            Assert.Equal(command.Address.Geolocation.Lat, user.Address?.Geolocation?.Lat);
            Assert.Equal(command.Address.Geolocation.Long, user.Address?.Geolocation?.Long);
        }

        [Fact]
        public void Map_User_To_UserDto_ShouldWork_WithValidData()
        {
            var user = new User("test@example.com", "testuser", "hashedpassword", UserRole.Customer)
            {
                Address = new Address("City", "Street", 123, "12345", new Geolocation(10.5, 20.5)),
                Name = new Name("John", "Doe")
            };

            var userDto = _mapper.Map<UserDto>(user);

            Assert.Equal(user.Email, userDto.Email);
            Assert.Equal(user.Username, userDto.Username);
            Assert.Equal(user.Name?.FirstName, userDto.Name?.FirstName);
            Assert.Equal(user.Name?.LastName, userDto.Name?.LastName);
            Assert.Equal(user.Address?.City, userDto.Address?.City);
            Assert.Equal(user.Address?.Street, userDto.Address?.Street);
            Assert.Equal(user.Address?.Number, userDto.Address?.Number);
            Assert.Equal(user.Address?.ZipCode, userDto.Address?.ZipCode);
            Assert.Equal(user.Address?.Geolocation?.Lat, userDto.Address?.Geolocation?.Lat);
            Assert.Equal(user.Address?.Geolocation?.Long, userDto.Address?.Geolocation?.Long);
        }

        [Fact]
        public void Map_User_To_UserDto_ShouldWork_WithNullAddressAndName()
        {
            var user = new User("test@example.com", "testuser", "hashedpassword", UserRole.Customer);

            var userDto = _mapper.Map<UserDto>(user);

            Assert.Equal(user.Email, userDto.Email);
            Assert.Equal(user.Username, userDto.Username);
            Assert.Null(userDto.Name);
            Assert.Null(userDto.Address);
        }

        [Fact]
        public void Map_UpdateUserCommand_To_User_ShouldWork_WithValidData()
        {
            var command = new UpdateUserCommand
            {
                Name = new NameDto { FirstName = "Jane", LastName = "Doe" },
                Address = new AddressDto
                {
                    City = "NewCity",
                    Street = "NewStreet",
                    Number = 456,
                    ZipCode = "67890",
                    Geolocation = new GeolocationDto { Lat = 30.5, Long = 40.5 }
                }
            };

            var user = new User("test@example.com", "testuser", "hashedpassword", UserRole.Customer)
            {
                Address = new Address("City", "Street", 123, "12345", new Geolocation(10.5, 20.5)),
                Name = new Name("John", "Doe")
            };

            _mapper.Map(command, user);

            Assert.Equal(command.Name.FirstName, user.Name?.FirstName);
            Assert.Equal(command.Name.LastName, user.Name?.LastName);
            Assert.Equal(command.Address.City, user.Address?.City);
            Assert.Equal(command.Address.Street, user.Address?.Street);
            Assert.Equal(command.Address.Number, user.Address?.Number);
            Assert.Equal(command.Address.ZipCode, user.Address?.ZipCode);
            Assert.Equal(command.Address.Geolocation.Lat, user.Address?.Geolocation?.Lat);
            Assert.Equal(command.Address.Geolocation.Long, user.Address?.Geolocation?.Long);
        }
    }
}