using AutoMapper;
using OwlCafe.DTOs;
using OwlCafe.Models;

namespace OwlCafe.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Category
            CreateMap<Category, CategoryDto>().ReverseMap();

            // MenuItem
            CreateMap<MenuItem, MenuItemDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category!.Name));
            CreateMap<MenuItemDto, MenuItem>();

            // Booking
            CreateMap<Booking, BookingDto>()
                .ForMember(dest => dest.TableNumber, opt => opt.MapFrom(src => src.Table != null ? src.Table.TableNumber.ToString() : string.Empty));
            CreateMap<BookingDto, Booking>();
        }
    }
}
