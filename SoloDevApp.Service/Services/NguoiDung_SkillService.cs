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
     
        Task<ResponseEntity> ThemNguoiDungSkill(ThemDanhSachSkill_NguoiDung model);

    }

    public class NguoiDung_SkillService : ServiceBase<NguoiDung_Skill, NguoiDung_SkillViewModel>, INguoiDung_SkillService
    {
        private INguoiDung_SkillRepository _nguoiDung_SkillRepository;
        public NguoiDung_SkillService(INguoiDung_SkillRepository nguoiDung_SkillRepository, IMapper mapper)
            : base(nguoiDung_SkillRepository, mapper)
        {
            _nguoiDung_SkillRepository = nguoiDung_SkillRepository;
        }

        public async Task<ResponseEntity> ThemNguoiDungSkill(ThemDanhSachSkill_NguoiDung model)
        {
            try
            {

                foreach(NguoiDung_SkillViewModel nguoiDung_Skill in model.lstSkill)
                {

                    List<KeyValuePair<string, dynamic>> columns =   new List<KeyValuePair<string, dynamic>>();
                    columns.Add(new KeyValuePair<string, dynamic>("IdSkill",nguoiDung_Skill.IdSkill));
                    columns.Add(new KeyValuePair<string, dynamic>("NguoiDungId", model.NguoiDungId));

                    NguoiDung_Skill checkNguoiDung_Skill = await _nguoiDung_SkillRepository.GetSingleByListConditionAsync(columns);
                    if (checkNguoiDung_Skill == null)
                    {
                        checkNguoiDung_Skill = new NguoiDung_Skill();
                        checkNguoiDung_Skill.IdSkill = nguoiDung_Skill.IdSkill;   
                        checkNguoiDung_Skill.NguoiDungId = nguoiDung_Skill.NguoiDungId;
                        checkNguoiDung_Skill.XacMinh = false;


                        await _nguoiDung_SkillRepository.InsertAsync(checkNguoiDung_Skill);
                    }
                   
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