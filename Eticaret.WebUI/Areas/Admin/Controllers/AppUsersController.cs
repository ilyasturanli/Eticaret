using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Eticaret.Core.Entities;
using Eticaret.Core.Helpers;
using Eticaret.Data;
using Microsoft.AspNetCore.Authorization;

namespace Eticaret.WebUI.Areas.Admin.Controllers
{
    [Area("Admin"),Authorize(Policy ="AdminPolicy")]
    public class AppUsersController : Controller
    {
        private readonly DatabaseContext _context;

        public AppUsersController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Admin/AppUsers
        public async Task<IActionResult> Index()
        {
            return View(await _context.AppUsers.ToListAsync());
        }

        // GET: Admin/AppUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUser = await _context.AppUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appUser == null)
            {
                return NotFound();
            }

            return View(appUser);
        }

        // GET: Admin/AppUsers/Create
        //İkisi de aynı ismi taşır ama HTTP methodu (GET / POST) farklı olduğu için çakışmaz. ALTTAKİ CREATE İLE...
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AppUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Surname,Email,Phone,Password,UserName,IsActive,IsAdmin")] AppUser appUser)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Email benzersizlik kontrolü
                    var existingUser = await _context.AppUsers.FirstOrDefaultAsync(u => u.Email == appUser.Email);
                    if (existingUser != null)
                    {
                        ModelState.AddModelError("Email", "Bu email adresi zaten kullanılıyor.");
                        return View(appUser);
                    }

                    // Otomatik alanları set et
                    appUser.CreateDate = DateTime.Now;
                    appUser.UserGuid = Guid.NewGuid();
                    
                    // UserName boşsa email kullan
                    if (string.IsNullOrEmpty(appUser.UserName))
                    {
                        appUser.UserName = appUser.Email;
                    }

                    // Şifreyi hash'le
                    appUser.Password = PasswordHelper.HashPassword(appUser.Password);

                    _context.Add(appUser);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Kullanıcı oluşturulurken hata oluştu: {ex.Message}");
                }
            }
            return View(appUser);
        }

        // GET: Admin/AppUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUser = await _context.AppUsers.FindAsync(id);
            if (appUser == null)
            {
                return NotFound();
            }
            return View(appUser);
        }

        // POST: Admin/AppUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,Email,Phone,UserName,Password,IsActive,IsAdmin,CreateDate,UserGuid")] AppUser appUser)
        {
            // Debug log'ları
            Console.WriteLine($"Edit action called with id: {id}");
            Console.WriteLine($"Received Name: {appUser.Name}");
            Console.WriteLine($"Received Surname: {appUser.Surname}");
            Console.WriteLine($"Received Email: {appUser.Email}");
            Console.WriteLine($"Received IsActive: {appUser.IsActive}");
            Console.WriteLine($"Received IsAdmin: {appUser.IsAdmin}");
            Console.WriteLine($"Received Password: {appUser.Password}");
            
            if (id != appUser.Id)
            {
                return NotFound();
            }

            // Password validation'ı tamamen bypass et
            ModelState.Remove("Password");
            
            if (ModelState.IsValid)
            {
                try
                {
                    var existingUser = await _context.AppUsers.FindAsync(id);
                    if (existingUser == null)
                    {
                        return NotFound();
                    }

                    Console.WriteLine($"Existing user - Name: {existingUser.Name}, Surname: {existingUser.Surname}");

                    // Email benzersizlik kontrolü (kendi email'i hariç)
                    var emailExists = await _context.AppUsers
                        .AnyAsync(u => u.Email == appUser.Email && u.Id != id);
                    if (emailExists)
                    {
                        ModelState.AddModelError("Email", "Bu email adresi başka bir kullanıcı tarafından kullanılıyor.");
                        return View(appUser);
                    }

                    // Mevcut kullanıcının özelliklerini güncelle
                    existingUser.Name = appUser.Name;
                    existingUser.Surname = appUser.Surname;
                    existingUser.Email = appUser.Email;
                    existingUser.Phone = appUser.Phone;
                    existingUser.IsActive = appUser.IsActive;
                    existingUser.IsAdmin = appUser.IsAdmin;
                    
                    Console.WriteLine($"After update - Name: {existingUser.Name}, Surname: {existingUser.Surname}");
                    
                    // CreateDate ve UserGuid korunmalı
                    existingUser.CreateDate = existingUser.CreateDate;
                    existingUser.UserGuid = existingUser.UserGuid;

                    // UserName boşsa email kullan
                    if (string.IsNullOrEmpty(appUser.UserName))
                    {
                        existingUser.UserName = appUser.Email;
                    }
                    else
                    {
                        existingUser.UserName = appUser.UserName;
                    }

                    // Şifre güncellenmişse hash'le
                    if (!string.IsNullOrEmpty(appUser.Password) && appUser.Password != existingUser.Password)
                    {
                        existingUser.Password = PasswordHelper.HashPassword(appUser.Password);
                        Console.WriteLine("Password updated");
                    }
                    else
                    {
                        // Mevcut şifreyi koru - hiçbir şey yapma
                        Console.WriteLine("Keeping existing password");
                    }

                    _context.Update(existingUser);
                    await _context.SaveChangesAsync();
                    
                    Console.WriteLine($"User updated successfully - Final Name: {existingUser.Name}");
                    
                    TempData["SuccessMessage"] = "Kullanıcı bilgileri başarıyla güncellendi!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error updating user: {ex.Message}");
                    ModelState.AddModelError("", $"Kullanıcı güncellenirken hata oluştu: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("ModelState is not valid:");
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }
            }
            return View(appUser);
        }

        // GET: Admin/AppUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUser = await _context.AppUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appUser == null)
            {
                return NotFound();
            }

            return View(appUser);
        }

        // POST: Admin/AppUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appUser = await _context.AppUsers.FindAsync(id);
            if (appUser != null)
            {
                _context.AppUsers.Remove(appUser);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppUserExists(int id)
        {
            return _context.AppUsers.Any(e => e.Id == id);
        }
    }
}
