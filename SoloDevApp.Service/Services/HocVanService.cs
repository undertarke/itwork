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
    public interface IHocVanService : IService<HocVan, HocVanViewModel>
    {

        Task<ResponseEntity> ThemHocVan(ThemDanhSachHoSo_NguoiDung model);


    }

    public class HocVanService : ServiceBase<HocVan, HocVanViewModel>, IHocVanService
    {
        private IHocVanRepository _hocVanRepository;
        public HocVanService(IHocVanRepository hocVanRepository, IMapper mapper)
            : base(hocVanRepository, mapper)
        {
            _hocVanRepository = hocVanRepository;
        }

        public async Task<ResponseEntity> ThemHocVan(ThemDanhSachHoSo_NguoiDung model)
        {
            try
            {

                IEnumerable<HocVan> lstHocVan = await _hocVanRepository.GetMultiByConditionAsync("NguoiDungId", model.NguoiDungId);

                if (lstHocVan.Count() > 0)
                {
                    List<dynamic> lstIdDelete = new List<dynamic>();
                    foreach (HocVan item in lstHocVan)
                    {
                        lstIdDelete.Add(item.Id);
                    }

                    await _hocVanRepository.DeleteByIdAsync(lstIdDelete);
                }

                foreach (dynamic item in model.lstHoSo)
                {

                    HocVan hocVan = new HocVan();
                    hocVan.TenTruong = item.tenTruong;
                    hocVan.MoTa = item.moTa;
                    hocVan.ChuyenNganh = item.chuyenNganh;
                    hocVan.NamBatDau = item.namBatDau;
                    hocVan.NamKetThuc = item.namKetThuc;
                    hocVan.TrinhDo = item.trinhDo;
                    hocVan.NguoiDungId = model.NguoiDungId;

                    await _hocVanRepository.InsertAsync(hocVan);
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