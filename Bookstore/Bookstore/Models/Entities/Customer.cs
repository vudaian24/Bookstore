using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Model.Entities
{
    public partial class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NumberPhone { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Address { get; set; }

    }
}
