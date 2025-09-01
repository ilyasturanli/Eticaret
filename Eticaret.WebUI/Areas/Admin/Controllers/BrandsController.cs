using Eticaret.Core.Entities;
using Eticaret.Data;
using Eticaret.WebUI.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.ConstrainedExecution;

namespace Eticaret.WebUI.Areas.Admin.Controllers
{
    [Area("Admin"),Authorize(Policy = "AdminPolicy")]
    public class BrandsController : Controller
    {
        private readonly DatabaseContext _context;

        public BrandsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Admin/Brands
        public async Task<IActionResult> Index()
        {
            return View(await _context.Brands.ToListAsync());
        }

        // GET: Admin/Brands/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brand = await _context.Brands
                .FirstOrDefaultAsync(m => m.Id == id);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        // GET: Admin/Brands/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Brands/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Brand brand, IFormFile? Logo)// burdaki logo ismi create.cshtmldeki logo isminden geliyor...
        {
            if (ModelState.IsValid)
            {
                brand.Logo = await FileHelper.FileLoaderAsync(Logo, "/Img/Brands/");
                await _context.AddAsync(brand);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(brand);
        }

        // GET: Admin/Brands/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }

        // POST: Admin/Brands/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Brand brand,IFormFile? Logo,bool cbResmiSil = false)
        {
            if (id != brand.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingBrand = await _context.Brands.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
                    if (existingBrand == null)
                    {
                        return NotFound();
                    }

                    if (Logo is not null)
                    {
                        var newFileName = await FileHelper.FileLoaderAsync(Logo, "/Img/Brands/");
                        if (!string.IsNullOrEmpty(existingBrand.Logo))
                        {
                            FileHelper.FileRemover(existingBrand.Logo, "/Img/Brands/");
                        }
                        brand.Logo = newFileName;
                    }
                    else if (cbResmiSil)
                    {
                        if (!string.IsNullOrEmpty(existingBrand.Logo))
                        {
                            FileHelper.FileRemover(existingBrand.Logo, "/Img/Brands/");
                        }
                        brand.Logo = string.Empty;
                    }
                    else
                    {
                        brand.Logo = existingBrand.Logo;
                    }

                    _context.Update(brand);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BrandExists(brand.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(brand);
        }

        // GET: Admin/Brands/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brand = await _context.Brands
                .FirstOrDefaultAsync(m => m.Id == id);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        // POST: Admin/Brands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var brand = await _context.Brands.Include(p => p.Products).FirstOrDefaultAsync(b => b.Id == id);

            // _context.Brands → Brands tablosuna bakıyoruz.

            //.Include(b => b.Products) → her Brand nesnesiyle birlikte o markaya bağlı Products listesini de yükle.

            //.FirstOrDefaultAsync(b => b.Id == id) → belirtilen id değerine sahip olan markayı getir(tek kayıt döner).
            if (brand != null)
            {
                
                if(!string.IsNullOrEmpty(brand.Logo))
                {
                    FileHelper.FileRemover(brand.Logo, "/Img/Brands/"); // Dosyayı silme işlemi...
                }
                // Markaya bağlı ürünlerin fotoğraflarını sil
                if (brand.Products != null && brand.Products.Any())
                //brand.Products.Any() → Eğer liste boş değilse (yani içinde en az 1 ürün varsa) true döner.
                {
                    foreach (var product in brand.Products)
                    {
                        if (!string.IsNullOrEmpty(product.Image))
                        {
                            FileHelper.FileRemover(product.Image, "/Img/Products/");
                        }
                    }
                    _context.Products.RemoveRange(brand.Products);
                }
                _context.Brands.Remove(brand);
                await _context.SaveChangesAsync();
            }

            
            return RedirectToAction(nameof(Index));
        }

        private bool BrandExists(int id)
        {
            return _context.Brands.Any(e => e.Id == id);
        }
    }
}
