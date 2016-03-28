/*
 * 檔案位置: Model\Sys_processcontrolInfo.cs
 * 程式碼產生時間: 2015/09/03 11:26:25
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("sys_processcontrol")]
    public partial class Sys_processcontrolInfo
    {
        /// <summary>
        /// 作業代碼 
        /// </summary>
        [Key]
        [Column("sys_pid")]
        public String Sys_pid { get; set; }

        /// <summary>
        /// 子功能代碼 
        /// </summary>
        [Key]
        [Column("sys_cid")]
        public String Sys_cid { get; set; }

        /// <summary>
        /// 子功能描述 
        /// </summary>
        [Column("sys_cnote")]
        public String Sys_cnote { get; set; }

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
