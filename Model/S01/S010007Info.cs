using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Model.S01
{
    public class S010007Info
    {
        public class Main
        {
            [Required(ErrorMessage = "[單位代碼]不可為空白")]
            public string Sys_uid { get; set; }
            [Required(ErrorMessage = "[單位名稱]不可為空白")]
            public string Sys_uname { get; set; }
        }
    }
}
