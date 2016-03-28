/*
 * 檔案位置: Model\Sys_roleInfo.cs
 * 程式碼產生時間: 2015/09/03 11:26:25
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("sys_role")]
    public partial class Sys_roleInfo
    {
        /// <summary>
        /// 角色代碼 
        /// </summary>
        [Key]
        [Column("sys_rid")]
        public String Sys_rid { get; set; }

        /// <summary>
        /// 角色名稱 
        /// </summary>
        [Column("sys_rname")]
        public String Sys_rname { get; set; }

        /// <summary>
        /// 角色描述 
        /// </summary>
        [Column("sys_rnote")]
        public String Sys_rnote { get; set; }

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
