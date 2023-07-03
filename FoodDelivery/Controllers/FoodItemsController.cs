using AutoMapper;
using FoodDelivery.Dto;
using FoodDelivery.Interfaces;
using FoodDelivery.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace FoodDelivery.Controllers
{
    [Route("api/fooditems")]
    [ApiController]
    public class FoodItemsController : ControllerBase
    {
        private readonly IFoodItemRepository _foodItemRepository;
        private readonly IMapper _mapper;

        public FoodItemsController(IFoodItemRepository foodItemRepository, IMapper mapper)
        {
            _foodItemRepository = foodItemRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFoodItemById(int id)
        {
            try
            {
                var foodItem = await _foodItemRepository.GetFoodItemById(id);

                if (foodItem == null)
                {
                    return NotFound();
                }

                return Ok(foodItem);
            }
            catch 
            {
                return Problem();
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchFoodItemsByName(string query)
        {
            try
            {
                var result = await _foodItemRepository.SearchFoodItemsByName(query);

                if (result.IsNullOrEmpty())
                {
                    return NotFound();
                }

                return Ok(result.Select(x => _mapper.Map<FoodItemDto>(x)));
            }
            catch 
            {
                return Problem();
            }
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> UpdateFoodItemById(int id, [FromBody] FoodItemDto foodItemDto)
        {
            try
            {
                var foodItem = await _foodItemRepository.UpdateFoodItemById(id, foodItemDto);

                if (!foodItem)
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
