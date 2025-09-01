using Eticaret.Core.Entities;
using Eticaret.Service.Abstract;
using Newtonsoft.Json;

namespace Eticaret.Service.Concrete
{
    public class CartService : ICartService
    {
        public List<CartLine> CartLines { get; } = new();// Sepet satırlarını tutan liste
        public void AddProduct(Product product, int quantity)
        {
            var urun = CartLines.FirstOrDefault(x => x.Product.Id == product.Id);
            if (urun != null)
            {
                urun.Quantity += quantity; // Eğer ürün zaten sepette varsa, miktarını artır
            }
            else
            {
                CartLines.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            }
        }

        public void ClearCart()
        {
            CartLines.Clear(); // Sepeti temizle
        }

        public decimal GetTotalPrice()
        {
            return CartLines.Sum(c => c.Product.Price * c.Quantity);

        }

        public void RemoveProduct(Product product)
        {
            CartLines.RemoveAll(p => p.Product.Id == product.Id);// Sepetten ürünü kaldır
        }

        public void UpdateProductQuantity(Product product, int quantity)
        {
            var urun = CartLines.FirstOrDefault(x => x.Product.Id == product.Id);
            if (urun != null)
            {
                urun.Quantity = quantity;
            }
            else
            {
                CartLines.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            }
        }
      
    }
}
