/*
 * 檔案位置: Model\Activity_apply_detailInfo.cs
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("activity_apply_detail")]
    public partial class Activity_apply_detailInfo
    {
        /// <summary>
        ///  
        /// </summary>
        [Key]
        [Column("aad_apply_id")]
        public Int32 Aad_apply_id { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Key]
        [Column("aad_col_id")]
        public Int32 Aad_col_id { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("aad_val")]
        public String Aad_val { get; set; }
    }
}
