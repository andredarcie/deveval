using AutoMapper;
using DevEval.Application.Users.Commands;
using DevEval.Application.Users.Dtos;
using DevEval.Domain.Entities.User;
using DevEval.Domain.ValueObjects;

namespace DevEval.Application.Users.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            // Map CreateUserCommand to User
            CreateMap<CreateUserCommand, User>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src =>
                    new Name(src.Name.FirstName, src.Name.LastName)))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src =>
                    src.Address != null
                        ? new Address(
                            src.Address.City,
                            src.Address.Street,
                            src.Address.Number,
                            src.Address.ZipCode,
                            src.Address.Geolocation != null
                                ? new Geolocation(src.Address.Geolocation.Lat, src.Address.Geolocation.Long)
                                : Geolocation.Empty)
                        : null))
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            // Map UpdateUserCommand to User
            CreateMap<UpdateUserCommand, User>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src =>
                    new Name(src.Name.FirstName, src.Name.LastName)))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src =>
                    src.Address != null
                        ? new Address(
                            src.Address.City,
                            src.Address.Street,
                            src.Address.Number,
                            src.Address.ZipCode,
                            src.Address.Geolocation != null
                                ? new Geolocation(src.Address.Geolocation.Lat, src.Address.Geolocation.Long)
                                : Geolocation.Empty)
                        : null))
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            // Map User to UserDto
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src =>
                    src.Name != null
                        ? new NameDto
                        {
                            FirstName = src.Name.FirstName,
                            LastName = src.Name.LastName
                        }
                        : null))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src =>
                    src.Address != null
                        ? new AddressDto
                        {
                            City = src.Address.City,
                            Street = src.Address.Street,
                            Number = src.Address.Number,
                            ZipCode = src.Address.ZipCode,
                            Geolocation = src.Address.Geolocation != null
                                ? new GeolocationDto
                                {
                                    Lat = src.Address.Geolocation.Lat,
                                    Long = src.Address.Geolocation.Long
                                }
                                : null
                        }
                        : null))
                .ReverseMap();

            // Map Name and Address individually
            CreateMap<Name, NameDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<Geolocation, GeolocationDto>().ReverseMap();
        }
    }
}