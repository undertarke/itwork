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
            CreateMap<HoSo_SkillViewModel, HoSo_Skill>();
            CreateMap<KinhNghiemViewModel, KinhNghiem>();
            CreateMap<HocVanViewModel, HocVan>();
            CreateMap<ChungChiViewModel, ChungChi>();
            CreateMap<DuAnViewModel, DuAn>().ForMember(entity => entity.Skill,
                                m => m.MapFrom(modelVm => JsonConvert.SerializeObject(modelVm.Skill)));
            CreateMap<KyNangMemViewModel, KyNangMem>();
            CreateMap<NgoaiNguViewModel, NgoaiNgu>();

        }
    }
}