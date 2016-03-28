using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;
using Model;
using DataAccess;
using System.Data;

namespace DataAccess.S01
{
    public class UCAccountRoleManagerData
    {
        #region 取得職位列表
        /// <summary>
        /// 取得職位列表
        /// </summary>
        /// <param name="sys_rid">角色代碼</param>
        /// <param name="sys_uid">單位代碼</param>
        /// <returns></returns>
        public List<Sys_role_positionInfo> GetRpidList(string sys_rid, string sys_uid)
        {
            var db = DAH.Db;
            string sql = @"
                select t.sys_rid, t.sys_uid, t.sys_rpid, t.sys_rpname
                from (
	                select 1 typ, rp.*
	                from sys_role_position rp
	                where rp.sys_rid = @sys_rid and rp.sys_uid = '" + AuthData.GlobalSymbol + @"'
	                union
	                select 2 typ, rp.*
	                from sys_role_position rp
	                where rp.sys_rid = @sys_rid and rp.sys_uid = @sys_uid and rp.sys_uid <> '" + AuthData.GlobalSymbol + @"') t
                order by t.typ, t.sys_seq";

            var param_lst = new List<IDataParameter>() { 
                db.GetParam("@sys_rid", sys_rid),
                db.GetParam("@sys_uid", sys_uid)
            };

            var lst = db.GetEnumerable<Sys_role_positionInfo>(sql, param_lst.ToArray()).ToList();
            return lst;
        }
        #endregion
    }
}
