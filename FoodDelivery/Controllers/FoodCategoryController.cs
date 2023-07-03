using FoodDelivery.Dto;
using FoodDelivery.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Controllers
{
    [Route("api/foodcategory")]
    [ApiController]
    public class FoodCategoryController : ControllerBase
    {
        private readonly IFoodCategoryRepository _foodCategoryRepository;

        public FoodCategoryController(IFoodCategoryRepository foodCategoryRepository)
        {
            _foodCategoryRepository = foodCategoryRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFoodCategoryById(int id)
        {
            try
            {
                var result = await _foodCategoryRepository.GetFoodCategoryById(id);

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
        
        [HttpPost("add")]
        [Authorize]
        public async Task<IActionResult> AddFoodCateogry([FromBody] FoodCategoryDto foodCategoryDto)
        {
            try
            {
                var result = await _foodCategoryRepository.AddFoodCateogry(foodCategoryDto);

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
        public async Task<IActionResult> UpdateFoodCategoryName(int id,[FromBody] string name)
        {
            try
            {
                var result = await _foodCategoryRepository.UpdateFoodCategoryName(id, name);

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
