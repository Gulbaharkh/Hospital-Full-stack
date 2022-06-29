using Final_Project.DAL;
using Final_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Project.Controllers
{
    public class DoctorController : Controller
    {
        private readonly AppDbContext _context;

        public DoctorController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            DoctorViewModel doctorViewModel = new DoctorViewModel
            {
                Abouts = _context.Abouts.ToList(),
                Doctors = _context.Doctors.ToList(),
            };
            return View(doctorViewModel);
        }
    }
}
