using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TechSouq.Application;
using TechSouq.Application.Dtos;
using TechSouq.Application.Services;

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
            var result = await _addressService.CreateAddress(address);

            if(result.IsSuccess)
            {
                return Ok(result);
            }

            return result.Status switch
            {
               
                OperationStatus.NotFound => NotFound(result),
                OperationStatus.BadRequest => BadRequest(result),
                _ => StatusCode(500, result)


            };

           
        }

        [HttpGet("Read")]
        public async Task <IActionResult> ReadAddresses(int UserId)
        {
            var result = await  _addressService.ReadAddresses(UserId);

            return result.Status switch
            {
                OperationStatus.Success => Ok(result),
                OperationStatus.NotFound => NotFound(result),
                OperationStatus.BadRequest => BadRequest(result),
                _ => StatusCode(500, result)
            };
        }


        [HttpPut("Update")]
        public async Task<IActionResult> UpdateAddress(AddressDto address)
        {
            var result = await _addressService.UpdateAdress(address);

            return result.Status switch
            {
                OperationStatus.Success => Ok(result),
                OperationStatus.NotFound => NotFound(result),
                OperationStatus.BadRequest => BadRequest(result),
                _ => StatusCode(500, result)

            };
        }
        [HttpDelete("Delete")]
        public async Task <IActionResult> DeleteAddress(int AddressId)
        {
            var result = await _addressService.DeleteAddress(AddressId);

            return result.Status switch
            {
                OperationStatus.Success => Ok(result),
                OperationStatus.NotFound => NotFound(result),
                OperationStatus.BadRequest => BadRequest(result),
                _ => StatusCode(500, result)

            };
        }



    }
}
