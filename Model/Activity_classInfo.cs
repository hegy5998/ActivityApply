/*
 * 檔案位置: Model\Activity_classInfo.cs
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("activity_class")]
    public partial class Activity_classInfo
    {
        /// <summary>
        ///  
        /// </summary>
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        [Column("ac_idn")]
        public Int32 Ac_idn { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("ac_title")]
        public String Ac_title { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("ac_desc")]
        public String Ac_desc { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("ac_seq")]
        public Int32? Ac_seq { get; set; }

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
    }
}
