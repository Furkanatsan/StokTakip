using StokTakip.Dal.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StokTakip.Bll.Dtos
{
    public class CategoryDto: DtoGetBase
    {
        public Category Category { get; set; }
    }
}
