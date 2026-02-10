using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSouq.Domain.Entities;

namespace TechSouq.Domain.Interfaces
{
    public interface  IAddressRepository
    {

        Task <int> CreateAddress(Address address);

        Task <ICollection<Address>> ReadAddresses(int UserId);

        Task <bool> UpdateAdress(Address address);

        Task <bool> DeleteAddress(int AddresId);

    }
}
