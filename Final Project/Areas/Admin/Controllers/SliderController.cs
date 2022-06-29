using Final_Project.DAL;
using Final_Project.Extensions;
using Final_Project.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace Final_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public SliderController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Slider> sliders = _context.Sliders.ToList();
            return View(sliders);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,Photo")] Slider slider)
        {

            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid) return View();
            if (!slider.Photo.IsValidType("image/"))
            {
                ModelState.AddModelError("", "Please select image type");
                return View();
            }
            if (!slider.Photo.IsValidSize(200))
            {
                ModelState.AddModelError("", "Image size should be less than 200kb");
                return View();
            }
            string filepath = Path.Combine("img", "slider");

            slider.ImageURL = await slider.Photo.SaveFileAsync(_env.WebRootPath, filepath);



            await _context.Sliders.AddAsync(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int? id)
        {

            if (id == null) return NotFound();
            Slider slider = await _context.Sliders.FirstOrDefaultAsync(slider => slider.Id == id);
            if (slider == null) return NotFound();
            return View(slider);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            Slider slider = await _context.Sliders.FirstOrDefaultAsync(slider => slider.Id == id);
            if (slider == null) return NotFound();
            return View(slider);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            if (id == null) return NotFound();
            Slider slider = await _context.Sliders.FirstOrDefaultAsync(slider => slider.Id == id);
            if (slider == null) return NotFound();
            _context.Sliders.Remove(slider);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {

            if (id == null) return NotFound();
            Slider slider = await _context.Sliders.FirstOrDefaultAsync(slider => slider.Id == id);
            if (slider == null) return NotFound();
            return View(slider);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Slider slider)
        {
            if (id == null) return NotFound();
            if (slider == null) return NotFound();
            Slider sliderView = await _context.Sliders.FirstOrDefaultAsync(slider => slider.Id == id);
            if (!ModelState.IsValid)
            {
                return View(sliderView);
            }
            Slider sliderDb = await _context.Sliders.FirstOrDefaultAsync(slider => slider.Title.ToLower().Trim() == slider.Title.ToLower().Trim());
            if (sliderDb != null && sliderDb.Id != id)
            {
                ModelState.AddModelError("Slider Name", "This slider already exist.");
                return View(sliderView);
            }
            sliderView.ImageURL = slider.ImageURL;
            sliderView.Title = slider.Title;
            sliderView.Description = slider.Description;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
