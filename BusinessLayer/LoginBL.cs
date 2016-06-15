using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DataAccess;
using Util;
using System.Web;

namespace BusinessLayer
{
    public class LoginBL : BaseBL
    {
        #region 檢查帳號密碼是否正確
        /// <summary>
        /// 檢查帳號密碼是否正確
        /// </summary>
        /// <param name="username">帳號</param>
        /// <param name="pwd">密碼</param>
        /// <param name="errorMsg">錯誤訊息</param>
        /// <returns>是否正確</returns>
        public CommonResult ValidateAccount(string act_id, string act_pwd)
        {
            var res = new CommonResult(true);
            if (!new Sys_accountData().ValidateAccount(act_id, act_pwd))
            {
                res.IsSuccess = false;
                res.Message = "帳號或密碼錯誤!";
            }

            return res;
        }
        #endregion

        public void DeleteApplyData() {
            Sys_login_logData _da = new Sys_login_logData();
            _da.DeleteApplyData();
        }
    }
}
