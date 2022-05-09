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
    public interface ICauHinhService : IService<CauHinh, CauHinhViewModel>
    {

         Task<ResponseEntity> LayChucDanh(string MaCauHinh);

    }

    public class CauHinhService : ServiceBase<CauHinh, CauHinhViewModel>, ICauHinhService
    {
        private ICauHinhRepository _cauHinhRepository;
        public CauHinhService(ICauHinhRepository cauHinhRepository, IMapper mapper)
            : base(cauHinhRepository, mapper)
        {
            _cauHinhRepository = cauHinhRepository;
        }


        public async Task<ResponseEntity> LayChucDanh(string MaCauHinh)
        {
            try
            {
                IEnumerable<CauHinh> lstChucDanh= await _cauHinhRepository.GetAllAsync();

                lstChucDanh = lstChucDanh.Where(n=>n.MaThuocTinh== MaCauHinh);

                return new ResponseEntity(StatusCodeConstants.OK, lstChucDanh);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}