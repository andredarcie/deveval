using System.ComponentModel;
using MediatR;

namespace DevEval.Application.Auth.Commands
{
    public class LoginUserCommand : IRequest<string>
    {
        [DefaultValue("admin")]
        public string Username { get; set; }

        [DefaultValue("Admin@123")]
        public string Password { get; set; }

        public LoginUserCommand(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}