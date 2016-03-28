/*
 * 檔案位置: Model\Sys_accountInfo.cs
 * 程式碼產生時間: 2015/09/03 11:26:23
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("sys_account")]
    public partial class Sys_accountInfo
    {
        /// <summary>
        /// 帳號 
        /// </summary>
        [Key]
        [Column("act_id")]
        public String Act_id { get; set; }

        /// <summary>
        /// 密碼 
        /// </summary>
        [Column("act_pwd")]
        public String Act_pwd { get; set; }

        /// <summary>
        /// 名稱 
        /// </summary>
        [Column("act_name")]
        public String Act_name { get; set; }

        /// <summary>
        /// 生日 
        /// </summary>
        [Column("act_birth")]
        public DateTime? Act_birth { get; set; }

        /// <summary>
        /// 信箱 
        /// </summary>
        [Column("act_mail")]
        public String Act_mail { get; set; }

        /// <summary>
        /// 身分證字號 
        /// </summary>
        [Column("act_idno")]
        public String Act_idno { get; set; }

        /// <summary>
        /// 啟用狀態(Y:正常, X:停用) 
        /// </summary>
        [Column("act_status")]
        public String Act_status { get; set; }

        /// <summary>
        /// 上次密碼更新時間 
        /// </summary>
        [Column("act_pwd_date")]
        public DateTime? Act_pwd_date { get; set; }

        /// <summary>
        /// 密碼到期後登入次數 
        /// </summary>
        [Column("act_pwd_expire_login_times")]
        public Int32? Act_pwd_expire_login_times { get; set; }

        /// <summary>
        /// 預設登入角色 
        /// </summary>
        [Column("login_sys_rid")]
        public String Login_sys_rid { get; set; }

        /// <summary>
        /// 預設登入單位 
        /// </summary>
        [Column("login_sys_uid")]
        public String Login_sys_uid { get; set; }

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
