using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Model.S01
{
    public class S010006Info
    {
        public class Main
        {
            [Required(ErrorMessage = "[角色代碼]不可為空白")]
            public string Sys_rid { get; set; }
            [Required(ErrorMessage = "[角色名稱]不可為空白")]
            public string Sys_rname { get; set; }
            [Required(ErrorMessage = "[角色說明]不可為空白")]
            public string Sys_rnote { get; set; }
            public int? Act_count { get; set; }
            public int? Sys_uid_count { get; set; }
            public int? Common_position_count { get; set; }
        }
    }
}
