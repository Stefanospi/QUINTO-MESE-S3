using PROGETTO_S3.Models;
using PROGETTO_S3.Models.ViewModel;

namespace PROGETTO_S3.Services.Cart
{
    public interface ICartService
    {
        Task AddToCart(int productId, int quantity);
        Task<List<CartItem>> GetCartItems();
        Task RemoveFromCart(int productId);
        Task<decimal> TotalAmountOfCart();
  
    }
}
