using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSouq.Application.Dtos;
using TechSouq.Domain.Entities;
using TechSouq.Domain.Interfaces;

namespace TechSouq.Application.Services
{
    public class CartService
    {
        private readonly ICartRepository _cartIRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CartService> _logger;

        public CartService(ICartRepository CartIRepository, IMapper mapper , ILogger<CartService> logger)
        {
            _cartIRepository = CartIRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<OperationResult<int>> CreateCart(CartDto cartDto)
        {
            try
            {
                var Cart = _mapper.Map<Cart>(cartDto);
                var newId = await _cartIRepository.CreateCart(Cart);

                if(newId <=0)
                {
                    _logger.LogError("Created Cart Failed With Data: {@Cart}", Cart);
                    return OperationResult<int>.Failure("Create Cart Faile Please Try Later");
                }

                _logger.LogInformation("Created Cart Successfully With Data: {@Cart}", Cart);
                return OperationResult<int>.Success(newId);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex,"Created Cart Failed With Data: {@Cart}", cartDto);
                return OperationResult<int>.Failure("Create Cart Faile Please Try Later");
            }

           
        }

        public async Task<OperationResult<CartDto>> ReadCart(int CartId)
        {
            if (CartId <= 0)
            {
                _logger.LogWarning("Read Cart Invalid Data CartId: {CartId}", CartId);
                return OperationResult<CartDto>.BadRequest($"Invalid Data", new List<string> { $"Invalid CartId: {CartId}" });
               
            }

            try
            {
                var result = await _cartIRepository.ReadCart(CartId);

                if(result == null)
                {
                    _logger.LogError("Read Cart Failed");
                    return OperationResult<CartDto>.Failure("Read Cart Faile Please Try Later");
                }

                var cartDto = _mapper.Map<CartDto>(result);

                _logger.LogInformation("Read Cart Successfully Data: {cartDto}", cartDto);
                return OperationResult<CartDto>.Success(cartDto);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Read Cart Failed");
                return OperationResult<CartDto>.Failure("Read Cart Failed Please Try Later");
            }
        }

        public async Task<OperationResult<bool>> UpdateCart(CartDto cartDto)
        {

            try
            {
                var cart = _mapper.Map<Cart>(cartDto);
                var result = await _cartIRepository.UpdateCart(cart);

                if(!result)
                {
                    _logger.LogError("Update Cart Failed Date: {@cart}", cart);
                    return OperationResult<bool>.Failure("Update Cart Failed Please Try Later");
                }

                _logger.LogInformation("Update Cart Successfully Date: {@cart}", cart);
                return OperationResult<bool>.Success(result);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Read Cart Failed");
                return OperationResult<bool>.Failure("Read Cart Failed Please Try Later");
            }

        }

        public async Task<OperationResult<bool>> DeleteCart(int CartId)
        {
            if (CartId <= 0)
            {
                _logger.LogWarning("Read Cart Invalid Data CartId: {CartId}", CartId);
                return OperationResult<bool>.BadRequest($"Invalid Data", new List<string> { $"Invalid CartId: {CartId}" });

            }

            try
            {
                var result = await _cartIRepository.DeleteCart(CartId);

                if(!result)
                {
                    _logger.LogError("Delete Cart Failed With Id: {Id}", CartId);
                    return OperationResult<bool>.Failure("Delete Cart Failed Please Try Later");
                }

                _logger.LogInformation("Delete Cart Successfully Id: {Id}", CartId);
                return OperationResult<bool>.Success(result);

            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Read Cart Failed");
                return OperationResult<bool>.Failure("Read Cart Failed Please Try Later");
            }
          
        }


    }
}
