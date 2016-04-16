/*
 * 檔案位置: DataAccess\Activity_sessionData.cs
 */

using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Util;

namespace DataAccess
{
    public class S020101Data : BaseData
    {
        /// <summary>
        /// 對應的資料庫
        /// </summary>
        public CommonDbHelper Db = DAH.Db;

        //取得已發佈活動資料
        public DataTable GetAlreadyList()
        {
            StringBuilder sql_sb = new StringBuilder();

            sql_sb.Append(@"
                SELECT act_idn, as_idn, as_isopen, act_title, as_title, as_num_limit, CONVERT(char(10), as_date_start, 111) as_date_start, SUBSTRING(CONVERT(char(8), as_date_start, 108), 0, 6) as_date_starttime, CONVERT(char(10), as_date_end, 111) as_date_end, SUBSTRING(CONVERT(char(8), as_date_end, 108), 0, 6) as_date_endtime, CONVERT(char(10), as_apply_start, 111) as_apply_start, SUBSTRING(CONVERT(char(8), as_apply_start, 108), 0, 6) as_apply_starttime, CONVERT(char(10), as_apply_end, 111) as_apply_end, SUBSTRING(CONVERT(char(8), as_apply_end, 108), 0, 6) as_apply_endtime, COUNT(aa_idn) as_num
                FROM activity, activity_session
                LEFT JOIN activity_apply
                ON aa_as = as_idn
                WHERE as_act = act_idn AND
                      as_isopen = 1 AND
                      CONVERT(datetime, as_date_end, 111) >= CONVERT(varchar, GETDATE(), 111)
                GROUP BY act_idn, as_idn, as_isopen, act_title, as_title, as_num_limit, as_date_start, as_date_end, as_apply_start, as_apply_end
                ORDER BY act_title");

            return Db.GetDataTable(sql_sb.ToString());
        }

        //取得未發佈活動資料
        public DataTable GetReadyList()
        {
            StringBuilder sql_sb = new StringBuilder();

            sql_sb.Append(@"
                SELECT act_idn, as_idn, as_isopen, act_title, as_title, as_num_limit, CONVERT(char(10), as_date_start, 111) as_date_start, SUBSTRING(CONVERT(char(8), as_date_start, 108), 0, 6) as_date_starttime, CONVERT(char(10), as_date_end, 111) as_date_end, SUBSTRING(CONVERT(char(8), as_date_end, 108), 0, 6) as_date_endtime, CONVERT(char(10), as_apply_start, 111) as_apply_start, SUBSTRING(CONVERT(char(8), as_apply_start, 108), 0, 6) as_apply_starttime, CONVERT(char(10), as_apply_end, 111) as_apply_end, SUBSTRING(CONVERT(char(8), as_apply_end, 108), 0, 6) as_apply_endtime, COUNT(aa_idn) as_num
                FROM activity, activity_session
                LEFT JOIN activity_apply
                ON aa_as = as_idn
                WHERE as_act = act_idn AND
                      as_isopen = 0 AND
                      CONVERT(datetime, as_date_end, 111) >= CONVERT(varchar, GETDATE(), 111)
                GROUP BY act_idn, as_idn, as_isopen, act_title, as_title, as_num_limit, as_date_start, as_date_end, as_apply_start, as_apply_end
                ORDER BY act_title");

            return Db.GetDataTable(sql_sb.ToString());
        }

        //取得已結束活動資料
        public DataTable GetEndList()
        {
            StringBuilder sql_sb = new StringBuilder();

            sql_sb.Append(@"
                SELECT as_idn, act_title, as_title, as_num_limit, CONVERT(char(10), as_date_start, 111) as_date_start, SUBSTRING(CONVERT(char(8), as_date_start, 108), 0, 6) as_date_starttime, CONVERT(char(10), as_date_end, 111) as_date_end, SUBSTRING(CONVERT(char(8), as_date_end, 108), 0, 6) as_date_endtime, CONVERT(char(10), as_apply_start, 111) as_apply_start, SUBSTRING(CONVERT(char(8), as_apply_start, 108), 0, 6) as_apply_starttime, CONVERT(char(10), as_apply_end, 111) as_apply_end, SUBSTRING(CONVERT(char(8), as_apply_end, 108), 0, 6) as_apply_endtime, COUNT(aa_idn) as_num
                FROM activity, activity_session
                LEFT JOIN activity_apply
                ON aa_as = as_idn
                WHERE as_act = act_idn AND
	                  CONVERT(datetime, as_date_end, 111) <= CONVERT(varchar, GETDATE(), 111)
                GROUP BY as_idn, act_title, as_title, as_num_limit, as_date_start, as_date_end, as_apply_start, as_apply_end
                ORDER BY act_title");

            return Db.GetDataTable(sql_sb.ToString());
        }

        //取得修改資料
        public DataTable GetEditData(int i)
        {
            StringBuilder sql_sb = new StringBuilder();

            sql_sb.Append(@"
                SELECT act_idn, act_title, act_desc, act_unit, act_contact_name, act_contact_phone, act_relate_file, act_relate_link, as_idn, as_act, as_date_start, as_date_end, as_apply_start, as_apply_end, as_position, as_gmap, as_num_limit, as_seq, as_title, as_isopen
                FROM activity, activity_session
                WHERE as_act = act_idn AND
	                  act_idn = @i");

            //修改資料的key
            var param_lst = new List<IDataParameter>() {
                Db.GetParam("@i", i),
            };

            return Db.GetDataTable(sql_sb.ToString(), param_lst.ToArray());
        }

        //取得刪除資料(若此活動已沒有場次則連活動一起刪除)
        public DataTable CheckDelData(int i)
        {
            StringBuilder sql_sb = new StringBuilder();

            sql_sb.Append(@"
                SELECT COUNT(*) as_number
                FROM activity, activity_session
                WHERE act_idn = as_act AND
	                  act_idn = @i");

            //欲刪除場次的活動序號
            var param_lst = new List<IDataParameter>() {
                Db.GetParam("@i", i),
            };

            return Db.GetDataTable(sql_sb.ToString(), param_lst.ToArray());
        }

        //取得活動與場次的發佈資訊(若有一場次發佈活動就發佈)
        public DataTable CheckisopenData(int i)
        {
            StringBuilder sql_sb = new StringBuilder();

            sql_sb.Append(@"
                SELECT COUNT(*) as_isopen
                FROM activity, activity_session
                WHERE act_idn = as_act AND
	                  act_idn = @i");

            //欲發佈場次的活動序號
            var param_lst = new List<IDataParameter>() {
                Db.GetParam("@i", i),
            };

            return Db.GetDataTable(sql_sb.ToString(), param_lst.ToArray());
        }

        //取得報名資料
        public DataTable GetApplyData(int i)
        {
            StringBuilder sql_sb = new StringBuilder();

            sql_sb.Append(@"
                SELECT act_idn, act_title, aa_idn, aa_name, aa_email, aad_apply_id, aad_col_id, aad_val, acc_idn, acc_asc, acc_title, acs_idn, acs_title, as_idn, as_title
                FROM activity, activity_apply, activity_apply_detail, activity_column, activity_section, activity_session
                WHERE as_act = act_idn AND
	                  aa_act = act_idn AND
	                  acc_asc = acs_idn AND
	                  acc_act = act_idn AND
	                  acs_act = act_idn AND
	                  aa_as = as_idn AND
	                  aad_apply_id = aa_idn AND
	                  aad_col_id = acc_idn AND
	                  act_idn = @i");

            //欲取得報名資料的場次序號
            var param_lst = new List<IDataParameter>() {
                Db.GetParam("@i", i),
            };

            return Db.GetDataTable(sql_sb.ToString(), param_lst.ToArray());
        }
    }
}
