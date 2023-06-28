using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Model.Entities
{
    public partial class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
    }
}
