using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechSouq.Application.Dtos;
using TechSouq.Application.Services;
using TechSouq.Application;

namespace TechSouq.API.Controllers
{
    [Route("api/[controller]")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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


        [HttpPut("Update")]
        public async Task<IActionResult> UpdateAddress(AddressDto address)
        {
            var Result = await _addressService.UpdateAdress(address);

            return Result.Status switch
            {
                OperationStatus.Success => Ok(Result.Message),
                OperationStatus.NotFound => NotFound(Result.Message),
                _ => StatusCode(500, "Unexpected Error")

            };
        }
        [HttpDelete("Delete")]
        public async Task <IActionResult> DeleteAddress(int AddressId)
        {
            var Result = await _addressService.DeleteAddress(AddressId);

            return Result.Status switch
            {
                OperationStatus.Success => Ok(Result.Message),
                OperationStatus.NotFound => NotFound(Result.Message),
                OperationStatus.Faild => BadRequest(Result.Message),
                _ => StatusCode(500, "Unexpected Error")

            };
        }



    }
}
