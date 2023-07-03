using FoodDelivery.Dto;
using FoodDelivery.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _customersRepository;

        public CustomersController(ICustomerRepository customersRepository)
        {
            _customersRepository = customersRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customersRepository.GetAllCustomers();
            return Ok(customers);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddCustomer([FromBody] CustomerDto customerDto)
        {
            try
            {
                var result = await _customersRepository.AddCustomer(customerDto);

                if (!result)
                {
                    return NotFound();
                }

                return Ok();
            }
            catch
            {
                return Problem();
            }
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] CustomerDto customerDto)
        {
            try
            {
                var result = await _customersRepository.UpdateCustomer(id, customerDto);

                if (!result)
                {
                    return NotFound();
                }

                return Ok(); 
            }
            catch 
            {
                return Problem();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            try
            {
                var result = await _customersRepository.GetCustomerById(id);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch 
            {
                return Problem();
            }
        }

        [HttpGet("qqqqq")]
        public async Task<IActionResult> Get()
        {
            var customers = await _customersRepository.Get(); 
            return Ok(customers);
        }
        
    }
}
