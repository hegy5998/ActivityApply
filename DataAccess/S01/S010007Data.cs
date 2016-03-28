using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Model;
using Util;
using DataAccess;

namespace DataAccess.S01
{
    public class S010007Data
    {
        public CommonDbHelper Db = DAH.Db;

        #region 取得有使用某單位的角色數量
        /// <summary>
        /// 取得有使用某單位的角色數量
        /// </summary>
        /// <param name="sys_uid">單位代碼</param>
        /// <returns></returns>
        public int GetRoleUnitCount(string sys_uid)
        {
            string sql = @"
                select count(distinct sys_rid)
                from sys_role_unit
                where sys_uid = @sys_uid";

            var param_lst = new List<IDataParameter>() { 
                Db.GetParam("@sys_uid", sys_uid)
            };

            var obj = Db.ExecuteScalar(sql, param_lst.ToArray());
            return CommonConvert.GetIntOrZero(obj);
        }
        #endregion
    }
}
