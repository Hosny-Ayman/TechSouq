using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechSouq.Application;
using TechSouq.Application.Dtos;
using TechSouq.Application.Services;

namespace TechSouq.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemsController : ControllerBase
    {

        private readonly CartItemService _CartItemService;

        public CartItemsController(CartItemService cartItemService)
        {
            _CartItemService = cartItemService;
        }

        [HttpPost("Creat")]
        public async Task<IActionResult> CreateItemCart(CartItemDto CartItem)
        {
            var result = await _CartItemService.AddCartItem(CartItem);

            return result.Status switch
            {
                OperationStatus.Success => Ok(result),
                OperationStatus.BadRequest => BadRequest(result),
                OperationStatus.NotFound => NotFound(result),
                _ => StatusCode(500, result)
            };
        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetCartItems(int CartId)
        {
            var result = await _CartItemService.GetCartItems(CartId);

            return result.Status switch
            {
                OperationStatus.Success => Ok(result),
                OperationStatus.BadRequest => BadRequest(result),
                OperationStatus.NotFound => NotFound(result),
                _ => StatusCode(500, result)
            };
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateCartItems(List<CartItemDto> cartItems)
        {
            var result = await _CartItemService.UpdateCartItems(cartItems);

            return result.Status switch
            {
                OperationStatus.Success => Ok(result),
                OperationStatus.BadRequest => BadRequest(result),
                OperationStatus.NotFound => NotFound(result),
                _ => StatusCode(500, result)
            };
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteCartItems(int CartId)
        {
            var result = await _CartItemService.DeleteCartItems(CartId);

            return result.Status switch
            {
                OperationStatus.Success => Ok(result),
                OperationStatus.BadRequest => BadRequest(result),
                OperationStatus.NotFound => NotFound(result),
                _ => StatusCode(500, result)
            };
        }
    }
}
