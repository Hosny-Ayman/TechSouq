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
    public class CartItemRepository : ICartItemRepository
    {

        private readonly AppDbContext _appDbContext;

        public CartItemRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<int> AddCartItem(CartItem cartItem)
        {
            _appDbContext.CartItems.Add(cartItem);

           
                await _appDbContext.SaveChangesAsync();
            
                return -1;
            

           

            return cartItem.Id;
        }

        public async Task<bool> DeleteCartItem(int CartItemId)
        {
            return await _appDbContext.CartItems.Where(x => x.Id == CartItemId).ExecuteDeleteAsync() > 0;
        }

        public async Task<List<CartItem>> GetCartItems(int CartItemId)
        {
            return await _appDbContext.CartItems.AsNoTracking().Where(x => x.Id == CartItemId).ToListAsync();
        }

        public async Task<bool> UpdateCartItems(List<CartItem> cartItem)
        {
            _appDbContext.CartItems.UpdateRange(cartItem);

            return await _appDbContext.SaveChangesAsync() > 0;
        }
    }
}
