using eTickets.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Cart
{
    public class ShoppingCart
    {
        public AppDbContext _context;
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> shoppingCartItems { get; set; }
        public ShoppingCart(AppDbContext context)
        {
            _context = context;
        }

        public static ShoppingCart GetShoppingCart(IServiceProvider serviceProvider) {
            ISession session = serviceProvider.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = serviceProvider.GetService<AppDbContext>();
            string CardId = session.GetString("CardId") ?? Guid.NewGuid().ToString();
            session.SetString("CardId", CardId);
            return new ShoppingCart(context) { ShoppingCartId=CardId};
        }


        public void AddItemToCart(Movie movie) {
            var shoppingCartItem = _context.shoppingCartItems.FirstOrDefault(n => n.Movie.Id == movie.Id &&
              n.ShoppingCartId == ShoppingCartId);
            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem()
                {
                    Movie = movie,
                    ShoppingCartId = ShoppingCartId,
                    Amount = 1
                };
                _context.shoppingCartItems.Add(shoppingCartItem);
            }
            else shoppingCartItem.Amount++;
            _context.SaveChanges();
        }
        public void RemoveItemFromCart(Movie movie)
        {
           var data= _context.shoppingCartItems.FirstOrDefault(n => n.Movie == movie 
           && n.ShoppingCartId==ShoppingCartId);
            if (data != null)
            {
                if (data.Amount > 1) data.Amount--;
                else _context.shoppingCartItems.Remove(data);
            }
            _context.SaveChanges();
        }

        public List<ShoppingCartItem> GetShoppingCartItems() {
            return shoppingCartItems ?? (shoppingCartItems = _context.shoppingCartItems.Where(n=>n.ShoppingCartId
            == ShoppingCartId).Include(n=>n.Movie).ToList());
        }
        public double GetShoppingCartTotal() {
            var total = _context.shoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId)
                .Select(n => n.Movie.Price *n.Amount).Sum();
            return total;
        }
        public async Task ClearShoppingCartAsync()
        {
            var items =await _context.shoppingCartItems.Where(n => n.ShoppingCartId
             == ShoppingCartId).ToListAsync();
            _context.shoppingCartItems.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
    }
}
