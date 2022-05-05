using System;

namespace SoloDevApp.Repository.Models
{
    public class HocVan
    {
        public int Id { get; set; }
        public string TenTruong { get; set; }
        public string ChuyenNganh { get; set; }
        public int NamBatDau { get; set; }
        public int NamKetThuc { get; set; }
        public string TrinhDo { get; set; }
        public string MoTa { get; set; }
        public int NguoiDungId { get; set; }
    }
}