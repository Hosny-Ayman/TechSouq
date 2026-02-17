using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;
using TechSouq.Application.Dtos;
using TechSouq.Application.Services;
using TechSouq.Application;

namespace TechSouq.API.Controllers
{
    [Route("api/[controller]")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ApiController]
    public class CartsController : ControllerBase
    {

        private readonly CartServices _CartServices;

        public CartsController(CartServices cartServices)
        {
            _CartServices = cartServices;
        }


        [HttpPost("Create")]
        public async Task<IActionResult> CreateCart(CartDto Cart)
        {
            var Result = await _CartServices.CreateCart(Cart);

            return Ok(Result);
        }

        [HttpGet("Read")]
        public async Task<IActionResult> ReadCart(int CartId)
        {
            var Result = await _CartServices.ReadCart(CartId);

            return Result.Status switch
            {
                OperationStatus.Success => Ok(Result.Data),
                OperationStatus.NotFound => BadRequest(Result.Message),
                //_ => StatusCode(500, "Unexpected Error")

            };
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateCart(CartDto Cart)
        {
            var Result = await _CartServices.UpdateCart(Cart);

            return Result.Status switch
            {
                OperationStatus.Success => Ok(Result.Message),
                OperationStatus.Failed => BadRequest(Result.Message),
                _ => StatusCode(500, "Unexpected Error")
            };
        }


        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteCart(int CartId)
        {
            var Result = await _CartServices.DeleteCart(CartId);

            return Result.Status switch
            {
                OperationStatus.Success => Ok(Result.Message),
                OperationStatus.Failed => BadRequest(Result.Message),
                _ => StatusCode(500, "Unexpected Error")
            };
        }

    }
}
