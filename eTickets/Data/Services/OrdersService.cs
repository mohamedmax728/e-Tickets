using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly AppDbContext _context;
        public OrdersService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Order>> GetOrdersByIdAsync(string UserId)
        {
            var Orders =await _context.Orders.Include(n=>n.orderItems).ThenInclude(n=>n.Movie).Where(n=>n.UserId==UserId)
                .ToListAsync();
            return Orders;
        }

        public async Task StoreOrderAsync(List<ShoppingCartItem> items, string UserId, string UserEmailAddress)
        {
            var Order = new Order()
            {
                UserId = UserId,
                Email=UserEmailAddress
            };
            await _context.Orders.AddAsync(Order);
            await _context.SaveChangesAsync();
            foreach(var item in items)
            {
                var Orderitem = new OrderItem()
                {
                    Amount = item.Amount,
                    MovieId = item.Movie.Id,
                    OrderId = Order.Id,
                    Price = item.Movie.Price
                };
                await _context.OrderItems.AddAsync(Orderitem);
            }
            await _context.SaveChangesAsync();
        }
    }
}
