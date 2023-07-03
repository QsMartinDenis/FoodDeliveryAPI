namespace FoodDelivery.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string HouseNumber { get; set; }
        public string StreetName { get; set;}
        public string City { get; set;}
        public string PostalCode { get; set;}
        public ICollection<Restaurant> Restaurants { get; set; }
    }
}
