﻿/*
 * 檔案位置: DataAccess\Activity_apply_detailData.cs
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
    
    public class Activity_apply_detailData : BaseData
    {
        /// <summary>
        /// 對應的Model型別
        /// </summary>
        private Type _modelType = typeof(Activity_apply_detailInfo);

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
                        (" + Db.GetSqlInsertField(_modelType, data_dict) + @") 
                    values (" + Db.GetSqlInsertValue(data_dict) + ")";
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
                    set " + Db.GetSqlSet(_modelType, newData_dict, "new_") + @"
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

        #region 查詢
        public List<Activity_apply_detailInfo> getApplyDetailList(int aad_apply_id)
        {
            string sql = @" SELECT  aad_apply_id, aad_col_id, aad_val
                            FROM    activity_apply_detail
                            WHERE   (aad_apply_id = @aad_apply_id);";
            IDataParameter[] param = { Db.GetParam("@aad_apply_id", aad_apply_id) };
            return Db.GetEnumerable<Activity_apply_detailInfo>(sql, param).ToList();
        }
        #endregion
    }
}
