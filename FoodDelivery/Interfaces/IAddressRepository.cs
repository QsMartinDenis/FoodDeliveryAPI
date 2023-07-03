using FoodDelivery.Dto;
using FoodDelivery.Models;

namespace FoodDelivery.Interfaces
{
    public interface IAddressRepository
    {
        public Task<AddressDto> GetAddressById(int id);
        public Task<bool> UpdateAdressById(int id, AddressDto addressDto);
    }
}
