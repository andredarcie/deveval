using AutoMapper;
using DevEval.Application.Users.Commands;
using DevEval.Application.Users.Dtos;
using DevEval.Common.Services;
using DevEval.Domain.Entities.User;
using DevEval.Domain.Repositories;
using MediatR;

namespace DevEval.Application.Users.Handlers
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, UserDto>
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public CreateUserHandler(IUserRepository repository, IMapper mapper, IPasswordService passwordService)
        {
            _repository = repository;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            request.Password = _passwordService.HashPassword(request.Password);

            var user = _mapper.Map<User>(request);

            var createdUser = await _repository.AddAsync(user);

            var result = _mapper.Map<UserDto>(createdUser);

            return result;
        }
    }
}
