using FoodDelivery.Dto;
using FoodDelivery.Models;

namespace FoodDelivery.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> GetUsers(string email, string password);
        public Task<bool> AddUser(UserAddDto userDto);
    }
}
