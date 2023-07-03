using FoodDelivery.Dto;

namespace FoodDelivery.Interfaces
{
    public interface IFoodItemRepository
    {
        public Task<IEnumerable<FoodItemDto>> GetFoodItemById(int id);
        public Task<IEnumerable<FoodItemDto>> SearchFoodItemsByName(string query);
        public Task<bool> UpdateFoodItemById(int id, FoodItemDto foodItemDto); 
    }
}
