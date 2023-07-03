
namespace FoodDelivery.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string RestaurantName { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public FoodCategory FoodCategory { get; set; }
        public ICollection<FoodOrder> FoodOrder { get; set; } = new List<FoodOrder>();
    }
}
