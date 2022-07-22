using StokTakip.Dal.Core.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StokTakip.Dal.Core.UnitOfWork.Abstract
{
    public interface IUnitOfWork:IAsyncDisposable//çöp yönetimi
    {
        ICategoryRepository Category { get; }
        IBookRepository Book { get; }
        IAuthorRepository Author { get; }
        Task<int> SaveAsync();//etkilenen kayıt sayısını almak istersek.
        //birden fazla kayıt attığımızda tek savechange ile kayıt ederiz.
    }
}
