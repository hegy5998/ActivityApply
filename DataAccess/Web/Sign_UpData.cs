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

        public List<Activity_columnInfo> GetQuestionList(int acc_act)
        {
            return _columnData.GetList(acc_act);
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
