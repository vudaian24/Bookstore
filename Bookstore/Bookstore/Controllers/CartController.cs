using Bookstore.Model.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Areas.User.Controllers
{
    public class CartController : Controller
    {
        private readonly BookstoreContext _context; 
        public CartController(BookstoreContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> List()
        {
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
            var result = await _context.Customers
                .Where(customer => customer.Id == userId)
                .Join(_context.Carts,
                    customer => customer.Id,
                    cart => cart.CustomerId,
                    (customer, cart) => new { customer, cart })
                .Join(_context.Books,
                    customercart => customercart.cart.BookId,
                    book => book.Id,
                    (customercart, book) => new {
                        BookId = customercart.cart.Id,
                        BookName = book.Name,
                        Price = book.Price
                    })
                .ToListAsync();
            return Json(result);
        }

        public async Task<IActionResult> GetById(int id)
        {
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0;

            var result = await _context.Carts
                .Where(cart => cart.Id == id)
                .Join(_context.Customers,
                    cart => cart.CustomerId,
                    customer => customer.Id,
                    (cart, customer) => new {cart, customer})
                .Join(_context.Books,
                    customercart => customercart.cart.BookId,
                    book => book.Id,
                    (customercart, book) => new {
                        CartId = customercart.cart.Id,
                        CustomerName = customercart.customer.Name,
                        CustomerNumberPhone = customercart.customer.NumberPhone,
                        CustomerAddress = customercart.customer.Address,
                        BookName = book.Name,
                        BookPrice = book.Price
                    })
                .FirstOrDefaultAsync();
            return Json(result);
        }

        [HttpPost]
        public IActionResult AddToCart(int bookId)
        {
            var session = HttpContext.Session;
            if(session.GetInt32("UserId") == null) 
            {
                return Json(new { error = "/Login/Index" });
            } else
            {
                int customerId = HttpContext.Session.GetInt32("UserId") ?? 0;
                var cart = new Cart
                {
                    BookId = bookId,
                    CustomerId = customerId
                };
                _context.Carts.Add(cart);
                _context.SaveChanges();
                return Json(new { success = "Thêm thành công vào giỏ hàng" });
            }
        }

        [HttpPost]
        public IActionResult Order(int id, int quantity, DateTime createdDate)
        {
            var cart = _context.Carts.FirstOrDefault(c => c.Id == id);
            var book = _context.Books.FirstOrDefault(u => u.Id == cart.BookId);
            var customer = _context.Customers.FirstOrDefault(u => u.Id == cart.CustomerId);
            int totalMoney = book.Price * quantity;

            var order = new Order
            {
                BookId = book.Id,
                CustomerId = customer.Id,
                Quantity = quantity,
                TotalMoney = totalMoney,
                CreatedDate = createdDate,
                Status = false,
                StatusDelete = false
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            return Json(new { success = "Đặt thành công" });
        }

        public JsonResult Delete(int id)
        {
            var cart = _context.Carts.FirstOrDefault(a => a.Id == id);
            _context.Carts.Remove(cart);
            _context.SaveChanges();
            return Json(new { success = "Xóa thành công" });
        }

        public IActionResult Index()
        {
            var session = HttpContext.Session;
            if (session.GetInt32("UserId") == null)
            {
                return Redirect("/Login/Index");
            }
            else
            {
                return View();
            }
        }
    }
}
