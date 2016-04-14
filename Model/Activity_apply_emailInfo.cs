﻿/*
 * 檔案位置: Model\Activity_apply_emailInfo.cs
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("activity_apply_email")]
    public partial class Activity_apply_emailInfo
    {
        /// <summary>
        ///  
        /// </summary>
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        [Column("aae_id")]
        public Int32 Aae_id { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("aae_email")]
        public String Aae_email { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("aae_password")]
        public String Aae_password { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("createtime")]
        public DateTime? Createtime { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("updtime")]
        public DateTime? Updtime { get; set; }
    }
}