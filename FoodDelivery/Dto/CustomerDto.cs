using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Dto
{
    public class CustomerDto
    {
        [Required, StringLength(20)]
        public string FirstName { get; set; }
        [Required, StringLength(20)]
        public string LastName { get; set; }
    }
}
