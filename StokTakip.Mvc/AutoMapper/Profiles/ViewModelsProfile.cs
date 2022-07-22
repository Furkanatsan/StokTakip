using AutoMapper;
using StokTakip.Bll.Dtos;
using StokTakip.Dal.Entities;
using StokTakip.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StokTakip.Mvc.AutoMapper.Profiles
{
    public class ViewModelsProfile:Profile
    {
        public ViewModelsProfile()
        {
            CreateMap<BookAddViewModel,BookAddDto>();
            CreateMap<BookUpdateViewModel,BookUpdateDto>();
        }
    }
}
