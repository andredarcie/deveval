using DevEval.Application.Auth.Commands;
using DevEval.Common.Services;
using DevEval.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Users.Handlers
{
    public class LoginUserHandler : IRequestHandler<LoginUserCommand, string>
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;

        public LoginUserHandler(IConfiguration configuration, IUserRepository userRepository, IPasswordService passwordService)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _passwordService = passwordService;
        }

        public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByUsernameAsync(request.Username);

            if (user == null || !_passwordService.VerifyPassword(user.Password, request.Password))
            {
                throw new UnauthorizedAccessException("Invalid username or password");
            }

            return GenerateJwtToken(user.Id, user.Username);
        }

        private string GenerateJwtToken(int userId, string username)
        {
            var key = _configuration["Jwt:Key"] ?? throw new ArgumentNullException("Jwt:Key configuration is missing");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("UserId", userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}