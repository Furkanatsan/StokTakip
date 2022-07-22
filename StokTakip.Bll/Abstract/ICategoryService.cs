using StokTakip.Bll.Dtos;
using StokTakip.Bll.ResultType.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StokTakip.Bll.Abstract
{
    public interface ICategoryService
    {
        Task<IDataResult<CategoryDto>> GetAsync(int categoryId);
        Task<IDataResult<CategoryListDto>> GetAllAsync();
        Task<IDataResult<CategoryDto>> AddAsync(CategoryAddDto categoryAddDto);//veri eklediğimizde veya güncellediğimizde geriye kateggoriDto dönmüş olacagız.bu sayede eklediğimiz yada güncellediğimiz verinin son hali elimizde olucak ve bunuda tablomuza ve toastera eklemek için kullanıyor olacagız.
        Task<IDataResult<CategoryDto>> UpdateAsync(CategoryUpdateDto categoryUpdateDto);
        Task<IDataResult<CategoryDto>> DeleteAsync(int categoryId);//isDeleted i true yapar
        Task<IResult> HardDeleteAsync(int categoryId);//gerçekten siler.
        Task<IDataResult<int>> CountAsync();
    }
}
