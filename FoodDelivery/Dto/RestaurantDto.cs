using FoodDelivery.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FoodDelivery.Dto
{
    public class RestaurantDto
    {
        public int Id { get; set; }
        [Required, StringLength(30)]
        public string RestaurantName { get; set; }
        [Required, Range(1, int.MaxValue)]
        public int AddressId { get; set; }
    }
}
