using Final_Project.DAL;
using Final_Project.Models;
using Final_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel
            {
                Sliders =_context.Sliders.ToList(),
                SliderIntros = _context.SliderIntros.ToList(),
                Abouts = _context.Abouts.ToList(),
                AboutImgs=_context.AboutImgs.FirstOrDefault(),
                Services = _context.Services.ToList(),
                Preferences = _context.Preferences.ToList(),
                PreferenceImgs = _context.PreferenceImgs.FirstOrDefault(),
                Doctors = _context.Doctors.ToList(),
                Counters = _context.Counters.ToList(),
                Blogs = _context.Blogs.ToList(),
                Consultations = _context.Consultations.FirstOrDefault()
            };
            return View(homeViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
