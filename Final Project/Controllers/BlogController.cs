using Final_Project.DAL;
using Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Project.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;

        public BlogController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        //public async Task<IActionResult> Index(int pageNumber = 1)
        //{
        //    return View(await PaginatedList<Blog>.CreateAsync(_context.Blogs, pageNumber, 5));
        //}
    }
}
