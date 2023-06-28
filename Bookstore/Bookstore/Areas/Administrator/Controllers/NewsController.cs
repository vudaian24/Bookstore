using Bookstore.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Areas.Administrator.Controllers
{
    public class NewsController : Controller
    {
		private readonly BookstoreContext _context;
		public NewsController(BookstoreContext context)
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
        public JsonResult List()
        {
			var news = _context.News.ToList();
			return Json(news);
		}

		[HttpPost]
		public IActionResult Add(string title, string content, string Image)
		{
			var news = new News
			{
				Title = title,
				Content = content,
				Image = Image
			};
			_context.News.Add(news);
			_context.SaveChanges();
			return Json(new { success = "Thêm thành công tin tức" });
        }
		[HttpPost]
		public IActionResult Update(int id, string title, string content, string Image)
		{
			var news = new News
			{
				Id = id,
				Title = title,
				Content = content,
				Image = Image
			};
			_context.News.Update(news);
			_context.SaveChanges();
			return Json(new { success = "Cập nhật thành công tin tức" });
		}
		public JsonResult GetById(int id)
		{
			var news = _context.News.FirstOrDefault(u => u.Id == id);
			if (news == null)
			{
				return Json(new { error = "Không có tin tức nào!" });
			}
			return Json(news);
		}

		public JsonResult Delete(int id)
		{
			var news = _context.News.FirstOrDefault(u => u.Id == id);
			if (news == null)
			{
				return Json(new { error = "Không có tin tức nào!" });
			}
			_context.News.Remove(news);
			_context.SaveChanges();
			return Json(new { success = "Xóa thành công tin tức" });
		}
	}
}
