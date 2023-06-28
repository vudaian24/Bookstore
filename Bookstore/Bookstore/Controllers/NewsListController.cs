using Bookstore.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Areas.User.Controllers
{
    public class NewsListController : Controller
    {
        private readonly BookstoreContext _context;
        public NewsListController(BookstoreContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult List()
        {
            var news = _context.News.ToList();
            return Json(news);
        }
    }
}
