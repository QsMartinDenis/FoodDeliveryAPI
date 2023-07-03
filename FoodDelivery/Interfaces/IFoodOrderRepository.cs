using FoodDelivery.Dto;

namespace FoodDelivery.Interfaces
{
    public interface IFoodOrderRepository
    {
        public Task<bool> AddFoodOrder(FoodOrderDto foodOrderDto);
        public Task<bool> CancelFoodOrder(int id);
        public Task<bool> SetFoodOrderDrive(int id, int driverId);
    }
}
