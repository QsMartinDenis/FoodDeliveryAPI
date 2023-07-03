using AutoMapper;
using FoodDelivery.Dto;
using FoodDelivery.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Controllers
{
    [Route("api/addresses")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly IAddressRepository _addressRepository;

        public AddressesController(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAddressById(int id)
        {
            try
            {
                var result = await _addressRepository.GetAddressById(id);

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

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> UpdateAdressById(int id, [FromBody] AddressDto addressDto)
        {
            try
            {
                var result = await _addressRepository.UpdateAdressById(id, addressDto);

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
