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
    [Route("api/duan")]
    [ApiController]
   /* [ApiKeyAuth]
    [Authorize(Roles = "DAALL")]*/

    public class DuAnController : ControllerBase
    {
        private IDuAnService _duAnService;

        public DuAnController(IDuAnService duAnService)
        {
            _duAnService = duAnService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ThemDanhSachSkill_NguoiDung model)
        {
            return await _duAnService.ThemDuAn(model);
        }
    }
}