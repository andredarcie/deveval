﻿using AutoMapper;
using DevEval.Application.Users.Dtos;
using DevEval.Application.Users.Queries;
using DevEval.Common.Helpers.Pagination;
using DevEval.Domain.Repositories;
using MediatR;

namespace DevEval.Application.Users.Handlers
{
    public class GetUsersHandler : IRequestHandler<GetUsersQuery, PaginatedResult<UserDto>>
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public GetUsersHandler(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllAsync(request.Parameters);

            return new PaginatedResult<UserDto>
            {
                Items = _mapper.Map<List<UserDto>>(result.Items),
                TotalItems = result.TotalItems,
                CurrentPage = result.CurrentPage,
                TotalPages = result.TotalPages
            };
        }
    }
}
