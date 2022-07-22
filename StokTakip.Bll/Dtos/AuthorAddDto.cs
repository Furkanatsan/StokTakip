using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StokTakip.Bll.Dtos
{
    public class AuthorAddDto
    {
        [DisplayName("Yazar Adı")]
        [Required(ErrorMessage = "Yazar Adı boi geçilemez.")]
        [MaxLength(100, ErrorMessage = "Yazar adı 100 karakterden büyük olamaz.")]
        [MinLength(5, ErrorMessage = "Yazar adı 5 karakterden az olamaz")]
        public string FullName { get; set; }
    }
}
