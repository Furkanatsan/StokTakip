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
    public class AuthorManager : IAuthorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AuthorManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResult> AddAsync(AuthorAddDto authorAddDto)
        {

            var author = _mapper.Map<Author>(authorAddDto);
            var addedAuthor = await _unitOfWork.Author.AddAsync(author);
            await _unitOfWork.SaveAsync();

            return new DataResult<AuthorDto>(ResultStatus.Success, $"{authorAddDto.FullName} adlı yazar başarıyla eklenmiştir.", new AuthorDto
            {
                Author = addedAuthor,
                ResultStatus = ResultStatus.Success,
                Message = $"{authorAddDto.FullName} adlı yazar başarıyla eklenmiştir."
            }); ;
        }

        public async Task<IDataResult<int>> CountAsync()
        {
            var authorCount = await _unitOfWork.Author.CountAsync();
            if (authorCount > -1)
            {
                return new DataResult<int>(ResultStatus.Success, authorCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Error, $"Beklenmeyen bir hata ile karşılaşıldı.", -1);
            }
        }

        public async Task<IResult> DeleteAsync(int authorId)
        {
            var author = await _unitOfWork.Author.GetAsync(c => c.ID == authorId);
            if (author != null)
            {
                var deletedAuthor = await _unitOfWork.Author.UpdateAsync(author);
                await _unitOfWork.SaveAsync();//delete ve save işlemi
                return new DataResult<AuthorDto>(ResultStatus.Success, $"{deletedAuthor.FullName} adlı yazar başarıyla silinmiştir.", new AuthorDto
                {
                    Author = deletedAuthor,
                    ResultStatus = ResultStatus.Success,
                    Message = $"{deletedAuthor.FullName} adlı kategori başarıyla silinmiştir."
                });
            }
            return new DataResult<AuthorDto>(ResultStatus.Error, $"Böyle bir yazar bulunamamıştır.", new AuthorDto
            {
                Author = null,
                ResultStatus = ResultStatus.Error,
                Message = $"Böyle bir yazar bulunamamıştır."
            });
        }

        public async Task<IDataResult<AuthorListDto>> GetAllAsync()
        {
            var author = await _unitOfWork.Author.GetAllAsync(null);//tüm kategorileri getiricek
            if (author.Count > -1)//0 kategori olabilir
            {
                return new DataResult<AuthorListDto>(ResultStatus.Success, new AuthorListDto
                {
                    Authors = author,
                    ResultStatus = ResultStatus.Success

                });
            }
            return new DataResult<AuthorListDto>(ResultStatus.Error, "Hiç bir yazar bulunamadı.", new AuthorListDto
            {
                Authors = null,
                ResultStatus = ResultStatus.Error,
                Message = "Hiç bir yazar bulunamadı."
            });
        }

        public async Task<IDataResult<AuthorDto>> GetAsync(int authorId)
        {
            var author = await _unitOfWork.Author.GetAsync(c => c.ID == authorId);//id ye göre kategorileri getiricek

            if (author != null)
            {
                return new DataResult<AuthorDto>(ResultStatus.Success, new AuthorDto
                {
                    Author = author,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<AuthorDto>(ResultStatus.Error, "Böyle bir yazar bulunamadı", new AuthorDto
            {
                Author = null,
                ResultStatus = ResultStatus.Error,
                Message = "Böyle bir yazar bulunamadı"
            }); ;
        }

        public async Task<IResult> HardDeleteAsync(int authorId)
        {
            var author = await _unitOfWork.Author.GetAsync(c => c.ID == authorId);
            if (author != null)
            {
                await _unitOfWork.Author.DeleteAsync(author);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, $"{author.FullName } adlı yazar başarıyla veritabanından silinmiştir.");
            }
            return new Result(ResultStatus.Error, "Böyle bir yazar bulunamadı.");
        }

        public async Task<IResult> UpdateAsync(AuthorUpdateDto authorUpdateDto)
        {
            var oldAuthor = await _unitOfWork.Author.GetAsync(c => c.ID == authorUpdateDto.ID);
            var author = _mapper.Map<AuthorUpdateDto, Author>(authorUpdateDto, oldAuthor);//Dto Da olmayan değerleri de almış olacağız.

            var updatedAuthor = await _unitOfWork.Author.UpdateAsync(author);
            await _unitOfWork.SaveAsync();//update ve save işlemi
            return new DataResult<AuthorDto>(ResultStatus.Success, $"{authorUpdateDto.FullName} adlı yazar başarıyla güncellenmiştir.", new AuthorDto
            {
                Author = updatedAuthor,
                ResultStatus = ResultStatus.Success,
                Message = $"{authorUpdateDto.FullName} adlı yazar başarıyla güncellenmiştir."
            });
        }
    }
}
