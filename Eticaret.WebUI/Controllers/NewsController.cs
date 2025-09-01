using Eticaret.Core.Entities;
using Eticaret.Service.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Eticaret.WebUI.Controllers
{
    public class NewsController : Controller
    {
        //private readonly DatabaseContext _context;
        //public NewsController(DatabaseContext context)
        //{
        //    _context = context;
        //}
        private readonly IService<News> _serviceNews;
        public NewsController(IService<News> serviceNews)
        {
            _serviceNews = serviceNews;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _serviceNews.GetAllAsync());
        }


        // GET: Admin/News/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _serviceNews.GetAsync(m => m.Id == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }
    }
}
