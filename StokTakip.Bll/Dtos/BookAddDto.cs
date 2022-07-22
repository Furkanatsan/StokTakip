﻿using StokTakip.Dal.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StokTakip.Bll.Dtos
{
    public class BookAddDto
    {
        [DisplayName("Kitap Adı")]
        [Required(ErrorMessage = "Kitap Adı boş geçilemez.")]
        [MaxLength(100, ErrorMessage = "Kitap adı 100 karakterden büyük olamaz.")]
        [MinLength(1, ErrorMessage = "Kitap adı 1 karakterden az olamaz")]
        public string Name { get; set; }
        [DisplayName("Stok")]
        [Required]
        public int Stock { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
