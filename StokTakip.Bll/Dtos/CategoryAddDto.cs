using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StokTakip.Bll.Dtos
{
    public class CategoryAddDto
    {
        [DisplayName("Kategori Adı")]
        [Required(ErrorMessage = "Kategori Adı boş geçilemez.")]
        [MaxLength(60, ErrorMessage = "Kategori adı 60 karakterden büyük olamaz.")]
        [MinLength(2, ErrorMessage = "Kategori adı 2 karakterden az olamaz")]
        public string Name { get; set; }
    }
}
