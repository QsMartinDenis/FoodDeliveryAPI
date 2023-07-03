using Microsoft.Identity.Client;

namespace FoodDelivery.Models
{
    public class FoodOrder
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int DeliveryAddressId { get; set; }
        public Address DeliveryAddress { get; set; }
        public int DriverId { get; set; }
        public User Driver { get; set; }
        public int OrderStatusId { get; set;}
        public OrderStatus OrderStatus { get; set; }
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
        public decimal DeliveryFee { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDateTime { get; set; }
        public DateTime RequestDeliveryDateTime { get; set; }
    }
}
