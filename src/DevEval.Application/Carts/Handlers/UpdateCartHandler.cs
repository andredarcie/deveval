using AutoMapper;
using DevEval.Application.Carts.Commands;
using DevEval.Application.Carts.Dtos;
using DevEval.Domain.Repositories;
using MediatR;

namespace DevEval.Application.Carts.Handlers
{
    public class UpdateCartHandler : IRequestHandler<UpdateCartCommand, CartDto>
    {
        private readonly ICartRepository _repository;
        private readonly IMapper _mapper;

        public UpdateCartHandler(ICartRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CartDto> Handle(UpdateCartCommand request, CancellationToken cancellationToken)
        {
            var existingCart = await _repository.GetByIdAsync(request.Id);

            if (existingCart == null) throw new KeyNotFoundException($"Cart with ID {request.Id} not found.");

            _mapper.Map(request, existingCart);

            var updatedCart = await _repository.UpdateAsync(existingCart);

            var result = _mapper.Map<CartDto>(updatedCart);

            return result;
        }
    }
}
