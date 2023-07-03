using Dapper;
using FoodDelivery.Context;
using FoodDelivery.Dto;
using FoodDelivery.Interfaces;
using FoodDelivery.Models;
using System.Data;

namespace FoodDelivery.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly FoodDeliveryContext _context;

        public CustomerRepository(FoodDeliveryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            var query = @"SELECT * 
                          FROM Customer";

            using (var connection = _context.CreateConnection())
            {
                var customer = await connection.QueryAsync<Customer>(query);

                return customer;
            }
        }

        public async Task<bool> AddCustomer(CustomerDto customerDto)
        {
            var query = @"INSERT INTO Customer (FirstName, LastName)
                          VALUES (@FirstName, @LastName)";

            var parameters = new DynamicParameters();
            parameters.Add("FirstName", customerDto.FirstName, DbType.String);
            parameters.Add("LastName", customerDto.LastName, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                var insertedRow = await connection.ExecuteAsync(query, parameters);
                return insertedRow > 0;
            }
        }

        public async Task<bool> UpdateCustomer(int id, CustomerDto customerDto)
        {
            var query = @"UPDATE Customer
                          SET FirstName = @FirstName, LastName = @LastName
                          WHERE Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("FirstName", customerDto.FirstName, DbType.String);
            parameters.Add("LastName", customerDto.LastName, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                var affectedRow = await connection.ExecuteAsync(query, parameters);

                return affectedRow > 0;
            }
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            var query = @"SELECT *
                          FROM Customer
                          WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<Customer>(query, new {Id = id});

                return result.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<UserRole>> Get()
        {
            using (var connection = _context.CreateConnection())
            {
                string query = @"SELECT u.*, r.*
                             FROM UserRole ur
                             INNER JOIN [User] u ON u.Id = ur.UserId
                             INNER JOIN [Role] r ON r.Id = ur.RoleId";

                var result = await connection.QueryAsync<User, Role, UserRole>(query, (u, r) =>
                {
                    var userRole = new UserRole
                    {
                        User = u,
                        Role = r
                    };

                    return userRole;
                });

                return result;
            }
        }
    }
}
