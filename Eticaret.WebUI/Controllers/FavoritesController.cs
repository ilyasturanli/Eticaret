using Eticaret.Core.Entities;
using Eticaret.Service.Abstract;
using Eticaret.WebUI.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;

namespace Eticaret.WebUI.Controllers
{
    public class FavoritesController : Controller
    {
        //private readonly DatabaseContext _context;
        //public FavoritesController(DatabaseContext context)
        //{
        //    _context = context;
        //}
        private readonly IService<Product> _service;

        public FavoritesController(IService<Product> service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            var favoriler = GetFavorites();
            return View(favoriler);
        }
        private List<Product> GetFavorites()
        {
            return HttpContext.Session.GetJson<List<Product>>("GetFavorites") ?? [];
        }
        public IActionResult Add(int ProductId)
        {
            var favoriler = GetFavorites();
            var product = _service.Find(ProductId);
            if(product != null && !favoriler.Any(p => p.Id == ProductId))
                //Daha önce favoriler listesine eklenmiş mi?
                //👉 Yani aynı ürün iki defa eklenmesin diye kontrol ediyor.
            {
                favoriler.Add(product);
                HttpContext.Session.SetJson("GetFavorites", favoriler);
            }
            return RedirectToAction("Index");
            
        }
        public IActionResult Remove(int ProductId)
        {
            var favoriler = GetFavorites();
            var product = _service.Find(ProductId);
            if (product != null && favoriler.Any(p => p.Id == ProductId))
            {
                favoriler.RemoveAll(i => i.Id == product.Id);// i favorilerdeki ürünlerimin ıd si...
                HttpContext.Session.SetJson("GetFavorites", favoriler);
            }
            return RedirectToAction("Index");

        }

        // AJAX için yeni metodlar
        [HttpPost]
        public IActionResult ToggleFavorite([FromBody] ToggleFavoriteRequest request)//Client (JavaScript / AJAX / Fetch) tarafında JSON body ile gönderilen ProductId parametresini alır. örnek productId:5
        {
            if (request == null || request.ProductId <= 0)
            {
                return Json(new { success = false, message = "Geçersiz ürün ID" });
            }
            
            var favoriler = GetFavorites();
            var product = _service.Find(request.ProductId);
            
            if (product == null)
            {
                return Json(new { success = false, message = $"Ürün bulunamadı (ID: {request.ProductId})" });
            }
            
            var isFavorite = favoriler.Any(p => p.Id == request.ProductId);
            
            if (isFavorite)
            {
                // Favorilerden çıkar
                favoriler.RemoveAll(i => i.Id == request.ProductId);
            }
            else
            {
                // Favorilere ekle
                favoriler.Add(product);
            }
            
            HttpContext.Session.SetJson("GetFavorites", favoriler);
            
            return Json(new { 
                success = true, 
                isFavorite = !isFavorite, 
                count = favoriler.Count,
                message = !isFavorite ? "Favorilere eklendi" : "Favorilerden çıkarıldı"
            });
        }
        
        [HttpGet]
        public IActionResult GetFavoriteCount()
        {
            var favoriler = GetFavorites();
            return Json(new { count = favoriler.Count });
        }
    }
    
    public class ToggleFavoriteRequest
    {
        public int ProductId { get; set; }
    }
}
