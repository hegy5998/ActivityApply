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
    public class S010006Data
    {
        public CommonDbHelper Db = DAH.Db;

        #region 取得角色清單
        /// <summary>
        /// 取得角色清單
        /// </summary>
        /// <returns></returns>
        public List<Model.S01.S010006Info.Main> GetRoleList()
        {
            string sql = @"
                select r.sys_rid, r.sys_rname, r.sys_rnote,
	                (select count(distinct act_id) from sys_account_role ar where ar.sys_rid = r.sys_rid) act_count,
	                (select count(*) from sys_role_unit ru where ru.sys_rid = r.sys_rid) sys_uid_count,
	                (select count(*) from sys_role_position rp where rp.sys_rid = r.sys_rid and rp.sys_uid = '" + AuthData.GlobalSymbol + @"') common_position_count
                from sys_role r";

            var lst = Db.GetEnumerable<Model.S01.S010006Info.Main>(sql).ToList();
            return lst;
        }
        #endregion

        #region 取得角色的單位資料筆數
        /// <summary>
        /// 取得角色的單位資料筆數
        /// </summary>
        /// <param name="sys_rid">角色代碼</param>
        /// <returns></returns>
        public int GetRoleUnitCount(string sys_rid)
        {
            string sql = @"
                select count(*)
                from sys_role_unit
                where sys_rid = @sys_rid";

            var param_lst = new List<IDataParameter>() { 
                Db.GetParam("@sys_rid", sys_rid)
            };

            var obj = Db.ExecuteScalar(sql, param_lst.ToArray());
            return CommonConvert.GetIntOrZero(obj);
        }
        #endregion

        #region 取得角色的通用職位資料筆數
        /// <summary>
        /// 取得角色的通用職位資料筆數
        /// </summary>
        /// <param name="sys_rid">角色代碼</param>
        /// <returns></returns>
        public int GetRoleCommonPositionCount(string sys_rid)
        {
            string sql = @"
                select count(*)
                from sys_role_position
                where sys_rid = @sys_rid";

            var param_lst = new List<IDataParameter>() { 
                Db.GetParam("@sys_rid", sys_rid)
            };

            var obj = Db.ExecuteScalar(sql, param_lst.ToArray());
            return CommonConvert.GetIntOrZero(obj);
        }
        #endregion
    }
}
