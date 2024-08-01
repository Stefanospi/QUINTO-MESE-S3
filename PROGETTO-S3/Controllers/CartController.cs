using Microsoft.AspNetCore.Mvc;
using PROGETTO_S3.Services.Cart;

namespace PROGETTO_S3.Controllers
{
    [Route("Cart")]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
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
    }
}
