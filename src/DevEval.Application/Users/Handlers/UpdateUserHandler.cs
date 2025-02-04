using AutoMapper;
using DevEval.Application.Users.Commands;
using DevEval.Application.Users.Dtos;
using DevEval.Common.Services;
using DevEval.Domain.Repositories;
using MediatR;

namespace DevEval.Application.Users.Handlers
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UserDto>
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public UpdateUserHandler(IUserRepository repository, IMapper mapper, IPasswordService passwordService)
        {
            _repository = repository;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _repository.GetByIdAsync(request.Id);

            if (existingUser == null) throw new KeyNotFoundException($"User with ID {request.Id} not found.");

            if (!string.IsNullOrEmpty(request.Password))
            {
                request.Password = _passwordService.HashPassword(request.Password);
            }

            _mapper.Map(request, existingUser);

            var updatedUser = await _repository.UpdateAsync(existingUser);

            var result = _mapper.Map<UserDto>(updatedUser);

            return result;
        }
    }
}