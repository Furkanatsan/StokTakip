using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StokTakip.Bll.Dtos
{
    public class UserLoginDto
    {
        [DisplayName("E-Posta Adresi")]
        [Required(ErrorMessage = "{0} Boş Geçilmemelidir.")]//display name de dne yazıyorsa {0}buraya eklenicek.
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DisplayName("Şifre")]
        [Required(ErrorMessage = "{0} Boş Geçilmemelidir.")]//display name de dne yazıyorsa {0}buraya eklenicek.
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DisplayName("Beni Hatırla")]
        public bool RememberMe { get; set; }//identityde verilen süre boyunca yeniden şifre girmesi gerekmeyecek.7 gün
    }
}
