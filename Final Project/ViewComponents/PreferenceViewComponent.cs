using Final_Project.DAL;
using Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Project.ViewComponents
{
    public class PreferenceViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;
        public PreferenceViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(int take)
        {
            List<Preference> preferences = _context.Preferences.Take(take).ToList();
            return View(await Task.FromResult(preferences));
        }
    }
}
