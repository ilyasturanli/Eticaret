using Eticaret.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eticaret.WebUI.ViewComponents
{
    public class Sliders : ViewComponent
    {
        private readonly DatabaseContext _context;

        public Sliders(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _context.Sliders.ToListAsync());
        }
    }
}
