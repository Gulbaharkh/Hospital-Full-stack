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
    public class DoctorController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public DoctorController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Doctor> doctors = _context.Doctors.ToList();
            return View(doctors);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Department,Photo,Facebook,Twitter,Linkedin,Instagram")] Doctor doctor)
        {

            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid) return View();
            if (!doctor.Photo.IsValidType("image/"))
            {
                ModelState.AddModelError("", "Please select image type");
                return View();
            }
            if (!doctor.Photo.IsValidSize(200))
            {
                ModelState.AddModelError("", "Image size should be less than 200kb");
                return View();
            }
            string filepath = Path.Combine("img", "doctor");

            doctor.Image = await doctor.Photo.SaveFileAsync(_env.WebRootPath, filepath);



            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int? id)
        {

            if (id == null) return NotFound();
            Doctor doctor = await _context.Doctors.FirstOrDefaultAsync(doctor => doctor.Id == id);
            if (doctor == null) return NotFound();
            return View(doctor);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            Doctor doctor = await _context.Doctors.FirstOrDefaultAsync(doctor => doctor.Id == id);
            if (doctor == null) return NotFound();
            return View(doctor);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            if (id == null) return NotFound();
            Doctor doctor = await _context.Doctors.FirstOrDefaultAsync(doctor => doctor.Id == id);
            if (doctor == null) return NotFound();
            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {

            if (id == null) return NotFound();
            Doctor doctor = await _context.Doctors.FirstOrDefaultAsync(doctor => doctor.Id == id);
            if (doctor == null) return NotFound();
            return View(doctor);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Doctor doctor)
        {
            if (id == null) return NotFound();
            if (doctor == null) return NotFound();
            Doctor doctorView = await _context.Doctors.FirstOrDefaultAsync(doctor => doctor.Id == id);
            if (!ModelState.IsValid)
            {
                return View(doctorView);
            }
            Doctor doctorDb = await _context.Doctors.FirstOrDefaultAsync(doctor => doctor.Name.ToLower().Trim() == doctor.Name.ToLower().Trim());
            if (doctorDb != null && doctorDb.Id != id)
            {
                ModelState.AddModelError("Doctor Name", "This doctor already exist.");
                return View(doctorView);
            }
            doctorView.Image = doctor.Image;
            doctorView.Name = doctor.Name;
            doctorView.Department = doctor.Department;
            doctorView.Facebook = doctor.Facebook;
            doctorView.Twitter = doctor.Twitter;
            doctorView.Linkedin = doctor.Linkedin;
            doctorView.Instagram = doctor.Instagram;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
