using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StokTakip.Dal.Core.Repository.Abstract
{
    public interface IRepository<T> where T : class, new()
    {
        Task<T> GetAsync(Expression<Func<T, bool>> predicate);//tekli sorgu
        Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null);  //çoklu sorgu listeye ihtiyacımız olduğu için 
        Task<T> AddAsync(T entity);//bir kategori eklemiş isek geriye bir kategori dönücek.
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);//örn:Böyle bir kullanıcı var mı kontrolu
        Task<int> CountAsync(Expression<Func<T, bool>> predicate = null);//total kaç makale ver kaç yotum var vs
    }
}
