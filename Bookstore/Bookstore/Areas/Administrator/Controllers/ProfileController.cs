using Bookstore.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Areas.Administrator.Controllers
{
    public class ProfileController : Controller
    {
        private readonly BookstoreContext _context;
        public ProfileController(BookstoreContext context)
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
        public async Task<IActionResult> GetAdmin()
        {
            var id = 2;
            var result = await _context.Admins
                .Where(admin => admin.Id == id)
                .Join(_context.AdminAccounts,
                    admin => admin.Id,
                    adminacc => adminacc.AdminId,
                    (admin, adminacc) => new {
                        id = admin.Id,
                        name = admin.Name,
                        numberPhone = admin.NumberPhone,
                        email = adminacc.Email,
                        address = admin.Address
                    }).FirstOrDefaultAsync();
            return Json(result);
        }

        [HttpPost]
        public IActionResult Update(int id, string name, string numberPhone, string address)
        {
            var admin = new Admin
            {
                Id = id,
                Name = name,
                NumberPhone = numberPhone,
                Address = address
            };
            _context.Admins.Update(admin);
            _context.SaveChanges();
            return Json(new { success = "Cập nhật thành công quản lý" });
        }
    }
}
