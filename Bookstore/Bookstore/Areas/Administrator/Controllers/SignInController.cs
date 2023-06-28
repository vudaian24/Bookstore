using Bookstore.Model.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Areas.Administrator.Controllers
{
    public class SignInController : Controller
    {
        private readonly BookstoreContext _context;
        public SignInController(BookstoreContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("AdminId") != null)
            {
                return Redirect("/Administrator/Dashboard/Index");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Index(string email, string password)
        {
            var admin = _context.AdminAccounts.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (admin != null)
            {
                // Lưu thông tin đăng nhập vào Session
                HttpContext.Session.SetInt32("AdminId", admin.AdminId);
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }

        }

        [HttpPost]
        public IActionResult Logout()
        {
            var session = HttpContext.Session;
            session.Remove("AdminId");

            // Trả về một response cho client
            return Json(new { success = true });
        }
    }
}
