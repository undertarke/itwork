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
    [Route("api/cauhinh")]
    [ApiController]
   /* [ApiKeyAuth]
    [Authorize(Roles = "DAALL")]*/

    public class CauHinhController : ControllerBase
    {
        private ICauHinhService _cauHinhService;

        public CauHinhController(ICauHinhService cauHinhService)
        {
            _cauHinhService = cauHinhService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await _cauHinhService.GetAllAsync();
        }

      
    }
}