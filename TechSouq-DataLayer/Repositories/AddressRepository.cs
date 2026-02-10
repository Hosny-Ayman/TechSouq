using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSouq.Infrastructure.Data;
using TechSouq.Domain.Interfaces;
using TechSouq.Domain.Entities;

namespace TechSouq.Infrastructure.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly AppDbContext _Context;

        public AddressRepository(AppDbContext context)
        {
            _Context = context;
        }

        public async Task <int>  CreateAddress(Address address)
        {
            await _Context.Addresses.AddAsync(address);

            await _Context.SaveChangesAsync();
            
            return address.Id;

        }

        public async Task <bool> DeleteAddress(int AddresId)
        {
            int Rowseffected = await _Context.Addresses.Where(x => x.Id == AddresId).ExecuteDeleteAsync();

            return Rowseffected > 0;
        }

        public async Task <ICollection<Address>> ReadAddresses(int UserId)
        {
           
            var address = await _Context.Addresses.AsNoTracking().Where(x=>x.UserId == UserId).ToListAsync();

            return address;

        }

        public async Task <bool> UpdateAdress(Address address)
        {
            _Context.Addresses.Update(address);

            return await _Context.SaveChangesAsync() > 0;

        }
    }
}
