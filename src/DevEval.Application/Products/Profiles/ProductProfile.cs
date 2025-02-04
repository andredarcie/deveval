using AutoMapper;
using DevEval.Application.Products.Commands;
using DevEval.Application.Products.Dtos;
using DevEval.Domain.Entities.Product;
using DevEval.Domain.ValueObjects;

namespace DevEval.Application.Products.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<CreateProductCommand, Product>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Ignora a propriedade Id
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src =>
                    src.Rating != null
                        ? new Rating(src.Rating.Rate, src.Rating.Count)
                        : Rating.Empty));

            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src =>
                    src.Rating != null
                        ? new RatingDto { Rate = src.Rating.Rate, Count = src.Rating.Count }
                        : null));

            CreateMap<UpdateProductCommand, Product>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Mantém o ID existente sem sobrescrevê-lo
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src =>
                    src.Rating != null
                        ? new Rating(src.Rating.Rate, src.Rating.Count)
                        : Rating.Empty));

            CreateMap<ProductDto, Product>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Evita que o ID seja alterado ao mapear de DTO para entidade
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src =>
                    src.Rating != null
                        ? new Rating(src.Rating.Rate, src.Rating.Count)
                        : Rating.Empty));

            CreateMap<Rating, RatingDto>().ReverseMap();
        }
    }
}