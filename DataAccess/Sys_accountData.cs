using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Util;

namespace DataAccess
{
    public class Sys_accountData : BaseData
    {
        /// <summary>
        /// 對應的Model型別
        /// </summary>
        private Type _modelType = typeof(Sys_accountInfo);

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
                #region 若有密碼欄位，則加密密碼
                var pwdDictKey = data_dict.GetKeyIgnoreCase("Act_pwd");
                if (pwdDictKey != null)
                    data_dict[pwdDictKey] = CommonHelper.GetMD5StrWithSaltValue(data_dict[pwdDictKey].ToString(), CommonHelper.GetSysConfig().SOLUTION_ACT_PWD_ENCRYPT_SALTED_VALUE);
                #endregion

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
                #region 若有密碼欄位，則加密密碼
                var pwdDictKey = newData_dict.GetKeyIgnoreCase("Act_pwd");
                if (pwdDictKey != null)
                    newData_dict[pwdDictKey] = CommonHelper.GetMD5StrWithSaltValue(newData_dict[pwdDictKey].ToString(), CommonHelper.GetSysConfig().SOLUTION_ACT_PWD_ENCRYPT_SALTED_VALUE);
                #endregion

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
        /// <param name="act_id">帳號</param>
        /// <returns></returns>
        public Sys_accountInfo GetInfo(string act_id)
        {
            Sys_accountInfo info = null;

            // 取得帳號資料
            string sql = @"
                SELECT act.*
                  FROM sys_account act
                 WHERE act.act_id = @act_id";

            var info_lst = Db.GetEnumerable<Sys_accountInfo>(sql, Db.GetParam("@act_id", act_id)).ToList();

            if (info_lst.Count() > 0)
            {
                info = info_lst[0];

                #region 取得帳號對應角色資料
                sql = @"
                    select ar.sys_rid, r.sys_rname, ar.sys_uid, u.sys_uname, ar.sys_rpid, rp.sys_rpname
                    from sys_account_role ar
                    inner join sys_role r on ar.sys_rid = r.sys_rid
                    inner join sys_unit u on ar.sys_uid = u.sys_uid
                    inner join sys_role_position rp on 
                        ar.sys_rid = rp.sys_rid and (ar.sys_uid = rp.sys_uid or rp.sys_uid = '" + AuthData.GlobalSymbol + @"') and ar.sys_rpid = rp.sys_rpid
                    where ar.act_id = @act_id
                    order by ar.sys_rid, ar.sys_uid, ar.sys_rpid";

                var dt = Db.GetDataTable(sql, Db.GetParam("@act_id", info.Act_id));
                info.Role_lst = new List<Sys_accountInfo.RoleInfo>();
                foreach (DataRow dr in dt.Rows)
                {
                    Sys_accountInfo.RoleInfo r = new Sys_accountInfo.RoleInfo();
                    r.Sys_rid = dr["sys_rid"].ToString();
                    r.Sys_rname = dr["sys_rname"].ToString();
                    r.Sys_uid = dr["sys_uid"].ToString();
                    r.Sys_uname = dr["sys_uname"].ToString();
                    r.Sys_rpid = dr["sys_rpid"].ToString();
                    r.Sys_rpname = dr["sys_rpname"].ToString();
                    info.Role_lst.Add(r);
                }
                #endregion
            }
            return info;
        }
        #endregion

        #region 檢查帳號密碼是否正確
        /// <summary>
        /// 檢查帳號密碼是否正確
        /// </summary>
        /// <param name="act_id">帳號</param>
        /// <param name="act_pwd">密碼</param>
        /// <returns>是否正確</returns>
        public bool ValidateAccount(string act_id, string act_pwd)
        {
            bool isValide = false;

            string sql = @"
                select act_id
                from sys_account
                where sys_account.act_id = @act_id
                    and sys_account.act_pwd = @act_pwd";

            var obj = Db.ExecuteScalar(sql, 
                Db.GetParam("@act_id", act_id), 
                Db.GetParam("@act_pwd", CommonHelper.GetMD5StrWithSaltValue(act_pwd, CommonHelper.GetSysConfig().SOLUTION_ACT_PWD_ENCRYPT_SALTED_VALUE)));
            if (obj != null)
                isValide = true;

            return isValide;
        }
        #endregion
    }
}
