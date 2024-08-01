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

            // Aggiunge il nuovo ordine al contesto dei dati
            _dataContext.Orders.Add(newOrder);
            await _dataContext.SaveChangesAsync(); // Salva per ottenere l'Id dell'ordine

            foreach (var item in cart)
            {
                var productOrdered = new OrderedProduct
                {
                    IdOrder = newOrder.IdOrder, // Utilizza l'Id dell'ordine appena creato
                    IdProduct = item.IdProduct,
                    Quantity = item.Quantity
                };
                _dataContext.OrderedProducts.Add(productOrdered);
            }

            await _dataContext.SaveChangesAsync();
            await _cartService.ClerCart(); // Assicurati che il metodo ClearCart esista in ICartService
        }
    }
}