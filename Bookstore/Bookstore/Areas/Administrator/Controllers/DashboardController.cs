using Bookstore.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Areas.Administrator.Controllers
{
    public class DashboardController : Controller
    {
        private readonly BookstoreContext _context;
        public DashboardController(BookstoreContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var session = HttpContext.Session;
            if (session.GetInt32("AdminId") == null)
            {
                return Redirect("/Administrator/SignIn/Index");
            }
            else
            {
                return View();
            }
        }
        public JsonResult GetNameAdmin()
        {
            var session = HttpContext.Session;
            if (session.GetInt32("AdminId") != null)
            {
                int Id = HttpContext.Session.GetInt32("AdminId") ?? 0;
                var admin = _context.Admins.FirstOrDefault(a => a.Id == Id);
                return Json(admin);
            }
            return Json(null);
        }
    }
}
