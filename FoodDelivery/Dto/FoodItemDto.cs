using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Dto
{
    public class FoodItemDto
    {
        [Required, StringLength(30)]
        public string FoodItemName { get; set; }
        [Required, Range(0, Double.MaxValue)]
        public decimal FoodItemPrice { get; set; }
    }
}
