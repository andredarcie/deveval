using AutoMapper;
using DevEval.Application.Carts.Dtos;
using DevEval.Application.Carts.Queries;
using DevEval.Domain.Repositories;
using MediatR;

namespace DevEval.Application.Carts.Handlers
{
    public class GetCartByIdHandler : IRequestHandler<GetCartByIdQuery, CartDto>
    {
        private readonly ICartRepository _repository;
        private readonly IMapper _mapper;

        public GetCartByIdHandler(ICartRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CartDto> Handle(GetCartByIdQuery request, CancellationToken cancellationToken)
        {
            var cart = await _repository.GetByIdAsync(request.Id);

            var result = _mapper.Map<CartDto>(cart);

            return result;
        }
    }
}
