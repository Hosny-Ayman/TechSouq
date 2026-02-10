using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechSouq.Application.Dtos;
using TechSouq.Application.Services;
using TechSouq.Application;

namespace TechSouq_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly AddressService _addressService;

        public AddressesController(AddressService AddressService)
        {
            _addressService = AddressService;
        }

        [HttpPost("Create")]
        public async Task <IActionResult> CreateAddress(AddressDto address)
        {
            var Result = await _addressService.CreateAddress(address);

            return Ok(Result);
        }

        [HttpGet("Read")]
        public async Task <IActionResult> ReadAddresses(int UserId)
        {
            var Result = await  _addressService.ReadAddresses(UserId);

            return Result.Status switch
            {
                OperationStatus.Success => Ok(Result.Data),
                OperationStatus.NotFound => NotFound(Result.Message)
            };
        }



    }
}
