using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace StokTakip.Dal.Entities
{
    public class Role:IdentityRole<int>//int primary key ile oluşacak
    {
    }
}
