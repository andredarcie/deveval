using DevEval.Domain.Enums;

namespace DevEval.Application.Users.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public NameDto? Name { get; set; } = new();
        public AddressDto? Address { get; set; } = new();
        public string Phone { get; set; } = string.Empty;
        public UserStatus Status { get; set; }
        public UserRole Role { get; set; }
    }
}
