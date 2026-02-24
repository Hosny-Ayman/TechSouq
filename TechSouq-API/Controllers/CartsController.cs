using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;
using TechSouq.Application;
using TechSouq.Application.Dtos;
using TechSouq.Application.Services;

namespace TechSouq.API.Controllers
{
    [Route("api/[controller]")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ApiController]
    public class CartsController : ControllerBase
    {

        private readonly CartService _CartServices;

        public CartsController(CartService cartServices)
        {
            _CartServices = cartServices;
        }


        [HttpPost("Create")]
        public async Task<IActionResult> CreateCart(CartDto Cart)
        {
            var result = await _CartServices.AddCart(Cart);

            return result.Status switch
            {
                OperationStatus.Success => Ok(result),
                OperationStatus.NotFound => NotFound(result),
                OperationStatus.BadRequest => BadRequest(result),
                _ => StatusCode(500, result)

            };
        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetCart(int CartId)
        {
            var result = await _CartServices.GetCart(CartId);

            return result.Status switch
            {
                OperationStatus.Success => Ok(result),
                OperationStatus.NotFound => NotFound(result),
                OperationStatus.BadRequest => BadRequest(result),
                _ => StatusCode(500, result)

            };
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateCart(CartDto Cart)
        {
            var result = await _CartServices.UpdateCart(Cart);

            return result.Status switch
            {
                OperationStatus.Success => Ok(result),
                OperationStatus.NotFound => NotFound(result),
                OperationStatus.BadRequest => BadRequest(result),
                _ => StatusCode(500, result)
            };
        }


        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteCart(int CartId)
        {
            var result = await _CartServices.DeleteCart(CartId);

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
