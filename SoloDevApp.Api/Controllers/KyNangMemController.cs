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
    [Route("api/KyNangMem")]
    [ApiController]
   /* [ApiKeyAuth]
    [Authorize(Roles = "DAALL")]*/

    public class KyNangMemController : ControllerBase
    {
        private IKyNangMemService _kyNangMemService;
        private INgoaiNguService _ngoaiNguService;

        public KyNangMemController(IKyNangMemService kyNangMemService, INgoaiNguService ngoaiNguService)
        {
            _kyNangMemService = kyNangMemService;
            _ngoaiNguService = ngoaiNguService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ThemDanhSachHoSo_NguoiDung model)
        {
            return await _kyNangMemService.ThemKyNangMem(model);
        }
        [HttpPost("ngoaingu")]
        public async Task<IActionResult> ThemNgoaiNgu([FromBody] ThemDanhSachHoSo_NguoiDung model)
        {
            return await _ngoaiNguService.ThemNgoaiNgu(model);
        }
    }
}