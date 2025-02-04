using AutoMapper;
using DevEval.Application.Users.Dtos;
using DevEval.Application.Users.Queries;
using DevEval.Domain.Repositories;
using MediatR;

namespace DevEval.Application.Users.Handlers
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public GetUserByIdHandler(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByIdAsync(request.Id);

            var result = _mapper.Map<UserDto>(user);

            return result;
        }
    }
}
