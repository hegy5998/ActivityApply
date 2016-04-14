/*
 * 檔案位置: DataAccess\Account_copperateData.cs
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
    public class Account_copperateData : BaseData
    {
        /// <summary>
        /// 對應的Model型別
        /// </summary>
        private Type _modelType = typeof(Account_copperateInfo);

        /// <summary>
        /// 對應的資料庫
        /// </summary>
        public CommonDbHelper Db = DAH.Db;

        #region 單筆資料維護
        #region 單筆新增
        /// <summary>
        /// 單筆新增
        /// </summary>
        /// <param name="data_dict">資料</param>
        /// <param name="checkDataRepeat">檢查資料是否重複(預設為會檢查)</param>
        /// <param name="loginUser">自定使用者資訊(預設為來自Session的登入者資訊)</param>
        /// <returns></returns>
        public CommonResult InsertData(Dictionary<string, object> data_dict, bool checkDataRepeat = true, Sys_accountInfo loginUser = null)
        {
            return InsertData(null, data_dict, checkDataRepeat, loginUser);
        }
        /// <summary>
        /// 單筆新增
        /// </summary>
        /// <param name="trans">Transaction</param>
        /// <param name="data_dict">資料</param>
        /// <param name="checkDataRepeat">檢查資料是否重複(預設為會檢查)</param>
        /// <param name="loginUser">自定使用者資訊(預設為來自Session的登入者資訊)</param>
        /// <returns></returns>
        public CommonResult InsertData(IDbTransaction trans, Dictionary<string, object> data_dict, bool checkDataRepeat = true, Sys_accountInfo loginUser = null)
        {
            if (loginUser == null) loginUser = CommonHelper.GetLoginUser();

            var res = Db.ValidatePreInsert(_modelType, trans, data_dict, checkDataRepeat);
            if (res.IsSuccess)
            {
                string sql = @"
                    insert into [" + _modelType.GetTableName() + @"] 
                        (" + Db.GetSqlInsertField(_modelType, data_dict) + @", [createid], [createtime], [updid], [updtime]) 
                    values (" + Db.GetSqlInsertValue(data_dict) + ", '" + loginUser.Act_id + "'" + ", (" + Db.DbNowTimeSQL + ")" + ", '" + loginUser.Act_id + "'" + ", (" + Db.DbNowTimeSQL + ")" + ")";
                res.AffectedRows = Db.ExecuteNonQuery(trans, sql, Db.GetParam(_modelType, data_dict));
                if (res.AffectedRows <= 0) res.IsSuccess = false;
            }
            return res;
        }
        #endregion

        #region 單筆更新
        /// <summary>
        /// 單筆更新
        /// </summary>
        /// <param name="oldData_dict">原資料PK</param>
        /// <param name="newData_dict">新資料</param>
        /// <param name="checkDataRepeat">檢查資料是否重複</param>
        /// <returns></returns>
        public CommonResult UpdateData(Dictionary<string, object> oldData_dict, Dictionary<string, object> newData_dict, bool checkDataRepeat = true, Sys_accountInfo loginUser = null)
        {
            return UpdateData(null, oldData_dict, newData_dict, checkDataRepeat, loginUser);
        }
        /// <summary>
        /// 單筆更新
        /// </summary>
        /// <param name="trans">Transaction</param>
        /// <param name="oldData_dict">原資料PK</param>
        /// <param name="newData_dict">新資料</param>
        /// <param name="checkDataRepeat">檢查資料是否重複</param>
        /// <returns></returns>
        public CommonResult UpdateData(IDbTransaction trans, Dictionary<string, object> oldData_dict, Dictionary<string, object> newData_dict, bool checkDataRepeat = true, Sys_accountInfo loginUser = null)
        {
            if (loginUser == null) loginUser = CommonHelper.GetLoginUser();

            var res = Db.ValidatePreUpdate(_modelType, trans, oldData_dict, newData_dict, checkDataRepeat);
            if (res.IsSuccess)
            {
                string sql = @"
                    update [" + _modelType.GetTableName() + @"] 
                    set " + Db.GetSqlSet(_modelType, newData_dict, "new_") + ", [updid] = '" + loginUser.Act_id + "'" + ", [updtime] = (" + Db.DbNowTimeSQL + ")" + @"
                    where " + Db.GetSqlWhere(_modelType, oldData_dict, "old_");

                res.AffectedRows = Db.ExecuteNonQuery(trans, sql, Db.GetParam(_modelType, oldData_dict, "old_").Concat(Db.GetParam(_modelType, newData_dict, "new_")).ToArray());
                if (res.AffectedRows <= 0)
                    res.IsSuccess = false;
            }
            return res;
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
        #endregion

        //取得協作者資料
        public DataTable GetList(int i)
        {
            StringBuilder sql_sb = new StringBuilder();

            sql_sb.Append(@"
                SELECT cop_act, cop_id, cop_authority
                FROM account_copperate, activity
                WHERE cop_act = act_idn AND
	                  cop_act = @i");

            //欲編輯協作者的的活動序號
            var param_lst = new List<IDataParameter>() {
                Db.GetParam("@i", i),
            };

            return Db.GetDataTable(sql_sb.ToString(), param_lst.ToArray());
        }

        // 取得Info資料
        public Account_copperateInfo GetInfo(string act_idn)
        {
            StringBuilder sql_sb = new StringBuilder();

            sql_sb.Append(@"
                SELECT cop_act, cop_id, cop_authority
                FROM account_copperate, activity
                WHERE cop_act = act_idn AND
	                  cop_act = @act_idn");

            var param_lst = new List<IDataParameter>() { 
                Db.GetParam("@act_idn", act_idn)
            };

            var lst = Db.GetEnumerable<Account_copperateInfo>(sql_sb.ToString(), param_lst.ToArray()).ToList();

            if (lst.Count() > 0)
                return lst[0];
            else
                return null;
        }

        // 取得列表資料
        public List<Model.Account_copperateInfo> GetDataList(string act_idn)
        {
            var db = DAH.Db;
            string sql = @"
                SELECT cop_act, cop_id, cop_authority
                FROM account_copperate, activity
                WHERE cop_act = act_idn AND
	                  cop_act = @act_idn";

            var lst = db.GetEnumerable<Model.Account_copperateInfo>(sql, db.GetParam("@act_idn", act_idn)).ToList();
            return lst;
        }
    }
}
