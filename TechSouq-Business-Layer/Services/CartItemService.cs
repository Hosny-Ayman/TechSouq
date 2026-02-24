using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSouq.Application.Dtos;
using TechSouq.Domain.Entities;
using TechSouq.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace TechSouq.Application.Services
{
    public class CartItemService
    {
        private readonly ICartItemRepository _CartItemRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CartItemService> _logger;

        public CartItemService(ICartItemRepository cartItemRepository , IMapper mapper, ILogger<CartItemService> Logger)
        {
            _CartItemRepository = cartItemRepository;
            _mapper = mapper;
            _logger = Logger;
        }

        public async Task<OperationResult<int>> AddCartItem(CartItemDto cartItemdto)
        {
           

            var cartItem = _mapper.Map<CartItem>(cartItemdto);

            try
            {
                var newId = await _CartItemRepository.AddCartItem(cartItem);

                _logger.LogInformation("Item added Successfully. ProductId: {ProductId}", cartItemdto.ProductId);

                return OperationResult<int>.Success(newId, $"Cart Items Added Successfully");
               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to add item. ProductId: {ProductId}", cartItemdto.ProductId);

                return OperationResult<int>.Failure($"Failed to add item. ProductId: {cartItemdto.ProductId}");
            }
           
        }

        public async Task<OperationResult< List<CartItemDto>>> GetCartItems(int CartId)
        {
            if (CartId <= 0)
            {
                _logger.LogWarning("User Try To Add CartId: {CartId}", CartId);

                return OperationResult<List<CartItemDto>>.BadRequest("Invalid Data", new List<string> { $"Invalid Cart ID: {CartId}" });

            }
          
            var result = await _CartItemRepository.GetCartItems(CartId);


            if (result == null || !result.Any())
            {

                _logger.LogWarning("No Items Found With CartId: {CartId}", CartId);
                return OperationResult<List<CartItemDto>>.NotFound("Cart not found");

            }

            var mapped = _mapper.Map<List<CartItemDto>>(result);

            return OperationResult<List<CartItemDto>>.Success(mapped);
        }

        public async Task<OperationResult< bool>> UpdateCartItems(List<CartItemDto> cartItems)
        {

            if (cartItems == null || !cartItems.Any())
            {
                _logger.LogWarning("Update attempt with empty or null cart items list.");
                return OperationResult<bool>.BadRequest("Update Failed Try Later");
            }

            var ctList = _mapper.Map<List<CartItem>>(cartItems);

            try
            {
                var result = await _CartItemRepository.UpdateCartItems(ctList);

                _logger.LogInformation("Successfully updated {Count} cart items.", ctList.Count);

                return OperationResult<bool>.Success(result);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update {Count} cart items.", ctList.Count);

                return OperationResult<bool>.Failure("Update Failed Try Later");
            }

        }

        public async Task<OperationResult<bool>> DeleteCartItems(int CartId)
        {
            if (CartId <= 0)
            {
                _logger.LogWarning("Delete CartItem With Id : {CartId} Invalid", CartId);
                return OperationResult<bool>.BadRequest("Invalid Data", new List<string> { $"Invalid CartItemId {CartId}" });
            }
            try
            {
                var result = await _CartItemRepository.DeleteCartItem(CartId);

                _logger.LogInformation("Delete CartItem With Id : {CartId} Successfully", CartId);
                return OperationResult<bool>.Success(result);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Delete CartItem With Id : {CartId} Failed", CartId);
                return OperationResult<bool>.Failure("Delete CartItem Failed Try Later");
            }

        }
    }
}
