/*
 * 檔案位置: Model\Activity_sectionInfo.cs
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("activity_section")]
    public partial class Activity_sectionInfo
    {
        /// <summary>
        ///  
        /// </summary>
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        [Column("acs_idn")]
        public Int32 Acs_idn { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("acs_act")]
        public Int32? Acs_act { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("acs_title")]
        public String Acs_title { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("acs_desc")]
        public String Acs_desc { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("acs_seq")]
        public Int32? Acs_seq { get; set; }

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
