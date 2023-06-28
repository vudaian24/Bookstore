using Bookstore.Model;
using Bookstore.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Bookstore.Controllers
{
    public class HomeController : Controller
    {
        private readonly BookstoreContext _context;
        public HomeController(BookstoreContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> GetBookList()
        {
            var result = await _context.Books
                .Join(_context.BookCategories,
                    book => book.BookCategoryId,
                    category => category.Id,
                    (book, category) => new { book, category })
                .Join(_context.Authors,
                    bookCategory => bookCategory.book.AuthorId,
                    author => author.Id,
                    (bookCategory, author) => new { bookCategory.book, bookCategory.category, author })
                .Join(_context.Publishers,
                    bookCategoryAuthor => bookCategoryAuthor.book.PublisherId,
                    publisher => publisher.Id,
                    (bookCategoryAuthor, publisher) => new
                    {
                        BookId = bookCategoryAuthor.book.Id,
                        BookName = bookCategoryAuthor.book.Name,
                        CategoryName = bookCategoryAuthor.category.Name,
                        AuthorName = bookCategoryAuthor.author.Name,
                        PublisherName = publisher.Name,
                        Price = bookCategoryAuthor.book.Price,
                        Image = bookCategoryAuthor.book.Image
                    })
                .ToListAsync();
            return Json(result);
        }

        public async Task<IActionResult> GetBestSellingBook()
        {
            var bestSellingBook = await _context.Orders
                .GroupBy(o => o.BookId)
                .Select(g => new
                {
                    BookId = g.Key,
                    TotalQuantity = g.Sum(o => o.Quantity)
                })
                .OrderByDescending(g => g.TotalQuantity)
                .FirstOrDefaultAsync();

            if (bestSellingBook != null)
            {
                var book = await _context.Books
                    .Where(b => b.Id == bestSellingBook.BookId)
                    .Select(b => new
                    {
                        BookId = b.Id,
                        BookName = b.Name,
                        Price = b.Price,
                        Image = b.Image
                    })
                    .FirstOrDefaultAsync();

                return Json(book);
            }

            return Ok();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}