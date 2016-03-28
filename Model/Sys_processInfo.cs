/*
 * 檔案位置: Model\Sys_processInfo.cs
 * 程式碼產生時間: 2015/09/03 11:26:24
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("sys_process")]
    public partial class Sys_processInfo
    {
        /// <summary>
        /// 模組代碼 
        /// </summary>
        [Column("sys_mid")]
        public String Sys_mid { get; set; }

        /// <summary>
        /// 作業代碼 
        /// </summary>
        [Key]
        [Column("sys_pid")]
        public String Sys_pid { get; set; }

        /// <summary>
        /// 作業名稱 
        /// </summary>
        [Column("sys_pname")]
        public String Sys_pname { get; set; }

        /// <summary>
        /// 作業網址 
        /// </summary>
        [Column("sys_purl")]
        public String Sys_purl { get; set; }

        /// <summary>
        /// 順序 
        /// </summary>
        [Column("sys_seq")]
        public Int32? Sys_seq { get; set; }

        /// <summary>
        /// 是否啟用(Y:是,N:否) 
        /// </summary>
        [Column("sys_enable")]
        public String Sys_enable { get; set; }

        /// <summary>
        /// 是否顯示在選單上(Y:是,N:否) 
        /// </summary>
        [Column("sys_show")]
        public String Sys_show { get; set; }

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
