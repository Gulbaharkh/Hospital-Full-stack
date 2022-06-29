using Final_Project.DAL;
using Final_Project.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public ServiceController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Service> services = _context.Services.ToList();
            return View(services);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Service service)
        {

            bool isExist = await _context.Services.AnyAsync(c => c.Title.ToLower() == service.Title.ToLower());
            if (isExist)
            {
                ModelState.AddModelError("Title", "This info already exists!");
                return View();
            }
            await _context.AddRangeAsync(service);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int? id)
        {

            if (id == null) return NotFound();
            Service service = await _context.Services.FirstOrDefaultAsync(c => c.Id == id);
            if (service == null) return NotFound();
            return View(service);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            Service service = await _context.Services.FirstOrDefaultAsync(c => c.Id == id);
            if (service == null) return NotFound();
            return View(service);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            if (id == null) return NotFound();
            Service service = await _context.Services.FirstOrDefaultAsync(c => c.Id == id);
            if (service == null) return NotFound();
            _context.Services.Remove(service);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {

            if (id == null) return NotFound();
            Service service = await _context.Services.FirstOrDefaultAsync(c => c.Id == id);
            if (service == null) return NotFound();
            return View(service);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Service service)
        {
            if (id == null) return NotFound();
            if (service == null) return NotFound();
            Service serviceIntroView = await _context.Services.FirstOrDefaultAsync(c => c.Id == id);
            if (!ModelState.IsValid)
            {
                return View(serviceIntroView);
            }
            Service serviceIntroDb = await _context.Services.FirstOrDefaultAsync(c => c.Title.ToLower().Trim() == service.Title.ToLower().Trim());
            if (serviceIntroDb != null && serviceIntroDb.Id != id)
            {
                ModelState.AddModelError("Title", " Already exist.");
                return View(serviceIntroView);
            }
            serviceIntroView.Title = service.Title;
            serviceIntroView.Description = service.Description;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
