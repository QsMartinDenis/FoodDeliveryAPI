using Dapper;
using FoodDelivery.Context;
using FoodDelivery.Dto;
using FoodDelivery.Interfaces;
using FoodDelivery.Models;
using Microsoft.IdentityModel.Tokens;
using System.Data;

namespace FoodDelivery.Repositories
{
    public class FoodCategoryRepository : IFoodCategoryRepository
    {
        private readonly FoodDeliveryContext _context;

        public FoodCategoryRepository(FoodDeliveryContext context)
        {
            _context = context;
        }
        public async Task<FoodCategoryDto> GetFoodCategoryById(int id)
        {
            var query = @"SELECT FoodCategoryName AS FoodCategoryName, RestaurantId AS RestaurantId
                          FROM FoodCategory
                          WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<FoodCategoryDto>(query, new {Id = id});

                return result.First();
            }
        }
        
        public async Task<bool> AddFoodCateogry(FoodCategoryDto foodCategoryDto)
        {
            var query = @"INSERT INTO FoodCategory (FoodCategoryName, RestaurantId)
                          VALUES (@FoodCategoryName, @RestaurantId)";

            var parameters = new DynamicParameters(foodCategoryDto);

            using (var connection = _context.CreateConnection())
            {
                var rowAffected = await connection.ExecuteAsync(query, parameters);

                return rowAffected > 0;
            }
        }


        public async Task<bool> UpdateFoodCategoryName(int id, string name)
        {
            var query = @"UPDATE FoodCategory
                          SET FoodCategoryName = @FoodCategoryName
                          WHERE Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("FoodCategoryName", name, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                var rowAffected = await connection.ExecuteAsync(query, new { Id = id, FoodCategoryName = name });

                return rowAffected > 0;
            }
        }
    }
}
