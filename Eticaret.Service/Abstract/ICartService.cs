using Eticaret.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eticaret.Service.Abstract
{
    public interface ICartService
    {
        List<CartLine> CartLines { get; } // Sepet satırlarını tutan liste
        void AddProduct(Product product,int quantity);// sepete ürün ekleme
        void UpdateProductQuantity(Product product, int quantity);// sepetteki ürünün miktarını güncelleme
        void RemoveProduct(Product product);// sepetteki ürünü kaldırma
        decimal GetTotalPrice();// sepetteki ürünlerin toplam fiyatını hesaplama
        void ClearCart();// sepeti temizleme
        //List<Product> GetCartItems();// sepetteki ürünleri listeleme
        //int GetTotalQuantity();// sepetteki ürünlerin toplam miktarını hesaplama
        //bool IsCartEmpty();// sepetin boş olup olmadığını kontrol etme
        
        
    }
}
