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
    public class CounterController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public CounterController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Counter> counter = _context.Counters.ToList();
            return View(counter);
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
            Counter counter = await _context.Counters.FirstOrDefaultAsync(c => c.Id == id);
            if (counter == null) return NotFound();
            return View(counter);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Counter counter)
        {
            if (id == null) return NotFound();
            if (counter == null) return NotFound();
            Counter counterIntroView = await _context.Counters.FirstOrDefaultAsync(c => c.Id == id);
            if (!ModelState.IsValid)
            {
                return View(counterIntroView);
            }
            Counter counterIntroDb = await _context.Counters.FirstOrDefaultAsync(c => c.Item.ToLower().Trim() == counter.Item.ToLower().Trim());
            if (counterIntroDb != null && counterIntroDb.Id != id)
            {
                ModelState.AddModelError("Title", " Already exist.");
                return View(counterIntroView);
            }
            counterIntroView.Item = counter.Item;
            counterIntroView.Count = counter.Count;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
