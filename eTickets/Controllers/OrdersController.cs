using eTickets.Data.Cart;
using eTickets.Data.Services;
using eTickets.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IMoviesService _Service;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrdersService _ordersService;
        public OrdersController(IMoviesService Service, ShoppingCart shoppingCart, IOrdersService ordersService)
        {
            _ordersService = ordersService;
            _Service = Service;
            _shoppingCart = shoppingCart;
        }
        public async Task<IActionResult> Index() 
        {
            string UserId = "";
            var Orders =await _ordersService.GetOrdersByIdAsync(UserId);
            return View(Orders);
        }
        public IActionResult ShoppingCart()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.shoppingCartItems = items;
            var response = new ShoppingCartMV()
            {
                shoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return View(response);
        }
        public async Task<IActionResult> AddItemToCart(int id) 
        {
            var item =await _Service.GetMovieByIdAsync(id);
            if (item != null)
            {
                _shoppingCart.AddItemToCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }
        public async Task<IActionResult> RemoveItemFromCart(int id)
        {
            var item = await _Service.GetMovieByIdAsync(id);
            if (item != null)
            {
                _shoppingCart.RemoveItemFromCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }
        public async Task<IActionResult> CompleteOrder() 
        {
            var items = _shoppingCart.GetShoppingCartItems();
            string UserId = "";
            string UserEmailAddress = "";

            await  _ordersService.StoreOrderAsync(items, UserId, UserEmailAddress);
            await _shoppingCart.ClearShoppingCartAsync();
            return View("OrderCompleted");
        }
    }
}
