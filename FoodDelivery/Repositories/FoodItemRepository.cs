using Dapper;
using FoodDelivery.Context;
using FoodDelivery.Dto;
using FoodDelivery.Interfaces;
using FoodDelivery.Models;
using Microsoft.IdentityModel.Tokens;
using System.Data;

namespace FoodDelivery.Repositories
{
    public class FoodItemRepository : IFoodItemRepository
    {
        private readonly FoodDeliveryContext _context;

        public FoodItemRepository(FoodDeliveryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FoodItemDto>> GetFoodItemById(int id)
        {
            var query = @"SELECT * 
                          FROM FoodItem
                          WHERE FoodCategoryId = @Id";

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<FoodItemDto>(query, new { Id = id });
                
                return result;
            }
        }

        public async Task<IEnumerable<FoodItemDto>> SearchFoodItemsByName(string query)
        {
            string name = $"%{query}%";

            var sqlQuery = @"SELECT *
                          FROM FoodItem
                          WHERE FoodItemName LIKE @name";

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<FoodItemDto>(sqlQuery, new { name });

                return result;
            }
        }

        public async Task<bool> UpdateFoodItemById(int id, FoodItemDto foodItemDto)
        {
            var query = @"UPDATE FoodItem
                          SET FoodItemName = @FoodItemName,
                          FoodItemPrice = @FoodItemPrice
                          WHERE Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("FoodItemName", foodItemDto.FoodItemName, DbType.String);
            parameters.Add("FoodItemPrice", foodItemDto.FoodItemPrice, DbType.Decimal);

            using (var connection = _context.CreateConnection())
            {
                var rowAffected = await connection.ExecuteAsync(query, parameters);

                return rowAffected > 0;
            }
        }
    }
}
