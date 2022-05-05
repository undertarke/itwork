using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SoloDevApp.Repository.Models;
using SoloDevApp.Repository.Repositories;
using SoloDevApp.Service.Constants;
using SoloDevApp.Service.Helpers;
using SoloDevApp.Service.Infrastructure;
using SoloDevApp.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SoloDevApp.Service.Services
{
    public interface INewMauService : IService<NewMau, NewMauViewModel>
    {
     
        Task<ResponseEntity> GenerateToken(string id);

    }

    public class NewMauService : ServiceBase<NewMau, NewMauViewModel>, INewMauService
    {
        private INewMauRepository _newMauRepository;
        private readonly IAppSettings _appSettings;

        public NewMauService(INewMauRepository newMauRepository, IMapper mapper, IAppSettings appSettings)
            : base(newMauRepository, mapper)
        {
            _newMauRepository = newMauRepository;
            _appSettings= appSettings;
        }


        /* public async Task<ResponseEntity> ThemNguoiDungSkill(ThemDanhSachSkill_NguoiDung model)
         {
             try
             {



                 return new ResponseEntity(StatusCodeConstants.OK, 1);
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }*/



        public async Task<ResponseEntity> GenerateToken(string id)
        {
            try
            {
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Expired, DateTime.Now.AddYears(23).ToString()));
                claims.Add(new Claim(ClaimTypes.Name, id));
                claims.Add(new Claim(ClaimTypes.Email, ""));
               
                claims.Add(new Claim(ClaimTypes.Role, ""));
                

                var secret = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var token = new JwtSecurityToken(
                        claims: claims,
                        notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                        expires: new DateTimeOffset(DateTime.Now.AddMinutes(60)).DateTime,
                        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
                    );
           
                return new ResponseEntity(StatusCodeConstants.OK, new JwtSecurityTokenHandler().WriteToken(token));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}