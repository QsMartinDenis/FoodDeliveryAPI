using FoodDelivery.Models;
using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Dto
{
    public class FoodOrderDto
    {
        [Required, Range(1,int.MaxValue)]
        public int CustomerId { get; set; }
        [Required, Range(1, int.MaxValue)]
        public int DeliveryAddressId { get; set; }
        [Required, Range(1, int.MaxValue)]
        public int DriverId { get; set; }
        [Required, Range(1, int.MaxValue)]
        public int OrderStatusId { get; set; }
        [Required, Range(1, int.MaxValue)]
        public int RestaurantId { get; set; }
        [Required, Range(0, Double.MaxValue)]
        public decimal DeliveryFee { get; set; }
        [Required, Range(0, Double.MaxValue)]
        public decimal TotalAmount { get; set; }
        public DateTime OrderDateTime { get; set; }
        public DateTime RequestDeliveryDateTime { get; set; }
    }
}
