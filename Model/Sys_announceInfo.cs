/*
 * 檔案位置: Model\Sys_announceInfo.cs
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("sys_announce")]
    public partial class Sys_announceInfo
    {
        /// <summary>
        /// 序號 
        /// </summary>
        [Key]
        [Column("sys_no")]
        public String Sys_no { get; set; }

        /// <summary>
        /// 標題 
        /// </summary>
        [Column("sys_title")]
        public String Sys_title { get; set; }

        /// <summary>
        /// 是否啟用(Y:是,N:否) 
        /// </summary>
        [Column("sys_enable")]
        public String Sys_enable { get; set; }

        /// <summary>
        /// 網址 
        /// </summary>
        [Column("sys_url")]
        public String Sys_url { get; set; }

        /// <summary>
        /// 公告時間 
        /// </summary>
        [Column("sys_date")]
        public DateTime? Sys_date { get; set; }

        /// <summary>
        /// 建立人 
        /// </summary>
        [Column("createid")]
        public String Createid { get; set; }

        /// <summary>
        /// 建立時間 
        /// </summary>
        [Column("createtime")]
        public DateTime? Createtime { get; set; }

        /// <summary>
        /// 異動人 
        /// </summary>
        [Column("updid")]
        public String Updid { get; set; }

        /// <summary>
        /// 異動時間 
        /// </summary>
        [Column("updtime")]
        public DateTime? Updtime { get; set; }
    }
}
