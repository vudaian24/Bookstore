using Bookstore.Model.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Areas.Administrator.Controllers
{
    public class BookCategoryController : Controller
    {
        private readonly BookstoreContext _context;
        public BookCategoryController(BookstoreContext context)
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
        public IActionResult List()
        {
            var bookCategories = _context.BookCategories.ToList();
            return Json(bookCategories);
        }

        [HttpPost]
        public IActionResult Add(string name, string description, bool status)
        {
            var bookCategory = new BookCategory
            {
                Name = name,
                Description = description,
                Status = status
            };
            _context.BookCategories.Add(bookCategory);
            _context.SaveChanges();
            return Json(new { success = "Thêm thành công loại sách" });
        }
        [HttpPost]
        public IActionResult Update(int id, string name, string description, bool status)
        {
            var bookCategory = new BookCategory
            {
                Id = id,
                Name = name,
                Description = description,
                Status = status
            };
            _context.BookCategories.Update(bookCategory);
            _context.SaveChanges();
            return Json(new { success = "Cập nhật thành công loại sách" });
        }
        public JsonResult GetById(int id)
        {
            var bookCategory = _context.BookCategories.FirstOrDefault(u => u.Id == id);
            if (bookCategory == null)
            {
                return Json(new { error = "Không có loại sách nào!" });
            }
            return Json(bookCategory);
        }

        public JsonResult Delete(int id)
        {
            var bookCategory = _context.BookCategories.FirstOrDefault(a => a.Id == id);
            if (bookCategory == null)
            {
                return Json(new { error = "Không có loại sách nào!" });
            }
            _context.BookCategories.Remove(bookCategory);
            _context.SaveChanges();
            return Json(new { success = "Xóa thành công loại sách" });
        }
    }
}
