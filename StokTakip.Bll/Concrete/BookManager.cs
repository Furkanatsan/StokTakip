using AutoMapper;
using StokTakip.Bll.Abstract;
using StokTakip.Bll.Dtos;
using StokTakip.Bll.ResultType.Abstract;
using StokTakip.Bll.ResultType.Concrete;
using StokTakip.Bll.ResultType.Enums;
using StokTakip.Dal.Core.UnitOfWork.Abstract;
using StokTakip.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StokTakip.Bll.Concrete
{
    public class BookManager : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BookManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IResult> AddAsync(BookAddDto bookAddDto)
        {
            var book = _mapper.Map<Book>(bookAddDto);
            await _unitOfWork.Book.AddAsync(book);
            await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, $"{bookAddDto.Name} adlı kitap başarıyla eklenmiştir.");
        }

        public async Task<IDataResult<int>> CountAsync()
        {
            var booksCount = await _unitOfWork.Book.CountAsync();
            if (booksCount > -1)
            {
                return new DataResult<int>(ResultStatus.Success, booksCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Error, $"Beklenmeyen bir hata ile karşılaşıldı.", -1);
            }
        }

        

        public async Task<IDataResult<BookListDto>> GetAllAsync()
        {
            var books = await _unitOfWork.Book.GetAllAsync();
            if (books.Count > -1)
            {
                return new DataResult<BookListDto>(ResultStatus.Success, new BookListDto
                {
                    Books = books,
                    ResultStatus = ResultStatus.Success
                });

            }
            return new DataResult<BookListDto>(ResultStatus.Error, "Kitaplar bulunamadı.",null);
        }

        public async Task<IDataResult<BookListDto>> GetAllByCategoryAsync(int categoryId)
        {
            var result = await _unitOfWork.Book.AnyAsync(c => c.ID == categoryId);
            if (result)
            {
                var books = await _unitOfWork.Book.GetAllAsync(a => a.CategoryId == categoryId);
                if (books.Count > -1)
                {
                    return new DataResult<BookListDto>(ResultStatus.Success, new BookListDto
                    {
                        Books = books,
                        ResultStatus = ResultStatus.Success

                    });
                }
                return new DataResult<BookListDto>(ResultStatus.Error, "Kitaplar bulunamadı.", new BookListDto { 
                Books=null,
                ResultStatus=ResultStatus.Error,
                Message="kitap bulunamadı"
                });
            }
            return new DataResult<BookListDto>(ResultStatus.Error, "Böyle bir kategori bulunamadı.", new BookListDto
            {
                Books = null,
                ResultStatus = ResultStatus.Error,
                Message = "kitap bulunamadı"
            });
        }

        public async Task<IDataResult<BookDto>> GetAsync(int bookId)
        {
            var book = await _unitOfWork.Book.GetAsync(a => a.ID == bookId);
            if (book != null)
            {
                return new DataResult<BookDto>(ResultStatus.Success, new BookDto
                {
                    Books = book,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<BookDto>(ResultStatus.Error, "Böyle bir kitap bulunamadı.", null);
        }

        public async Task<IResult> HardDeleteAsync(int bookId)
        {
            var result = await _unitOfWork.Book.AnyAsync(a => a.ID == bookId);
            if (result)
            {
                var book = await _unitOfWork.Book.GetAsync(a => a.ID == bookId);
                await _unitOfWork.Book.DeleteAsync(book);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, $"{book.Name} adlı kitap başarıyla veritabanından silinmiştir.");
            }
            return new Result(ResultStatus.Error, "Böyle bir kitap bulunamadı.");
        }

        public async Task<IResult> UpdateAsync(BookUpdateDto bookUpdateDto)
        {
            var book = _mapper.Map<Book>(bookUpdateDto);
            await _unitOfWork.Book.UpdateAsync(book);
            await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, $"{bookUpdateDto.Name} adlı kitap başarıyla güncellenmiştir");
        }
    }
}
