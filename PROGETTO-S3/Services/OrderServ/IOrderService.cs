using PROGETTO_S3.Models;

namespace PROGETTO_S3.Services.OrderServ
{
    public interface IOrderService
    {
        public Task CreateOrder(Order order);
        public Task<List<Order>> GetAllOrders();
        public Task<Order> IsProcessedTrue(int id);

    }
}
