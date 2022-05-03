using AutoMapper;
using Newtonsoft.Json;
using SoloDevApp.Repository.Models;
using SoloDevApp.Service.ViewModels;
using SoloDevApp.Service.Utilities;

namespace SoloDevApp.Service.AutoMapper
{
    public class ViewModelToEntityProfile : Profile
    {
        public ViewModelToEntityProfile()
        {
           
            CreateMap<NewMauViewModel, NewMau>();
            CreateMap<NguoiDungViewModel, NguoiDung>();
            CreateMap<SkillViewModel, Skill>();
            CreateMap<NguoiDung_SkillViewModel, NguoiDung_Skill>();
            CreateMap<CauHinhViewModel, CauHinh>();
        }
    }
}