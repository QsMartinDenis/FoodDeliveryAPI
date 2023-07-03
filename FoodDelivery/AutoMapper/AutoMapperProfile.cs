using AutoMapper;
using FoodDelivery.Dto;
using FoodDelivery.Models;

namespace FoodDelivery.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<Address, AddressDto>();
            CreateMap<AddressDto, Address>();

            CreateMap<FoodCategory, FoodCategoryDto>();

            CreateMap<FoodOrder, FoodOrderDto>();

            CreateMap<Restaurant, RestaurantDto>();
            CreateMap<RestaurantDto, Restaurant>();

            CreateMap<Restaurant, FoodCategoryDto>();

            CreateMap<FoodItem, FoodItemDto>();

            CreateMap<Customer, CustomerDto>();
        }
    }
}
