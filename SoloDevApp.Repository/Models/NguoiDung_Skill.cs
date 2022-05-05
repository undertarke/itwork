using System;

namespace SoloDevApp.Repository.Models
{
    public class NguoiDung_Skill
    {
        public int Id { get; set; }
        public int NguoiDungId { get; set; }
        public int IdSkill { get; set; }
        public int CapDo { get; set; }

    }
}