using Bookstore.Model.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Bookstore.Areas.Administrator.Controllers
{
    public class AuthorController : Controller
    {
        private readonly BookstoreContext _context;
        public AuthorController(BookstoreContext context)
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
            var authors = _context.Authors.ToList();
            return Json(authors);
        }

        [HttpPost]
        public IActionResult Add(string name, DateTime birthDay, string description)
        {
            var author = new Author
            {
                Name = name,
                BirthDay = birthDay,
                Description = description
            };
            _context.Authors.Add(author);
            _context.SaveChanges();
            return Json(new { success = "Thêm thành công tác giả" });
        }
        [HttpPost]
        public IActionResult Update(int id, string name, DateTime birthDay, string description)
        {
            var author = new Author
            {
                Id = id,
                Name = name,
                BirthDay = birthDay,
                Description = description
            };
            _context.Authors.Update(author);
            _context.SaveChanges();
            return Json(new { success = "Cập nhật thành công tác giả" });
        }
        public JsonResult GetById(int id)
        {
            var author = _context.Authors.FirstOrDefault(u => u.Id == id);
            if (author == null)
            {
                return Json(new { error = "Không có tác giả nào!" });
            }
            return Json(author);
        }

        public JsonResult Delete(int id)
        {
            var author = _context.Authors.FirstOrDefault(a => a.Id == id);
            if (author == null)
            {
                return Json(new { error = "Không có tác giả nào!" });
            }
            _context.Authors.Remove(author);
            _context.SaveChanges();
            return Json(new { success = "Xóa thành công tác giả" });
        }
    }
}
