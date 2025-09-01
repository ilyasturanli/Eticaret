using Eticaret.Core.Entities;
using Eticaret.Service.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Eticaret.WebUI.ViewComponents
{
    public class Categories : ViewComponent
    {
        //private readonly DatabaseContext _context;

        //public Categories(DatabaseContext context)
        //{
        //    _context = context;
        //}
        private readonly IService<Category> _serviceCategory;
        public Categories(IService<Category> serviceCategory)
        {
            _serviceCategory = serviceCategory;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _serviceCategory.GetAllAsync(c => c.IsActive && c.IsTopMenu));
        }
    }
}
