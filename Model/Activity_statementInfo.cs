/*
 * 檔案位置: Model\Activity_statementInfo.cs
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("activity_statement")]
    public partial class Activity_statementInfo
    {
        /// <summary>
        ///  
        /// </summary>
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        [Column("ast_id")]
        public Int32 Ast_id { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("ast_title")]
        [Required(ErrorMessage = "[標題]不可為空白!")]
        public String Ast_title { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("ast_desc")]
        public String Ast_desc { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("ast_year")]
        public Int32 Ast_year { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("ast_month")]
        public Int32 Ast_month { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("ast_public")]
        public String Ast_public { get; set; }

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
        [Column("uptime")]
        public DateTime? Uptime { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Column("ast_content")]
        public String Ast_content { get; set; }
    }
}
