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
    public class CartServices
    {
        private readonly ICartRepository _cartIRepository;

        public CartServices(ICartRepository CartIRepository)
        {
            _cartIRepository = CartIRepository;
        }

        public async Task<int> CreateCart(CartDto Cart)
        {
            Cart ca = new Cart();
            ca.UserId = Cart.UserId;

            return await _cartIRepository.CreateCart(ca);
        }

        public async Task<OperationResult<CartDto>> ReadCart(int CartId)
        {
            if (CartId < 0)
            {
                return new OperationResult<CartDto>
                {
                    Message = "Id Most be Positave",
                    Status = OperationStatus.Failed,
                    Data = null
                };
            }
            else
            {
                var Result = await _cartIRepository.ReadCart(CartId);

                if(Result!=null)
                {
                    return new OperationResult<CartDto>
                    {
                       
                        Status = OperationStatus.Success,
                        Data = new CartDto
                        {
                            Id = Result.Id,
                            UserId = Result.UserId,
                            Status = Result.Status,
                        }
                    };
                }
                else
                {
                    return new OperationResult<CartDto>
                    {
                        Message="Not Found",
                        Status = OperationStatus.NotFound,
                        Data = null
                      
                    };
                }

               
            }
        }

        public async Task<OperationResult<bool>> UpdateCart(CartDto Cart)
        {

           

            Cart ca = new Cart();
            ca.Id = Cart.Id;
            ca.UserId = Cart.UserId;
            ca.Status = Cart.Status;

            var Result = await _cartIRepository.UpdateCart(ca);

            if(Result)
            {
                return new OperationResult<bool>
                {
                    Message = "Update Successfully",
                    Status = OperationStatus.Success
                };
            }
            else
            {

                return new OperationResult<bool>
                {
                    Message = "Update Failed",
                    Status = OperationStatus.Failed
                };


            }

        }

        public async Task<OperationResult<bool>> DeleteCart(int CartId)
        {
            if (CartId < 0)
            {
                return new OperationResult<bool>
                {
                    Message = "CartId Most be Postitave",
                    Status = OperationStatus.NotFound
                };

            }
            else
            {

                var Result = await _cartIRepository.DeleteCart(CartId);

                if (Result)
                {
                    return new OperationResult<bool>
                    {
                        Message = "Deleted Successfully",
                        Status = OperationStatus.Success
                    };
                }
                else
                {
                    return new OperationResult<bool>
                    {
                        Message = "Deleted Failed",
                        Status = OperationStatus.Failed
                    };
                }


            }
        }


    }
}
