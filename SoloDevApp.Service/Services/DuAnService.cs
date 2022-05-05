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
    public interface IDuAnService : IService<DuAn, DuAnViewModel>
    {

        Task<ResponseEntity> ThemDuAn(ThemDanhSachHoSo_NguoiDung model);


    }

    public class DuAnService : ServiceBase<DuAn, DuAnViewModel>, IDuAnService
    {
        private IDuAnRepository _duAnRepository;
        public DuAnService(IDuAnRepository duAnRepository, IMapper mapper)
            : base(duAnRepository, mapper)
        {
            _duAnRepository = duAnRepository;
        }

        public async Task<ResponseEntity> ThemDuAn(ThemDanhSachHoSo_NguoiDung model)
        {
            try
            {

                IEnumerable<DuAn> lstDuAn = await _duAnRepository.GetMultiByConditionAsync("NguoiDungId", model.NguoiDungId);

                if (lstDuAn.Count() > 0)
                {
                    List<dynamic> lstIdDelete = new List<dynamic>();
                    foreach (DuAn item in lstDuAn)
                    {
                        lstIdDelete.Add(item.Id);
                    }

                    await _duAnRepository.DeleteByIdAsync(lstIdDelete);
                }

                foreach (dynamic item in model.lstHoSo)
                {

                    DuAn duAn = new DuAn();
                    duAn.TenDuAn = item.TenDuAn;
                    duAn.Skill = item.Skill;
                    duAn.SoThanhVien = item.SoThanhVien;
                    duAn.LinkDemo = item.LinkDemo;
                    duAn.MoTa = item.MoTa;
                    duAn.NguoiDungId = model.NguoiDungId;

                    await _duAnRepository.InsertAsync(duAn);
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