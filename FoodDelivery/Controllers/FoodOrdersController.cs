using FoodDelivery.Dto;
using FoodDelivery.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Controllers
{
    [Route("api/foodorders")]
    [ApiController]
    public class FoodOrdersController : ControllerBase
    {
        private readonly IFoodOrderRepository _foodOrderRepository;

        public FoodOrdersController(IFoodOrderRepository foodOrderRepository)
        {
            _foodOrderRepository = foodOrderRepository;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddFoodOrder(FoodOrderDto foodOrderDto)
        {
            try
            {
                var result = await _foodOrderRepository.AddFoodOrder(foodOrderDto);

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

        [HttpPut("cancel/{id}")]
        public async Task<IActionResult> CancelFoodOrder(int id)
        {
            try
            {
                var result = await _foodOrderRepository.CancelFoodOrder(id);

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
        [HttpPut("setdriver/{id}")]
        [Authorize(Roles = "Admin,Driver")]
        public async Task<IActionResult> SetFoodOrderDrive(int id, [FromBody] int driverId)
        {
            try
            {
                var result = await _foodOrderRepository.SetFoodOrderDrive(id, driverId);

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
    }
}
