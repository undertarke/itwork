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
using System.Threading.Tasks;

namespace SoloDevApp.Service.Services
{
    public interface INguoiDung_SkillService : IService<NguoiDung_Skill, NguoiDung_SkillViewModel>
    {
     
        Task<ResponseEntity> ThemNguoiDungSkill(ThemDanhSachHoSo_NguoiDung model);
        Task<ResponseEntity> LayNguoiDung_Skill(string NguoiDungId);

    }

    public class NguoiDung_SkillService : ServiceBase<NguoiDung_Skill, NguoiDung_SkillViewModel>, INguoiDung_SkillService
    {
        private INguoiDung_SkillRepository _nguoiDung_SkillRepository;
        public NguoiDung_SkillService(INguoiDung_SkillRepository nguoiDung_SkillRepository, IMapper mapper)
            : base(nguoiDung_SkillRepository, mapper)
        {
            _nguoiDung_SkillRepository = nguoiDung_SkillRepository;
        }

        public async Task<ResponseEntity> ThemNguoiDungSkill(ThemDanhSachHoSo_NguoiDung model)
        {
            try
            {

                foreach(dynamic nguoiDung_Skill in model.lstHoSo)
                {

                    List<KeyValuePair<string, dynamic>> columns =   new List<KeyValuePair<string, dynamic>>();
                    columns.Add(new KeyValuePair<string, dynamic>("IdSkill",nguoiDung_Skill.idSkill));
                    columns.Add(new KeyValuePair<string, dynamic>("NguoiDungId", model.NguoiDungId));

                    NguoiDung_Skill checkNguoiDung_Skill = await _nguoiDung_SkillRepository.GetSingleByListConditionAsync(columns);
                    if (checkNguoiDung_Skill == null)
                    {
                        checkNguoiDung_Skill = new NguoiDung_Skill();
                        checkNguoiDung_Skill.IdSkill = nguoiDung_Skill.idSkill;   
                        checkNguoiDung_Skill.NguoiDungId = model.NguoiDungId;
                        checkNguoiDung_Skill.CapDo = nguoiDung_Skill.capDo;

                        await _nguoiDung_SkillRepository.InsertAsync(checkNguoiDung_Skill);
                    }
                    else
                    {
                        checkNguoiDung_Skill.CapDo= nguoiDung_Skill.capDo;
                        await _nguoiDung_SkillRepository.UpdateAsync(checkNguoiDung_Skill.Id,checkNguoiDung_Skill);

                    }

                }

                IEnumerable<NguoiDung_Skill> lstNguoiDung_Skill = await _nguoiDung_SkillRepository.GetMultiByConditionAsync("NguoiDungId", model.NguoiDungId);


                return new ResponseEntity(StatusCodeConstants.OK, lstNguoiDung_Skill);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseEntity> LayNguoiDung_Skill(string NguoiDungId)
        {
            try
            {


                IEnumerable<NguoiDung_Skill> lstNguoiDung_Skill = await _nguoiDung_SkillRepository.GetMultiByConditionAsync("NguoiDungId", NguoiDungId);


                return new ResponseEntity(StatusCodeConstants.OK, lstNguoiDung_Skill);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}