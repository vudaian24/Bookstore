using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Model.Entities
{
    public partial class Publisher
    {
        public Publisher()
        {
            Books = new HashSet<Book>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}
