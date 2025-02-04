using MediatR;
using DevEval.Application.Sales.Dtos;
using DevEval.Domain.Entities.Sale;
using AutoMapper;
using DevEval.Application.Carts.Commands;
using DevEval.Domain.Repositories;

namespace DevEval.Application.Carts.Handlers
{
    public class ConvertCartToSaleHandler : IRequestHandler<ConvertCartToSaleCommand, SaleDto>
    {
        private readonly ICartRepository _cartRepository;
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public ConvertCartToSaleHandler(
            ICartRepository cartRepository,
            ISaleRepository saleRepository,
            IMapper mapper)
        {
            _cartRepository = cartRepository;
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<SaleDto> Handle(ConvertCartToSaleCommand request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetByIdWithProductsAsync(request.CartId);

            if (cart == null)
            {
                throw new KeyNotFoundException("Cart not found.");
            }

            if (cart.Products == null || !cart.Products.Any())
            {
                throw new InvalidOperationException("Cannot convert an empty cart to a sale.");
            }

            var sale = Sale.FromCart(cart);

            var createdSale = await _saleRepository.AddAsync(sale);

            await _cartRepository.DeleteAsync(cart.Id);

            return _mapper.Map<SaleDto>(createdSale);
        }
    }
}