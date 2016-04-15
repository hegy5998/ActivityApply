using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace DataAccess.S01
{
    public class UCProcessAuthManagerData :BaseData
    {
        #region 取得作業的權限資料
        /// <summary>
        /// 取得作業的權限資料
        /// </summary>
        /// <param name="sys_pid">作業代碼</param>
        /// <returns></returns>
        public List<Model.S01.UCProcessAuthManagerInfo.Main> GetList(string sys_pid)
        {
            var db = DAH.Db;
            string sql = @"
                select pr.sys_pid, pr.sys_rid, r.sys_rname, pr.sys_uid, u.sys_uname, pr.sys_rpid, isnull(rp.sys_rpname, '不分') sys_rpname, pr.sys_modify
                from sys_process_role pr
                inner join sys_role r on r.sys_rid = pr.sys_rid
                inner join sys_unit u on u.sys_uid = pr.sys_uid
                left join sys_role_position rp on rp.sys_rid = pr.sys_rid and rp.sys_uid = pr.sys_uid and rp.sys_rpid = pr.sys_rpid
                where pr.sys_pid = @sys_pid";

            var lst = db.GetEnumerable<Model.S01.UCProcessAuthManagerInfo.Main>(sql, db.GetParam("@sys_pid", sys_pid)).ToList();

            return lst;
        }
        #endregion
    }
}
