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
    public class SignSearchContextData : BaseData
    {
        private Type _modelType = typeof(Activity_apply_emailInfo);
        CommonDbHelper Db = DAH.Db;


        #region 查詢報名資訊(檢視報名資料)
        public DataTable GetActivityData(string aa_email)
        {
            string sql = @"SELECT activity.act_idn,activity.act_title,activity.act_class,
                                  activity_session.as_idn,activity_session.as_title,activity_session.as_date_end,activity_session.as_date_start,
		                          activity_apply.aa_name,activity_apply.aa_idn,activity_apply.updtime
                           From activity,activity_session,activity_apply
                           WHERE activity_apply.aa_act = activity.act_idn 
                           AND activity_apply.aa_as = activity_session.as_idn 
                           AND activity_apply.aa_email = @aa_email
                           ORDER BY activity_apply.updtime";
            IDataParameter[] param = { Db.GetParam("@aa_email", aa_email) };
            return Db.GetDataTable(sql, param);
        }
        #endregion

        #region 查詢報名資訊(寄送取消報名資料)
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


        #region 查詢報名欄位序號
        public DataTable GetColumnData(string acc_act)
        {
            string sql = @"SELECT  acc_idn
                           FROM    activity_column
                           WHERE   (acc_act = @acc_act)";
            IDataParameter[] param = { Db.GetParam("@acc_act", acc_act) };
            return Db.GetDataTable(sql, param);
        }
        #endregion

        #region 單筆刪除
        /// <summary>
        /// 單筆刪除
        /// </summary>
        /// <param name="data_dict">資料</param>
        /// <returns></returns>
        public CommonResult DeleteData(Dictionary<string, object> data_dict, Sys_accountInfo loginUser = null)
        {
            return DeleteData(null, data_dict, loginUser);
        }
        /// <summary>
        /// 單筆刪除
        /// </summary>
        /// <param name="trans">Transaction</param>
        /// <param name="data_dict">資料</param>
        /// <returns></returns>
        public CommonResult DeleteData(IDbTransaction trans, Dictionary<string, object> data_dict, Sys_accountInfo loginUser = null)
        {
            if (loginUser == null) loginUser = CommonHelper.GetLoginUser();

            var res = Db.ValidatePreDelete(_modelType, data_dict);
            if (res.IsSuccess)
            {
                string sql = @"delete [" + _modelType.GetTableName() + @"] where " + Db.GetSqlWhere(_modelType, data_dict);
                res.AffectedRows = Db.ExecuteNonQuery(trans, sql, Db.GetParam(_modelType, data_dict));
                if (res.AffectedRows <= 0) res.IsSuccess = false;
            }
            return res;
        }
        #endregion



    }
}
