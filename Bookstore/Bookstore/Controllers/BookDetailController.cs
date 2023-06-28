using Bookstore.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Areas.User.Controllers
{
    public class BookDetailController : Controller
    {
        private readonly BookstoreContext _context;
        public BookDetailController(BookstoreContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> GetBookId(int id)
        {
            var result = await _context.Books
                .Where(book => book.Id == id)
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
                .FirstOrDefaultAsync();
            return Json(result);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
