﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Model.Entities
{
    public partial class Cart
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int CustomerId { get; set; }

    }
}
