using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Dto
{
    public class RestaurantAddDto
    {
        [Required, StringLength(30)]
        public string RestaurantName { get; set; }
        [Required, Range(1, int.MaxValue)]
        public int AddressId { get; set; }
    }
}
