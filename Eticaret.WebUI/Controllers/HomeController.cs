using Eticaret.Core.Entities;
using Eticaret.Service.Abstract;
using Eticaret.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Eticaret.WebUI.Controllers
{
    public class HomeController : Controller
    {
        //private readonly DatabaseContext _context;

        //public HomeController(DatabaseContext context)
        //{
        //    _context = context;
        //}
        private readonly IService<AppUser> _serviceAppUser;
        private readonly IService<Product> _serviceProduct;
        private readonly IService<Slider> _serviceSlider;
        private readonly IService<News> _serviceNews;
        private readonly IService<Contact> _serviceContact;

        public HomeController(IService<Product> serviceProduct,IService<Slider> serviceSlider,IService<News> serviceNews,IService<Contact> serviceContact,IService<AppUser> serviceAppUser)
        {
            _serviceAppUser = serviceAppUser;
            _serviceProduct = serviceProduct;
            _serviceSlider = serviceSlider;
            _serviceNews = serviceNews;
            _serviceContact = serviceContact;
        }
        public async Task<IActionResult> Index()
        {
            var model = new HomePageViewModel()
            {
                Sliders = await _serviceSlider.GetAllAsync(),
                Products = await _serviceProduct.GetAllAsync(p => p.IsActive && p.IsHome),
                News = await _serviceNews.GetAllAsync()
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [Route("AccessDenied")]// bunun anlamý normalde /Home/AccessDenied e gidicegine direk /AccessDenied e götürüyo...
        public IActionResult AccessDenied()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> ContactUs()
        {
            var model = new Contact();
            if (User.Identity.IsAuthenticated)
            {
                var userguid = User.FindFirst("UserGuid").Value;
                if (!string.IsNullOrEmpty(userguid))
                {
                    var user = await _serviceAppUser.GetAsync(x => x.UserGuid.ToString() == userguid);
                    if (user != null)
                    {
                        model.Name = user.Name;
                        model.Surname = user.Surname;
                        model.Email = user.Email;
                        model.Phone = user.Phone;
                    }
                }
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ContactUs(Contact contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _serviceContact.AddAsync(contact);
                    var sonuc =  await _serviceContact.SaveChangesAsync();
                    if(sonuc > 0)
                    {
                        TempData["Message"] = @"<div class=""alert alert-success alert-dismissible fade show"" role=""alert"">
  <strong>Mesajýnýz baþarýyla gönderilmiþtir!</strong>
  <button type=""button"" class=""btn-close"" data-bs-dismiss=""alert"" aria-label=""Close""></button>
</div>";
                        //await MailHelper.SendEmailAsync(contact);
                        return RedirectToAction("ContactUs");// http nin yerinde ContactUs a geri yönlendiriyo...
                    }
                }
                catch(Exception)
                {
                    ModelState.AddModelError("", "Hata olustu...");
                }
            }
            return View(contact);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
