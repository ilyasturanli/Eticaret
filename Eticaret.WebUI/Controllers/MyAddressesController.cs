using Eticaret.Core.Entities;
using Eticaret.Service.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Eticaret.WebUI.Controllers
{
    [Authorize]
    public class MyAddressesController : Controller
    {
        private readonly IService<AppUser> _serviceAppUser;
        private readonly IService<Adress> _serviceAddress;
        public MyAddressesController(IService<AppUser> serviceAppUser, IService<Adress> serviceAddress)
        {
            _serviceAppUser = serviceAppUser;
            _serviceAddress = serviceAddress;
        }
        public async Task<IActionResult> Index()
        {
            var appUser = await _serviceAppUser.GetAsync(x => x.UserGuid.ToString() ==
            HttpContext.User.FindFirst("UserGuid").Value);
            if (appUser == null)
            {
                return NotFound("Kullanıcı Datası bulunamadı! Oturumunuzu Kapatıp Lütfen Tekrar Giris Yapınız...");
            }
            var model = await _serviceAddress.GetAllAsync(u => u.AppUserId == appUser.Id);
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        // POST: MyAddresses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Adress adress)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var appUser = await _serviceAppUser.GetAsync(x => x.UserGuid.ToString() ==
                    HttpContext.User.FindFirst("UserGuid").Value);

                    if(appUser != null)
                    {
                        adress.AppUserId = appUser.Id;
                        
                        // Eğer yeni adres hem aktif, hem fatura, hem teslimat adresi olarak işaretlenmişse
                        // önceden bu üç özelliğe de sahip olan adreslerin işaretlerini kaldır
                        if (adress.IsActive && adress.IsBillingAdress && adress.IsDeliveryAdress)
                        {
                            var existingAddresses = await _serviceAddress.GetAllAsync(x => 
                                x.AppUserId == appUser.Id && 
                                x.IsActive && 
                                x.IsBillingAdress && 
                                x.IsDeliveryAdress);
                            
                            foreach (var existingAddress in existingAddresses)
                            {
                                existingAddress.IsActive = false;
                                existingAddress.IsBillingAdress = false;
                                existingAddress.IsDeliveryAdress = false;
                                _serviceAddress.Update(existingAddress);
                            }
                        }
                        
                        _serviceAddress.Add(adress);
                        await _serviceAddress.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch(Exception)
                {
                    ModelState.AddModelError("","Hata Olustu...");
                }               
            }
            ModelState.AddModelError("","Kayıt Basarısız...");
            return View(adress);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var appUser = await _serviceAppUser.GetAsync(x => x.UserGuid.ToString() ==
            HttpContext.User.FindFirst("UserGuid").Value);
            if(appUser == null)
            {
                return NotFound("Kullanıcı Datası bulunamadı! Oturumunuzu Kapatıp Lütfen Tekrar Giris Yapınız...");
            }
            var model = await _serviceAddress.GetAsync(u => u.AdressGuid.ToString() == id && u.AppUserId ==appUser.Id);
            if(model == null)
            {
                return NotFound("Adres Bilgisi Bulunamadı!");
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id,Adress adress)
        {
            var appUser = await _serviceAppUser.GetAsync(x => x.UserGuid.ToString() ==
            HttpContext.User.FindFirst("UserGuid").Value);
            if (appUser == null)
            {
                return NotFound("Kullanıcı Datası bulunamadı! Oturumunuzu Kapatıp Lütfen Tekrar Giris Yapınız...");
            }
            var model = await _serviceAddress.GetAsync(u => u.AdressGuid.ToString() == id && u.AppUserId == appUser.Id);
            if (model == null)
            {
                return NotFound("Adres Bilgisi Bulunamadı!");
            }
            model.Title = adress.Title;
            model.City = adress.City;
            model.District = adress.District;
            model.OpenAdress = adress.OpenAdress;
            model.BillingType = adress.BillingType;
            model.CompanyName = adress.CompanyName;
            model.TaxNumber = adress.TaxNumber;
            model.TaxOffice = adress.TaxOffice;
            model.IsDeliveryAdress = adress.IsDeliveryAdress;
            model.IsBillingAdress = adress.IsBillingAdress;
            model.IsActive = adress.IsActive;
            
            // Eğer düzenlenen adres hem aktif, hem fatura, hem teslimat adresi olarak işaretlenmişse
            // önceden bu üç özelliğe de sahip olan diğer adreslerin işaretlerini kaldır
            if (model.IsActive && model.IsBillingAdress && model.IsDeliveryAdress)
            {
                var existingAddresses = await _serviceAddress.GetAllAsync(x => 
                    x.AppUserId == appUser.Id && 
                    x.Id != model.Id &&
                    x.IsActive && 
                    x.IsBillingAdress && 
                    x.IsDeliveryAdress);
                
                foreach (var existingAddress in existingAddresses)
                {
                    existingAddress.IsActive = false;
                    existingAddress.IsBillingAdress = false;
                    existingAddress.IsDeliveryAdress = false;
                    _serviceAddress.Update(existingAddress);
                }
            }
            try
            {
                _serviceAddress.Update(model);
                await _serviceAddress.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            catch (Exception)
            {
                ModelState.AddModelError("","Hata Oluştu...");
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var appUser = await _serviceAppUser.GetAsync(x => x.UserGuid.ToString() ==
           HttpContext.User.FindFirst("UserGuid").Value);
            if (appUser == null)
            {
                return NotFound("Kullanıcı Datası bulunamadı! Oturumunuzu Kapatıp Lütfen Tekrar Giris Yapınız...");
            }
            var model = await _serviceAddress.GetAsync(u => u.AdressGuid.ToString() == id && u.AppUserId == appUser.Id);
            if (model == null)
            {
                return NotFound("Adres Bilgisi Bulunamadı!");
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id,Adress adress)
        {
            var appUser = await _serviceAppUser.GetAsync(x => x.UserGuid.ToString() ==
           HttpContext.User.FindFirst("UserGuid").Value);
            if (appUser == null)
            {
                return NotFound("Kullanıcı Datası bulunamadı! Oturumunuzu Kapatıp Lütfen Tekrar Giris Yapınız...");
            }
            var model = await _serviceAddress.GetAsync(u => u.AdressGuid.ToString() == id && u.AppUserId == appUser.Id);
            if (model == null)
            {
                return NotFound("Adres Bilgisi Bulunamadı!");
            }
            try
            {
                _serviceAddress.Delete(model);
                await _serviceAddress.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Hata Oluştu...");
            }
            return View(model);
        }
    }
}
