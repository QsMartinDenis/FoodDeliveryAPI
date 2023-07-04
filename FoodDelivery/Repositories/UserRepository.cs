using Dapper;
using FoodDelivery.Context;
using FoodDelivery.Dto;
using FoodDelivery.Interfaces;
using FoodDelivery.Models;
using System.Data;

namespace FoodDelivery.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FoodDeliveryContext _context;

        public UserRepository(FoodDeliveryContext context)
        {
            _context = context;
        }

        public async Task<bool> AddUser(UserAddDto user)
        {
            var query = @"INSERT INTO [User]
                          (FirstName, LastName, Email, Password)
                          VALUES (@FirstName, @LastName, @Email, @Password)";

            var parameters = new DynamicParameters();
            parameters.Add("FirstName", user.FirstName, DbType.String);
            parameters.Add("LastName", user.LastName, DbType.String);
            parameters.Add("Email", user.Email, DbType.String);
            parameters.Add("Password", user.Password, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                var rowAffected = await connection.ExecuteAsync(query, parameters);

                return rowAffected > 0;
            }
        }

        public async Task<User> GetUsers(UserLogin userLogin)
        {
            var query = @"SELECT u.Id AS Id, u.FirstName AS FirstName, u.LastName AS LastName, 
                          u.Email AS Email, u.[Password] AS [Password], r.RoleName AS RoleName
                          FROM UserRole ur
                          INNER JOIN [User] u ON u.Id = ur.UserId
                          INNER JOIN [Role] r ON r.Id = ur.RoleId
                          WHERE Email = @Email AND [Password] COLLATE Latin1_General_CS_AS = @Password";


            using (var connection = _context.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("Email", userLogin.Email, DbType.String);
                parameters.Add("Password", userLogin.Password, DbType.String);

                var userRolesDictionary = new Dictionary<int, User>();

                var result = await connection.QueryAsync<User, string, User>(query,
                    (user, role) =>
                    {
                        if (!userRolesDictionary.TryGetValue(user.Id, out var currentUser))
                        {
                            currentUser = user;
                            currentUser.Roles = new List<string>();
                            userRolesDictionary.Add(currentUser.Id, currentUser);
                        }

                        currentUser.Roles.Add(role);

                        return currentUser;
                    },
                    parameters,
                    splitOn: "RoleName");

                return result.FirstOrDefault();
            }
        }
    }
}
