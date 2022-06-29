//using Final_Project.DAL;
//using Final_Project.Extensions;
//using Final_Project.Models;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.ModelBinding;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Final_Project.Areas.Admin.Controllers
//{
//    [Area("Admin")]
//    [Authorize(Roles = "Admin")]
//    public class BlogController : Controller
//    {
//        private readonly AppDbContext _context;
//        private readonly IWebHostEnvironment _env;
//        public BlogController(AppDbContext context, IWebHostEnvironment env)
//        {
//            _context = context;
//            _env = env;
//        }
//        public IActionResult Index(int? page)
//        {
//            ViewBag.PageCount = Decimal.Ceiling((decimal)_context.Blogs.Where(c => c.IsDeleted == false).Count() / 3);
//            ViewBag.Page = page;
//            if (page == null)
//            {
//                List<Blog> blogs = _context.Blogs.Where(b => b.IsDeleted == false).OrderByDescending(e => e.Id).Take(3).ToList();
//                return View(blogs);
//            }
//            return View(_context.Blogs.Where(b => b.IsDeleted == false).OrderByDescending(e => e.Id).Skip(((int)page - 1) * 3).Take(3).ToList());

//        }

//        public IActionResult Create()
//        {
//            return View();
//        }
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create(Blog blog)
//        {
//            if (!ModelState.IsValid) return View();
//            if (blog.Photo == null)
//            {
//                ModelState.AddModelError("Photo", "Please take Photo");
//                return View();
//            }
//            if (!blog.Photo.IsImage())
//            {
//                ModelState.AddModelError("Photo", "Please take image file");
//                return View();
//            }
//            if (blog.Photo.MaxSize(400))
//            {
//                ModelState.AddModelError("Photo", "Please max 400kb");
//                return View();
//            }
//            string folder = Path.Combine("img", "blog");
//            Blog newBlog = new Blog
//            {
//                Image = await blog.Photo.SaveImageAsync(_env.WebRootPath, folder),
//                Title = blog.Title,
//                Text = blog.Text,
//                Date = blog.Date,

//            };


//            await _context.Blogs.AddAsync(newBlog);

//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }
//        public async Task<IActionResult> Update(int? id)
//        {
//            if (id == null) return NotFound();
//            Blog blog = await _context.Blogs.FirstOrDefaultAsync(b => b.Id == id);
//            if (blog == null) return NotFound();
//            return View(blog);
//        }
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Update(int id, Blog blog)
//        {

//            Blog oldBlog = await _context.Blogs.FirstOrDefaultAsync(c => c.Id == id);
//            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid) return View();
        
//            string folder = Path.Combine("img", "blog");

//            oldBlog.Image = await blog.Photo.SaveImageAsync(_env.WebRootPath, folder);

//            oldBlog.Id = id;
//            oldBlog.Title = blog.Title;
//            oldBlog.Text = blog.Text;
//            oldBlog.Date = blog.Date;
//            _context.Update(oldBlog);

//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null) return NotFound();
//            Blog blog = await _context.Blogs.FirstOrDefaultAsync(b => b.Id == id);
//            if (blog == null) return NotFound();
//            return View(blog);
//        }
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null) return NotFound();
//            Blog blog = await _context.Blogs.FirstOrDefaultAsync(b => b.Id == id);
//            if (blog.IsDeleted == false)
//            {
//                blog.IsDeleted = true;
//            }
//            else
//            {
//                blog.IsDeleted = false;
//            }
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }
//    }
//}
