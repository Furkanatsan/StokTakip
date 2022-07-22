using Microsoft.EntityFrameworkCore;
using StokTakip.Dal.Core.Repository.Abstract;
using StokTakip.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StokTakip.Dal.Core.Repository.Concrete
{
    public class CategoryRepository:RepositoryBase<Category>,ICategoryRepository
    {
        public CategoryRepository(DbContext context):base(context)
        {

        }
        //Sınıfa özel metodlar yazmak istersek burayı kullanabiliriz.Bundan dolayı context gönderdik
    }
}
