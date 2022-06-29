using Final_Project.DAL;
using Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Project.ViewComponents
{
    public class DoctorViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;
        public DoctorViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(int take)
        {
            List<Doctor> doctors = _context.Doctors.Take(take).ToList();
            return View(await Task.FromResult(doctors));
        }
    }
}
