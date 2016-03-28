/*
 * 檔案位置: Model\Sys_loginldapInfo.cs
 * 程式碼產生時間: 2015/09/03 11:26:24
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("sys_loginldap")]
    public partial class Sys_loginldapInfo
    {
        /// <summary>
        /// 識別碼 
        /// </summary>
        [Key]
        [Column("id")]
        public String Id { get; set; }

        /// <summary>
        /// 使用者帳號 
        /// </summary>
        [Column("act_id")]
        public String Act_id { get; set; }

        /// <summary>
        /// 系統種類 
        /// </summary>
        [Column("sys_type")]
        public String Sys_type { get; set; }

        /// <summary>
        /// 時間 
        /// </summary>
        [Column("log_time")]
        public DateTime? Log_time { get; set; }
    }
}
