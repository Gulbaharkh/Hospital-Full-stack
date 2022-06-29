using Final_Project.DAL;
using Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Project.ViewComponents
{
    public class DepartmentViewComponent:ViewComponent
    {
        private readonly AppDbContext _context;
        public DepartmentViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(int take)
        {
            List<Department> departments = _context.Departments.Take(take).ToList();
            return View(await Task.FromResult(departments));
        }
    }
}
