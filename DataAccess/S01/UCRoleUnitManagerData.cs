using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Util;
using System.Data;

namespace DataAccess.S01
{
    public class UCRoleUnitManagerData
    {
        #region 取得角色對應單位資料
        /// <summary>
        /// 取得角色對應單位資料
        /// </summary>
        /// <param name="sys_rid">角色代碼</param>
        /// <returns></returns>
        public List<Model.S01.UCRoleUnitManagerInfo.Main> GetRoleUnitList(string sys_rid)
        {
            var db = DAH.Db;
            string sql = @"
                select ru.sys_rid, ru.sys_uid, u.sys_uname,
				    (select count(*) 
                        from sys_role_position rp 
                        where rp.sys_rid = ru.sys_rid 
                            and rp.sys_uid = ru.sys_uid) sys_rpid_count,
                    (select count(distinct act_id) 
                        from sys_account_role ar 
                        where ar.sys_rid = ru.sys_rid 
                            and ar.sys_uid = ru.sys_uid) act_count
                from sys_role_unit ru
                inner join sys_unit u on ru.sys_uid = u.sys_uid
                where ru.sys_rid = @sys_rid
                order by ru.sys_uid";

            var lst = db.GetEnumerable<Model.S01.UCRoleUnitManagerInfo.Main>(sql, db.GetParam("@sys_rid", sys_rid)).ToList();
            return lst;
        }
        #endregion

        #region 取得某角色單位，有多少帳號使用該身分
        /// <summary>
        /// 取得某角色單位，有多少帳號使用該身分
        /// </summary>
        /// <param name="sys_rid">角色代碼</param>
        /// <param name="sys_uid">單位代碼</param>
        /// <returns></returns>
        public int GetActRoleUnitCount(string sys_rid, string sys_uid)
        {
            var db = DAH.Db;
            string sql = @"
                select count(distinct act_id)
                from sys_account_role
                where sys_rid = @sys_rid
                    and sys_uid = @sys_uid";

            var param_lst = new List<IDataParameter>() { 
                db.GetParam("@sys_rid", sys_rid),
                db.GetParam("@sys_uid", sys_uid)
            };

            var obj = db.ExecuteScalar(sql, param_lst.ToArray());
            return CommonConvert.GetIntOrZero(obj);
        }
        #endregion

        #region 取得某角色單位設定的職位數
        /// <summary>
        /// 取得某角色單位設定的職位數
        /// </summary>
        /// <param name="sys_rid">角色代碼</param>
        /// <param name="sys_uid">單位代碼</param>
        /// <returns></returns>
        public int GetRoleUnitPositionCount(string sys_rid, string sys_uid)
        {
            var db = DAH.Db;
            string sql = @"
                select count(*)
                from sys_role_position
                where sys_rid = @sys_rid
                    and sys_uid = @sys_uid";

            var param_lst = new List<IDataParameter>() { 
                db.GetParam("@sys_rid", sys_rid),
                db.GetParam("@sys_uid", sys_uid)
            };

            var obj = db.ExecuteScalar(sql, param_lst.ToArray());
            return CommonConvert.GetIntOrZero(obj);
        }
        #endregion
    }
}
