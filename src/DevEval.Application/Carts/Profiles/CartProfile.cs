using AutoMapper;
using DevEval.Application.Carts.Commands;
using DevEval.Application.Carts.Dtos;
using DevEval.Domain.Entities.Cart;

namespace DevEval.Application.Carts.Profiles
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<CreateCartCommand, Cart>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src =>
                    src.Products != null
                        ? src.Products.Select(p => new CartProduct(p.ProductId, p.UnitPrice, p.Quantity)).ToList()
                        : new List<CartProduct>()))
                .ConstructUsing(src =>
                    new Cart(
                        src.UserId,
                        DateTime.UtcNow,
                        new List<CartProduct>()
                    ));

            CreateMap<Cart, CartDto>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("yyyy-MM-dd")))
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));

            CreateMap<CartProduct, CartProductDto>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));

            CreateMap<CartProductDto, CartProduct>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ConstructUsing(src =>
                    new CartProduct(
                        src.ProductId,
                        src.UnitPrice,
                        src.Quantity
                    ));
        }
    }
}