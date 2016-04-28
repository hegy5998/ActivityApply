using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace DataAccess.S01
{
    

    public class S020104Data
    {

        Activity_classData _da = new Activity_classData();

        CommonDbHelper Db = DAH.Db;

        #region 取得特定模組或特定系統的作業
        /// <summary>
        /// 取得特定模組或特定系統的作業
        /// </summary>
        /// <param name="sys_id">指定系統代碼</param>
        /// <param name="sys_mid">指定模組代碼</param>
        /// <returns>資料</returns>
        public List<Model.S01.S010009Info.Main> GetListBySystemModule(string sys_id = "", string sys_mid = "")
        {
            var db = DAH.Db;
            var param = new List<IDataParameter>();
            StringBuilder sql_sb = new StringBuilder();
            sql_sb.Append(@"
                select p.*, m.sys_mname, s.sys_id, s.sys_name, 
                    (select count(*) from sys_process_role pr where pr.sys_pid = p.sys_pid) auth_count,
                    (select count(*) from sys_processcontrol pc where pc.sys_pid = p.sys_pid) sys_cid_count
                from sys_process p, sys_module m, sys_system s
                where p.sys_mid = m.sys_mid
	                and m.sys_id = s.sys_id");

            // 篩選系統代碼
            if (!sys_id.IsNullOrWhiteSpace())
            {
                sql_sb.Append(" and s.sys_id = @sys_id");
                param.Add(db.GetParam("@sys_id", sys_id));
            }

            // 篩選模組代碼
            if (!sys_mid.IsNullOrWhiteSpace())
            {
                sql_sb.Append(" and m.sys_mid = @sys_mid");
                param.Add(db.GetParam("@sys_mid", sys_mid));
            }

            sql_sb.Append(" order by s.sys_id, m.sys_mid, p.sys_pid");

            var lst = db.GetEnumerable<Model.S01.S010009Info.Main>(sql_sb.ToString(), param.ToArray()).ToList();

            return lst;
        }
        #endregion

        public DataTable GetSessionList(int as_act)
        {
            string sql = @"SELECT   activity_session.*,
	                            (SELECT COUNT(*) FROM activity_apply WHERE  aa_act = as_act AND aa_as = as_idn) AS apply_num
                           FROM activity_session
                           WHERE  (as_act = @as_act)
                           ORDER BY   as_idn";
            IDataParameter[] param = { Db.GetParam("@as_act", as_act) };
            return Db.GetDataTable(sql, param);
        }

        public List<ActivityInfo> GetActivityList(int act_idn)
        {
            string sql = @" SELECT   activity.*
                            FROM    activity
                            cross apply 
                                    (select top 1 COUNT(*) as num
                                    from activity_session 
                                    where   as_act = @act_idn 
				                            AND CONVERT(DATETIME, as_date_end, 121) >=  CONVERT(varchar(256), GETDATE(), 121)) as ac_session
                            WHERE   (act_idn = @act_idn)  AND ac_session.num > 0";
            IDataParameter[] param = { Db.GetParam("@act_idn", act_idn) };
            return Db.GetEnumerable<ActivityInfo>(sql, param).ToList();
        }

    }
}
