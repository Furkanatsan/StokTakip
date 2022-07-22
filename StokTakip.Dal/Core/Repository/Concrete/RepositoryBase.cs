using Microsoft.EntityFrameworkCore;
using StokTakip.Dal.Core.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StokTakip.Dal.Core.Repository.Concrete
{
    public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        protected readonly DbContext _context;

        public RepositoryBase(DbContext context)
        {
            _context = context;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            return entity;
        }
        
        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().AnyAsync(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            return await (predicate == null ? _context.Set<TEntity>().CountAsync() : _context.Set<TEntity>().CountAsync(predicate));
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await Task.Run(() => { _context.Set<TEntity>().Remove(entity); }); //async olmadığı için task işlemini kendimiz oluşturduk.
        }

        public async Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();//burdaki işlemleri include propertyde de kullancağımız için oluşturduk
            if (predicate != null)//filtre null değilse
            {
                query = query.Where(predicate);

            }
            return await query.ToListAsync();//bize gelen değerlere göre  query oluşturup kullanıcıya liste olarak dönüyoruz. 
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            query = query.Where(predicate);
            return await query.SingleOrDefaultAsync(); //tek nesne dönüyoruz
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            await Task.Run(() => { _context.Set<TEntity>().Update(entity); });//task içerisinde çalıştırdık.
            return entity;
        }
    }
}
