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
    [Route("api/chungchi")]
    [ApiController]
   /* [ApiKeyAuth]
    [Authorize(Roles = "DAALL")]*/

    public class ChungChiController : ControllerBase
    {
        private IChungChiService _chungChiService;

        public ChungChiController(IChungChiService chungChiService)
        {
            _chungChiService = chungChiService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ThemDanhSachHoSo_NguoiDung model)
        {
            return await _chungChiService.ThemChungChi(model);
        }

    }
}