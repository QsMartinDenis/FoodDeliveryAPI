namespace FoodDelivery.Models
{
    public class FoodCategory
    {
        public int Id { get; set; }
        public string FoodCategoryName { get; set; }
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
        public ICollection<FoodItem> FoodItems { get; set; } = new List<FoodItem>();
    }
}
