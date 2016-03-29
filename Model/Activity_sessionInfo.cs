/*
 * 檔案位置: Model\Activity_sessionInfo.cs
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("activity_session")]
    public partial class Activity_sessionInfo
    {
        /// <summary>
        ///  
        /// </summary>
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        [Column("as_idn")]
        public Int32 As_idn { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("as_act")]
        public Int32 As_act { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("as_date_start")]
        public DateTime? As_date_start { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("as_date_end")]
        public DateTime? As_date_end { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("as_apply_start")]
        public DateTime? As_apply_start { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("as_apply_end")]
        public DateTime? As_apply_end { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("as_position")]
        public String As_position { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("as_gmap")]
        public String As_gmap { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("as_num_limit")]
        public Int32 As_num_limit { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("as_seq")]
        public Int32? As_seq { get; set; }

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
        [Column("as_title")]
        public String As_title { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("as_isopen")]
        public Byte? As_isopen { get; set; }
    }
}
