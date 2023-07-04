using FoodDelivery.Dto;
using FoodDelivery.Models;

namespace FoodDelivery.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> GetUsers(UserLogin userLogin);
        public Task<bool> AddUser(UserAddDto userDto);
    }
}
