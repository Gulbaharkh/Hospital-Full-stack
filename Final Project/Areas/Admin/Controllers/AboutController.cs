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
    public class AboutController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public AboutController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<About> abouts = _context.Abouts.ToList();
            return View(abouts);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(About about)
        {

            bool isExist = await _context.Abouts.AnyAsync(c => c.Title.ToLower() == about.Title.ToLower());
            if (isExist)
            {
                ModelState.AddModelError("Title", "This info already exists!");
                return View();
            }
            await _context.AddRangeAsync(about);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int? id)
        {

            if (id == null) return NotFound();
            About about = await _context.Abouts.FirstOrDefaultAsync(c => c.Id == id);
            if (about == null) return NotFound();
            return View(about);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            About about = await _context.Abouts.FirstOrDefaultAsync(c => c.Id == id);
            if (about == null) return NotFound();
            return View(about);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            if (id == null) return NotFound();
            About about = await _context.Abouts.FirstOrDefaultAsync(c => c.Id == id);
            if (about == null) return NotFound();
            _context.Abouts.Remove(about);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {

            if (id == null) return NotFound();
            About about = await _context.Abouts.FirstOrDefaultAsync(c => c.Id == id);
            if (about == null) return NotFound();
            return View(about);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, About about)
        {
            if (id == null) return NotFound();
            if (about == null) return NotFound();
            About aboutIntroView = await _context.Abouts.FirstOrDefaultAsync(c => c.Id == id);
            if (!ModelState.IsValid)
            {
                return View(aboutIntroView);
            }
            About aboutIntroDb = await _context.Abouts.FirstOrDefaultAsync(c => c.Title.ToLower().Trim() == about.Title.ToLower().Trim());
            if (aboutIntroDb != null && aboutIntroDb.Id != id)
            {
                ModelState.AddModelError("Title", " Already exist.");
                return View(aboutIntroView);
            }
            aboutIntroView.Title = about.Title;
            aboutIntroView.Description = about.Description;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
