using Eticaret.Core.Entities;

namespace Eticaret.Service.Abstract
{
    public interface IOrderService : IService<Order>
    {
        Task<Order> CreateOrderAsync(int userId, List<CartLine> cartLines, string deliveryAddress, string billingAddress);
        Task<List<Order>> GetUserOrdersAsync(int userId);
        Task<Order> GetOrderWithDetailsAsync(int orderId);
    }
}
