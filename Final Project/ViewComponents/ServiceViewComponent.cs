using Final_Project.DAL;
using Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Project.ViewComponents
{
    public class ServiceViewComponent:ViewComponent
    {
        private readonly AppDbContext _context;
        public ServiceViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(int take)
        {
            List<Service> services = _context.Services.Take(take).ToList();
            return View(await Task.FromResult(services));
        }
    }
}
