using System;

namespace SoloDevApp.Repository.Models
{
    public class NguoiDung
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
    }
}