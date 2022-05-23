using AutoMapper;
using Newtonsoft.Json;
using SoloDevApp.Repository.Models;
using SoloDevApp.Repository.Repositories;
using SoloDevApp.Service.Constants;
using SoloDevApp.Service.Infrastructure;
using SoloDevApp.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SoloDevApp.Service.Services
{
    public interface IHoSo_SkillService : IService<HoSo_Skill, HoSo_SkillViewModel>
    {

        Task<ResponseEntity> ThemHoSoSkill(ThemDanhSachHoSo_NguoiDung model);


    }

    public class HoSo_SkillService : ServiceBase<HoSo_Skill, HoSo_SkillViewModel>, IHoSo_SkillService
    {
        private IHoSo_SkillRepository _hoSo_SkillRepository;
        public HoSo_SkillService(IHoSo_SkillRepository hoSo_SkillRepository, IMapper mapper)
            : base(hoSo_SkillRepository, mapper)
        {
            _hoSo_SkillRepository = hoSo_SkillRepository;
        }

        public async Task<ResponseEntity> ThemHoSoSkill(ThemDanhSachHoSo_NguoiDung model)
        {
            try
            {

                IEnumerable<HoSo_Skill> lstHoSo_Skill = await _hoSo_SkillRepository.GetMultiByConditionAsync("NguoiDungId", model.NguoiDungId);

                if(lstHoSo_Skill.Count() > 0)
                {
                    List<dynamic> lstIdDelete = new List<dynamic>();
                    foreach (HoSo_Skill item in lstHoSo_Skill)
                    {
                        lstIdDelete.Add(item.Id);
                    }
                   
                    await _hoSo_SkillRepository.DeleteByIdAsync(lstIdDelete);
                }

                foreach (dynamic item in model.lstHoSo)
                {

                    HoSo_Skill hoSo_Skill = new HoSo_Skill();
                    hoSo_Skill.IdSkill = item.idSkill;
                    hoSo_Skill.NguoiDungId = item.nguoiDungId;
                    hoSo_Skill.CapDo = item.capDo;

                    await _hoSo_SkillRepository.InsertAsync(hoSo_Skill);
                }


                return new ResponseEntity(StatusCodeConstants.OK, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}