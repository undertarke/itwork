using System;

namespace SoloDevApp.Repository.Models
{
    public class KinhNghiem
    {
        public int Id { get; set; }
        public string TenCongTy { get; set; }
        public string ChucDanh { get; set; }
        public string NgayBatDau { get; set; }
        public string NgayKetThuc { get; set; }
        public string MoTa { get; set; }
        public int NguoiDungId { get; set; }
    }
}