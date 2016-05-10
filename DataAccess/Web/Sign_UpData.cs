using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;
using Model;
using DataAccess;
using System.Data;

namespace DataAccess.Web
{
    public class Sign_UpData : BaseData
    {
        CommonDbHelper Db = DAH.Db;
        Activity_sectionData _sectionData = new Activity_sectionData();
        Activity_columnData _columnData = new Activity_columnData();
        Activity_applyData _applyData = new Activity_applyData();
        Activity_apply_detailData _apply_detailData = new Activity_apply_detailData();

        #region 查詢
        public List<Activity_sectionInfo> GetSectionList(int acs_act)
        {
            return _sectionData.GetList(acs_act);
        }
        public List<Activity_columnInfo> GetQuestionList(int acc_act, int as_idn)
        {
            return _columnData.GetList(acc_act,as_idn);
        }
        public DataTable GetActivityData(int as_act, int as_idn)
        {
            string sql = @" SELECT  activity.act_title, activity.act_unit,
                                    activity.act_contact_name, activity.act_contact_phone, 
                                    activity.act_short_link, 
                                    activity_session.as_title, activity_session.as_position, 
                                    activity_session.as_date_start, activity_session.as_date_end
                            FROM    activity, activity_session
                            WHERE   activity.act_idn = activity_session.as_act AND as_idn = @as_idn AND as_act = @as_act";
            IDataParameter[] param = { Db.GetParam("@as_act", as_act), Db.GetParam("@as_idn", as_idn) };
            return Db.GetDataTable(sql, param);
        }
        public DataTable GetApplyProve(int aa_idn)
        {
            string sql = @" SELECT  activity.act_title, 
                                    activity_session.as_title, 
                                    activity_session.as_date_start, 
                                    activity_session.as_date_end, 
                                    activity_apply.aa_name, 
                                    activity_apply.updtime,
                                    activity_apply.aa_email
                            FROM    activity, activity_session, activity_apply
                            WHERE   activity.act_idn = activity_session.as_act AND
		                            activity_session.as_idn = activity_apply.aa_as AND
		                            activity_session.as_act = activity_apply.aa_act AND
		                            activity_apply.aa_idn = @aa_idn;";
            IDataParameter[] param = { Db.GetParam("@aa_idn", aa_idn) };
            return Db.GetDataTable(sql, param);
        }
        public List<Activity_sessionInfo> isBetweenApplyDate(int as_idn)
        {
            string sql = @" SELECT  activity_session.*
                            FROM    activity_session
                            WHERE   CONVERT(varchar(256), GETDATE(), 121) > activity_session.as_apply_start AND
							        CONVERT(varchar(256), GETDATE(), 121) < activity_session.as_apply_end AND
		                            activity_session.as_idn = @as_idn;";
            IDataParameter[] param = { Db.GetParam("@as_idn", as_idn) };
            return Db.GetEnumerable<Activity_sessionInfo>(sql, param).ToList();
        }
        public List<Activity_sessionInfo> isOpen(int as_idn)
        {
            string sql = @" SELECT  activity_session.*
                            FROM    activity_session
                            WHERE   activity_session.as_isopen = 1 AND
		                            activity_session.as_idn = @as_idn;";
            IDataParameter[] param = { Db.GetParam("@as_idn", as_idn) };
            return Db.GetEnumerable<Activity_sessionInfo>(sql, param).ToList();
        }
        public List<Activity_sessionInfo> isFull(int aa_as)
        {
            string sql = @" SELECT as_idn 
                            FROM activity_session 
                            cross apply 
                                (SELECT COUNT(*) as num 
                                FROM activity_apply 
                                WHERE aa_as = @aa_as ) as apply 
                            WHERE as_idn = @aa_as 
                            GROUP BY as_num_limit,apply.num,as_idn 
                            HAVING apply.num < as_num_limit;";
            IDataParameter[] param = { Db.GetParam("@aa_as", aa_as) };
            return Db.GetEnumerable<Activity_sessionInfo>(sql, param).ToList();
        }
        public List<Activity_applyInfo> isRepeatApply(int aa_as, string aa_email, string aa_name)
        {
            string sql = @" SELECT  activity_apply.*
                            FROM    activity_apply
                            WHERE   activity_apply.aa_as = @aa_as AND
                                    activity_apply.aa_email = @aa_email AND
                                    activity_apply.aa_name = @aa_name;";
            IDataParameter[] param = { Db.GetParam("@aa_as", aa_as),
                                        Db.GetParam("@aa_email", aa_email),
                                        Db.GetParam("@aa_name", aa_name)};
            return Db.GetEnumerable<Activity_applyInfo>(sql, param).ToList();
        }
        #endregion

        #region 新增
        public CommonResult InsertData_apply(Dictionary<string, object> data_dict, IDbTransaction trans = null, bool checkDataRepeat = true, Sys_accountInfo loginUser = null)
        {
            Type _modelType = typeof(Activity_applyInfo);
            var res = Db.ValidatePreInsert(_modelType, trans, data_dict, checkDataRepeat);
            if (res.IsSuccess)
            {
                string sql = @"
                    insert into [" + _modelType.GetTableName() + @"] 
                        (" + Db.GetSqlInsertField(_modelType, data_dict) + @", [createtime], [updtime]) 
                    values (" + Db.GetSqlInsertValue(data_dict) + ", (" + Db.DbNowTimeSQL + "), (" + Db.DbNowTimeSQL + ")" + @")
                    select @@identity";
                res.Message = Db.ExecuteScalar(sql, Db.GetParam(_modelType, data_dict)).ToString();
                if (res.Message.Equals("")) res.IsSuccess = false;
            }
            return res;
        }

        public CommonResult InsertData_apply_detail(Dictionary<string, object> data_dict, IDbTransaction trans = null, bool checkDataRepeat = true, Sys_accountInfo loginUser = null)
        {
            Type _modelType = typeof(Activity_apply_detailInfo);
            var res = Db.ValidatePreInsert(_modelType, trans, data_dict, checkDataRepeat);
            if (res.IsSuccess)
            {
                string sql = @"
                    insert into [" + _modelType.GetTableName() + @"] 
                        (" + Db.GetSqlInsertField(_modelType, data_dict) + @") 
                    values (" + Db.GetSqlInsertValue(data_dict) + ")";
                res.AffectedRows = Db.ExecuteNonQuery(trans, sql, Db.GetParam(_modelType, data_dict));
                if (res.AffectedRows <= 0) res.IsSuccess = false;
            }
            return res;
        }
        #endregion
    }
}
