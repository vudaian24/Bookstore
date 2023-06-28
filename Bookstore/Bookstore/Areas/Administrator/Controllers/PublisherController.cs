using Bookstore.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Areas.Administrator.Controllers
{
    public class PublisherController : Controller
    {
        private readonly BookstoreContext _context;
        public PublisherController(BookstoreContext context)
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
            var publishers = _context.Publishers.ToList();
            return Json(publishers);
        }

        [HttpPost]
        public IActionResult Add(string name, string description)
        {
            var publisher = new Publisher
            {
                Name = name,
                Description = description
            };
            _context.Publishers.Add(publisher);
            _context.SaveChanges();
            return Json(new { success = "Thêm thành công nhà xuất bản" });
        }
        [HttpPost]
        public IActionResult Update(int id, string name, string description)
        {
            var publisher = new Publisher
            {
                Id = id,
                Name = name,
                Description = description
            };
            _context.Publishers.Update(publisher);
            _context.SaveChanges();
            return Json(new { success = "Cập nhật thành công nhà xuất bản" });
        }
        public JsonResult GetById(int id)
        {
            var publisher = _context.Publishers.FirstOrDefault(u => u.Id == id);
            if (publisher == null)
            {
                return Json(new { error = "Không có nhà xuất bản nào!" });
            }
            return Json(publisher);
        }

        public JsonResult Delete(int id)
        {
            var publisher = _context.Publishers.FirstOrDefault(a => a.Id == id);
            if (publisher == null)
            {
                return Json(new { error = "Không có nhà xuất bản nào!" });
            }
            _context.Publishers.Remove(publisher);
            _context.SaveChanges();
            return Json(new { success = "Xóa thành công nhà xuất bản" });
        }
    }
}
