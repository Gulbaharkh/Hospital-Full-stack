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
    public class PreferenceController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public PreferenceController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Preference> preferences = _context.Preferences.ToList();
            return View(preferences);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Preference preference)
        {

            bool isExist = await _context.Preferences.AnyAsync(c => c.Title.ToLower() == preference.Title.ToLower());
            if (isExist)
            {
                ModelState.AddModelError("Title", "This info already exists!");
                return View();
            }
            await _context.AddRangeAsync(preference);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int? id)
        {

            if (id == null) return NotFound();
            Preference preference = await _context.Preferences.FirstOrDefaultAsync(c => c.Id == id);
            if (preference == null) return NotFound();
            return View(preference);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            Preference preference = await _context.Preferences.FirstOrDefaultAsync(c => c.Id == id);
            if (preference == null) return NotFound();
            return View(preference);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            if (id == null) return NotFound();
            Preference preference = await _context.Preferences.FirstOrDefaultAsync(c => c.Id == id);
            if (preference == null) return NotFound();
            _context.Preferences.Remove(preference);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {

            if (id == null) return NotFound();
            Preference preference = await _context.Preferences.FirstOrDefaultAsync(c => c.Id == id);
            if (preference == null) return NotFound();
            return View(preference);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Preference preference)
        {
            if (id == null) return NotFound();
            if (preference == null) return NotFound();
            Preference preferenceIntroView = await _context.Preferences.FirstOrDefaultAsync(c => c.Id == id);
            if (!ModelState.IsValid)
            {
                return View(preferenceIntroView);
            }
            Preference preferenceIntroDb = await _context.Preferences.FirstOrDefaultAsync(c => c.Title.ToLower().Trim() == preference.Title.ToLower().Trim());
            if (preferenceIntroDb != null && preferenceIntroDb.Id != id)
            {
                ModelState.AddModelError("Title", " Already exist.");
                return View(preferenceIntroView);
            }
            preferenceIntroView.Title = preference.Title;
            preferenceIntroView.Description = preference.Description;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
