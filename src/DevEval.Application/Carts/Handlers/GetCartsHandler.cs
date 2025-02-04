﻿using AutoMapper;
using DevEval.Application.Carts.Dtos;
using DevEval.Application.Carts.Queries;
using DevEval.Common.Helpers.Pagination;
using DevEval.Domain.Repositories;
using MediatR;

namespace DevEval.Application.Carts.Handlers
{
    public class GetCartsHandler : IRequestHandler<GetCartsQuery, PaginatedResult<CartDto>>
    {
        private readonly ICartRepository _repository;
        private readonly IMapper _mapper;

        public GetCartsHandler(ICartRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<CartDto>> Handle(GetCartsQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllWithProductsAsync(request.Parameters);

            return new PaginatedResult<CartDto>
            {
                Items = _mapper.Map<List<CartDto>>(result.Items),
                TotalItems = result.TotalItems,
                CurrentPage = result.CurrentPage,
                TotalPages = result.TotalPages
            };
        }
    }
}
