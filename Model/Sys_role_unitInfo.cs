/*
 * 檔案位置: Model\Sys_role_unitInfo.cs
 * 程式碼產生時間: 2015/09/03 11:26:25
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("sys_role_unit")]
    public partial class Sys_role_unitInfo
    {
        /// <summary>
        /// 角色代碼 
        /// </summary>
        [Key]
        [Column("sys_rid")]
        public String Sys_rid { get; set; }

        /// <summary>
        /// 單位代碼 
        /// </summary>
        [Key]
        [Column("sys_uid")]
        public String Sys_uid { get; set; }

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
