using AutoMapper;
using DevEval.Application.Sales.Commands;
using DevEval.Application.Sales.Dtos;
using DevEval.Domain.Entities.Sale;

namespace DevEval.Application.Sales.Profiles
{
    public class SaleProfile : Profile
    {
        public SaleProfile()
        {
            CreateMap<CreateSaleCommand, Sale>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // ID will be generated
                .ForMember(dest => dest.SaleDate, opt => opt.MapFrom(src => src.SaleDate == default ? DateTime.UtcNow : src.SaleDate))
                .ForMember(dest => dest.IsCancelled, opt => opt.MapFrom(_ => false))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items.Select(itemDto =>
                    new SaleItem(itemDto.ProductId, itemDto.Quantity, itemDto.UnitPrice, itemDto.Discount)).ToList()));

            CreateMap<Sale, SaleDto>()
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

            CreateMap<SaleItem, SaleItemDto>()
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
                .ReverseMap()
                .ForMember(dest => dest.TotalPrice, opt => opt.Ignore()) // Calculated property
                .ConstructUsing(src => new SaleItem(src.ProductId, src.Quantity, src.UnitPrice, src.Discount));
        }
    }
}