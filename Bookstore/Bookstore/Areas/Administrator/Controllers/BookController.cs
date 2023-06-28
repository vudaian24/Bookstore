using Bookstore.Model.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Areas.Administrator.Controllers
{
    public class BookController : Controller
    {
        private readonly BookstoreContext _context;
        public BookController(BookstoreContext context)
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

        [HttpPost]
        public IActionResult Add(string bookName, string bookCategory, string bookAuthor, string bookPublisher, string bookPrice, string bookImage)
        {
            var category = _context.BookCategories.FirstOrDefault(u => u.Name == bookCategory);
            var author = _context.Authors.FirstOrDefault(u => u.Name == bookAuthor);
            var publisher = _context.Publishers.FirstOrDefault(u => u.Name == bookPublisher);

            if (author == null)
            {
                return Json(new { error = "Không tìm thấy tác giả." });
            }
            else if (publisher == null)
            {
                return Json(new { error = "Không tìm thấy nhà xuất bản." });
            }
            else if (category == null)
            {
                return Json(new { error = "Không tìm thấy loại sách." });
            }
            else
            {
                var book = new Book
                {
                    Name = bookName,
                    BookCategoryId = category.Id,
                    AuthorId = author.Id,
                    PublisherId = publisher.Id,
                    Price = Convert.ToInt32(bookPrice),
                    Image = bookImage
                };

                _context.Books.Add(book);
                _context.SaveChanges();

                return Json(new { success = "Thêm thành công sách." });
            }
        }

        [HttpPost]
        public IActionResult Update(int bookId, string bookName, string bookCategory, string bookAuthor, string bookPublisher, string bookPrice, string bookImage)
        {
            var category = _context.BookCategories.FirstOrDefault(u => u.Name == bookCategory);
            var author = _context.Authors.FirstOrDefault(u => u.Name == bookAuthor);
            var publisher = _context.Publishers.FirstOrDefault(u => u.Name == bookPublisher);

            if (author == null)
            {
                return Json(new { error = "Không tìm thấy tác giả." });
            }
            else if (publisher == null)
            {
                return Json(new { error = "Không tìm thấy nhà xuất bản." });
            }
            else if (category == null)
            {
                return Json(new { error = "Không tìm thấy loại sách." });
            }
            else
            {
                var book = new Book
                {
                    Id = bookId,
                    Name = bookName,
                    BookCategoryId = category.Id,
                    AuthorId = author.Id,
                    PublisherId = publisher.Id,
                    Price = Convert.ToInt32(bookPrice),
                    Image = bookImage
                };

                _context.Books.Update(book);
                _context.SaveChanges();

                return Json(new { success = "Cập nhật thành công sách." });
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var book = _context.Books.FirstOrDefault(u => u.Id == id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }

        public IActionResult GetByAuthorId(int id)
        {
            var book = _context.Books.FirstOrDefault(u => u.AuthorId == id);
            if(book == null) {
                return Json(new { success = false });
            } else {
                return Json(new { success = true });
            }
        }

        public async Task<IActionResult> DeleteByAuthorId(int id)
        {
            var book = _context.Books.FirstOrDefault(u => u.AuthorId == id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }

        public IActionResult GetByPublisherId(int id)
        {
            var book = _context.Books.FirstOrDefault(u => u.PublisherId == id);
            if (book == null)
            {
                return Json(new { success = false });
            }
            else
            {
                return Json(new { success = true });
            }
        }

        public async Task<IActionResult> DeleteByPublisherId(int id)
        {
            var book = _context.Books.FirstOrDefault(u => u.PublisherId == id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }

        public IActionResult GetByCategoryId(int id)
        {
            var book = _context.Books.FirstOrDefault(u => u.PublisherId == id);
            if (book == null)
            {
                return Json(new { success = false });
            }
            else
            {
                return Json(new { success = true });
            }
        }

        public async Task<IActionResult> DeleteByCategoryId(int id)
        {
            var book = _context.Books.FirstOrDefault(u => u.BookCategoryId == id);
            _context.Books.Remove(book);
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
