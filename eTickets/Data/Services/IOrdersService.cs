using eTickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
    public interface IOrdersService
    {
        Task StoreOrderAsync(List<ShoppingCartItem> items, string UserId, string UserEmailAddress);
        Task<List<Order>> GetOrdersByIdAsync(string UserId);
    }
}
