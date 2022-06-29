using Final_Project.DAL;
using Final_Project.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderIntroController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public SliderIntroController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<SliderIntro> sliderIntros = _context.SliderIntros.ToList();
            return View(sliderIntros);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderIntro sliderIntro)
        {

            bool isExist = await _context.SliderIntros.AnyAsync(c => c.Title.ToLower() == sliderIntro.Title.ToLower());
            if (isExist)
            {
                ModelState.AddModelError("Title", "This SliderIntro already exists!");
                return View();
            }
            await _context.AddRangeAsync(sliderIntro);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int? id)
        {

            if (id == null) return NotFound();
            SliderIntro sliderIntro = await _context.SliderIntros.FirstOrDefaultAsync(c => c.Id == id);
            if (sliderIntro == null) return NotFound();
            return View(sliderIntro);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            SliderIntro sliderIntro = await _context.SliderIntros.FirstOrDefaultAsync(c => c.Id == id);
            if (sliderIntro == null) return NotFound();
            return View(sliderIntro);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            if (id == null) return NotFound();
            SliderIntro sliderIntro = await _context.SliderIntros.FirstOrDefaultAsync(c => c.Id == id);
            if (sliderIntro == null) return NotFound();
            _context.SliderIntros.Remove(sliderIntro);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {

            if (id == null) return NotFound();
            SliderIntro sliderIntro = await _context.SliderIntros.FirstOrDefaultAsync(c => c.Id == id);
            if (sliderIntro == null) return NotFound();
            return View(sliderIntro);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, SliderIntro sliderIntro)
        {
            if (id == null) return NotFound();
            if (sliderIntro == null) return NotFound();
            SliderIntro sliderIntroView = await _context.SliderIntros.FirstOrDefaultAsync(c => c.Id == id);
            if (!ModelState.IsValid)
            {
                return View(sliderIntroView);
            }
            SliderIntro sliderIntroDb = await _context.SliderIntros.FirstOrDefaultAsync(c => c.Title.ToLower().Trim() == sliderIntro.Title.ToLower().Trim());
            if (sliderIntroDb != null && sliderIntroDb.Id != id)
            {
                ModelState.AddModelError("Intro Name", " Already exist.");
                return View(sliderIntroView);
            }
            sliderIntroView.Title = sliderIntro.Title;
            sliderIntroView.Description = sliderIntro.Description;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
