using Bookstore.Model.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Bookstore.Areas.Administrator.Controllers
{
    public class CustomerController : Controller
    {
        private readonly BookstoreContext _context;
        public CustomerController(BookstoreContext context)
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
            var customers = _context.Customers.ToList();
            return Json(customers);
        }

        [HttpPost]
        public IActionResult Add(string name, string numberPhone, DateTime birthDay, string address)
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
            return Json(new { success = "Thêm thành công khách hàng" });
        }
        [HttpPost]
        public IActionResult Update(int id, string name, string numberPhone, DateTime birthDay, string address)
        {
            var customer = new Customer
            {
                Id = id,
                Name = name,
                NumberPhone = numberPhone,
                BirthDay = birthDay,
                Address = address
            };
            _context.Customers.Update(customer);
            _context.SaveChanges();
            return Json(new { success = "Cập nhật thành công khách hàng" });
        }
        public JsonResult GetById(int id)
        {
            var customer = _context.Customers.FirstOrDefault(u => u.Id == id);
            if (customer == null)
            {
                return Json(new { error = "Không có khách hàng nào!" });
            }
            return Json(customer);
        }

        public JsonResult Delete(int id)
        {
            var customersAcc = _context.CustomerAccounts.FirstOrDefault(a => a.CustomerId == id);
            if (customersAcc != null)
            {
                _context.CustomerAccounts.Remove(customersAcc);
                _context.SaveChanges();
            }
            var customers = _context.Customers.FirstOrDefault(a => a.Id == id);
            if (customers != null)
            {
                _context.Customers.Remove(customers);
                _context.SaveChanges();
            }

            return Json(new { success = "Xóa thành công khách hàng" });
        }

		public JsonResult GetUserInfo()
		{
			var session = HttpContext.Session;
			if (session.GetInt32("UserId") != null)
			{
				int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
				var customers = _context.Customers.FirstOrDefault(a => a.Id == userId);
				return Json(customers);
			}
			return Json(null);
		}
	}
}
