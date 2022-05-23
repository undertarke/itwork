using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SoloDevApp.Api.Filters;
using SoloDevApp.Service.Constants;
using SoloDevApp.Service.Services;
using SoloDevApp.Service.Utilities;
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

        [HttpPut]
        public async Task<IActionResult> Put([FromHeader] string Authorization, [FromBody] NguoiDungViewModel model)
        {
            if (Authorization?.Length > 0)
            {
                string id = FuncUtilities.GetUserIdFromHeaderToken(Authorization);
                return await _nguoiDungService.UpdateAsync(id, model);


            }
            return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, "token expired");
        }

        [HttpPost("login-user")]
        public async Task<IActionResult> LoginFacebook([FromBody] LoginUser model)
        {
            return await _nguoiDungService.LoginFacebook(model);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] List<dynamic> Ids)
        {
            return await _nguoiDungService.DeleteByIdAsync(Ids);
        }

        [HttpGet("lay-thong-tin-user")]
        public async Task<IActionResult> LayThongTinUser([FromHeader] string Authorization)
        {
            if (Authorization?.Length > 0)
            {
                string id = FuncUtilities.GetUserIdFromHeaderToken(Authorization);
                return await _nguoiDungService.LayThongTinUser(id);


            }
            return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, "token expired");
        }

        [HttpPost("dang-ky")]
        public async Task<IActionResult> SignUp([FromBody] SignUpUser model)
        {
            
                return await _nguoiDungService.SignUp(model);

        }
        [HttpPost("doi-mat-khau")]
        public async Task<IActionResult> ChangePass([FromHeader] string Authorization, [FromBody] ChangePassUser model)
        {

            if (Authorization?.Length > 0)
            {
                string id = FuncUtilities.GetUserIdFromHeaderToken(Authorization);

                return await _nguoiDungService.ChangePass(id,model);
            }
            return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, "token expired");

        }
    }
}