using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;
using System.Data;

namespace DataAccess.S01
{
    public class UCProcessSubFuncAuthManagerData
    {
        #region 取得特定作業的子功能列表
        /// <summary>
        /// 取得特定作業的子功能列表
        /// </summary>
        /// <param name="sys_pid">作業代碼</param>
        /// <returns></returns>
        public List<Model.S01.UCProcessSubFuncAuthManagerInfo.Main> GetList(string sys_pid)
        {
            var db = DAH.Db;
            string sql = @"
                select sys_pid, sys_cid, sys_cnote,
                    (select count(*) from sys_processcontrol_role pr 
                    where pr.sys_pid = c.sys_pid and pr.sys_cid = c.sys_cid) auth_count
                from sys_processcontrol c
                where sys_pid = @sys_pid
                order by sys_cid";

            var lst = db.GetEnumerable<Model.S01.UCProcessSubFuncAuthManagerInfo.Main>(sql, db.GetParam("@sys_pid", sys_pid)).ToList();
            return lst;
        }
        #endregion

        #region 取得特定作業子功能的權限清單
        /// <summary>
        /// 取得特定作業子功能的權限清單
        /// </summary>
        /// <param name="sys_pid">作業代碼</param>
        /// <param name="sys_cid">子功能代碼</param>
        /// <returns></returns>
        public List<Model.S01.UCProcessSubFuncAuthManagerInfo.Auth> GetAuthList(string sys_pid, string sys_cid)
        {
            var db = DAH.Db;
            string sql = @"
                select pr.sys_pid, pr.sys_cid, pr.sys_rid, r.sys_rname, pr.sys_uid, u.sys_uname, pr.sys_rpid, isnull(rp.sys_rpname, '不分') sys_rpname
                from sys_processcontrol_role pr
                inner join sys_role r on r.sys_rid = pr.sys_rid
                inner join sys_unit u on u.sys_uid = pr.sys_uid
                left join sys_role_position rp on rp.sys_rid = pr.sys_rid and rp.sys_rpid = pr.sys_rpid
                where pr.sys_pid = @sys_pid
                    and pr.sys_cid = @sys_cid";

            var param_lst = new List<IDataParameter>() { 
                db.GetParam("@sys_pid", sys_pid),
                db.GetParam("@sys_cid", sys_cid)
            };

            var lst = db.GetEnumerable<Model.S01.UCProcessSubFuncAuthManagerInfo.Auth>(sql, param_lst.ToArray()).ToList();
            return lst;
        }
        #endregion
    }
}
