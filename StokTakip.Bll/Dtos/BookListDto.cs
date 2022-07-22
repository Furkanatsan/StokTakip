using StokTakip.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StokTakip.Bll.Dtos
{
    public class BookListDto : DtoGetBase
    {
        public IList<Book> Books { get; set; }
    }
}
