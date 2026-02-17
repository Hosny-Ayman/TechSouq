using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSouq.Domain.Entities;

namespace TechSouq.Domain.Interfaces
{
    public interface ICartItemRepository
    {

        Task<int> CreateCartItem(CartItem cartItem);

        Task<List<CartItem>> ReadCartItems(int id);

        Task<bool> UpdateCartItems (List <CartItem> cartItem);

        Task<bool> DeleteCartItem (int id);

    }
}
