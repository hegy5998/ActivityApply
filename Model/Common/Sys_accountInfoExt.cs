using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Serializable]
    public partial class Sys_accountInfo
    {
        #region 角色資訊
        public List<RoleInfo> Role_lst { get; set; }
        [Serializable]
        public class RoleInfo
        {
            /// <summary>
            /// 角色代碼
            /// </summary>
            public string Sys_rid { get; set; }
            /// <summary>
            /// 角色名稱
            /// </summary>
            public string Sys_rname { get; set; }
            /// <summary>
            /// 單位代碼
            /// </summary>
            public string Sys_uid { get; set; }
            /// <summary>
            /// 單位名稱
            /// </summary>
            public string Sys_uname { get; set; }
            /// <summary>
            /// 職位代碼
            /// </summary>
            public string Sys_rpid { get; set; }
            /// <summary>
            /// 職位名稱
            /// </summary>
            public string Sys_rpname { get; set; }
        }
        #endregion
    }
}
