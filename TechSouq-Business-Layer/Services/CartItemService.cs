using AutoMapper;
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
    public class CartItemService
    {
        private readonly ICartItemRepository _CartItemRepository;
        private readonly IMapper _mapper;

        public CartItemService(ICartItemRepository cartItemRepository , IMapper mapper)
        {
            _CartItemRepository = cartItemRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<int>> CreatCartItem(CartItemDto cartItemdto)
        {
           

            var cartItem = _mapper.Map<CartItem>(cartItemdto);

            try
            {
                var newId = await _CartItemRepository.CreateCartItem(cartItem);

                return new OperationResult<int>
                {
                    Data = newId,
                    Status = OperationStatus.Success,
                    Message = "Item added successfully"
                };
            }
            catch (Exception ex)
            {
                return new OperationResult<int>
                {
                    Data = 0,
                    Status = OperationStatus.Failed,
                    Message = $"Failed to add item: {ex.Message}" 
                };
            }
           
        }

        public async Task<OperationResult< List<CartItemDto>>> ReadCartItems(int CartId)
        {
            if (CartId <= 0)
            {
                return new OperationResult<List<CartItemDto>>
                {
                    Message = "CartId must be greater than 0",
                    Status = OperationStatus.BadRequest,
                   
                };
            }
          
            var Result = await _CartItemRepository.ReadCartItems(CartId);


            if (Result == null)
            {
                return new OperationResult<List<CartItemDto>>
                {
                    Message = "Cart not found",
                    Status = OperationStatus.NotFound
                };
            }

            var mapped = Result.Select(x => new CartItemDto
            {
                Id = x.Id,
                CartId = x.CartId,
                ProductId = x.ProductId,
                Quantity = x.Quantity
            }).ToList();

            return new OperationResult<List<CartItemDto>>
            {
                Status = OperationStatus.Success,
                Data = mapped
            };
        }

        public async Task<OperationResult< bool>> UpdateCartItems(List<CartItemDto> cartItem)
        {

            var ctList = cartItem.Select(item => new CartItem
            {
                Id = item.Id,
                CartId = item.CartId,
                ProductId = item.ProductId,
                Quantity = item.Quantity

            }).ToList();

            var Result = await _CartItemRepository.UpdateCartItems(ctList);


            return new OperationResult<bool>
            {
                Message = Result ? "Update Successfully" : "Update Failed",
                Status = Result ? OperationStatus.Success : OperationStatus.Failed,
                Data = Result
            };
           
           
        }
    }
}
