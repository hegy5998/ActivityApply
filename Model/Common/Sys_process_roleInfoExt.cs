using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class Sys_process_roleInfo
    {
        /// <summary>
        /// 系統代碼
        /// </summary>
        public string Sys_id { get; set; }
        /// <summary>
        /// 系統名稱
        /// </summary>
        public string Sys_name { get; set; }
        /// <summary>
        /// 模組代碼
        /// </summary>
        public string Sys_mid { get; set; }
        /// <summary>
        /// 模組名稱
        /// </summary>
        public string Sys_mname { get; set; }
        /// <summary>
        /// 系統名稱
        /// </summary>
        public string Sys_pname { get; set; }
        /// <summary>
        /// 子功能數量
        /// </summary>
        public int? Sys_cid_count { get; set; }
    }
}
