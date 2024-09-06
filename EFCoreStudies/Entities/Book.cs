using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Book
    {
        public Book()
        {
            Authors = new HashSet<Author>();
        }
        public int ID { get; set; }
        public string BookName { get; set; }
        public ICollection<Author> Authors { get; set; }
    }
}
