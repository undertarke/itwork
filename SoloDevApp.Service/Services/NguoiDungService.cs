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
using System.Linq;
using System.Threading.Tasks;

namespace SoloDevApp.Service.Services
{
    public interface INguoiDungService : IService<NguoiDung, NguoiDungViewModel>
    {

         Task<ResponseEntity> LayThongTinUser(string id);

    }

    public class NguoiDungService : ServiceBase<NguoiDung, NguoiDungViewModel>, INguoiDungService
    {
        private INguoiDungRepository _nguoiDungRepository;
        private IChungChiRepository _chungChiRepository;
        private IDuAnRepository _duAnRepository;
        private IHocVanRepository _hocVanRepository;
        private IKinhNghiemRepository _kinhNghiemRepository;
        private IKyNangMemRepository _kyNangMemRepository;
        private IHoSo_SkillRepository _hoSo_SkillRepository;

        public NguoiDungService(INguoiDungRepository nguoiDungRepository,
         IChungChiRepository chungChiRepository,
         IDuAnRepository duAnRepository,
         IHocVanRepository hocVanRepository,
         IKinhNghiemRepository kinhNghiemRepository,
         IKyNangMemRepository kyNangMemRepository,
         IHoSo_SkillRepository hoSo_SkillRepository,
        IMapper mapper)
            : base(nguoiDungRepository, mapper)
        {
            _nguoiDungRepository = nguoiDungRepository;
            _chungChiRepository = chungChiRepository;
            _duAnRepository = duAnRepository;
            _hocVanRepository = hocVanRepository;
            _kinhNghiemRepository = kinhNghiemRepository;
            _kyNangMemRepository = kyNangMemRepository;
            _hoSo_SkillRepository = hoSo_SkillRepository;
        }

        public async Task<ResponseEntity> LayThongTinUser(string id)
        {
            try
            {
                IEnumerable<ChungChi> lstChungChi = await _chungChiRepository.GetMultiByConditionAsync("NguoiDungId", id);
                IEnumerable<DuAn> lstDuAn = await _duAnRepository.GetMultiByConditionAsync("NguoiDungId", id);
                IEnumerable<HocVan> lstHocVan = await _hocVanRepository.GetMultiByConditionAsync("NguoiDungId", id);
                IEnumerable<KinhNghiem> lstkinhNghiem = await _kinhNghiemRepository.GetMultiByConditionAsync("NguoiDungId", id);
                IEnumerable<KyNangMem> lstKyNangMem = await _kyNangMemRepository.GetMultiByConditionAsync("NguoiDungId", id);
                IEnumerable<HoSo_Skill> lstHoSo_Skill = await _hoSo_SkillRepository.GetMultiByConditionAsync("NguoiDungId", id);

                NguoiDung nguoiDung = await _nguoiDungRepository.GetSingleByIdAsync(id);

                ThongTinNguoiDung thongTinNguoiDung = new ThongTinNguoiDung();

                thongTinNguoiDung = _mapper.Map<ThongTinNguoiDung>(nguoiDung);

                thongTinNguoiDung.ChungChi = lstChungChi.ToList();
                thongTinNguoiDung.DuAn = _mapper.Map<List<DuAnViewModel>>(lstDuAn); 
                thongTinNguoiDung.HocVan = lstHocVan.ToList();
                thongTinNguoiDung.KinhNghiem = lstkinhNghiem.ToList();
                thongTinNguoiDung.KyNangMem = lstKyNangMem.ToList();
                thongTinNguoiDung.HoSo_Skill = lstHoSo_Skill.ToList();

                return new ResponseEntity(StatusCodeConstants.OK, thongTinNguoiDung);

            }


            catch (Exception ex)
            {
                throw ex;
            }
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