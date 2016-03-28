using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.S01
{
    public class UCAccountRoleManagerInfo
    {
        [Serializable]
        public class Main
        {
            public string Sys_rid { get; set; }
            public string Sys_rname { get; set; }
            public string Sys_uid { get; set; }
            public string Sys_uname { get; set; }
            public List<string> RolePosition_lst { get; set; }
            public Main()
            {
                RolePosition_lst = new List<string>();
            }
        }
    }
}
