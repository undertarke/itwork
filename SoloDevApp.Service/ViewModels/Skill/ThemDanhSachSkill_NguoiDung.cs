using System;
using System.Collections.Generic;

namespace SoloDevApp.Service.ViewModels
{
    public class ThemDanhSachSkill_NguoiDung
    {
        public int NguoiDungId { get; set; }
        public List<NguoiDung_SkillViewModel> lstSkill { get;set; }
    }
}