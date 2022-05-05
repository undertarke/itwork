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
    public interface IChungChiService : IService<ChungChi, ChungChiViewModel>
    {

        Task<ResponseEntity> ThemChungChi(ThemDanhSachHoSo_NguoiDung model);


    }

    public class ChungChiService : ServiceBase<ChungChi, ChungChiViewModel>, IChungChiService
    {
        private IChungChiRepository _chungChiRepository;
        public ChungChiService(IChungChiRepository chungChiRepository, IMapper mapper)
            : base(chungChiRepository, mapper)
        {
            _chungChiRepository = chungChiRepository;
        }

        public async Task<ResponseEntity> ThemChungChi(ThemDanhSachHoSo_NguoiDung model)
        {
            try
            {

                IEnumerable<ChungChi> lstChungChi = await _chungChiRepository.GetMultiByConditionAsync("NguoiDungId", model.NguoiDungId);

                if (lstChungChi.Count() > 0)
                {
                    List<dynamic> lstIdDelete = new List<dynamic>();
                    foreach (ChungChi item in lstChungChi)
                    {
                        lstIdDelete.Add(item.Id);
                    }

                    await _chungChiRepository.DeleteByIdAsync(lstIdDelete);
                }

                foreach (dynamic item in model.lstHoSo)
                {

                    ChungChi chungChi = new ChungChi();
                    chungChi.TenChungChi = item.TenChungChi;
                    chungChi.ToChuc = item.ToChuc;
                    chungChi.Nam = item.Nam;
                    chungChi.LinkChungChi = item.LinkChungChi;
                    chungChi.NguoiDungId = model.NguoiDungId;

                    await _chungChiRepository.InsertAsync(chungChi);
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