using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSouq.Domain.Entities;
using TechSouq.Domain.Interfaces;
using TechSouq.Infrastructure.Data;

namespace TechSouq.Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {

        private readonly AppDbContext _appDbContext;

        public CartRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<int> CreateCart(Cart Cart)
        {
           _appDbContext.Carts.Add(Cart);

           await _appDbContext.SaveChangesAsync();

           return Cart.Id;
        }

        public async Task<bool> DeleteCart(int CartId)
        {
           return await _appDbContext.Carts.Where(x => x.Id == CartId).ExecuteDeleteAsync() > 0;
        }

        public async Task<Cart> ReadCart(int CartId)
        {
            return await _appDbContext.Carts.AsNoTracking().FirstOrDefaultAsync(x => x.Id == CartId);
        }

        public async Task<bool> UpdateCart(Cart Cart)
        {
            _appDbContext.Carts.Update(Cart);

            return await _appDbContext.SaveChangesAsync() > 0;
        }
    }
}
