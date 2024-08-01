using PROGETTO_S3.Models;
using PROGETTO_S3.Services.Cart;

namespace PROGETTO_S3.Services.OrderServ
{
    public class OrderService : IOrderService
    {
        private readonly DataContext _dataContext;
        private readonly ICartService _cartService;
        public OrderService(DataContext dataContext,ICartService cartService)
        {
            _dataContext = dataContext;
            _cartService = cartService;
        }
        public async Task CreateOrder(Order order)
        {
            var cart = await _cartService.GetCartItems();

            var newOrder = new Order
            {
                Address = order.Address,
                AdditionalNotes = order.AdditionalNotes,
                IsProcessed = false,
                OrderDate = DateTime.Now,
                TotalAmount = await _cartService.TotalAmountOfCart(),
                IdUser = order.IdUser,
            };
            _dataContext.Orders.Add(newOrder);
            await _dataContext.SaveChangesAsync();

            foreach(var item in cart)
            {
                var productOrdered = new OrderedProduct
                {
                    IdOrder = order.IdOrder,
                    IdProduct = item.IdProduct,
                    Quantity = item.Quantity
                };
                _dataContext.OrderedProducts.Add(productOrdered);
                await _dataContext.SaveChangesAsync();
            }

            await _cartService.ClerCart();
        }
    }
}
