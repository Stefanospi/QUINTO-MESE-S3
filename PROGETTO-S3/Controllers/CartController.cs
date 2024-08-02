using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROGETTO_S3.Models;
using PROGETTO_S3.Services.Cart;
using PROGETTO_S3.Services.OrderServ;

namespace PROGETTO_S3.Controllers
{
    [Route("Cart")]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;
        private readonly DataContext _dataContext;
        public CartController(ICartService cartService,IOrderService orderService,DataContext dataContext)
        {
            _cartService = cartService;
            _orderService = orderService;
            _dataContext = dataContext;
        }

        [HttpPost("AddToCart")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(int productId,int quantity)
        {
            await _cartService.AddToCart(productId, quantity);
            return RedirectToAction("ProductList","Product");
        }

        [HttpGet("ViewCart")]
        public async Task<IActionResult> ViewCart()
        {
            var cartItems = await _cartService.GetCartItems();
            var totalAmount = await _cartService.TotalAmountOfCart();
            ViewBag.TotalAmount = totalAmount;
            return View(cartItems);
        }

        [HttpPost("RemoveFromCart")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            await _cartService.RemoveFromCart(productId);
            return RedirectToAction("ViewCart", "Cart");
        }
        [HttpGet("Checkout")]
        public async Task<IActionResult> Checkout()
        {
            var cartItems= await _cartService.GetCartItems();
            var totalAmount = await _cartService.TotalAmountOfCart();
            ViewBag.TotalAmount = totalAmount;
            return View(cartItems);
        }


        [HttpPost("Checkout")]
        public async Task<IActionResult> Checkout(string address, string additionalNotes)
        {
            var username = User.Identity.Name;
            if (string.IsNullOrEmpty(username)) return Unauthorized("User not logged in.");

            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null) return NotFound("User not found.");

            try
            {
                var order = new Order
                {
                    Address = address,
                    AdditionalNotes = additionalNotes,
                    IdUser = user.IdUser
                };

                await _orderService.CreateOrder(order);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                // Log the exception ex for further analysis
                return View(await _cartService.GetCartItems());
            }
        }
        [HttpGet("AllOrders")]
        public async Task<IActionResult> AllOrders()
        {
            var orders = await _orderService.GetAllOrders();
            return View(orders);
        }

        [HttpPost("IsProcessed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IsProcessed(int idOrder)
        {
            await _orderService.IsProcessedTrue(idOrder);
            return RedirectToAction("AllOrders","Cart");
        }
        [HttpGet]
        public async Task<IActionResult> GetProcessedOrdersCount()
        {
            var processedOrdersCount = await _dataContext.Orders.CountAsync(o => o.IsProcessed == true);

            return Ok(processedOrdersCount);
        }

        [HttpGet]
        public async Task<IActionResult> GetTotalIncome(DateTime date)
        {
            var totalIncome = await _dataContext.Orders
                .Where(
                o => o.OrderDate.Date == date.Date
                    )
                .SumAsync(o => o.TotalAmount);

            return Ok(totalIncome);
        }
    }
}
