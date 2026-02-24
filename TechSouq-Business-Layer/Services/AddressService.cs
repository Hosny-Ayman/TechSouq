using AutoMapper;
using Microsoft.Extensions.Logging;
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
        private readonly IMapper _mapper;
        private readonly ILogger<AddressService> _logger;

        public AddressService(IAddressRepository addressRepository,IMapper mapper , ILogger<AddressService> logger)
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<OperationResult<int>> AddAddress(AddressDto Address)
        {
            var ad = _mapper.Map<Address>(Address);
            try
            {
                _logger.LogInformation("Address Added Successfuly With Id {Id}",ad.Id);

                var newId =  await _addressRepository.AddAddress(ad);

                return OperationResult<int>.Success(newId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,$"Address Added Failed");

                return OperationResult<int>.Failure("Add Address Failed Try Later");
            }
        }

        public async Task<OperationResult<List<AddressDto>>> GetAddresses(int userId)
        {
            if(userId<=0)
            {
                _logger.LogWarning($"Invalid Data {userId}");
                return OperationResult<List<AddressDto>>.BadRequest("Invalid Data", new List<string> { $"Invalid userId {userId}" });
            }

            try
            {
                var addresses = await _addressRepository.GetAddresses(userId);

                if(addresses==null || !addresses.Any())
                {
                   
                    _logger.LogWarning("Addresses Not Found For UserId: {UserId}", userId);
                    return OperationResult<List<AddressDto>>.NotFound("Addresses Not Found");
                }


                var addressesDto = _mapper.Map<List<AddressDto>>(addresses);

                _logger.LogInformation("User Get Addresses: {@Addresses}", addresses);
                return OperationResult<List<AddressDto>>.Success(addressesDto);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAddresses Failed for UserId: {UserId}", userId);
                return OperationResult<List<AddressDto>>.Failure("Addresses Read Failed. Try Later.");
            }
                
        }

        public async Task<OperationResult <bool>> UpdateAdress(AddressDto address)
        {
          

            var ad = _mapper.Map<Address>(address);

            try
            {
                var result = await _addressRepository.UpdateAdress(ad);


                _logger.LogInformation("Update Address With Id {Id}", address.Id);
                 return OperationResult<bool>.Success(result);
               

            }
            catch(Exception ex)
            {
                _logger.LogError(ex,"Update Address With Id {Id} Failed", address.Id);
                return OperationResult<bool>.Failure("Update Failed Try Later");
            }

        }

        public async Task<OperationResult <bool>> DeleteAddress(int AddresId)
        {
            if (AddresId <= 0)
            {
                _logger.LogWarning("Delete Address With Invalid AddressId {AddressId}", AddresId);
                return OperationResult<bool>.BadRequest($"Invalid Data ", new List<string> { $"Invalid AddressId {AddresId}" });
               
            }

            try
            {
                var result = await _addressRepository.DeleteAddress(AddresId);

                _logger.LogInformation("Addres With Id : {AddresId} Deleted Successfully", AddresId);
                return OperationResult<bool>.Success(result);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex,"Addres With Id : {AddresId} Deleted Failed", AddresId);
                return OperationResult<bool>.Failure("Deleted Failed Try Later");
            }

        }




    }
}
