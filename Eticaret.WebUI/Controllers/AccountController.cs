using Eticaret.Core.Entities;
using Eticaret.Core.Helpers;
using Eticaret.Service.Abstract;
using Eticaret.WebUI.Models;
using Eticaret.WebUI.Utils;
using Microsoft.AspNetCore.Authentication;//login
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;//login
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;//login

namespace Eticaret.WebUI.Controllers
{
    public class AccountController : Controller
    {
        //private readonly DatabaseContext _context;
        //public AccountController(DatabaseContext context)
        //{
        //    _context = context;
        //}

        private readonly IService<AppUser> _service;
        private readonly IOrderService _orderService;

        public AccountController(IService<AppUser> service, IOrderService orderService)
        {
            _service = service;
            _orderService = orderService;
        }

        [Authorize]
        public async Task<IActionResult> MyOrders()
        {
            try
            {
                var userGuidClaim = HttpContext.User.FindFirst("UserGuid");
                if (userGuidClaim == null)
                {
                    return RedirectToAction("SignIn");
                }

                var user = await _service.GetAsync(x => x.UserGuid.ToString() == userGuidClaim.Value);
                if (user == null)
                {
                    return RedirectToAction("SignIn");
                }

                var orders = await _orderService.GetUserOrdersAsync(user.Id);
                return View(orders);
            }
            catch
            {
                TempData["ErrorMessage"] = "Siparişleriniz yüklenirken bir hata oluştu.";
                return RedirectToAction("Index");
            }
        }

        [Authorize]
        public async Task<IActionResult> OrderDetail(int id)
        {
            try
            {
                var userGuidClaim = HttpContext.User.FindFirst("UserGuid");
                if (userGuidClaim == null)
                {
                    return RedirectToAction("SignIn");
                }

                var user = await _service.GetAsync(x => x.UserGuid.ToString() == userGuidClaim.Value);
                if (user == null)
                {
                    return RedirectToAction("SignIn");
                }

                var order = await _orderService.GetOrderWithDetailsAsync(id);
                if (order == null || order.AppUserId != user.Id)
                {
                    TempData["ErrorMessage"] = "Sipariş bulunamadı.";
                    return RedirectToAction("MyOrders");
                }

                return View(order);
            }
            catch
            {
                TempData["ErrorMessage"] = "Sipariş detayları yüklenirken bir hata oluştu.";
                return RedirectToAction("MyOrders");
            }
        }

        [Authorize]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Index()
        {
            AppUser user = await _service.GetAsync
                (x => x.UserGuid.ToString() == HttpContext.User.FindFirst("UserGuid").Value);
            if(user == null)
            {
                return NotFound();
            }
            var model = new UserEditViewModel()
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Phone = user.Phone,
                Email = user.Email,
            };
            return View(model);
        }
        [HttpPost,Authorize]
        public async Task<IActionResult> Index(UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Guid userGuid = Guid.Parse(HttpContext.User.FindFirst("UserGuid").Value);

                    AppUser user = await _service.GetAsync(x => x.UserGuid == userGuid);
                    if (user is not null)
                    {
                        user.Name = model.Name;
                        user.Surname = model.Surname;
                        // Email alanı readonly olduğu için model'den al
                        user.Email = model.Email;
                        user.Phone = model.Phone;
                        
                        // Şifre sadece değiştirildiğinde hash'le
                        if (!string.IsNullOrEmpty(model.Password))
                        {
                            user.Password = PasswordHelper.HashPassword(model.Password);
                        }
                        
                        _service.Update(user);
                        await _service.SaveChangesAsync();

                        // Başarı mesajı ve yönlendirme için TempData kullanıyoruz
                        TempData["SuccessMessage"] = "Değişiklikler başarıyla kaydedildi!";
                        return RedirectToAction(nameof(Index)); // redirect ile mesaj göster
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Hata Olustu...");
                }
            }
            return View(model);
        }
        public IActionResult SignIn(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(LoginViewModel loginViewModel)
        {
            // RememberMe değerini güvenli bir şekilde işle
            if (loginViewModel.RememberMe == null)
            {
                loginViewModel.RememberMe = false;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var account = await _service.GetAsync
                        (x => x.Email==loginViewModel.Email && x.IsActive);
                    
                    if(account == null)
                    {
                        ModelState.AddModelError("", "Giriş başarısız!");
                    }
                    else
                    {
                        // Şifre doğrulaması
                        if (!PasswordHelper.VerifyPassword(loginViewModel.Password, account.Password))
                        {
                            ModelState.AddModelError("", "Email veya şifre hatalı!");
                            return View(loginViewModel);
                        }

                        var claims = new List<Claim>()
                        {
                            new(ClaimTypes.Name,account.Name),
                            new(ClaimTypes.Role,account.IsAdmin ? "Admin":"Customer"),
                            new(ClaimTypes.Email,account.Email),
                            new("UserId",account.Id.ToString()),
                            new("UserGuid",account.UserGuid.ToString()),
                        };
                        var userIdentity = new ClaimsIdentity(claims,"Login");
                        ClaimsPrincipal userPrincipal = new ClaimsPrincipal(userIdentity);
                        
                        // RememberMe değerine göre authentication ayarları
                        var authProperties = new AuthenticationProperties
                        {
                            IsPersistent = loginViewModel.RememberMe.Value
                        };
                        
                        await HttpContext.SignInAsync(userPrincipal, authProperties);
                        return Redirect(string.IsNullOrEmpty(loginViewModel.ReturnUrl) ? "/" : 
                            loginViewModel.ReturnUrl);
                    }
                }
                catch(Exception)
                {
                    //loglama
                    ModelState.AddModelError("","Hata Olustu...");
                }
                
            }
            return View(loginViewModel);
        }
        public IActionResult SignUp(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(AppUser appUser, string returnUrl = null)
        {
            try
            {
                // Email benzersizlik kontrolü
                var existingUser = await _service.GetAsync(x => x.Email == appUser.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Email", "Bu email adresi zaten kullanılıyor.");
                    return View(appUser);
                }

                // Otomatik alanları set et
                appUser.CreateDate = DateTime.Now;
                appUser.UserGuid = Guid.NewGuid();
                appUser.IsAdmin = false; // Default value for new users
                appUser.IsActive = true; // Default value for new users
                
                // UserName boşsa email kullan
                if (string.IsNullOrEmpty(appUser.UserName))
                {
                    appUser.UserName = appUser.Email;
                }

                // Şifreyi hash'le
                appUser.Password = PasswordHelper.HashPassword(appUser.Password);

                await _service.AddAsync(appUser);
                var result = await _service.SaveChangesAsync();

                if (result > 0)
                {
                    TempData["SuccessMessage"] = "Kayıt başarıyla tamamlandı! Giriş yapabilirsiniz.";
                    
                    // Eğer returnUrl varsa oraya yönlendir, yoksa giriş sayfasına
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction(nameof(SignIn));
                }
                else
                {
                    ModelState.AddModelError("", "Kayıt sırasında veritabanı hatası oluştu.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Kayıt sırasında hata oluştu: {ex.Message}");
            }
            
            return View(appUser);
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ForgotPassword(string Email)
        {
            if (string.IsNullOrEmpty(Email))
            {
                ModelState.AddModelError("Email", "Email alanı gereklidir.");
                return View();
            }
            AppUser user = _service.Get(x => x.Email == Email);
            if(user == null)
            {
                ModelState.AddModelError("Email", "Bu email adresi ile kayıtlı kullanıcı bulunamadı.");
                return View();
            }
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";
            return RedirectToAction("SignIn");
            //RedirectToAction("SignIn") dediğin şey,
            //controller içindeki veya belirttiğin controller’daki bir action metodun ismidir.
        }
    }
}
