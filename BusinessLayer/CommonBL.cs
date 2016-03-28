using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess;
using Util;
using Model;
using System.Web;
using System.Web.UI;
using System.Data;

namespace BusinessLayer
{
    public class CommonBL : BaseBL
    {
        #region 寫入登入Log
        /// <summary>
        /// 寫入登入log
        /// </summary>
        /// <param name="sys_pid">作業代碼</param>
        /// <param name="statusType">狀態</param>
        /// <param name="note">說明</param>
        public static void WriteLoginOrProcessLog(string sys_pid, Model.Sys_login_logInfo.StatusType statusType, string note = "")
        {
            bool isDebugMode = false;
#if DEBUG
            isDebugMode = true;
#endif
            if (isDebugMode == false)
            {
                var dict = new Dictionary<string, object>();
                dict["no"] = Guid.NewGuid().ToString("N");
                dict["logtime"] = CommonHelper.GetDBDateTime();
                dict["cnt_ip"] = CommonHelper.GetClientIP();
                dict["srv_ip"] = CommonHelper.GetServerIP();
                dict["act_id"] = (CommonHelper.GetLoginUser() != null) ? CommonHelper.GetLoginUser().Act_id : "";
                dict["sys_pid"] = sys_pid;
                switch (statusType)
                { 
                    case Sys_login_logInfo.StatusType.Success:
                        dict["status"] = "Y";
                        break;
                    case Sys_login_logInfo.StatusType.Fail:
                        dict["status"] = "X";
                        break;
                }
                dict["note"] = note;

                new DataAccess.Sys_login_logData().InsertData(dict, false);
            }
        }
        #endregion
    }
}
