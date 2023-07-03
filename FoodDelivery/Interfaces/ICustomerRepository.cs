using FoodDelivery.Dto;
using FoodDelivery.Models;

namespace FoodDelivery.Interfaces
{
    public interface ICustomerRepository
    {
        public Task<IEnumerable<Customer>> GetAllCustomers();
        public Task<bool> AddCustomer(CustomerDto customer);
        public Task<bool> UpdateCustomer(int id, CustomerDto customerDto);
        public Task<Customer> GetCustomerById(int id);

        public Task<IEnumerable<UserRole>> Get();
    }
}
