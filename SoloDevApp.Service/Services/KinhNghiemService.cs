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
    public interface IKinhNghiemService : IService<KinhNghiem, KinhNghiemViewModel>
    {

        Task<ResponseEntity> ThemKinhNghiem(ThemDanhSachHoSo_NguoiDung model);


    }

    public class KinhNghiemService : ServiceBase<KinhNghiem, KinhNghiemViewModel>, IKinhNghiemService
    {
        private IKinhNghiemRepository _kinhNghiemRepository;
        public KinhNghiemService(IKinhNghiemRepository kinhNghiemRepository, IMapper mapper)
            : base(kinhNghiemRepository, mapper)
        {
            _kinhNghiemRepository = kinhNghiemRepository;
        }
        public async Task<ResponseEntity> ThemKinhNghiem(ThemDanhSachHoSo_NguoiDung model)
        {
            try
            {

                IEnumerable<KinhNghiem> lstkinhNghiem = await _kinhNghiemRepository.GetMultiByConditionAsync("NguoiDungId", model.NguoiDungId);

                if (lstkinhNghiem.Count() > 0)
                {
                    List<dynamic> lstIdDelete = new List<dynamic>();
                    foreach (KinhNghiem item in lstkinhNghiem)
                    {
                        lstIdDelete.Add(item.Id);
                    }

                    await _kinhNghiemRepository.DeleteByIdAsync(lstIdDelete);
                }

                foreach (dynamic item in model.lstHoSo)
                {

                    KinhNghiem kinhNghiem = new KinhNghiem();
                    kinhNghiem.TenCongTy = item.TenCongTy;
                    kinhNghiem.MoTa = item.MoTa;
                    kinhNghiem.NgayBatDau = item.NgayBatDau;
                    kinhNghiem.NgayKetThuc = item.NgayKetThuc;
                    kinhNghiem.NguoiDungId = model.NguoiDungId;
                    kinhNghiem.ChucDanh = item.ChucDanh;

                    await _kinhNghiemRepository.InsertAsync(kinhNghiem);
                }


                return new ResponseEntity(StatusCodeConstants.OK, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}