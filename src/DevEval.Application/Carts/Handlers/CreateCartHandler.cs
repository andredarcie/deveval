using AutoMapper;
using DevEval.Application.Carts.Commands;
using DevEval.Application.Carts.Dtos;
using DevEval.Domain.Entities.Cart;
using DevEval.Domain.Repositories;
using MediatR;

namespace DevEval.Application.Carts.Handlers
{
    public class CreateCartHandler : IRequestHandler<CreateCartCommand, CartDto>
    {
        private readonly ICartRepository _repository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public CreateCartHandler(ICartRepository repository, IMapper mapper, IProductRepository productRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<CartDto> Handle(CreateCartCommand request, CancellationToken cancellationToken)
        {
            request.Date = DateTime.UtcNow;

            var cart = _mapper.Map<Cart>(request);

            foreach (var cartProduct in cart.Products.Where(product => product != null))
            {
                var product = await _productRepository.GetByIdAsync(cartProduct.ProductId);

                if (product == null)
                {
                    throw new ArgumentException($"Product with ID {cartProduct.Id} does not exist.", nameof(cartProduct.Id));
                }
            }

            var createdCart = await _repository.AddAsync(cart);
            return _mapper.Map<CartDto>(createdCart);
        }
    }
}
