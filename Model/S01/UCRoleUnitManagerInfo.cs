using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Model.S01
{
    public class UCRoleUnitManagerInfo
    {
        public class Main
        {
            public string Sys_rid { get; set; }
            [Required(ErrorMessage="請選擇[單位]")]
            public string Sys_uid { get; set; }
            public string Sys_uname { get; set; }
            public int? Sys_rpid_count { get; set; }
            public int? Act_count { get; set; }
        }
    }
}
