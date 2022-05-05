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
    [Route("api/kinhnghiem")]
    [ApiController]
   /* [ApiKeyAuth]
    [Authorize(Roles = "DAALL")]*/

    public class KinhNghiemController : ControllerBase
    {
        private IKinhNghiemService _kinhNghiemService;

        public KinhNghiemController(IKinhNghiemService kinhNghiemService)
        {
            _kinhNghiemService = kinhNghiemService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await _kinhNghiemService.GetAllAsync();
        }
       

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ThemDanhSachHoSo_NguoiDung model)
        {
            return await _kinhNghiemService.ThemKinhNghiem(model);
        }

    }
}