using DevEval.Application.Users.Dtos;
using MediatR;

namespace DevEval.Application.Users.Queries
{
    public class GetUserByIdQuery : IRequest<UserDto>
    {
        public int Id { get; set; }

        public GetUserByIdQuery(int id)
        {
            Id = id;
        }
    }
}
