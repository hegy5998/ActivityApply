/*
 * 檔案位置: Model\Sys_systemInfo.cs
 * 程式碼產生時間: 2015/09/03 11:26:26
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("sys_system")]
    public partial class Sys_systemInfo
    {
        /// <summary>
        /// 系統代碼 
        /// </summary>
        [Key]
        [Column("sys_id")]
        public String Sys_id { get; set; }

        /// <summary>
        /// 系統名稱 
        /// </summary>
        [Column("sys_name")]
        public String Sys_name { get; set; }

        /// <summary>
        /// 網址 
        /// </summary>
        [Column("sys_url")]
        public String Sys_url { get; set; }

        /// <summary>
        /// 系統選單圖片檔名 
        /// </summary>
        [Column("sys_menuimg")]
        public String Sys_menuimg { get; set; }

        /// <summary>
        /// 系統banner檔名 
        /// </summary>
        [Column("sys_bannerimg")]
        public String Sys_bannerimg { get; set; }

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
