/*
 * 檔案位置: Model\Sys_moduleInfo.cs
 * 程式碼產生時間: 2015/09/03 11:26:24
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("sys_module")]
    public partial class Sys_moduleInfo
    {
        /// <summary>
        /// 系統代碼 
        /// </summary>
        [Column("sys_id")]
        public String Sys_id { get; set; }

        /// <summary>
        /// 模組代碼 
        /// </summary>
        [Key]
        [Column("sys_mid")]
        public String Sys_mid { get; set; }

        /// <summary>
        /// 模組名稱 
        /// </summary>
        [Column("sys_mname")]
        public String Sys_mname { get; set; }

        /// <summary>
        /// 順序 
        /// </summary>
        [Column("sys_mseq")]
        public Int32? Sys_mseq { get; set; }

        /// <summary>
        /// 是否啟用(Y:是,N:否) 
        /// </summary>
        [Column("sys_enable")]
        public String Sys_enable { get; set; }

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
