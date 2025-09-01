using Eticaret.Core.Entities;
using Eticaret.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using static NuGet.Packaging.PackagingConstants;
using System.ComponentModel.DataAnnotations;

namespace Eticaret.WebUI.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Policy = "AdminPolicy")]
    public class OrdersController : Controller
    {
        private readonly DatabaseContext _context;

        public OrdersController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Admin/Orders
        public async Task<IActionResult> Index()
        {
            return View(await _context.Orders
                .Include(o => o.AppUser)//Orders tablosunu çekerken, ilişkili AppUser bilgisini de getir
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync());
        }

        // GET: Admin/Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(u => u.AppUser)
                .Include(o => o.OrderLines)
                    .ThenInclude(ol => ol.Product)
                        .ThenInclude(p => p.Brand)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Admin/Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OrderNumber,TotalPrice,AppUserId,CustomerId,BillingAddress,DeliveryAddress,OrderDate")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Admin/Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.AppUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Admin/Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string billingAddress, string deliveryAddress, EnumOrderState orderState)
        {
            try
            {
                // Mevcut siparişi getir
                var existingOrder = await _context.Orders.FindAsync(id);
                if (existingOrder == null)
                {
                    return NotFound();
                }

                // Debug için log
                System.Diagnostics.Debug.WriteLine($"Order ID: {id}");
                System.Diagnostics.Debug.WriteLine($"BillingAddress: {billingAddress}");
                System.Diagnostics.Debug.WriteLine($"DeliveryAddress: {deliveryAddress}");
                System.Diagnostics.Debug.WriteLine($"OrderState: {orderState}");

                // Sadece izin verilen alanları güncelle
                existingOrder.BillingAddress = billingAddress;
                existingOrder.DeliveryAddress = deliveryAddress;
                existingOrder.OrderState = orderState;

                _context.Update(existingOrder);
                await _context.SaveChangesAsync();
                
                System.Diagnostics.Debug.WriteLine("Sipariş başarıyla güncellendi!");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Hata: {ex.Message}");
                
                // Hata durumunda AppUser bilgilerini yükle
                var orderWithUser = await _context.Orders
                    .Include(o => o.AppUser)
                    .FirstOrDefaultAsync(m => m.Id == id);
                return View(orderWithUser);
            }
        }

        // GET: Admin/Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.AppUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Admin/Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
