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
    public class AuthorProfile:Profile
    {
        public AuthorProfile()
        {
            CreateMap<AuthorAddDto, Author>();
            CreateMap<AuthorUpdateDto, Author>().ReverseMap();
            CreateMap<AuthorUpdateDto, AuthorDto>().ReverseMap();
        }
    }
}
