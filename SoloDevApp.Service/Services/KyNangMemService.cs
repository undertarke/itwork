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
    public interface IKyNangMemService : IService<KyNangMem, KyNangMemViewModel>
    {

        Task<ResponseEntity> ThemKyNangMem(ThemDanhSachHoSo_NguoiDung model);

    }

    public class KyNangMemService : ServiceBase<KyNangMem, KyNangMemViewModel>, IKyNangMemService
    {
        private IKyNangMemRepository _kyNangMemRepository;
        public KyNangMemService(IKyNangMemRepository kyNangMemRepository, IMapper mapper)
            : base(kyNangMemRepository, mapper)
        {
            _kyNangMemRepository = kyNangMemRepository;
        }

        public async Task<ResponseEntity> ThemKyNangMem(ThemDanhSachHoSo_NguoiDung model)
        {
            try
            {

                IEnumerable<KyNangMem> lstKyNangMem = await _kyNangMemRepository.GetMultiByConditionAsync("NguoiDungId", model.NguoiDungId);

                if (lstKyNangMem.Count() > 0)
                {
                    List<dynamic> lstIdDelete = new List<dynamic>();
                    foreach (KyNangMem item in lstKyNangMem)
                    {
                        lstIdDelete.Add(item.Id);
                    }

                    await _kyNangMemRepository.DeleteByIdAsync(lstIdDelete);
                }

                foreach (dynamic item in model.lstHoSo)
                {

                    KyNangMem kyNangMem = new KyNangMem();
                    kyNangMem.TenKyNang = item.tenKyNang;
                    kyNangMem.NguoiDungId = model.NguoiDungId;

                    await _kyNangMemRepository.InsertAsync(kyNangMem);
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