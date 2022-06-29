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
    public class ConsultationController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public ConsultationController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Consultation> consultations = _context.Consultations.ToList();
            return View(consultations);
        }
        //public IActionResult Create()
        //{
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(Counter counter)
        //{

        //    bool isExist = await _context.Counters.AnyAsync(c => c.Item.ToLower() == counter.Item.ToLower());
        //    if (isExist)
        //    {
        //        ModelState.AddModelError("Title", "This info already exists!");
        //        return View();
        //    }
        //    await _context.AddRangeAsync(counter);
        //    await _context.SaveChangesAsync();

        //    return RedirectToAction(nameof(Index));
        //}

        //public async Task<IActionResult> Detail(int? id)
        //{

        //    if (id == null) return NotFound();
        //    Counter counter = await _context.Counters.FirstOrDefaultAsync(c => c.Id == id);
        //    if (counter == null) return NotFound();
        //    return View(counter);
        //}

        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null) return NotFound();
        //    Counter counter = await _context.Counters.FirstOrDefaultAsync(c => c.Id == id);
        //    if (counter == null) return NotFound();
        //    return View(counter);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[ActionName("Delete")]
        //public async Task<IActionResult> DeletePost(int? id)
        //{
        //    if (id == null) return NotFound();
        //    Counter counter = await _context.Counters.FirstOrDefaultAsync(c => c.Id == id);
        //    if (counter == null) return NotFound();
        //    _context.Counters.Remove(counter);
        //    await _context.SaveChangesAsync();

        //    return RedirectToAction(nameof(Index));
        //}
        public async Task<IActionResult> Update(int? id)
        {

            if (id == null) return NotFound();
            Consultation consultation = await _context.Consultations.FirstOrDefaultAsync(c => c.Id == id);
            if (consultation == null) return NotFound();
            return View(consultation);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Consultation consultation)
        {
            if (id == null) return NotFound();
            if (consultation == null) return NotFound();
            Consultation consultationIntroView = await _context.Consultations.FirstOrDefaultAsync(c => c.Id == id);
            if (!ModelState.IsValid)
            {
                return View(consultationIntroView);
            }

            consultationIntroView.Date = consultation.Date;
            consultationIntroView.Doctor = consultation.Doctor;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
