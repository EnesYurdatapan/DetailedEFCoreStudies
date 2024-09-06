﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Author
    {
        public Author()
        {
            Books=new HashSet<Book>();
        }
        public int Id { get; set; }
        public string AuthorName { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
