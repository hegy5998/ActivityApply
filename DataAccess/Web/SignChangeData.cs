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
    public class SignChangeData : BaseData
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

        public List<Activity_apply_detailInfo> getApplyDetailList(int aad_apply_id)
        {
            return _apply_detailData.getApplyDetailList(aad_apply_id);
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
