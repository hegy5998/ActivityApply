using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.S01
{
    public class S010003Info
    {
        public class Main
        {
            [Required(ErrorMessage = "[系統代碼]不可為空白!")]
            public String Sys_id { get; set; }
            [Required(ErrorMessage = "[系統名稱]不可為空白!")]
            public String Sys_name { get; set; }
            public String Sys_url { get; set; }
            public String Sys_menuimg { get; set; }
            public String Sys_bannerimg { get; set; }
            public Int32? Sys_seq { get; set; }
            public String Sys_enable { get; set; }
        }
    }
}
