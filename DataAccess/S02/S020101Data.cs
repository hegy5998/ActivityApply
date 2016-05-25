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
        ActivityData activityData = new ActivityData();
        Activity_columnData _columnda = new Activity_columnData();
        Activity_sectionData _sectionData = new Activity_sectionData();
        Activity_sessionData _sessionData = new Activity_sessionData();
        ActivityData _activityData = new ActivityData();
        Activity_apply_detailData _applyData = new Activity_apply_detailData();

        //Sys_accountInfo loginUser = CommonHelper.GetLoginUser();

        #region 取得活動資料
        public List<ActivityInfo> getActivity(int act_idn)
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
        #endregion

        #region 取得場次資料
        public List<Activity_sessionInfo> getSession(int as_act)
        {
            string sql = @"SELECT   activity_session.*
                           FROM    activity_session
                           WHERE   (as_act = @as_act) ";
            IDataParameter[] param = { Db.GetParam("@as_act", as_act) };
            return Db.GetEnumerable<Activity_sessionInfo>(sql, param).ToList();
        }
        #endregion

        #region 取得區塊資料
        public List<Activity_sectionInfo> getSection(int acs_act)
        {
            string sql = @"SELECT   activity_section.*
                           FROM    activity_section
                           WHERE   (acs_act = @acs_act) ";
            IDataParameter[] param = { Db.GetParam("@acs_act", acs_act) };
            return Db.GetEnumerable<Activity_sectionInfo>(sql, param).ToList();
        }
        #endregion

        #region 取得欄位資料
        public List<Activity_columnInfo> getColumn(int acc_act)
        {
            string sql = @"SELECT   activity_column.*
                           FROM    activity_column
                           WHERE   (acc_act = @acc_act) 
                            ORDER BY acc_seq,acc_asc";
            IDataParameter[] param = { Db.GetParam("@acc_act", acc_act) };
            return Db.GetEnumerable<Activity_columnInfo>(sql, param).ToList();
        }
        #endregion

        #region 修改報名表
        #region 刪除報名表欄位
        public CommonResult Delete_Column_Data(Dictionary<string, object> dict)
        {
            return _columnda.DeleteData(dict);
        }
        #endregion

        #region 刪除區塊
        public CommonResult Delete_Section_Data(Dictionary<string, object> dict)
        {
            return _sectionData.DeleteData(dict);
        }
        public int Delete_Session_apply_Data(string aa_as)
        {
            string sql = @" DELETE 
                        FROM activity_apply_detail
                        WHERE aad_apply_id 
                        IN (
                        SELECT aa_idn 
                        FROM activity_apply 
                        WHERE activity_apply_detail.aad_apply_id = aa_idn AND aa_as = @aa_as 
                        );
                        DELETE 
                        FROM activity_apply
                        WHERE aa_as = @aa_as ";
            IDataParameter[] param = { Db.GetParam("@aa_as", aa_as) };
            int res = Db.ExecuteNonQuery(sql, param);
            return res;
        }
        #endregion

        #region 刪除場次
        public CommonResult Delete_session(Dictionary<string, object> dict)
        {
            return _sessionData.DeleteData(dict);
        }
        #endregion

        #region 刪除報名欄位資料
        public int Delete_apply_detail(string aad_col_id)
        {
            string sql = @" DELETE 
                            FROM activity_apply_detail
                            WHERE aad_col_id = @aad_col_id ";
            IDataParameter[] param = { Db.GetParam("@aad_col_id", aad_col_id) };
            int res = Db.ExecuteNonQuery(sql, param);
            return res;
        }
        #endregion       

        #region 更新區塊
        public CommonResult Update_Section_Data(Dictionary<string, object> old_dict, Dictionary<string, object> new_dict)
        {
            return _sectionData.UpdateData(old_dict, new_dict);
        }
        #endregion

        #region 更新報名表
        public CommonResult Update_Column_Data(Dictionary<string, object> old_dict, Dictionary<string, object> new_dict)
        {
            return _columnda.UpdateData(old_dict, new_dict);
        }
        #endregion

        #region 更新場次
        public CommonResult Update_Session_Data(Dictionary<string, object> old_dict, Dictionary<string, object> new_dict)
        {
            return _sessionData.UpdateData(old_dict, new_dict);
        }
        #endregion

        #region 更新活動
        public CommonResult Update_Activity_Data(Dictionary<string, object> old_dict, Dictionary<string, object> new_dict)
        {
            return _activityData.UpdateData(old_dict, new_dict);
        }
        #endregion

        #region 新增區塊
        public CommonResult InsertData_Activity_Section(Dictionary<string, object> dict)
        {
            return _sectionData.InsertData(dict);
        }
        #endregion

        #region 新增報名表欄位
        public CommonResult InsertData_Activity_Column(Dictionary<string, object> dict)
        {
            return _columnda.InsertData(dict);
        }
        #endregion

        #region 新增場次
        public CommonResult InsertData_session(Dictionary<string, object> dict)
        {
            return _sessionData.InsertData(dict);
        }
        #endregion
        #endregion

        #region 取得已發佈活動資料
        public DataTable GetAlreadyList()
        {
            StringBuilder sql_sb = new StringBuilder();
            Sys_accountInfo loginUser = CommonHelper.GetLoginUser();

            if (loginUser.Act_id == "S001")
            {
                sql_sb.Append(@"
                SELECT act_idn, as_idn, as_isopen, act_title, as_title, as_num_limit,as_short_link, CONVERT(char(10), as_date_start, 111) as_date_start, SUBSTRING(CONVERT(char(8), as_date_start, 108), 0, 6) as_date_starttime, CONVERT(char(10), as_date_end, 111) as_date_end, SUBSTRING(CONVERT(char(8), as_date_end, 108), 0, 6) as_date_endtime, CONVERT(char(10), as_apply_start, 111) as_apply_start, SUBSTRING(CONVERT(char(8), as_apply_start, 108), 0, 6) as_apply_starttime, CONVERT(char(10), as_apply_end, 111) as_apply_end, SUBSTRING(CONVERT(char(8), as_apply_end, 108), 0, 6) as_apply_endtime, COUNT(aa_idn) as_num, act_isopen, activity_session.createid
                FROM activity, activity_session
                LEFT JOIN activity_apply
                ON aa_as = as_idn
                WHERE as_act = act_idn AND
                      as_isopen = 1 AND
                      CONVERT(datetime, as_date_end, 121) >= CONVERT(varchar, GETDATE(), 121)
                GROUP BY act_idn, as_idn, as_isopen, act_title, as_title, as_num_limit, as_date_start, as_date_end, as_apply_start, as_apply_end,as_short_link, act_isopen, activity_session.createid");
            }
            else
            {
                sql_sb.Append(@"
                    (SELECT act_idn, as_idn, as_isopen, act_title, as_title, as_num_limit,as_short_link, CONVERT(char(10), as_date_start, 111) as_date_start, SUBSTRING(CONVERT(char(8), as_date_start, 108), 0, 6) as_date_starttime, CONVERT(char(10), as_date_end, 111) as_date_end, SUBSTRING(CONVERT(char(8), as_date_end, 108), 0, 6) as_date_endtime, CONVERT(char(10), as_apply_start, 111) as_apply_start, SUBSTRING(CONVERT(char(8), as_apply_start, 108), 0, 6) as_apply_starttime, CONVERT(char(10), as_apply_end, 111) as_apply_end, SUBSTRING(CONVERT(char(8), as_apply_end, 108), 0, 6) as_apply_endtime, COUNT(aa_idn) as_num, act_isopen, activity_session.createid
                    FROM activity, activity_session
                    LEFT JOIN activity_apply
                    ON aa_as = as_idn
                    WHERE as_act = act_idn AND
                            as_isopen = 1 AND
                            CONVERT(datetime, as_date_end, 121) >= CONVERT(varchar, GETDATE(), 121) AND
                            activity_session.createid LIKE '" + loginUser.Act_id + @"'
                    GROUP BY act_idn, as_idn, as_isopen, act_title, as_title, as_num_limit, as_date_start, as_date_end, as_apply_start, as_apply_end, act_isopen, activity_session.createid)
                    UNION
                    (SELECT act_idn, as_idn, as_isopen, act_title, as_title, as_num_limit, CONVERT(char(10), as_date_start, 111) as_date_start, SUBSTRING(CONVERT(char(8), as_date_start, 108), 0, 6) as_date_starttime, CONVERT(char(10), as_date_end, 111) as_date_end, SUBSTRING(CONVERT(char(8), as_date_end, 108), 0, 6) as_date_endtime, CONVERT(char(10), as_apply_start, 111) as_apply_start, SUBSTRING(CONVERT(char(8), as_apply_start, 108), 0, 6) as_apply_starttime, CONVERT(char(10), as_apply_end, 111) as_apply_end, SUBSTRING(CONVERT(char(8), as_apply_end, 108), 0, 6) as_apply_endtime, COUNT(aa_idn) as_num, act_isopen, activity_session.createid
                    FROM activity, account_copperate, activity_session
                    LEFT JOIN activity_apply
                    ON aa_as = as_idn
                    WHERE as_act = act_idn AND
                            as_isopen = 1 AND
                            CONVERT(datetime, as_date_end, 121) >= CONVERT(varchar, GETDATE(), 121) AND
		                    cop_act = act_idn AND
                            cop_id LIKE '" + loginUser.Act_id + @"'
                    GROUP BY act_idn, as_idn, as_isopen, act_title, as_title, as_num_limit, as_date_start, as_date_end, as_apply_start, as_apply_end,as_short_link, act_isopen, activity_session.createid)
                    ORDER BY act_idn");
            }
            
            return Db.GetDataTable(sql_sb.ToString());
        }
        #endregion

        #region 取得未發佈活動資料
        public DataTable GetReadyList()
        {
            StringBuilder sql_sb = new StringBuilder();
            Sys_accountInfo loginUser = CommonHelper.GetLoginUser();

            if (loginUser.Act_id == "S001")
            {
                sql_sb.Append(@"
                SELECT act_idn, as_idn, as_isopen, act_title, as_title, as_num_limit,as_short_link, CONVERT(char(10), as_date_start, 111) as_date_start, SUBSTRING(CONVERT(char(8), as_date_start, 108), 0, 6) as_date_starttime, CONVERT(char(10), as_date_end, 111) as_date_end, SUBSTRING(CONVERT(char(8), as_date_end, 108), 0, 6) as_date_endtime, CONVERT(char(10), as_apply_start, 111) as_apply_start, SUBSTRING(CONVERT(char(8), as_apply_start, 108), 0, 6) as_apply_starttime, CONVERT(char(10), as_apply_end, 111) as_apply_end, SUBSTRING(CONVERT(char(8), as_apply_end, 108), 0, 6) as_apply_endtime, COUNT(aa_idn) as_num, act_isopen, activity_session.createid
                FROM activity, activity_session
                LEFT JOIN activity_apply
                ON aa_as = as_idn
                WHERE as_act = act_idn AND
                      as_isopen = 0 AND
                      CONVERT(datetime, as_date_end, 121) >= CONVERT(varchar, GETDATE(), 121)
                GROUP BY act_idn, as_idn, as_isopen, act_title, as_title, as_num_limit,as_short_link, as_date_start, as_date_end, as_apply_start, as_apply_end, act_isopen, activity_session.createid");
            }
            else
            {
                sql_sb.Append(@"
                    (SELECT act_idn, as_idn, as_isopen, act_title, as_title, as_num_limit,as_short_link, CONVERT(char(10), as_date_start, 111) as_date_start, SUBSTRING(CONVERT(char(8), as_date_start, 108), 0, 6) as_date_starttime, CONVERT(char(10), as_date_end, 111) as_date_end, SUBSTRING(CONVERT(char(8), as_date_end, 108), 0, 6) as_date_endtime, CONVERT(char(10), as_apply_start, 111) as_apply_start, SUBSTRING(CONVERT(char(8), as_apply_start, 108), 0, 6) as_apply_starttime, CONVERT(char(10), as_apply_end, 111) as_apply_end, SUBSTRING(CONVERT(char(8), as_apply_end, 108), 0, 6) as_apply_endtime, COUNT(aa_idn) as_num, act_isopen, activity_session.createid
                    FROM activity, activity_session
                    LEFT JOIN activity_apply
                    ON aa_as = as_idn
                    WHERE as_act = act_idn AND
                          as_isopen = 0 AND
                          CONVERT(datetime, as_date_end, 121) >= CONVERT(varchar, GETDATE(), 121) AND
                          activity_session.createid LIKE '" + loginUser.Act_id + @"'
                    GROUP BY act_idn, as_idn, as_isopen, act_title, as_title, as_num_limit, as_date_start, as_date_end, as_apply_start, as_apply_end, act_isopen, activity_session.createid)
                    UNION
                    (SELECT act_idn, as_idn, as_isopen, act_title, as_title, as_num_limit, CONVERT(char(10), as_date_start, 111) as_date_start, SUBSTRING(CONVERT(char(8), as_date_start, 108), 0, 6) as_date_starttime, CONVERT(char(10), as_date_end, 111) as_date_end, SUBSTRING(CONVERT(char(8), as_date_end, 108), 0, 6) as_date_endtime, CONVERT(char(10), as_apply_start, 111) as_apply_start, SUBSTRING(CONVERT(char(8), as_apply_start, 108), 0, 6) as_apply_starttime, CONVERT(char(10), as_apply_end, 111) as_apply_end, SUBSTRING(CONVERT(char(8), as_apply_end, 108), 0, 6) as_apply_endtime, COUNT(aa_idn) as_num, act_isopen, activity_session.createid
                    FROM activity, account_copperate, activity_session
                    LEFT JOIN activity_apply
                    ON aa_as = as_idn
                    WHERE as_act = act_idn AND
                          as_isopen = 0 AND
                          CONVERT(datetime, as_date_end, 121) >= CONVERT(varchar, GETDATE(), 121) AND
		                  cop_act = act_idn AND
                          cop_id LIKE '" + loginUser.Act_id + @"'
                    GROUP BY act_idn, as_idn, as_isopen, act_title, as_title, as_num_limit,as_short_link, as_date_start, as_date_end, as_apply_start, as_apply_end, act_isopen, activity_session.createid)
                    ORDER BY act_idn");
            }

            return Db.GetDataTable(sql_sb.ToString());
        }
        #endregion

        #region 取得已結束活動資料
        public DataTable GetEndList()
        {
            StringBuilder sql_sb = new StringBuilder();
            Sys_accountInfo loginUser = CommonHelper.GetLoginUser();

            if (loginUser.Act_id == "S001")
            {
                sql_sb.Append(@"
                SELECT as_idn, act_title, as_title, as_num_limit, CONVERT(char(10), as_date_start, 111) as_date_start, SUBSTRING(CONVERT(char(8), as_date_start, 108), 0, 6) as_date_starttime, CONVERT(char(10), as_date_end, 111) as_date_end, SUBSTRING(CONVERT(char(8), as_date_end, 108), 0, 6) as_date_endtime, CONVERT(char(10), as_apply_start, 111) as_apply_start, SUBSTRING(CONVERT(char(8), as_apply_start, 108), 0, 6) as_apply_starttime, CONVERT(char(10), as_apply_end, 111) as_apply_end, SUBSTRING(CONVERT(char(8), as_apply_end, 108), 0, 6) as_apply_endtime, COUNT(aa_idn) as_num, act_idn
                FROM activity, activity_session
                LEFT JOIN activity_apply
                ON aa_as = as_idn
                WHERE as_act = act_idn AND
	                  CONVERT(datetime, as_date_end, 121) <= CONVERT(varchar, GETDATE(), 121)
                GROUP BY as_idn, act_title, as_title, as_num_limit, as_date_start, as_date_end, as_apply_start, as_apply_end, act_idn
                ORDER BY act_title");
            }
            else
            {
                sql_sb.Append(@"
                    SELECT as_idn, act_title, as_title, as_num_limit, CONVERT(char(10), as_date_start, 111) as_date_start, SUBSTRING(CONVERT(char(8), as_date_start, 108), 0, 6) as_date_starttime, CONVERT(char(10), as_date_end, 111) as_date_end, SUBSTRING(CONVERT(char(8), as_date_end, 108), 0, 6) as_date_endtime, CONVERT(char(10), as_apply_start, 111) as_apply_start, SUBSTRING(CONVERT(char(8), as_apply_start, 108), 0, 6) as_apply_starttime, CONVERT(char(10), as_apply_end, 111) as_apply_end, SUBSTRING(CONVERT(char(8), as_apply_end, 108), 0, 6) as_apply_endtime, COUNT(aa_idn) as_num, act_idn
                    FROM activity, activity_session
                    LEFT JOIN activity_apply
                    ON aa_as = as_idn
                    WHERE as_act = act_idn AND
	                      CONVERT(datetime, as_date_end, 121) <= CONVERT(varchar, GETDATE(), 121) AND
                          activity_session.createid LIKE '" + loginUser.Act_id + @"'
                    GROUP BY as_idn, act_title, as_title, as_num_limit, as_date_start, as_date_end, as_apply_start, as_apply_end, act_idn
                    ORDER BY act_title");
            }

            return Db.GetDataTable(sql_sb.ToString());
        }
        #endregion

        #region 取得修改活動資料
//        public DataTable GetEditData(int i)
//        {
//            StringBuilder sql_sb = new StringBuilder();

//            sql_sb.Append(@"
//                SELECT act_idn, act_title, act_desc, act_unit, act_contact_name, act_contact_phone, act_relate_file, act_relate_link, as_idn, as_act, as_date_start, as_date_end, as_apply_start, as_apply_end, as_position, as_gmap, as_num_limit, as_seq, as_title, as_isopen
//                FROM activity, activity_session
//                WHERE as_act = act_idn AND
//	                  act_idn = @i");

//            //修改資料的key
//            var param_lst = new List<IDataParameter>() {
//                Db.GetParam("@i", i),
//            };

//            return Db.GetDataTable(sql_sb.ToString(), param_lst.ToArray());
//        }
        #endregion

        #region 取得刪除資料(若此活動已沒有場次則連活動一起刪除)
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
        #endregion

        #region 取得活動與場次的發佈資訊(若有一場次發佈活動就發佈)
        public DataTable CheckCloseData(int i)
        {
            StringBuilder sql_sb = new StringBuilder();

            sql_sb.Append(@"
                SELECT act_idn, act_isopen, as_idn, as_isopen
                FROM activity, activity_session
                WHERE act_idn = as_act AND
	                  act_idn = @i");

            //欲發佈場次的活動序號
            var param_lst = new List<IDataParameter>() {
                Db.GetParam("@i", i),
            };

            return Db.GetDataTable(sql_sb.ToString(), param_lst.ToArray());
        }
        #endregion

        #region 取得報名詳細資料(欄位資料)
        public DataTable GetApplyDataDetail(int i)
        {
            StringBuilder sql_sb = new StringBuilder();

            sql_sb.Append(@"
                SELECT act_idn, act_title, acc_idn, acc_title, as_idn, as_title, acc_type, acc_option, acc_required
                FROM activity, activity_column, activity_session
                WHERE acc_act = act_idn AND
	                  as_act = act_idn AND
	                  as_idn = @i
                ORDER BY acc_seq");

            //欲取得報名資料的場次序號
            var param_lst = new List<IDataParameter>() {
                Db.GetParam("@i", i),
            };

            return Db.GetDataTable(sql_sb.ToString(), param_lst.ToArray());
        }
        #endregion

        #region 取得報名詳細欄位實際值
        public DataTable GetApplyDataColumn(int col, int asidn)
        {
            StringBuilder sql_sb = new StringBuilder();

            sql_sb.Append(@"
                SELECT aa_idn, aad_val, activity_apply.createtime
                FROM activity, activity_apply, activity_apply_detail, activity_column, activity_session
                WHERE as_act = act_idn AND
	                  aa_as = as_idn AND	  
	                  aad_apply_id = aa_idn AND
	                  aad_col_id = acc_idn AND
                      acc_act = act_idn AND
	                  as_idn = @asidn AND
	                  acc_idn = @col
                ORDER BY aa_idn");

            //欲取得欄位值的欄位序號
            var param_lst = new List<IDataParameter>() {
                Db.GetParam("@asidn", asidn),
                Db.GetParam("@col", col)
            };

            return Db.GetDataTable(sql_sb.ToString(), param_lst.ToArray());
        }
        #endregion

        #region 取得欲刪除的報名資料
        public DataTable GetApplyDeleteData(int asidn, int aa)
        {
            StringBuilder sql_sb = new StringBuilder();

            sql_sb.Append(@"
                SELECT aad_col_id
                FROM activity, activity_session, activity_column, activity_apply, activity_apply_detail
                WHERE as_act = act_idn AND
	                  aa_as = as_idn AND
	                  acc_act = act_idn AND
	                  aad_apply_id = aa_idn AND
	                  aad_col_id = acc_idn AND
	                  as_idn = @asidn AND
	                  aa_idn = @aa");

            //欲取得刪除的報名資料的場次以及報名序號
            var param_lst = new List<IDataParameter>() {
                Db.GetParam("@asidn", asidn),
                Db.GetParam("@aa", aa),
            };

            return Db.GetDataTable(sql_sb.ToString(), param_lst.ToArray());
        }
        #endregion

        #region 取得活動idn
        public DataTable Getactidn (int asidn)
        {
            StringBuilder sql_sb = new StringBuilder();

            sql_sb.Append(@"
                SELECT act_idn
                FROM activity, activity_session
                WHERE as_act = act_idn AND
	                  as_idn = @asidn");

            var param_lst = new List<IDataParameter>() {
                Db.GetParam("@asidn", asidn),
            };

            return Db.GetDataTable(sql_sb.ToString(), param_lst.ToArray());
        }
        #endregion

        #region 新增報名資料&取得key
        public DataTable GetApplyidn(Dictionary<string, object> data_dict)
        {
            StringBuilder sql_sb = new StringBuilder();
            Type _modelType = typeof(Activity_applyInfo);
            IDbTransaction trans = null;
            bool checkDataRepeat = true;
            Sys_accountInfo loginUser = null;

            var res = Db.ValidatePreInsert(_modelType, trans, data_dict, checkDataRepeat);
            if (res.IsSuccess)
            {
                string sql = @"
                    insert into [" + _modelType.GetTableName() + @"] 
                        (" + Db.GetSqlInsertField(_modelType, data_dict) + @", [createid], [createtime], [updid], [updtime]) 
                    values (" + Db.GetSqlInsertValue(data_dict) + ", '" + loginUser.Act_id + "'" + ", (" + Db.DbNowTimeSQL + ")" + ", '" + loginUser.Act_id + "'" + ", (" + Db.DbNowTimeSQL + ")" + ")"
                    + "SELECT LAST_INSERT_ID()";

                res.AffectedRows = Db.ExecuteNonQuery(trans, sql, Db.GetParam(_modelType, data_dict));
                if (res.AffectedRows <= 0) res.IsSuccess = false;
            }

            return Db.GetDataTable(sql_sb.ToString());
        }
        #endregion

        #region 取得活動名稱 & 場次名稱
        public DataTable Getactas(int i)
        {
            StringBuilder sql_sb = new StringBuilder();

            sql_sb.Append(@"
                SELECT act_title, as_title
                FROM activity, activity_session
                WHERE as_act = act_idn AND
                      as_idn = @i");

            //場次序號
            var param_lst = new List<IDataParameter>() {
                Db.GetParam("@i", i),
            };

            return Db.GetDataTable(sql_sb.ToString(), param_lst.ToArray());
        }
        #endregion

        #region 取得查詢資料
        //取得查詢資料(依分類)
        public DataTable GetQueryClassData(string keyword, int tab, string cl)
        {
            StringBuilder sql_sb = new StringBuilder();
            Sys_accountInfo loginUser = CommonHelper.GetLoginUser();

            if (cl == "全部")
            {
                if (loginUser.Act_id == "S001")
                {
                    sql_sb.Append(@"
                        SELECT as_idn, act_title, as_title, as_num_limit,as_short_link, CONVERT(char(10), as_date_start, 111) as_date_start, SUBSTRING(CONVERT(char(8), as_date_start, 108), 0, 6) as_date_starttime, CONVERT(char(10), as_date_end, 111) as_date_end, SUBSTRING(CONVERT(char(8), as_date_end, 108), 0, 6) as_date_endtime, CONVERT(char(10), as_apply_start, 111) as_apply_start, SUBSTRING(CONVERT(char(8), as_apply_start, 108), 0, 6) as_apply_starttime, CONVERT(char(10), as_apply_end, 111) as_apply_end, SUBSTRING(CONVERT(char(8), as_apply_end, 108), 0, 6) as_apply_endtime, COUNT(aa_idn) as_num, act_idn, ac_title, as_isopen, act_isopen, activity_session.createid
                        FROM activity, activity_class, activity_session
                        LEFT JOIN activity_apply
                        ON aa_as = as_idn
                        WHERE as_act = act_idn AND
		                      (act_title LIKE '%" + @keyword + @"%' OR
                              as_title LIKE '%" + @keyword + @"%') AND
		                      as_isopen = @tab AND
                              act_class = ac_idn AND
	                          CONVERT(datetime, as_date_end, 121) >= CONVERT(varchar, GETDATE(), 121)
                        GROUP BY as_idn, act_title, as_title, as_num_limit,as_short_link, as_date_start, as_date_end, as_apply_start, as_apply_end, act_idn, ac_title, as_isopen, act_isopen, activity_session.createid
                        ORDER BY act_title");
                }
                else
                {
                    sql_sb.Append(@"
                        (SELECT act_idn, as_idn, as_isopen, act_title, as_title, as_num_limit,as_short_link, CONVERT(char(10), as_date_start, 111) as_date_start, SUBSTRING(CONVERT(char(8), as_date_start, 108), 0, 6) as_date_starttime, CONVERT(char(10), as_date_end, 111) as_date_end, SUBSTRING(CONVERT(char(8), as_date_end, 108), 0, 6) as_date_endtime, CONVERT(char(10), as_apply_start, 111) as_apply_start, SUBSTRING(CONVERT(char(8), as_apply_start, 108), 0, 6) as_apply_starttime, CONVERT(char(10), as_apply_end, 111) as_apply_end, SUBSTRING(CONVERT(char(8), as_apply_end, 108), 0, 6) as_apply_endtime, COUNT(aa_idn) as_num, act_isopen, activity_session.createid
                        FROM activity, activity_class, activity_session
                        LEFT JOIN activity_apply
                        ON aa_as = as_idn
                        WHERE as_act = act_idn AND
		                      (act_title LIKE '%" + @keyword + @"%' OR
                              as_title LIKE '%" + @keyword + @"%') AND
                              as_isopen = @tab AND
                              act_class = ac_idn AND
                              CONVERT(datetime, as_date_end, 121) >= CONVERT(varchar, GETDATE(), 121) AND
                        GROUP BY act_idn, as_idn, as_isopen, act_title, as_title, as_num_limit,as_short_link, as_date_start, as_date_end, as_apply_start, as_apply_end, act_isopen, activity_session.createid)
                        UNION
                        (SELECT act_idn, as_idn, as_isopen, act_title, as_title, as_num_limit,as_short_link, CONVERT(char(10), as_date_start, 111) as_date_start, SUBSTRING(CONVERT(char(8), as_date_start, 108), 0, 6) as_date_starttime, CONVERT(char(10), as_date_end, 111) as_date_end, SUBSTRING(CONVERT(char(8), as_date_end, 108), 0, 6) as_date_endtime, CONVERT(char(10), as_apply_start, 111) as_apply_start, SUBSTRING(CONVERT(char(8), as_apply_start, 108), 0, 6) as_apply_starttime, CONVERT(char(10), as_apply_end, 111) as_apply_end, SUBSTRING(CONVERT(char(8), as_apply_end, 108), 0, 6) as_apply_endtime, COUNT(aa_idn) as_num, act_isopen, activity_session.createid
                        FROM activity, account_copperate, activity_session
                        LEFT JOIN activity_apply
                        ON aa_as = as_idn
                        WHERE as_act = act_idn AND
                              (act_title LIKE '%" + @keyword + @"%' OR
                              as_title LIKE '%" + @keyword + @"%') AND
                              as_isopen = @tab AND
                              CONVERT(datetime, as_date_end, 121) >= CONVERT(varchar, GETDATE(), 121) AND
		                      cop_act = act_idn AND
                              cop_id LIKE '" + loginUser.Act_id + @"'
                        GROUP BY act_idn, as_idn, as_isopen, act_title, as_title, as_num_limit,as_short_link, as_date_start, as_date_end, as_apply_start, as_apply_end, act_isopen, activity_session.createid)
                        ORDER BY act_idn");
                }
            }
            else
            {
                if (loginUser.Act_id == "S001")
                {
                    sql_sb.Append(@"
                        SELECT as_idn, act_title, as_title, as_num_limit,as_short_link, CONVERT(char(10), as_date_start, 111) as_date_start, SUBSTRING(CONVERT(char(8), as_date_start, 108), 0, 6) as_date_starttime, CONVERT(char(10), as_date_end, 111) as_date_end, SUBSTRING(CONVERT(char(8), as_date_end, 108), 0, 6) as_date_endtime, CONVERT(char(10), as_apply_start, 111) as_apply_start, SUBSTRING(CONVERT(char(8), as_apply_start, 108), 0, 6) as_apply_starttime, CONVERT(char(10), as_apply_end, 111) as_apply_end, SUBSTRING(CONVERT(char(8), as_apply_end, 108), 0, 6) as_apply_endtime, COUNT(aa_idn) as_num, act_idn, ac_title, as_isopen, act_isopen, activity_session.createid
                        FROM activity, activity_class, activity_session
                        LEFT JOIN activity_apply
                        ON aa_as = as_idn
                        WHERE as_act = act_idn AND
                              ac_title LIKE '" + @cl + @"' AND
		                      (act_title LIKE '%" + @keyword + @"%' OR
                              as_title LIKE '%" + @keyword + @"%') AND
		                      as_isopen = @tab AND
                              act_class = ac_idn AND
	                          CONVERT(datetime, as_date_end, 121) >= CONVERT(varchar, GETDATE(), 121)
                        GROUP BY as_idn, act_title, as_title, as_num_limit,as_short_link, as_date_start, as_date_end, as_apply_start, as_apply_end, act_idn, ac_title, as_isopen, act_isopen, activity_session.createid
                        ORDER BY act_title");
                }
                else
                {
                    sql_sb.Append(@"
                        (SELECT act_idn, as_idn, as_isopen, act_title, as_title, as_num_limit,as_short_link, CONVERT(char(10), as_date_start, 111) as_date_start, SUBSTRING(CONVERT(char(8), as_date_start, 108), 0, 6) as_date_starttime, CONVERT(char(10), as_date_end, 111) as_date_end, SUBSTRING(CONVERT(char(8), as_date_end, 108), 0, 6) as_date_endtime, CONVERT(char(10), as_apply_start, 111) as_apply_start, SUBSTRING(CONVERT(char(8), as_apply_start, 108), 0, 6) as_apply_starttime, CONVERT(char(10), as_apply_end, 111) as_apply_end, SUBSTRING(CONVERT(char(8), as_apply_end, 108), 0, 6) as_apply_endtime, COUNT(aa_idn) as_num, act_isopen, activity_session.createid
                        FROM activity, activity_class, activity_session
                        LEFT JOIN activity_apply
                        ON aa_as = as_idn
                        WHERE as_act = act_idn AND
                              ac_title LIKE '" + @cl + @"' AND
		                      (act_title LIKE '%" + @keyword + @"%' OR
                              as_title LIKE '%" + @keyword + @"%') AND
                              as_isopen = @tab AND
                              act_class = ac_idn AND
                              CONVERT(datetime, as_date_end, 121) >= CONVERT(varchar, GETDATE(), 121) AND
                              activity_session.createid LIKE '" + loginUser.Act_id + @"'
                        GROUP BY act_idn, as_idn, as_isopen, act_title, as_title, as_num_limit,as_short_link, as_date_start, as_date_end, as_apply_start, as_apply_end, act_isopen, activity_session.createid)
                        UNION
                        (SELECT act_idn, as_idn, as_isopen, act_title, as_title, as_num_limit,as_short_link, CONVERT(char(10), as_date_start, 111) as_date_start, SUBSTRING(CONVERT(char(8), as_date_start, 108), 0, 6) as_date_starttime, CONVERT(char(10), as_date_end, 111) as_date_end, SUBSTRING(CONVERT(char(8), as_date_end, 108), 0, 6) as_date_endtime, CONVERT(char(10), as_apply_start, 111) as_apply_start, SUBSTRING(CONVERT(char(8), as_apply_start, 108), 0, 6) as_apply_starttime, CONVERT(char(10), as_apply_end, 111) as_apply_end, SUBSTRING(CONVERT(char(8), as_apply_end, 108), 0, 6) as_apply_endtime, COUNT(aa_idn) as_num, act_isopen, activity_session.createid
                        FROM activity, account_copperate, activity_class, activity_session
                        LEFT JOIN activity_apply
                        ON aa_as = as_idn
                        WHERE as_act = act_idn AND
                              ac_title LIKE '" + @cl + @"' AND
		                      (act_title LIKE '%" + @keyword + @"%' OR
                              as_title LIKE '%" + @keyword + @"%') AND
                              as_isopen = @tab AND
                              CONVERT(datetime, as_date_end, 121) >= CONVERT(varchar, GETDATE(), 121) AND
		                      cop_act = act_idn AND
                              cop_id LIKE '" + loginUser.Act_id + @"'
                        GROUP BY act_idn, as_idn, as_isopen, act_title, as_title, as_num_limit,as_short_link, as_date_start, as_date_end, as_apply_start, as_apply_end, act_isopen, activity_session.createid)
                        ORDER BY act_idn");
                }
            }

            //關鍵字 & 頁籤
            var param_lst = new List<IDataParameter>() {
                Db.GetParam("@keyword", keyword),
                Db.GetParam("@tab", tab),
                Db.GetParam("@cl", cl),
            };

            return Db.GetDataTable(sql_sb.ToString(), param_lst.ToArray());
        }
        #endregion

        #region 取得已結束活動查詢資料
        //取得已結束活動查詢資料(依分類)
        public DataTable GetQueryEndClassData(string keyword, string cl)
        {
            StringBuilder sql_sb = new StringBuilder();
            Sys_accountInfo loginUser = CommonHelper.GetLoginUser();

            if (cl == "全部")
            {
                if (loginUser.Act_id == "S001")
                {
                    sql_sb.Append(@"
                        SELECT as_idn, act_title, as_title, as_num_limit, CONVERT(char(10), as_date_start, 111) as_date_start, SUBSTRING(CONVERT(char(8), as_date_start, 108), 0, 6) as_date_starttime, CONVERT(char(10), as_date_end, 111) as_date_end, SUBSTRING(CONVERT(char(8), as_date_end, 108), 0, 6) as_date_endtime, CONVERT(char(10), as_apply_start, 111) as_apply_start, SUBSTRING(CONVERT(char(8), as_apply_start, 108), 0, 6) as_apply_starttime, CONVERT(char(10), as_apply_end, 111) as_apply_end, SUBSTRING(CONVERT(char(8), as_apply_end, 108), 0, 6) as_apply_endtime, COUNT(aa_idn) as_num, act_idn, ac_title, as_isopen, act_isopen, activity_session.createid
                        FROM activity, activity_class, activity_session
                        LEFT JOIN activity_apply
                        ON aa_as = as_idn
                        WHERE as_act = act_idn AND
		                      (act_title LIKE '%" + @keyword + @"%' OR
                              as_title LIKE '%" + @keyword + @"%') AND
                              act_class = ac_idn AND
	                          CONVERT(datetime, as_date_end, 121) <= CONVERT(varchar, GETDATE(), 121)
                        GROUP BY as_idn, act_title, as_title, as_num_limit, as_date_start, as_date_end, as_apply_start, as_apply_end, act_idn, ac_title, as_isopen, act_isopen, activity_session.createid
                        ORDER BY act_title");
                }
                else
                {
                    sql_sb.Append(@"
                        (SELECT act_idn, as_idn, as_isopen, act_title, as_title, as_num_limit, CONVERT(char(10), as_date_start, 111) as_date_start, SUBSTRING(CONVERT(char(8), as_date_start, 108), 0, 6) as_date_starttime, CONVERT(char(10), as_date_end, 111) as_date_end, SUBSTRING(CONVERT(char(8), as_date_end, 108), 0, 6) as_date_endtime, CONVERT(char(10), as_apply_start, 111) as_apply_start, SUBSTRING(CONVERT(char(8), as_apply_start, 108), 0, 6) as_apply_starttime, CONVERT(char(10), as_apply_end, 111) as_apply_end, SUBSTRING(CONVERT(char(8), as_apply_end, 108), 0, 6) as_apply_endtime, COUNT(aa_idn) as_num, act_isopen, activity_session.createid
                        FROM activity, activity_class, activity_session
                        LEFT JOIN activity_apply
                        ON aa_as = as_idn
                        WHERE as_act = act_idn AND
                              ac_title LIKE '" + @cl + @"' AND
		                      (act_title LIKE '%" + @keyword + @"%' OR
                              as_title LIKE '%" + @keyword + @"%') AND
                              act_class = ac_idn AND
                              CONVERT(datetime, as_date_end, 121) <= CONVERT(varchar, GETDATE(), 121) AND
                              activity_session.createid LIKE '" + loginUser.Act_id + @"'
                        GROUP BY act_idn, as_idn, as_isopen, act_title, as_title, as_num_limit, as_date_start, as_date_end, as_apply_start, as_apply_end, act_isopen, activity_session.createid)
                        UNION
                        (SELECT act_idn, as_idn, as_isopen, act_title, as_title, as_num_limit, CONVERT(char(10), as_date_start, 111) as_date_start, SUBSTRING(CONVERT(char(8), as_date_start, 108), 0, 6) as_date_starttime, CONVERT(char(10), as_date_end, 111) as_date_end, SUBSTRING(CONVERT(char(8), as_date_end, 108), 0, 6) as_date_endtime, CONVERT(char(10), as_apply_start, 111) as_apply_start, SUBSTRING(CONVERT(char(8), as_apply_start, 108), 0, 6) as_apply_starttime, CONVERT(char(10), as_apply_end, 111) as_apply_end, SUBSTRING(CONVERT(char(8), as_apply_end, 108), 0, 6) as_apply_endtime, COUNT(aa_idn) as_num, act_isopen, activity_session.createid
                        FROM activity, account_copperate, activity_class, activity_session
                        LEFT JOIN activity_apply
                        ON aa_as = as_idn
                        WHERE as_act = act_idn AND
                              ac_title LIKE '" + @cl + @"' AND
		                      (act_title LIKE '%" + @keyword + @"%' OR
                              as_title LIKE '%" + @keyword + @"%') AND
                              CONVERT(datetime, as_date_end, 121) <= CONVERT(varchar, GETDATE(), 121) AND
		                      cop_act = act_idn AND
                              cop_id LIKE '" + loginUser.Act_id + @"'
                        GROUP BY act_idn, as_idn, as_isopen, act_title, as_title, as_num_limit, as_date_start, as_date_end, as_apply_start, as_apply_end, act_isopen, activity_session.createid)
                        ORDER BY act_idn");
                }
            }
            else
            {
                if (loginUser.Act_id == "S001") 
                {
                    sql_sb.Append(@"
                        SELECT as_idn, act_title, as_title, as_num_limit, CONVERT(char(10), as_date_start, 111) as_date_start, SUBSTRING(CONVERT(char(8), as_date_start, 108), 0, 6) as_date_starttime, CONVERT(char(10), as_date_end, 111) as_date_end, SUBSTRING(CONVERT(char(8), as_date_end, 108), 0, 6) as_date_endtime, CONVERT(char(10), as_apply_start, 111) as_apply_start, SUBSTRING(CONVERT(char(8), as_apply_start, 108), 0, 6) as_apply_starttime, CONVERT(char(10), as_apply_end, 111) as_apply_end, SUBSTRING(CONVERT(char(8), as_apply_end, 108), 0, 6) as_apply_endtime, COUNT(aa_idn) as_num, act_idn, ac_title, as_isopen, act_isopen, activity_session.createid
                        FROM activity, activity_class, activity_session
                        LEFT JOIN activity_apply
                        ON aa_as = as_idn
                        WHERE as_act = act_idn AND
                              ac_title LIKE '" + @cl + @"' AND
		                      (act_title LIKE '%" + @keyword + @"%' OR
                              as_title LIKE '%" + @keyword + @"%') AND
                              act_class = ac_idn AND
	                          CONVERT(datetime, as_date_end, 121) <= CONVERT(varchar, GETDATE(), 121)
                        GROUP BY as_idn, act_title, as_title, as_num_limit, as_date_start, as_date_end, as_apply_start, as_apply_end, act_idn, ac_title, as_isopen, act_isopen, activity_session.createid
                        ORDER BY act_title");
                }
                else
                {
                    sql_sb.Append(@"
                        SELECT as_idn, act_title, as_title, as_num_limit, CONVERT(char(10), as_date_start, 111) as_date_start, SUBSTRING(CONVERT(char(8), as_date_start, 108), 0, 6) as_date_starttime, CONVERT(char(10), as_date_end, 111) as_date_end, SUBSTRING(CONVERT(char(8), as_date_end, 108), 0, 6) as_date_endtime, CONVERT(char(10), as_apply_start, 111) as_apply_start, SUBSTRING(CONVERT(char(8), as_apply_start, 108), 0, 6) as_apply_starttime, CONVERT(char(10), as_apply_end, 111) as_apply_end, SUBSTRING(CONVERT(char(8), as_apply_end, 108), 0, 6) as_apply_endtime, COUNT(aa_idn) as_num, act_idn
                        FROM activity, activity_session
                        LEFT JOIN activity_apply
                        ON aa_as = as_idn
                        WHERE as_act = act_idn AND
                              ac_title LIKE '" + @cl + @"' AND
		                      (act_title LIKE '%" + @keyword + @"%' OR
                              as_title LIKE '%" + @keyword + @"%') AND
                              act_class = ac_idn AND
	                          CONVERT(datetime, as_date_end, 121) <= CONVERT(varchar, GETDATE(), 121) AND
                              activity_session.createid LIKE '" + loginUser.Act_id + @"'
                        GROUP BY as_idn, act_title, as_title, as_num_limit, as_date_start, as_date_end, as_apply_start, as_apply_end, act_idn
                        ORDER BY act_title");
                }
            }

            //關鍵字
            var param_lst = new List<IDataParameter>() {
                Db.GetParam("@keyword", keyword),
                Db.GetParam("@cl", cl),
            };

            return Db.GetDataTable(sql_sb.ToString(), param_lst.ToArray());
        }
        #endregion

        #region 取得多選的選項資料
        public DataTable GetMultiOption(int i)
        {
            StringBuilder sql_sb = new StringBuilder();

            sql_sb.Append(@"
                SELECT acc_option
                FROM activity_column
                WHERE acc_idn = @i");

            //欄位key
            var param_lst = new List<IDataParameter>() {
                Db.GetParam("@i", i),
            };

            return Db.GetDataTable(sql_sb.ToString(), param_lst.ToArray());
        }
        #endregion

        //取得場次的限制人數
        public DataTable GetApplyLimit(int i)
        {
            StringBuilder sql_sb = new StringBuilder();

            sql_sb.Append(@"
                SELECT as_num_limit
                FROM activity_session
                WHERE as_idn = @i");

            //場次idn
            var param_lst = new List<IDataParameter>() {
                Db.GetParam("@i", i),
            };

            return Db.GetDataTable(sql_sb.ToString(), param_lst.ToArray());
        }

        //取得email資料
        public DataTable GetEmailData ()
        {
            StringBuilder sql_sb = new StringBuilder();

            sql_sb.Append(@"
            SELECT aae_email
            FROM activity_apply_email");

            return Db.GetDataTable(sql_sb.ToString());
        }

        //取得帳號資料
        public DataTable GetAccountData()
        {
            StringBuilder sql_sb = new StringBuilder();

            sql_sb.Append(@"
            SELECT act_id
            FROM sys_account");

            return Db.GetDataTable(sql_sb.ToString());
        }

        //取得報名者的報名資料(activity_apply)
        public DataTable GetApplyData(int i)
        {
            StringBuilder sql_sb = new StringBuilder();

            sql_sb.Append(@"
                SELECT aa_idn, aa_name
                FROM activity, activity_session, activity_apply
                WHERE as_act = act_idn AND
		                aa_as = as_idn AND
	                    as_idn = @i
                ORDER BY aa_idn");

            //場次idn
            var param_lst = new List<IDataParameter>() {
                Db.GetParam("@i", i)};

            return Db.GetDataTable(sql_sb.ToString(), param_lst.ToArray());
        }

        //取得報名者的實際報名資料(activity_apply_detail)
        public DataTable GetApplyDetailData(int i)
        {
            StringBuilder sql_sb = new StringBuilder();

            sql_sb.Append(@"
                SELECT acc_title, aad_apply_id, aad_col_id, aad_val
                FROM activity, activity_column, activity_session, activity_apply_detail, activity_apply
                WHERE acc_act = act_idn AND
	                    as_act = act_idn AND
		                aad_apply_id = aa_idn AND
		                aad_col_id = acc_idn AND
	                    as_idn = @i
                ORDER BY acc_seq");

            //場次idn
            var param_lst = new List<IDataParameter>() {
                Db.GetParam("@i", i)};

            return Db.GetDataTable(sql_sb.ToString(), param_lst.ToArray());
        }

        //取得分類資料
        public DataTable GetClassData()
        {
            StringBuilder sql_sb = new StringBuilder();

            sql_sb.Append(@"
                SELECT ac_title, ac_idn
                FROM activity_class");

            return Db.GetDataTable(sql_sb.ToString());
        }
    }
}
