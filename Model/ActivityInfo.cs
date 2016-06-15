/*
 * 檔案位置: Model\ActivityInfo.cs
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("activity")]
    public partial class ActivityInfo
    {
        /// <summary>
        ///  
        /// </summary>
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        [Column("act_idn")]
        public Int32 Act_idn { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("act_title")]
        public String Act_title { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("act_desc")]
        public String Act_desc { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("act_unit")]
        public String Act_unit { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("act_target")]
        public String Act_target { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("act_contact_name")]
        public String Act_contact_name { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("act_contact_phone")]
        public String Act_contact_phone { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("act_relate_file")]
        public String Act_relate_file { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("act_image")]
        public String Act_image { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("act_relate_link")]
        public String Act_relate_link { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("act_short_link")]
        public String Act_short_link { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("act_class")]
        public Int32? Act_class { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("act_num_limit")]
        public Int32? Act_num_limit { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("act_as")]
        public Int32? Act_as { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("act_isopen")]
        public Byte? Act_isopen { get; set; }

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
