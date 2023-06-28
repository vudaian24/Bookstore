using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace Bookstore.Model.Entities
{
    public partial class BookCategory
    {
        public BookCategory()
        {
            Books = new HashSet<Book>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}
