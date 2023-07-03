using FoodDelivery.Dto;
using FoodDelivery.Models;

namespace FoodDelivery.Interfaces
{
    public interface IRestaurantRepository
    {
        public Task<IEnumerable<Restaurant>> GetRestaurants();
        public Task<bool> AddRestaurant(RestaurantAddDto restaurantDto);
        public Task<IEnumerable<Restaurant>> GetRestaurantDetailsById(int id);
        public Task<bool> UpdateRestaurantNameById(int id, string restaurantName);
        
        public Task<IEnumerable<Restaurant>> GetMenuByRestaurantId(int id);
    }
}
