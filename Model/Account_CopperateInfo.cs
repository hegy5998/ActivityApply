/*
 * 檔案位置: Model\Account_copperateInfo.cs
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("account_copperate")]
    public partial class Account_copperateInfo
    {
        /// <summary>
        ///  
        /// </summary>
        [Key]
        [Column("cop_act")]
        public Int32 Cop_act { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Key]
        [Column("cop_id")]
        public String Cop_id { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("cop_authority")]
        public String Cop_authority { get; set; }

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
