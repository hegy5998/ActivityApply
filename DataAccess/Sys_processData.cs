using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Util;

namespace DataAccess
{
    public class Sys_processData : BaseData
    {
        /// <summary>
        /// 對應的Model型別
        /// </summary>
        private Type _modelType = typeof(Sys_processInfo);

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
                    values (" + Db.GetSqlInsertValue(data_dict) + ", '" + loginUser.Act_id + "'"+ ", (" + Db.DbNowTimeSQL + ")"+ ", '" + loginUser.Act_id + "'"+ ", (" + Db.DbNowTimeSQL + ")" + ")";
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
                    set " + Db.GetSqlSet(_modelType, newData_dict, "new_")+ ", [updid] = '" + loginUser.Act_id + "'"+ ", [updtime] = (" + Db.DbNowTimeSQL + ")" + @"
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

        #region 取得單筆資料
        /// <summary>
        /// 取得單筆資料
        /// </summary>
        /// <param name="sys_pid">作業代碼</param>
        /// <returns>查詢的資料</returns>
        public Sys_processInfo GetInfo(string sys_pid)
        {
            StringBuilder sql_sb = new StringBuilder();
            sql_sb.Append(@"
                select p.*, m.sys_mname, s.sys_id, s.sys_name
                from sys_process p, sys_module m, sys_system s
                where p.sys_mid = m.sys_mid
	                and m.sys_id = s.sys_id
	                and p.sys_pid = @sys_pid");

            var param_lst = new List<IDataParameter>() { 
                Db.GetParam("@sys_pid", sys_pid)
            };

            var lst = Db.GetEnumerable<Sys_processInfo>(sql_sb.ToString(), param_lst.ToArray()).ToList();

            if (lst.Count() > 0)
                return lst[0];
            else
                return null;
        }
        #endregion

        #region 取得特定模組或特定系統的作業
        /// <summary>
        /// 取得特定模組或特定系統的作業
        /// </summary>
        /// <param name="sys_id">指定系統代碼</param>
        /// <param name="sys_mid">指定模組代碼</param>
        /// <returns>資料</returns>
        public List<Sys_processInfo> GetListBySystemModule(string sys_id = "", string sys_mid = "")
        {
            var param = new List<IDataParameter>();
            StringBuilder sql_sb = new StringBuilder();
            sql_sb.Append(@"
                select p.*, m.sys_mname, s.sys_id, s.sys_name, 
                    (select count(*) from sys_processcontrol pc where pc.sys_pid = p.sys_pid) sys_cid_count
                from sys_process p, sys_module m, sys_system s
                where p.sys_mid = m.sys_mid
	                and m.sys_id = s.sys_id");

            // 篩選系統代碼
            if (!sys_id.IsNullOrWhiteSpace())
            {
                sql_sb.Append(" and s.sys_id = @sys_id");
                param.Add(Db.GetParam("@sys_id", sys_id));
            }

            // 篩選模組代碼
            if (!sys_mid.IsNullOrWhiteSpace())
            {
                sql_sb.Append(" and m.sys_mid = @sys_mid");
                param.Add(Db.GetParam("@sys_mid", sys_mid));
            }

            sql_sb.Append(" order by s.sys_id, m.sys_mid, p.sys_pid");

            var lst = Db.GetEnumerable<Sys_processInfo>(sql_sb.ToString(), param.ToArray()).ToList();

            return lst;
        }
        #endregion

        #region 更新系統代碼
        /// <summary>
        /// 更新系統代碼
        /// </summary>
        /// <param name="trans">Transaction</param>
        /// <param name="old_sys_id">原系統代碼</param>
        /// <param name="new_sys_id">新系統代碼</param>
        /// <returns></returns>
        public int UpdateSysMid(IDbTransaction trans, string oldSysMid, string newSysMid)
        {
            string sql = @"
                update sys_process
                set sys_mid = @newSysMid
                where sys_mid = @oldSysMid";

            var param_lst = new List<IDataParameter>() { 
                Db.GetParam("@oldSysMid", oldSysMid),
                Db.GetParam("@newSysMid", newSysMid)
            };

            int val = Db.ExecuteNonQuery(trans, sql, param_lst.ToArray());
            return val;
        }
        #endregion
    }
}
