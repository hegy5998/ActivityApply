/*
 * 檔案位置: Model\Activity_applyInfo.cs
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("activity_apply")]
    public partial class Activity_applyInfo
    {
        /// <summary>
        ///  
        /// </summary>
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        [Column("aa_idn")]
        public Int32 Aa_idn { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("aa_act")]
        public Int32? Aa_act { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("aa_portal")]
        public String Aa_portal { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("aa_name")]
        public String Aa_name { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("aa_email")]
        public String Aa_email { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("createid")]
        public String Createid { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("createtime")]
        public DateTime? Createtime { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("updid")]
        public String Updid { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("updtime")]
        public DateTime? Updtime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("aa_as")]
        public int aa_as { get; set; }
    }
}
