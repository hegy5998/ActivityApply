using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.S01
{
    public class UCProcessSubFuncAuthManagerInfo
    {
        public class Main
        {
            public string Sys_pid { get; set; }
            public string Sys_cid { get; set; }
            public string Sys_cnote { get; set; }
            public int? Auth_count { get; set; }
        }

        public class Auth
        {
            public string Sys_pid { get; set; }
            public string Sys_cid { get; set; }
            public string Sys_rid { get; set; }
            public string Sys_rname { get; set; }
            [Required(ErrorMessage = "缺少對應的[單位]資料!")]
            public string Sys_uid { get; set; }
            public string Sys_uname { get; set; }
            [Required(ErrorMessage = "缺少對應的[職位]資料!")]
            public string Sys_rpid { get; set; }
            public string Sys_rpname { get; set; }
        }
    }
}
