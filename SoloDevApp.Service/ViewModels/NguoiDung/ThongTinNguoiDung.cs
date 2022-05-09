using SoloDevApp.Repository.Models;
using System;
using System.Collections.Generic;

namespace SoloDevApp.Service.ViewModels
{
    public class ThongTinNguoiDung  
    {
        public int Id { get; set; }
        public int ChucDanh { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string DienThoai { get; set; }
        public string NgaySinh { get; set; }
        public int CapBac { get; set; }
        public int GioiTinh { get; set; }
        public string QuocGia { get; set; }
        public string DiaChi { get; set; }
        public string Facebook { get; set; }
        public string Github { get; set; }
        public string Linkedin { get; set; }
        public double Rank { get; set; }
        public DateTime NgayCapNhat { get; set; }
        public int TrangThai { get; set; }

        public List<ChungChi> ChungChi { get; set; }
        public List<DuAnViewModel> DuAn { get; set; }
        public List<HocVan> HocVan { get; set; }
        public List<KinhNghiem> KinhNghiem { get; set; }
        public List<KyNangMem> KyNangMem { get; set; }
        public List<HoSo_Skill> HoSo_Skill { get; set; }
    }
}