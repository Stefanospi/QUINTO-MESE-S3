using PROGETTO_S3.Models;
using PROGETTO_S3.Models.ViewModel;
using PROGETTO_S3.Services.Products;

namespace PROGETTO_S3.Services.Cart
{
    public class CartService : ICartService
    {
        private readonly DataContext _dataContext;
        private readonly IProductService _productService;
        private static List<CartItem> _cartItems = new List<CartItem>();
        public CartService(DataContext dataContext,IProductService productService)
        {
            _dataContext = dataContext;
            _productService = productService;

        }
        public async Task AddToCart(int productId, int quantity)
        {
            var product = await _productService.GetProductById(productId);
            if (product != null) // Verifica se il prodotto esiste
            {
                var cart = _cartItems.FirstOrDefault(p => p.IdProduct == productId);
                if (cart != null)
                {
                    cart.Quantity += quantity;
                }
                else
                {
                    var newCart = new CartItem
                    {
                        IdProduct = product.IdProduct,
                        ProductName = product.Name,
                        Price = product.Price,
                        Quantity = quantity,
                    };
                    _cartItems.Add(newCart);
                }
            }
        }

        public Task<List<CartItem>> GetCartItems()
        {
            return Task.FromResult( _cartItems );
        }


        public Task RemoveFromCart(int productId)
        {
            var itemToRemove = _cartItems.FirstOrDefault(c => c.IdProduct == productId);
            if( itemToRemove != null)
            {
                _cartItems.Remove(itemToRemove);
            }
            return Task.FromResult( _cartItems );
        }

        public Task<decimal> TotalAmountOfCart()
        {
            var totalAmount = _cartItems.Sum(c => c.Price * c.Quantity);
            return Task.FromResult(totalAmount);
        }

        public Task<List<CartItem>> ClerCart()
        {
            _cartItems.Clear();
            return Task.FromResult( _cartItems );
        }
    }
}
