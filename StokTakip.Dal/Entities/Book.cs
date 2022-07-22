using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StokTakip.Dal.Entities
{
    public class Book
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public int Stock { get; set; }
        public DateTime? UpdatedDate { get; set; } = DateTime.Now;
    }
}
