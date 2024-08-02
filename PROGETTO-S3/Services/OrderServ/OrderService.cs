using Microsoft.EntityFrameworkCore;
using PROGETTO_S3.Models;
using PROGETTO_S3.Services.Cart;

namespace PROGETTO_S3.Services.OrderServ
{
    public class OrderService : IOrderService
    {
        private readonly DataContext _dataContext;
        private readonly ICartService _cartService;
        public OrderService(DataContext dataContext, ICartService cartService)
        {
            _dataContext = dataContext;
            _cartService = cartService;
        }

        public async Task CreateOrder(Order order)
        {
            if (order == null) throw new ArgumentNullException(nameof(order));

            var cart = await _cartService.GetCartItems() ?? throw new InvalidOperationException("Cart items not found.");
            var totalAmount = await _cartService.TotalAmountOfCart();

            var newOrder = new Order
            {
                Address = order.Address,
                AdditionalNotes = order.AdditionalNotes,
                IsProcessed = false,
                OrderDate = DateTime.Now,
                TotalAmount = totalAmount,
                IdUser = order.IdUser,
            };

            _dataContext.Orders.Add(newOrder);
            await _dataContext.SaveChangesAsync(); 

            foreach (var item in cart)
            {
                var productOrdered = new OrderedProduct
                {
                    IdOrder = newOrder.IdOrder, 
                    IdProduct = item.IdProduct,
                    Quantity = item.Quantity
                };
                _dataContext.OrderedProducts.Add(productOrdered);
            }

            await _dataContext.SaveChangesAsync();
            await _cartService.ClerCart(); 
        }
        public async Task<List<Order>> GetAllOrders()
        {
            var orderListFalse = await _dataContext.Orders
                .ToListAsync();

            return orderListFalse;

        }

        public async Task<Order> IsProcessedTrue(int idOrder)
        {
            var order = _dataContext.Orders.FirstOrDefault(o => o.IdOrder == idOrder);
            if (order.IsProcessed == false)
            {
                order.IsProcessed = true;
                await _dataContext.SaveChangesAsync();
            }
            else
            {
                order.IsProcessed = false;
                await _dataContext.SaveChangesAsync();
            }

            return order;
        }

    }
}