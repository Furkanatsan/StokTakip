using StokTakip.Bll.Dtos;
using StokTakip.Bll.ResultType.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StokTakip.Bll.Abstract
{
    public interface IBookService
    {
        Task<IDataResult<BookDto>> GetAsync(int bookId);
        Task<IDataResult<BookListDto>> GetAllAsync();//tümünü getir
        Task<IDataResult<BookListDto>> GetAllByCategoryAsync(int categoryId);//kategoriye göre kitapları getir.
        Task<IResult> AddAsync(BookAddDto bookAddDto);
        Task<IResult> UpdateAsync(BookUpdateDto bookUpdateDto);
        Task<IResult> HardDeleteAsync(int bookId);//gerçekten siler.
        Task<IDataResult<int>> CountAsync();
    }
}
