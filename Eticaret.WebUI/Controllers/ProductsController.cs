using Eticaret.Core.Entities;
using Eticaret.Data;
using Eticaret.Service.Abstract;
using Eticaret.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eticaret.WebUI.Controllers
{
    public class ProductsController : Controller
    {
        //private readonly DatabaseContext _context;
        //public ProductsController(DatabaseContext context)
        //{
        //    _context = context;
        //}
        private readonly IService<Product> _serviceProduct;

        public ProductsController(IService<Product> serviceProduct)
        {
            _serviceProduct = serviceProduct;
        }
        // GET: Admin/Products
        public async Task<IActionResult> Index(string arama = "")
        {
            var databaseContext = _serviceProduct.GetQueryable().Where(p=>p.IsActive && p.Name.Contains(arama)).Include(p => p.Brand).Include(p => p.Category);
            //Include → ilişkili tabloları eager loading ile çekmek için kullanılır.
            return View(await databaseContext.ToListAsync());
        }
        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _serviceProduct.GetQueryable()
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            var model = new ProductDetailsViewModel
            {
                Product = product,
                RelatedProducts =  _serviceProduct.GetQueryable()
                    .Where(p => p.IsActive && p.CategoryId == product.CategoryId && p.Id != product.Id)
                    //.Take(4) // İlgili kategorideki 4 ürünü al
            };
            return View(model);
        }
    }
}
