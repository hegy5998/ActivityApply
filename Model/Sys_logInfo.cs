/*
 * 檔案位置: Model\Sys_logInfo.cs
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("sys_log")]
    public partial class Sys_logInfo
    {
        /// <summary>
        /// 序號 
        /// </summary>
        [Key]
        [Column("log_idn")]
        public String Log_idn { get; set; }

        /// <summary>
        /// 發生時間 
        /// </summary>
        [Column("log_date")]
        public DateTime? Log_date { get; set; }

        /// <summary>
        /// 使用者帳號 
        /// </summary>
        [Column("log_user")]
        public String Log_user { get; set; }

        /// <summary>
        /// 使用者作業系統 
        /// </summary>
        [Column("log_os")]
        public String Log_os { get; set; }

        /// <summary>
        /// 使用者瀏覽器 
        /// </summary>
        [Column("log_browser")]
        public String Log_browser { get; set; }

        /// <summary>
        /// 錯誤網址 
        /// </summary>
        [Column("log_page")]
        public String Log_page { get; set; }

        /// <summary>
        /// 使用者IP 
        /// </summary>
        [Column("log_ip")]
        public String Log_ip { get; set; }

        /// <summary>
        /// 錯誤內容 
        /// </summary>
        [Column("log_content")]
        public String Log_content { get; set; }

        /// <summary>
        /// 錯誤類型 
        /// </summary>
        [Column("log_type")]
        public String Log_type { get; set; }

        /// <summary>
        /// 伺服器IP 
        /// </summary>
        [Column("log_servip")]
        public String Log_servip { get; set; }

        /// <summary>
        /// 錯誤描述標題 
        /// </summary>
        [Column("log_subject")]
        public String Log_subject { get; set; }
    }
}
