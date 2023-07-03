using Dapper;
using FoodDelivery.Context;
using FoodDelivery.Dto;
using FoodDelivery.Interfaces;
using FoodDelivery.Models;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.IdentityModel.Tokens;
using System.Data;

namespace FoodDelivery.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly FoodDeliveryContext _context;

        public RestaurantRepository(FoodDeliveryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Restaurant>> GetRestaurants()
        {
            using (var connection = _context.CreateConnection())
            {
                var query = @"SELECT * FROM Restaurant";

                var result = await connection.QueryAsync<Restaurant>(query);

                return result;
            }
        }

        public async Task<bool> AddRestaurant(RestaurantAddDto restaurantDto) 
        {
            var query = @"INSERT INTO Restaurant (RestaurantName, AddressId)
                          VALUES (@RestaurantName, @AddressId);";

            var parameters = new DynamicParameters();
            parameters.Add("RestaurantName", restaurantDto.RestaurantName, DbType.String);
            parameters.Add("AddressId", restaurantDto.AddressId, DbType.Int32);
  

            using (var connection = _context.CreateConnection())
            {
                var rowAffected = await connection.ExecuteAsync(query, parameters);
                return rowAffected > 0 ;
            }
        }

        public async Task<IEnumerable<Restaurant>> GetRestaurantDetailsById(int id)
        {
            using (var connection = _context.CreateConnection())
            {
                var query = @"SELECT r.*, a.*, c.*, o.*
                              FROM Restaurant AS r
                              JOIN [Address] AS a ON r.AddressId = a.Id
                              JOIN FoodCategory AS c ON r.Id = c.RestaurantId
                              JOIN FoodOrder AS o ON r.Id = o.RestaurantId
                              WHERE r.Id = @Id";

                var result = await connection.QueryAsync<Restaurant, Address, FoodCategory, FoodOrder, Restaurant>
                    (query, (restuarant, address, foodCategory, foodOrder) => {

                        restuarant.Address = address;
                        restuarant.FoodCategory = foodCategory;
                        restuarant.FoodOrder.Add(foodOrder);

                    return restuarant;
                }, new { id });

                return result;
            }
        }

        public async Task<bool> UpdateRestaurantNameById(int id, string restaurantName)
        {
            var query = @"UPDATE Restaurant
                          SET RestaurantName = @RestaurantName
                          WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var affectedRow = await connection.ExecuteAsync(query, new { Id = id, RestaurantName = restaurantName });
                return affectedRow > 0;
            }
        }

        public async Task<IEnumerable<Restaurant>> GetMenuByRestaurantId(int id)
        {
            var query = @"SELECT *
                          FROM Restaurant AS r
                          JOIN FoodCategory AS fc ON r.Id = fc.RestaurantId
                          JOIN FoodItem AS fi ON fc.Id = fi.FoodCategoryId
                          WHERE r.Id = @Id";

            using (var connect = _context.CreateConnection())
            {
                var result = await connect.QueryAsync<Restaurant, FoodCategory, FoodItem, Restaurant>
                    (query, (restaurant, foodCategory, foodItem) => {

                        foodCategory.FoodItems.Add(foodItem);
                        restaurant.FoodCategory = foodCategory;
                        
                        return restaurant;

                    }, new { Id = id });

                var itemList = new List<FoodItem>();

                foreach (var restaurant in result)
                {
                    foreach (var item in restaurant.FoodCategory.FoodItems)
                    {
                        itemList.Add(item); 
                    }
                }

                result.First().FoodCategory.FoodItems = itemList;

                return result;
            }
        }
    }
}
