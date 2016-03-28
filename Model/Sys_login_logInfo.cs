/*
 * 檔案位置: Model\Sys_login_logInfo.cs
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("sys_login_log")]
    public partial class Sys_login_logInfo
    {
        /// <summary>
        /// 流水號 
        /// </summary>
        [Key]
        [Column("no")]
        public String No { get; set; }

        /// <summary>
        /// 紀錄時間 
        /// </summary>
        [Column("logtime")]
        public DateTime? Logtime { get; set; }

        /// <summary>
        /// 使用者IP 
        /// </summary>
        [Column("cnt_ip")]
        public String Cnt_ip { get; set; }

        /// <summary>
        /// 伺服器IP 
        /// </summary>
        [Column("srv_ip")]
        public String Srv_ip { get; set; }

        /// <summary>
        /// 使用者帳號 
        /// </summary>
        [Column("act_id")]
        public String Act_id { get; set; }

        /// <summary>
        /// 登入作業代碼 
        /// </summary>
        [Column("sys_pid")]
        public String Sys_pid { get; set; }

        /// <summary>
        /// 狀態(Y:正常, X:失敗) 
        /// </summary>
        [Column("status")]
        public String Status { get; set; }

        /// <summary>
        /// 備註 
        /// </summary>
        [Column("note")]
        public String Note { get; set; }
    }
}
