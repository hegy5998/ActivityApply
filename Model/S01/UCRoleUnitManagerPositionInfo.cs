using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Model.S01
{
    public class UCRoleUnitManagerPositionInfo
    {
        public class PositionSetting
        {
            /// <summary>
            /// 通用職位的最小代碼
            /// </summary>
            public static int MinCommonPositionId = 1;
            /// <summary>
            /// 通用職位的最大代碼
            /// </summary>
            public static int MaxCommonPositionId = 49;
            /// <summary>
            /// 特定單位職位的最小代碼
            /// </summary>
            public static int MinUnitPositionId = 51;
            /// <summary>
            /// 特定單位職位的最大代碼
            /// </summary>
            public static int MaxUnitPositionId = 99;
        }

        public class Main
        {
            public string Sys_rid { get; set; }
            public string Sys_uid { get; set; }
            [Required(ErrorMessage="[職位代碼]不可為空白!")]
            public string Sys_rpid { get; set; }
            [Required(ErrorMessage = "[職位名稱]不可為空白!")]
            public string Sys_rpname { get; set; }
            public int? Sys_seq { get; set; }
            public int? Act_count { get; set; }
        }

        public class RoleUnit
        {
            public string Sys_rid { get; set; }
            public string Sys_rname { get; set; }
            public string Sys_uid { get; set; }
            public string Sys_uname { get; set; }
        }
    }
}
