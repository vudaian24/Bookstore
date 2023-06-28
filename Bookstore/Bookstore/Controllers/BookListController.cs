using Bookstore.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Areas.User.Controllers
{
    public class BookListController : Controller
    {
        private readonly BookstoreContext _context;
        public BookListController(BookstoreContext context)
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
        public async Task<IActionResult> GetById(int id)
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
