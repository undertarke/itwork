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
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SoloDevApp.Service.Services
{
    public interface INguoiDungService : IService<NguoiDung, NguoiDungViewModel>
    {

         Task<ResponseEntity> LayThongTinUser(string id);
         Task<ResponseEntity> LoginFacebook(LoginUser model);
         Task<ResponseEntity> SignUp(SignUpUser model);
        Task<ResponseEntity> ChangePass(string id, ChangePassUser model);

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
        private INgoaiNguRepository _ngoaiNguRepository;

        private readonly IAppSettings _appSettings;

        public NguoiDungService(INguoiDungRepository nguoiDungRepository,
         IChungChiRepository chungChiRepository,
         IDuAnRepository duAnRepository,
         IHocVanRepository hocVanRepository,
         IKinhNghiemRepository kinhNghiemRepository,
         IKyNangMemRepository kyNangMemRepository,
         IHoSo_SkillRepository hoSo_SkillRepository,
         INgoaiNguRepository ngoaiNguRepository,
          IAppSettings appSettings,
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
            _ngoaiNguRepository = ngoaiNguRepository;
            _appSettings = appSettings;
        }


        //token: thanh cong, null: that bai
        public async Task<ResponseEntity> LoginFacebook(LoginUser model)
        {
            try
            {
                NguoiDung nguoiDung = new NguoiDung();

                if (model.IdFacebook != "0")
                {
                    nguoiDung = await _nguoiDungRepository.GetSingleByConditionAsync("IdFacebook",model.IdFacebook);
                    if(nguoiDung == null)
                    {
                        nguoiDung = new NguoiDung();
                        nguoiDung.IdFacebook=model.IdFacebook;
                        nguoiDung = await _nguoiDungRepository.InsertAsync(nguoiDung);
                    }
                }

                if(model.IdGoogle != "0")
                {
                    nguoiDung = await _nguoiDungRepository.GetSingleByConditionAsync("IdGoogle", model.IdGoogle);
                    if (nguoiDung == null)
                    {
                        nguoiDung = new NguoiDung();
                        nguoiDung.IdGoogle = model.IdGoogle;
                        nguoiDung = await _nguoiDungRepository.InsertAsync(nguoiDung);
                    }
                }

                if (model.Email != "0")
                {
                    /* BCrypt.Net.BCrypt.Verify("", ""); */
                   /* BCrypt.Net.BCrypt.HashPassword */
                /* $2b$10$IqzYoIK.gQeSbNu8yKQzvuNcY6bsK2EhhDbDrgQ5HFK4utzF6rvkK*/
                /*pass: 123456*/
                   nguoiDung = await _nguoiDungRepository.GetSingleByConditionAsync("Email", model.Email);

                    string sMatKhau = BCrypt.Net.BCrypt.HashPassword(model.Pass);

                    if(nguoiDung == null || !BCrypt.Net.BCrypt.Verify(model.Pass, nguoiDung.Pass))
                    {
                        nguoiDung = null;
                    }

                }

                if (nguoiDung == null)
                {
                    return new ResponseEntity(StatusCodeConstants.NOT_FOUND);

                }

                Task<string> sToken = GenerateToken(nguoiDung);

                
                return new ResponseEntity(StatusCodeConstants.OK, sToken.Result);

            }


            catch (Exception ex)
            {
                throw ex;
            }
        }

        //0: trung mail , 1: thanh cong
        public async Task<ResponseEntity> SignUp(SignUpUser model)
        {
            try
            {
                NguoiDung nguoiDung = await _nguoiDungRepository.GetSingleByConditionAsync("Email", model.Email);
                if(nguoiDung != null)
                {
                    return new ResponseEntity(StatusCodeConstants.OK, 0,"Địa chỉ email đã tồn tại !");

                }

                nguoiDung = new NguoiDung();
                nguoiDung.HoTen = model.HoTen;
                nguoiDung.Email = model.Email;
                nguoiDung.Pass = BCrypt.Net.BCrypt.HashPassword(model.Pass);

                await _nguoiDungRepository.InsertAsync(nguoiDung);


                return new ResponseEntity(StatusCodeConstants.OK, 1);

            }


            catch (Exception ex)
            {
                throw ex;
            }
        }


        //0: mat khau cu khong dung , 1: thanh cong
        public async Task<ResponseEntity> ChangePass(string id, ChangePassUser model)
        {
            try
            {
                NguoiDung nguoiDung = await _nguoiDungRepository.GetSingleByIdAsync(id);
                if (nguoiDung == null)
                {
                    return new ResponseEntity(StatusCodeConstants.BAD_REQUEST);

                }

                if(!BCrypt.Net.BCrypt.Verify(model.PassOld, nguoiDung.Pass))
                {
                    return new ResponseEntity(StatusCodeConstants.OK,0,"Mật khẩu cũ không đúng !");

                }

               string sMatKhau = BCrypt.Net.BCrypt.HashPassword(model.PassNew);
                nguoiDung.Pass = sMatKhau;

                await _nguoiDungRepository.UpdateAsync(nguoiDung.Id, nguoiDung);


                return new ResponseEntity(StatusCodeConstants.OK, 1);

            }


            catch (Exception ex)
            {
                throw ex;
            }
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
                IEnumerable<NgoaiNgu> lstNgoaiNgu = await _ngoaiNguRepository.GetMultiByConditionAsync("NguoiDungId", id);

                NguoiDung nguoiDung = await _nguoiDungRepository.GetSingleByIdAsync(id);

                ThongTinNguoiDung thongTinNguoiDung = new ThongTinNguoiDung();

                thongTinNguoiDung = _mapper.Map<ThongTinNguoiDung>(nguoiDung);

                thongTinNguoiDung.ChungChi = lstChungChi.ToList();
                thongTinNguoiDung.DuAn = _mapper.Map<List<DuAnViewModel>>(lstDuAn); 
                thongTinNguoiDung.HocVan = lstHocVan.ToList();
                thongTinNguoiDung.KinhNghiem = lstkinhNghiem.ToList();
                thongTinNguoiDung.KyNangMem = lstKyNangMem.ToList();
                thongTinNguoiDung.HoSo_Skill = lstHoSo_Skill.ToList();
                thongTinNguoiDung.NgoaiNgu = lstNgoaiNgu.ToList();


                return new ResponseEntity(StatusCodeConstants.OK, thongTinNguoiDung);

            }


            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<string> GenerateToken(NguoiDung entity,string quyen="")
        {
            try
            {
                string sNguoiDung = JsonConvert.SerializeObject(entity);
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, sNguoiDung));
                var secret = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var token = new JwtSecurityToken(
                        claims: claims,
                        notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                        expires: new DateTimeOffset(DateTime.Now.AddDays(7)).DateTime,
                        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
                    );
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}