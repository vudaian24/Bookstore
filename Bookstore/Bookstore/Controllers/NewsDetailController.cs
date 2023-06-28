using Bookstore.Model.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Areas.User.Controllers
{
    public class NewsDetailController : Controller
    {
        private readonly BookstoreContext _context;
        public NewsDetailController(BookstoreContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            //if (HttpContext.Session.GetInt32("UserId") == null)
            //{
            //    return Redirect("/User/SignInSignUp/Index");
            //}
            return View();
        }

        public JsonResult GetNewsId(int id)
        {
            var news = _context.News.FirstOrDefault(u => u.Id == id);
            return Json(news);
        }
    }
}
