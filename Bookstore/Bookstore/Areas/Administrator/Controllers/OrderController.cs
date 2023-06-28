using Bookstore.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Areas.Administrator.Controllers
{
    public class OrderController : Controller
    {
        private readonly BookstoreContext _context;
        public OrderController(BookstoreContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> GetOrderList()
        {
            var result = await _context.Orders
                .Join(_context.Books,
                    order => order.BookId,
                    book => book.Id,
                    (order, book) => new { order, book })
                .Join(_context.Customers,
                    orderBook => orderBook.order.CustomerId,
                    customer => customer.Id,
                    (orderBook, customer) => new
                    {
                        OrderId = orderBook.order.Id,
                        BookName = orderBook.book.Name,
                        CustomerName = customer.Name,
                        CustomerNumberPhone = customer.NumberPhone,
                        Quanity = orderBook.order.Quantity,
                        TotalMoney = orderBook.order.TotalMoney,
                        CreatedDate = orderBook.order.CreatedDate,
                        Status = orderBook.order.Status,
                    })
                .ToListAsync();
            return Json(result);
        }
        public async Task<IActionResult> GetById()
        {
            var result = await _context.Orders
                .Join(_context.Books,
                    order => order.BookId,
                    book => book.Id,
                    (order, book) => new { order, book })
                .Join(_context.Customers,
                    orderBook => orderBook.order.CustomerId,
                    customer => customer.Id,
                    (orderBook, customer) => new
                    {
                        OrderId = orderBook.order.Id,
                        BookName = orderBook.book.Name,
                        CustomerName = customer.Name,
                        CustomerNumberPhone = customer.NumberPhone,
                        Quanity = orderBook.order.Quantity,
                        TotalMoney = orderBook.order.TotalMoney,
                        CreatedDate = orderBook.order.CreatedDate,
                        Status = orderBook.order.Status,
                    })
                .FirstOrDefaultAsync();
            return Json(result);
        }


        [HttpPost]
        public IActionResult Update(int id, string bookName, string customerName, int quantity, DateTime createdDate, bool status)
        {
            var book = _context.Books.FirstOrDefault(u => u.Name == bookName);
            var customer = _context.Customers.FirstOrDefault(u => u.Name == customerName);
            int totalMoney = book.Price * quantity;

            if (book == null)
            {
                return Json(new { error = "Không tìm thấy sách." });
            }
            else if (customer == null)
            {
                return Json(new { error = "Không tìm thấy khách hàng." });
            }
            else
            {
                var order = new Order
                {
                    Id = id,
                    BookId = book.Id,
                    CustomerId = customer.Id,
                    Quantity = quantity,
                    TotalMoney = totalMoney,
                    CreatedDate = createdDate,
                    Status = status,
                    StatusDelete = false
                };

                _context.Orders.Update(order);
                _context.SaveChanges();

                return Json(new { success = "Cập nhật thành công đơn hàng." });
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var order = _context.Orders.FirstOrDefault(u => u.Id == id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return Json(new { success = true });
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
    }
}
