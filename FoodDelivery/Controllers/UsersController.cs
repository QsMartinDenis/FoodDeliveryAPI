using FoodDelivery.Dto;
using FoodDelivery.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("add")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddUser([FromBody] UserAddDto userAddDto)
        {
            try
            {
                var result = await _userRepository.AddUser(userAddDto);

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
