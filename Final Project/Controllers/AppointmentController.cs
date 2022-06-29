using Final_Project.DAL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Project.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly AppDbContext _context;
        public AppointmentController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetDate()
        {
            var date = _context.Consultations.Find(1);
            return Json(date.Date);
        }
    }


}
