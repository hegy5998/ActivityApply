using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable]
    public partial class Activity_sectionInfo
    {
        /// <summary>
        /// 系統代碼
        /// </summary>
        [Column("as_title")]
        public string As_title { get; set; }
    }
}
