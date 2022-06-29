using Final_Project.DAL;
using Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Project.ViewComponents
{
    public class SliderIntroViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;
        public SliderIntroViewComponent (AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            SliderIntro sliderIntro = _context.SliderIntros.FirstOrDefault();
            return View(await Task.FromResult(sliderIntro));
        }
    }
}
