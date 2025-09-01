using Eticaret.Core.Entities;
using Eticaret.Data;
using Eticaret.Service.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Eticaret.Service.Concrete
{
    public class OrderService : Service<Order>, IOrderService
    {
        private readonly DatabaseContext _context;

        public OrderService(DatabaseContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Order> CreateOrderAsync(int userId, List<CartLine> cartLines, string deliveryAddress, string billingAddress)
        {
            // Sipariş numarası oluştur
            string orderNumber = GenerateOrderNumber();
            
            // Toplam fiyatı hesapla
            decimal totalPrice = cartLines.Sum(x => x.Product.Price * x.Quantity);
            
            // Kargo ücreti hesapla
            decimal shippingCost = totalPrice >= 10000 ? 0 : 450;
            decimal finalTotal = totalPrice + shippingCost;

            // Order oluştur
            var order = new Order
            {
                OrderNumber = orderNumber,
                TotalPrice = finalTotal,
                AppUserId = userId,
                CustomerId = userId.ToString(),
                BillingAddress = billingAddress,
                DeliveryAddress = deliveryAddress,
                OrderDate = DateTime.Now,
                OrderState = EnumOrderState.Waiting,
                OrderLines = new List<OrderLine>()
            };

            // OrderLines oluştur
            foreach (var cartLine in cartLines)
            {
                var orderLine = new OrderLine
                {
                    ProductId = cartLine.Product.Id,
                    Quantity = cartLine.Quantity,
                    UnitPrice = cartLine.Product.Price
                };
                order.OrderLines.Add(orderLine);
            }

            // Veritabanına kaydet
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            return order;
        }

        public async Task<List<Order>> GetUserOrdersAsync(int userId)
        {
            return await _context.Orders
                .Include(x => x.OrderLines!)
                .ThenInclude(x => x.Product)
                .Where(x => x.AppUserId == userId)
                .OrderByDescending(x => x.OrderDate)
                .ToListAsync();
        }

        public async Task<Order> GetOrderWithDetailsAsync(int orderId)
        {
            return await _context.Orders
                .Include(x => x.OrderLines!)
                .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.Id == orderId);
        }

        private string GenerateOrderNumber()
        {
            // Sipariş numarası formatı: YYYYMMDD-XXXXX
            string datePart = DateTime.Now.ToString("yyyyMMdd");
            string randomPart = new Random().Next(10000, 99999).ToString();
            return $"{datePart}-{randomPart}";
        }
    }
}
