using FoodDelivery.Dto;
using FoodDelivery.Models;

namespace FoodDelivery.Interfaces
{
    public interface IUserRepository
    {
        public Task<IEnumerable<User>> GetUsers();
        public Task<bool> AddUser(UserAddDto userDto);
    }
}
