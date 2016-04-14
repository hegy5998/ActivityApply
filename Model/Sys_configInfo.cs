/*
 * 檔案位置: Model\Sys_configInfo.cs
 * 程式碼產生時間: 2015/09/03 11:26:24
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("sys_config")]
    public partial class Sys_configInfo
    {
        /// <summary>
        /// 參數名稱 
        /// </summary>
        [Key]
        [Column("sys_name")]
        public String Sys_name { get; set; }

        /// <summary>
        /// 參數描述 
        /// </summary>
        [Column("sys_note")]
        public String Sys_note { get; set; }

        /// <summary>
        /// 參數值 
        /// </summary>
        [Column("sys_value")]
        public String Sys_value { get; set; }

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
