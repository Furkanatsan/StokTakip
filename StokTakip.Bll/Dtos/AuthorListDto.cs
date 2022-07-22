using StokTakip.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StokTakip.Bll.Dtos
{
    public class AuthorListDto : DtoGetBase
    {
        public IList<Author> Authors { get; set; }
    }
}
