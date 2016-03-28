using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Model.S01
{
    public class S010004Info
    {
        public class Main
        {
            [Required(ErrorMessage = "[系統代碼]不可為空白!")]
            public string Sys_id { get; set; }
            [Required(ErrorMessage="[模組代碼]不可為空白!")]
            public string Sys_mid { get; set; }
            [Required(ErrorMessage = "[模組名稱]不可為空白!")]
            public string Sys_mname { get; set; }
            public int? Sys_mseq { get; set; }
            public string Sys_enable { get; set; }
        }
    }
}
