namespace FoodDelivery.Models
{
    public class FoodOrderItem
    {
        public FoodOrder FoodOrder { get; set; }
        public FoodItem FoodItem { get; set; }
        public decimal FoodItemPrice { get; set; }
        public int Quantity { get; set; }
    }
}
