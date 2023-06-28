using Bookstore.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Controllers
{
    public class RegisterController : Controller
    {
		private readonly BookstoreContext _context;
		public RegisterController(BookstoreContext context)
		{
			_context = context;
		}
		public JsonResult CheckCustomerExists(string email)
		{
			var customer = _context.CustomerAccounts.FirstOrDefault(c => c.Email == email);
			var result = (customer != null);
			return Json(result);
		}
		[HttpPost]
		public IActionResult Add(string name, string numberPhone, DateTime birthDay, string address, string email, string password)
		{
			var customer = new Customer
			{
				Name = name,
				NumberPhone = numberPhone,
				BirthDay = birthDay,
				Address = address
			};


			_context.Customers.Add(customer);
			_context.SaveChanges();

			var user = _context.Customers.FirstOrDefault(u => u.Name == name);
			int customerId = user.Id;

			var customerAcc = new CustomerAccount
			{
				CustomerId = customerId,
				Email = email,
				Password = password
			};
			_context.CustomerAccounts.Add(customerAcc);
			_context.SaveChanges();
			return Json(new { success = "Đăng ký thành công khách hàng" });
		}
		public IActionResult Index()
        {
            return View();
        }
    }
}
