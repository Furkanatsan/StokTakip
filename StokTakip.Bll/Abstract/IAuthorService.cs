using StokTakip.Bll.Dtos;
using StokTakip.Bll.ResultType.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StokTakip.Bll.Abstract
{
    public interface IAuthorService
    {
        Task<IDataResult<AuthorDto>> GetAsync(int authorId);
        Task<IDataResult<AuthorListDto>> GetAllAsync();//tümünü getir
        Task<IResult> AddAsync(AuthorAddDto authorAddDto);
        Task<IResult> UpdateAsync(AuthorUpdateDto authorUpdateDto);
        Task<IResult> DeleteAsync(int authorId);//isDeleted i true yapar
        Task<IResult> HardDeleteAsync(int authorId);//gerçekten siler.
        Task<IDataResult<int>> CountAsync();
    }
}
