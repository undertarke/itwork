using AutoMapper;
using Newtonsoft.Json;
using SoloDevApp.Repository.Models;
using SoloDevApp.Service.Utilities;
using SoloDevApp.Service.ViewModels;
using System.Collections.Generic;

namespace SoloDevApp.Service.AutoMapper
{
    public class EntityToViewModelProfile : Profile
    {
        public EntityToViewModelProfile()
        {
           
            CreateMap<NewMau, NewMauViewModel>();
            CreateMap<NguoiDung, NguoiDungViewModel>();
            CreateMap<Skill, SkillViewModel>();
            CreateMap<NguoiDung_Skill, NguoiDung_SkillViewModel>();
            CreateMap<CauHinh, CauHinhViewModel>();
        }
    }
}