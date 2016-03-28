using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Util;

namespace DataAccess
{
    public class Sys_processcontrol_roleData : BaseData
    {
        /// <summary>
        /// 對應的Model型別
        /// </summary>
        private Type _modelType = typeof(Sys_processcontrol_roleInfo);

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

        #region 更新作業代碼
        /// <summary>
        /// 更新作業代碼
        /// </summary>
        /// <param name="trans">Transaction</param>
        /// <param name="old_sys_pid">原作業代碼</param>
        /// <param name="new_sys_pid">新作業代碼</param>
        public int UpdateSysPid(IDbTransaction trans, string old_sys_pid, string new_sys_pid)
        {
            string sql = @"
                update sys_processcontrol_role
                set sys_pid = @new_sys_pid
                where sys_pid = @old_sys_pid";

            var param_lst = new List<IDataParameter>()
            {
                Db.GetParam("@old_sys_pid", old_sys_pid),
                Db.GetParam("@new_sys_pid", new_sys_pid)
            };

            int val = Db.ExecuteNonQuery(trans, sql, param_lst.ToArray());
            return val;
        }
        #endregion

        #region 刪除特定作業代碼的資料
        /// <summary>
        /// 刪除特定作業代碼的資料
        /// </summary>
        /// <param name="trans">Transaction</param>
        /// <param name="sys_pid">作業代碼</param>
        public int DeleteBySysPid(IDbTransaction trans, string sys_pid)
        {
            string sql = @"
                delete sys_processcontrol_role
                where sys_pid = @sys_pid";

            var param_lst = new List<IDataParameter>()
            {
                Db.GetParam("@sys_pid", sys_pid)
            };

            int val = Db.ExecuteNonQuery(trans, sql, param_lst.ToArray());
            return val;
        }
        #endregion

        #region 更新作業子功能代碼
        /// <summary>
        /// 更新作業子功能代碼
        /// </summary>
        /// <param name="trans">Transaction</param>
        /// <param name="old_sys_pid">原作業代碼</param>
        /// <param name="old_sys_cid">原子功能代碼</param>
        /// <param name="new_sys_cid">新子功能代碼</param>
        public int UpdateSysCid(IDbTransaction trans, string old_sys_pid, string old_sys_cid, string new_sys_cid)
        {
            string sql = @"
                update sys_processcontrol_role
                set sys_cid = @new_sys_cid
                where sys_pid = @old_sys_pid
                    and sys_cid = @old_sys_cid";

            var param_lst = new List<IDataParameter>() 
            { 
                Db.GetParam("@old_sys_pid", old_sys_pid),
                Db.GetParam("@old_sys_cid", old_sys_cid),
                Db.GetParam("@new_sys_cid", new_sys_cid)
            };

            int val = Db.ExecuteNonQuery(trans, sql, param_lst.ToArray());

            return val;
        }
        #endregion

        #region 刪除特定子功能權限
        /// <summary>
        /// 刪除特定子功能權限
        /// </summary>
        /// <param name="trans">Transaction</param>
        /// <param name="sys_pid">作業代碼</param>
        /// <param name="sys_cid">子功能代碼</param>
        public int DeleteDataBySysCid(IDbTransaction trans, string sys_pid, string sys_cid)
        {
            string sql = @"
                delete sys_processcontrol_role
                where sys_pid = @sys_pid
                    and sys_cid = @sys_cid";

            var param_lst = new List<IDataParameter>() 
            { 
                Db.GetParam("@sys_pid", sys_pid),
                Db.GetParam("@sys_cid", sys_cid)
            };

            int val = Db.ExecuteNonQuery(trans, sql, param_lst.ToArray());

            return val;
        }
        #endregion

        #region 更新角色代碼
        /// <summary>
        /// 更新角色代碼
        /// </summary>
        /// <param name="trans">Transaction</param>
        /// <param name="old_sys_rid">原角色代碼</param>
        /// <param name="new_sys_rid">新角色代碼</param>
        public int UpdateSysRid(IDbTransaction trans, string old_sys_rid, string new_sys_rid)
        {
            string sql = @"
                update sys_processcontrol_role
                set sys_rid = @new_sys_rid
                where sys_rid = @old_sys_rid";

            var param_lst = new List<IDataParameter>() { 
                Db.GetParam("@new_sys_rid", new_sys_rid),
                Db.GetParam("@old_sys_rid", old_sys_rid)
            };

            int val = Db.ExecuteNonQuery(trans, sql, param_lst.ToArray());
            return val;
        }
        #endregion

        #region 刪除特定角色的資料
        /// <summary>
        /// 刪除特定角色的資料
        /// </summary>
        /// <param name="trans">Transaction</param>
        /// <param name="sys_rid">角色代碼</param>
        /// <returns></returns>
        public int DeleteByRole(IDbTransaction trans, string sys_rid)
        {
            string sql = @"
                delete sys_processcontrol_role
                where sys_rid = @sys_rid";

            int val = Db.ExecuteNonQuery(trans, sql, Db.GetParam("@sys_rid", sys_rid));
            return val;
        }
        #endregion

        #region 刪除特定角色單位的資料
        /// <summary>
        /// 刪除特定角色單位的資料
        /// </summary>
        /// <param name="trans">Transaction</param>
        /// <param name="sys_rid">角色代碼</param>
        /// <param name="sys_uid">單位代碼</param>
        public int DeleteByRoleUnit(IDbTransaction trans, string sys_rid, string sys_uid)
        {
            string sql = @"
                delete sys_processcontrol_role 
                where sys_rid = @sys_rid
                    and sys_uid = @sys_uid";

            var param_lst = new List<IDataParameter>() { 
                Db.GetParam("@sys_rid", sys_rid),
                Db.GetParam("@sys_uid", sys_uid)
            };

            int val = Db.ExecuteNonQuery(trans, sql, param_lst.ToArray());
            return val;
        }
        #endregion

        #region 刪除特定角色單位職位的資料
        /// <summary>
        /// 刪除特定角色單位職位的資料
        /// </summary>
        /// <param name="trans">Transaction</param>
        /// <param name="sys_rid">角色代碼</param>
        /// <param name="sys_uid">單位代碼</param>
        /// <param name="sys_rpid">職位代碼</param>
        public int DeleteByRoleUnitPosition(IDbTransaction trans, string sys_rid, string sys_uid, string sys_rpid)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"
                delete sys_processcontrol_role 
                where sys_rid = @sys_rid and sys_rpid = @sys_rpid");

            var param_lst = new List<IDataParameter>() { 
                Db.GetParam("@sys_rid", sys_rid),
                Db.GetParam("@sys_rpid", sys_rpid)
            };

            #region 如果非通用職位，則指定單位代碼
            if (sys_rpid != AuthData.GlobalSymbol)
            {
                sb.Append(" and sys_uid = @sys_uid");
                param_lst.Add(Db.GetParam("@sys_uid", sys_uid));
            }
            #endregion

            int val = Db.ExecuteNonQuery(trans, sb.ToString(), param_lst.ToArray());
            return val;
        }
        #endregion
    }
}
