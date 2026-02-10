using TechSouq.Domain.Interfaces; 
using TechSouq.Domain.Entities;
using TechSouq.Application.Dtos;


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
    }
}
