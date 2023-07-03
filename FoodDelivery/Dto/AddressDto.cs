using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Dto
{
    public class AddressDto
    {
        [Required, StringLength(10)]
        public string HouseNumber { get; set; }
        [Required, StringLength(30)]
        public string StreetName { get; set; }
        [Required, StringLength(20)]
        public string City { get; set; }
        [Required, StringLength(20)]
        public string PostalCode { get; set; }
    }
}
