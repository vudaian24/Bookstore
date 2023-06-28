using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Model.Entities
{
    public partial class AdminAccount
    {
        public int AdminId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual Admin AdminIdNavigation { get; set; }
    }
}
