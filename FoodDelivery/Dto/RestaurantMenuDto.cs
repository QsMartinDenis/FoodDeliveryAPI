using FoodDelivery.Models;

namespace FoodDelivery.Dto
{
    public class RestaurantMenuDto
    {
        public int Id { get; set; }
        public string RestaurantName { get; set; }
        public string FoodCategoryName { get; set; }
        public ICollection<FoodItemDto> FoodItems { get; set; } = new List<FoodItemDto>();
    }
}
