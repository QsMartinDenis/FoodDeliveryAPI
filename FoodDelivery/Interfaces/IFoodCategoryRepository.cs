using FoodDelivery.Dto;

namespace FoodDelivery.Interfaces
{
    public interface IFoodCategoryRepository
    {
        public Task<FoodCategoryDto> GetFoodCategoryById(int id);
        public Task<bool> AddFoodCateogry(FoodCategoryDto foodCategoryDto);
        public Task<bool> UpdateFoodCategoryName(int id, string name);
    }
}
