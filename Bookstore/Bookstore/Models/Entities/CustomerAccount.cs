using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Model.Entities
{
    public partial class CustomerAccount
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual Customer CustomerIdNavigation { get; set; }
    }
}
