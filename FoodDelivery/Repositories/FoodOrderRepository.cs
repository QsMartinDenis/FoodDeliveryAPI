using Dapper;
using FoodDelivery.Context;
using FoodDelivery.Dto;
using FoodDelivery.Interfaces;
using System.Data;

namespace FoodDelivery.Repositories
{
    public class FoodOrderRepository : IFoodOrderRepository
    {
        private readonly FoodDeliveryContext _context;

        public FoodOrderRepository(FoodDeliveryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FoodOrderDto>> GetFoodOrders()
        {
            var query = @"SELECT * FROM FoodOrder";

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<FoodOrderDto>(query);

                return result;
            }
        }

        public async Task<bool> AddFoodOrder(FoodOrderDto foodOrderDto)
        {
            var query = @"INSERT INTO FoodOrder (
                            CustomerId,
                            DeliveryAddressId,
                            DriverId,
                            OrderStatusId,
                            RestaurantId,
                            DeliveryFee,
                            TotalAmount,
                            OrderDateTime,
                            RequestDeliveryDateTime)
                          VALUES (
                            @CustomerId,
                            @DeliveryAddressId,
                            @DriverId,
                            @OrderStatusId,
                            @RestaurantId,
                            @DeliveryFee,
                            @TotalAmount,
                            @OrderDateTime,
                            @RequestDeliveryDateTime);";

            var parameters = new DynamicParameters();
            parameters.Add("CustomerId", foodOrderDto.CustomerId, DbType.Int32);
            parameters.Add("DeliveryAddressId", foodOrderDto.DeliveryAddressId, DbType.Int32);
            parameters.Add("DriverId", foodOrderDto.DriverId, DbType.Int32);
            parameters.Add("OrderStatusId", foodOrderDto.OrderStatusId, DbType.Int32);
            parameters.Add("RestaurantId", foodOrderDto.RestaurantId, DbType.Int32);
            parameters.Add("DeliveryFee", foodOrderDto.DeliveryFee, DbType.Decimal);
            parameters.Add("TotalAmount", foodOrderDto.TotalAmount, DbType.Decimal);
            parameters.Add("OrderDateTime", foodOrderDto.OrderDateTime, DbType.DateTime);
            parameters.Add("RequestedDeliveryDateTime", foodOrderDto.RequestDeliveryDateTime, DbType.DateTime);

            using (var connection = _context.CreateConnection())
            {
                var affectedRow = await connection.ExecuteAsync(query, foodOrderDto);

                return affectedRow > 0;
            }
        }

        public async Task<bool> CancelFoodOrder(int id)
        {
            var query = @"UPDATE FoodOrder
                          SET OrderStatusId = 2
                          WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var affectedRow = await connection.ExecuteAsync(query, new { Id = id });

                return affectedRow > 0;  
            }
        }


        public async Task<bool> SetFoodOrderDrive(int id, int driverId)
        {
            var query = @"UPDATE FoodOrder
                                SET DriverId = @DriverId
                                WHERE Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("DriverId", driverId, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                var rowAffected = await connection.ExecuteAsync(query, parameters);

                return rowAffected > 0;
            }
        }
    }
}
