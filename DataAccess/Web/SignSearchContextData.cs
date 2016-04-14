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


        #region 查詢報名資訊
        public DataTable GetActivityData(string aa_email)
        {
            string sql = @"SELECT activity.act_idn,activity.act_title,activity.act_class,
                                  activity_session.as_idn,activity_session.as_title,
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

        #region 查詢
        public DataTable GetEmailData(string aae_email)
        {
            string sql = @"SELECT  aae_email, aae_password
                           FROM     activity_apply_email
                           WHERE   (aae_password = @aae_email)";
            IDataParameter[] param = { Db.GetParam("@aae_email", aae_email) };
            return Db.GetDataTable(sql, param);
        }
        #endregion


        #region 查詢
        public CommonResult DeleteData(Dictionary<string, object> data_dict)
        {

            var res = Db.ValidatePreDelete(_modelType,data_dict);
            if (res.IsSuccess)
            {
                string sql = @"delete [" + _modelType.GetTableName() + @"] where " + Db.GetSqlWhere(_modelType, data_dict);
                res.AffectedRows = Db.ExecuteNonQuery(null, sql, Db.GetParam(_modelType, data_dict));
                if (res.AffectedRows <= 0) res.IsSuccess = false;
            }
            return res;
        }
        #endregion




    }
}
