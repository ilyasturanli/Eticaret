using Eticaret.Core.Entities;
using Eticaret.Service.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eticaret.WebUI.Controllers
{
    public class CategoriesController : Controller
    {
        //private readonly DatabaseContext _context;

        //public CategoriesController(DatabaseContext context)
        //{
        //    _context = context;
        //}
        private readonly IService<Category> _service;

        public CategoriesController(IService<Category> service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = await _service.GetQueryable().Include(p => p.Products)
                .FirstOrDefaultAsync(c => c.Id == id);
            return View(category);
        }
    }
}
