using Bookstore.Model.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Controllers
{
	public class LoginController : Controller
	{
		private readonly BookstoreContext _context;
		public LoginController(BookstoreContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
            if (HttpContext.Session.GetInt32("UserId") != null)
            {
                string returnUrl = HttpContext.Session.GetString("ReturnUrl");
                return Json(returnUrl);
            }
            return View();
        }

		[HttpPost]
		public IActionResult Index(string email, string password)
		{
			var user = _context.CustomerAccounts.FirstOrDefault(u => u.Email == email && u.Password == password);
			if (user != null)
			{
				// Lưu thông tin đăng nhập vào Session
				HttpContext.Session.SetInt32("UserId", user.CustomerId);
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
			session.Remove("UserId");

			return Json(new { success = true });
		}

		public JsonResult CheckCustomerExists(string email)
		{
			var customer = _context.CustomerAccounts.FirstOrDefault(c => c.Email == email);
			var result = (customer != null);
			return Json(result);
		}

		[HttpPost]
		public IActionResult SaveReturnUrlToSession(string url)
		{
			HttpContext.Session.SetString("ReturnUrl", url);
			return Ok();
		}
	}
}
