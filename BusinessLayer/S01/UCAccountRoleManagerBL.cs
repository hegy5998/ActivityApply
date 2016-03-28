using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;
using Model;
using DataAccess;

namespace BusinessLayer.S01
{
    public class UCAccountRoleManagerBL
    {
        DataAccess.S01.UCAccountRoleManagerData _da = new DataAccess.S01.UCAccountRoleManagerData();

        #region 取得特定角色單位的職位資料列表(含通用職位)
        /// <summary>
        /// 取得特定角色單位的職位資料列表
        /// </summary>
        /// <param name="sys_rid">角色代碼</param>
        /// <param name="sys_uid">單位代碼</param>
        /// <returns></returns>
        public List<Sys_role_positionInfo> GetRpidList(string sys_rid, string sys_uid)
        {
            return _da.GetRpidList(sys_rid, sys_uid);
        }
        #endregion
    }
}
