using StokTakip.Dal.Context;
using StokTakip.Dal.Core.Repository.Abstract;
using StokTakip.Dal.Core.Repository.Concrete;
using StokTakip.Dal.Core.UnitOfWork.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StokTakip.Dal.Core.UnitOfWork.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StokTakipDbContext _context;
        private readonly CategoryRepository _categoryRepository;
        private readonly BookRepository _bookRepository;
        private readonly AuthorRepository _authorRepository;
        public UnitOfWork(StokTakipDbContext context)
        {
            _context = context;
        }

        public ICategoryRepository Category => _categoryRepository ?? new CategoryRepository(_context);

        public IBookRepository Book => _bookRepository ?? new BookRepository(_context);

        public IAuthorRepository Author => _authorRepository ?? new AuthorRepository(_context);

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();//int döner.
        }
    }
}
