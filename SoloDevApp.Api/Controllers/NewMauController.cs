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
    [Route("api/NewMau")]
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

        [HttpGet]
        //[Authorize(Roles = "VIEW_ROLE")]
        public async Task<IActionResult> Get()
        {
            return await _newMauService.GetAllAsync();
        }

      

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return await _newMauService.GetSingleByIdAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NewMauViewModel model)
        {
            return await _newMauService.InsertAsync(model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] NewMauViewModel model)
        {
            return await _newMauService.UpdateAsync(id, model);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] List<dynamic> Ids)
        {
            return await _newMauService.DeleteByIdAsync(Ids);
        }
    }
}