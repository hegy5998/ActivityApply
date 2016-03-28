/*
 * 檔案位置: Model\Sys_processcontrol_roleInfo.cs
 * 程式碼產生時間: 2015/09/03 11:26:25
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("sys_processcontrol_role")]
    public partial class Sys_processcontrol_roleInfo
    {
        /// <summary>
        /// 角色代碼 
        /// </summary>
        [Key]
        [Column("sys_rid")]
        public String Sys_rid { get; set; }

        /// <summary>
        /// 單位代碼(*:表示全部單位) 
        /// </summary>
        [Key]
        [Column("sys_uid")]
        public String Sys_uid { get; set; }

        /// <summary>
        /// 職位代碼(*:表示全部職位) 
        /// </summary>
        [Key]
        [Column("sys_rpid")]
        public String Sys_rpid { get; set; }

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
