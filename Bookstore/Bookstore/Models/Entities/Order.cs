using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Model.Entities
{
    public partial class Order
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int CustomerId { get; set; }
        public int Quantity { get; set; }
        public int TotalMoney { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool Status { get; set; }
        public bool StatusDelete { get; set; }
        public virtual ICollection<Pay> Pays { get; set; }
    }
}
