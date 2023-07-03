using AutoMapper;
using FoodDelivery.Dto;
using FoodDelivery.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace FoodDelivery.Controllers
{
    [Route("api/restaurants")]
    [ApiController]
    [AllowAnonymous]
    public class RestaurantsController : ControllerBase
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;

        public RestaurantsController(IRestaurantRepository restaurantRepository, IMapper mapper)
        {
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetRestaurants()
        {
            try
            {
                var restaurants = await _restaurantRepository.GetRestaurants();

                if (restaurants.IsNullOrEmpty())
                {
                    return NotFound();
                }

                var restaurantsDto = restaurants.Select(r => _mapper.Map<RestaurantDto>(r));

                return Ok(restaurantsDto);
            }
            catch
            {
                return Problem();
            }
        }
        
        [HttpPost("add")]
        public async Task<IActionResult> AddRestaurant([FromBody] RestaurantAddDto restaurantDto)
        {
            var result = await _restaurantRepository.AddRestaurant(restaurantDto);

            if (!result)
            {
                return NotFound();
            }   

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRestaurantById(int id)
        {
            try
            {
                var result = await _restaurantRepository.GetRestaurantDetailsById(id);
                var restaurant = result.SingleOrDefault();

                if (restaurant == null)
                {
                    return NotFound();
                }

                var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);
                var addressDto = _mapper.Map<AddressDto>(restaurant.Address);
                var foodCategoryDto = _mapper.Map<FoodCategoryDto>(restaurant.FoodCategory);
                var foodOrderDto = _mapper.Map<IEnumerable<FoodOrderDto>>(restaurant.FoodOrder);

                return Ok(new { restaurantDto, addressDto, foodCategoryDto, foodOrderDto });
            }
            catch
            {
                return Problem();
            }
        }

        [HttpPut("/edit/{id}")]
        public async Task<IActionResult> UpdateRestaurantNameById(int id, [FromBody] string restaurantName)
        {
            try
            {
                var result = await _restaurantRepository.UpdateRestaurantNameById(id, restaurantName);

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

        [HttpGet("menu/{id}")]
        public async Task<IActionResult> GetRestaurantMenu(int id)
        {
            try
            {
                var result = await _restaurantRepository.GetMenuByRestaurantId(id);

                if (result.IsNullOrEmpty())
                {
                    return NotFound();
                }

                var restaurant = result.First();

                var restaurantMenuDto = new RestaurantMenuDto()
                {
                    Id = restaurant.Id,
                    RestaurantName = restaurant.RestaurantName,
                    FoodCategoryName = restaurant.FoodCategory.FoodCategoryName
                };

                foreach (var item in restaurant.FoodCategory.FoodItems)
                {
                    restaurantMenuDto.FoodItems.Add(_mapper.Map<FoodItemDto>(item));
                }

                return Ok(restaurantMenuDto);
            }
            catch 
            {
                return Problem();
            }
        }
    }
}
