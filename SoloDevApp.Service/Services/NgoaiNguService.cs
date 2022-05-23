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
    public interface INgoaiNguService : IService<NgoaiNgu, NgoaiNguViewModel>
    {

        Task<ResponseEntity> ThemNgoaiNgu(ThemDanhSachHoSo_NguoiDung model);


    }

    public class NgoaiNguService : ServiceBase<NgoaiNgu, NgoaiNguViewModel>, INgoaiNguService
    {
        private INgoaiNguRepository _ngoaiNguRepository;
        public NgoaiNguService(INgoaiNguRepository ngoaiNguRepository, IMapper mapper)
            : base(ngoaiNguRepository, mapper)
        {
            _ngoaiNguRepository = ngoaiNguRepository;
        }

        public async Task<ResponseEntity> ThemNgoaiNgu(ThemDanhSachHoSo_NguoiDung model)
        {
            try
            {

                IEnumerable<NgoaiNgu> lstNgoaiNgu = await _ngoaiNguRepository.GetMultiByConditionAsync("NguoiDungId", model.NguoiDungId);

                if (lstNgoaiNgu.Count() > 0)
                {
                    List<dynamic> lstIdDelete = new List<dynamic>();
                    foreach (NgoaiNgu item in lstNgoaiNgu)
                    {
                        lstIdDelete.Add(item.Id);
                    }

                    await _ngoaiNguRepository.DeleteByIdAsync(lstIdDelete);
                }

                foreach (dynamic item in model.lstHoSo)
                {

                    NgoaiNgu ngoaiNgu = new NgoaiNgu();
                    ngoaiNgu.TenNgonNgu = item.tenNgonNgu;
                    ngoaiNgu.CapDo = item.capDo;
                    ngoaiNgu.NguoiDungId = model.NguoiDungId;

                    await _ngoaiNguRepository.InsertAsync(ngoaiNgu);
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