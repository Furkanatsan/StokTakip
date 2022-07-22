using AutoMapper;
using StokTakip.Bll.Dtos;
using StokTakip.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StokTakip.Bll.AutoMapper.Profiles
{
    public class CategoryProfile:Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryAddDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();
            CreateMap<CategoryUpdateDto, CategoryDto>().ReverseMap();
        }
     

    }
}
