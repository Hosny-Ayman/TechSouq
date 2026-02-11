using System.ComponentModel.DataAnnotations;
using System.Net;
using TechSouq.Application.Dtos;
using TechSouq.Domain.Entities;
using TechSouq.Domain.Interfaces; 


namespace TechSouq.Application.Services
{
    public class AddressService
    {
        private readonly IAddressRepository _addressRepository;

        public AddressService(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<int> CreateAddress(AddressDto Address)
        {

            Address ad = new Address();
            ad.City = Address.City;
            ad.Street = Address.Street;
            ad.Phone = Address.Phone;
            ad.UserId = Address.UserId;

           return await _addressRepository.CreateAddress(ad);
        }

        public async Task<OperationResult<ICollection<AddressDto>>> ReadAddresses(int UserId)
        {
            if(UserId<=0)
            {
                return new OperationResult<ICollection<AddressDto>>
                {
                    Message = "UserId Most be Positave Number",
                    Status = OperationStatus.NotFound,
                };
            }

            else
            {

                var Addresses = await _addressRepository.ReadAddresses(UserId);


                return new OperationResult<ICollection<AddressDto>>
                {
                    Status = OperationStatus.Success,
                    Data = Addresses.Select(x => new AddressDto
                    {
                        Id = x.Id,
                        City = x.City,
                        Street = x.Street,
                        Phone = x.Phone,
                        UserId = x.UserId



                    }).ToList()
                };
            }
                
        }

        public async Task<OperationResult <bool>> UpdateAdress(AddressDto address)
        {
            if(address.Id <=0)
            {
                return new OperationResult<bool>
                {
                    Message = "Id Most be Postitave",
                    Status = OperationStatus.NotFound,
                   
                };
            }

            Address ad = new Address();

            ad.Id = address.Id;
            ad.City = address.City;
            ad.Street = address.Street;
            ad.Phone = address.Phone;
            ad.UserId = address.UserId;

            var Result = await _addressRepository.UpdateAdress(ad);

            if (Result)
                return new OperationResult<bool>
                {
                    Status = OperationStatus.Success,
                    Message="Update Successfully"

                };
            else
                return new OperationResult<bool>
                {
                    Status = OperationStatus.Faild,
                     Message = "Update Failed"
                };
        }

        public async Task<OperationResult <bool>> DeleteAddress(int AddresId)
        {
            if (AddresId <= 0)
            {
                return new OperationResult<bool>
                {
                    Message = "Id Most be Postitave",
                    Status = OperationStatus.NotFound,

                };
            }
            else
            {

                var Result = await _addressRepository.DeleteAddress(AddresId);

                if(Result)
                {
                    return new OperationResult<bool>
                    {
                        Message = "Delete Successfully",
                        Status = OperationStatus.Success,

                    };
                }
                else
                {
                    return new OperationResult<bool>
                    {
                        Message = "Delete Failed",
                        Status = OperationStatus.Faild,

                    };
                }

                
            }
        }




    }
}
