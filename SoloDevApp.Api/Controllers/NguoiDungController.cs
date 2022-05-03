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
    [Route("api/nguoidung")]
    [ApiController]
   /* [ApiKeyAuth]
    [Authorize(Roles = "DAALL")]*/

    public class NguoiDungController : ControllerBase
    {
        private INguoiDungService _nguoiDungService;

        public NguoiDungController(INguoiDungService nguoiDungService)
        {
            _nguoiDungService = nguoiDungService;
        }

        [HttpGet]
        //[Authorize(Roles = "VIEW_ROLE")]
        public async Task<IActionResult> Get()
        {
            return await _nguoiDungService.GetAllAsync();
        }

      

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return await _nguoiDungService.GetSingleByIdAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NguoiDungViewModel model)
        {
            return await _nguoiDungService.InsertAsync(model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] NguoiDungViewModel model)
        {
            return await _nguoiDungService.UpdateAsync(id, model);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] List<dynamic> Ids)
        {
            return await _nguoiDungService.DeleteByIdAsync(Ids);
        }
    }
}