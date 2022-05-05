using System;

namespace SoloDevApp.Repository.Models
{
    public class ChungChi
    {
        public int Id { get; set; }
        public string TenChungChi { get; set; }
        public string ToChuc { get; set; }
        public int Nam { get; set; }
        public string LinkChungChi { get; set; }
        public int NguoiDungId { get; set; }
    }
}