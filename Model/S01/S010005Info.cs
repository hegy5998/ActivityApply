using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Model.S01
{
    public class S010005Info
    {
        public class Main
        {
            public string Sys_mid { get; set; }
            [Required(ErrorMessage = "[作業代碼]不可為空白")]
            public string Sys_pid { get; set; }
            [Required(ErrorMessage = "[作業名稱]不可為空白")]
            public string Sys_pname { get; set; }
            public string Sys_purl { get; set; }
            public int? Sys_seq { get; set; }
            public string Sys_show { get; set; }
            public string Sys_enable { get; set; }
        }

        public class SubControl
        {
            public string Sys_pid { get; set; }
            [Required(ErrorMessage = "[子功能名稱]不可為空白!")]
            public string Sys_cid { get; set; }
            [Required(ErrorMessage = "[子功能描述]不可為空白!")]
            public string Sys_cnote { get; set; }
        }
    }
}
