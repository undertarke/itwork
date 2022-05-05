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
    [Route("api/hocvan")]
    [ApiController]
   /* [ApiKeyAuth]
    [Authorize(Roles = "DAALL")]*/

    public class HocVanController : ControllerBase
    {
        private IHocVanService _hocVanService;

        public HocVanController(IHocVanService hocVanService)
        {
            _hocVanService = hocVanService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ThemDanhSachHoSo_NguoiDung model)
        {
            return await _hocVanService.ThemHocVan(model);
        }

    }
}