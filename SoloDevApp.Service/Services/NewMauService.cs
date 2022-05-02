using AutoMapper;
using Newtonsoft.Json;
using SoloDevApp.Repository.Models;
using SoloDevApp.Repository.Repositories;
using SoloDevApp.Service.Constants;
using SoloDevApp.Service.Infrastructure;
using SoloDevApp.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SoloDevApp.Service.Services
{
    public interface INewMauService : IService<NewMau, NewMauViewModel>
    {
     
       // Task<ResponseEntity> XoaLopTaiLieu(NewMau model);

    }

    public class NewMauService : ServiceBase<NewMau, NewMauViewModel>, INewMauService
    {
        private INewMauRepository _newMauRepository;
        public NewMauService(INewMauRepository newMauRepository, IMapper mapper)
            : base(newMauRepository, mapper)
        {
            _newMauRepository = newMauRepository;
        }

      /*  private async Task<string> GenerateToken(NguoiDung entity)
        {
            try
            {
                NhomQuyen nhomQuyen = await _nhomQuyenRepository.GetSingleByIdAsync(entity.MaNhomQuyen);
                if (nhomQuyen == null)
                    return string.Empty;
                List<string> roles = JsonConvert.DeserializeObject<List<string>>(nhomQuyen.DanhSachQuyen);
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Expired, DateTime.Now.AddYears(23).ToString()));
                claims.Add(new Claim(ClaimTypes.Name, entity.Id));
                claims.Add(new Claim(ClaimTypes.Email, entity.Email));
                foreach (var item in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, item.Trim()));
                }

                var secret = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var token = new JwtSecurityToken(
                        claims: claims,
                        notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                        expires: new DateTimeOffset(DateTime.Now.AddMinutes(60)).DateTime,
                        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
                    );
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }*/


    }
}