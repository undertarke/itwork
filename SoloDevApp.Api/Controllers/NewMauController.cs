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
    [Route("api/newmau")]
    [ApiController]
   /* [ApiKeyAuth]
    [Authorize(Roles = "DAALL")]*/

    public class NewMauController : ControllerBase
    {
        private INewMauService _newMauService;

        public NewMauController(INewMauService newMauService)
        {
            _newMauService = newMauService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return await _newMauService.GenerateToken(id);
        }

      
    }
}