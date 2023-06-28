using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Model.Entities
{
    public partial class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BookCategoryId { get; set; }
        public int AuthorId { get; set; }
        public int PublisherId { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
    }
}
