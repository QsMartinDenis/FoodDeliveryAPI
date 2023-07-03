using Dapper;
using FoodDelivery.Context;
using FoodDelivery.Dto;
using FoodDelivery.Interfaces;
using FoodDelivery.Models;
using Microsoft.IdentityModel.Tokens;
using System.Data;

namespace FoodDelivery.Repositories
{
    public class AddressRepository : IAddressRepository
    {

        private readonly FoodDeliveryContext _context;

        public AddressRepository(FoodDeliveryContext context)
        {
            _context = context;
        }

        public async Task<AddressDto> GetAddressById(int id)
        {
            var query = @"SELECT * 
                          FROM Address
                          WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<AddressDto>(query, new {Id = id});
                
                return result.First();
            }
        }

        public async Task<bool> UpdateAdressById(int id, AddressDto addressDto)
        {
            var query = @"UPDATE Address 
                          SET
                          HouseNumber = @HouseNumber,
                          StreetName = @StreetName,
                          City = @City,
                          PostalCode = @PostalCode
                          WHERE Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("HouseNumber", addressDto.HouseNumber, DbType.String);
            parameters.Add("StreetName", addressDto.StreetName, DbType.String);
            parameters.Add("City", addressDto.City, DbType.String);
            parameters.Add("PostalCode", addressDto.PostalCode, DbType.String);


            using (var connection = _context.CreateConnection())
            {

                var affectedRow = await connection.ExecuteAsync(query, parameters);

                return affectedRow > 0;
            }

        }
    }
}
