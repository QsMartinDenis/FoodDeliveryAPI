using FoodDelivery.Models;
using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Dto
{
    public class FoodCategoryDto
    {
        [Required, StringLength(20)]
        public string FoodCategoryName { get; set; }
        [Required, Range(1, int.MaxValue)]
        public int RestaurantId { get; set; }
    }
}
