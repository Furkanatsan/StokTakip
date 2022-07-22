using StokTakip.Dal.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StokTakip.Mvc.Models
{
    public class BookUpdateViewModel
    {
        [Required]
        public int ID { get; set; }
        [DisplayName("Kitap Adı")]
        [Required(ErrorMessage = "Kitap Adı boş geçilemez.")]
        [MaxLength(100, ErrorMessage = "Kitap adı 100 karakterden büyük olamaz.")]
        [MinLength(1, ErrorMessage = "Kitap adı 1 karakterden az olamaz")]
        public string Name { get; set; }
        [DisplayName("Stok")]
        [Required]
        public int Stock { get; set; }
        [DisplayName("Kategori")]
        [Required(ErrorMessage = "boş geçilemez.")]
        public int CategoryId { get; set; }
        public IList<Category> Categories { get; set; }
        [DisplayName("Yazar")]
        [Required(ErrorMessage = "boş geçilemez.")]
        public int AuthorId { get; set; }
        public IList<Author> Authors { get; set; }
    }
}
