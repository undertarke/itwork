using System;
using System.Collections.Generic;

namespace SoloDevApp.Repository.Models
{
    public class DuAn
    {
        public int Id { get; set; }
        public string TenDuAn { get; set; }
        public string Skill { get; set; }
        public int SoThanhVien { get; set; }
        public string LinkDemo { get; set; }
        public string MoTa { get; set; }
        public int NguoiDungId { get; set; }
    }
}