using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SoloDevApp.Api.Filters;
using SoloDevApp.Service.Services;
using SoloDevApp.Service.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoloDevApp.Api.Controllers
{
    [Route("api/skill")]
    [ApiController]
   /* [ApiKeyAuth]
    [Authorize(Roles = "DAALL")]*/

    public class SkillController : ControllerBase
    {
        private ISkillService _skillService;
        private INguoiDung_SkillService _nguoiDung_SkillService;

        public SkillController(ISkillService skillService, INguoiDung_SkillService nguoiDung_SkillService)
        {
            _skillService = skillService;
            _nguoiDung_SkillService=nguoiDung_SkillService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await _skillService.GetAllAsync();
        }

      

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return await _skillService.GetSingleByIdAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SkillViewModel model)
        {
            return await _skillService.InsertAsync(model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] SkillViewModel model)
        {
            return await _skillService.UpdateAsync(id, model);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] List<dynamic> Ids)
        {
            return await _skillService.DeleteByIdAsync(Ids);
        }


        //nguoidung skill 
        [HttpGet("user-skill")]
        public async Task<IActionResult> LayNguoiDungSkill()
        {
            return await _nguoiDung_SkillService.GetAllAsync();
        }

        [HttpPost("user-skill")]
        public async Task<IActionResult> ThemNguoiDungSkill([FromBody] ThemDanhSachSkill_NguoiDung model)
        {
            return await _nguoiDung_SkillService.ThemNguoiDungSkill(model);
        }
    }
}