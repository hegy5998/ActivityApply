/*
 * 檔案位置: Model\Activity_columnInfo.cs
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("activity_column")]
    public partial class Activity_columnInfo
    {
        /// <summary>
        ///  
        /// </summary>
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        [Column("acc_idn")]
        public Int32 Acc_idn { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("acc_asc")]
        public Int32? Acc_asc { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("acc_act")]
        public Int32? Acc_act { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("acc_title")]
        public String Acc_title { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("acc_desc")]
        public String Acc_desc { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("acc_seq")]
        public Int32? Acc_seq { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("acc_type")]
        public String Acc_type { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("acc_option")]
        public String Acc_option { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("acc_required")]
        public Byte? Acc_required { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("acc_validation")]
        public String Acc_validation { get; set; }

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
