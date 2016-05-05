using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Model;
using Util;
using DataAccess;
using System.Web.UI;
using System.Configuration;
using System.Data.Common;

namespace ActivityApply
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            #region 設定資料庫連線
            // 指定系統主要資料庫
            DAH.Db = Util.Settings.SystemDb = new CommonDbHelper(ConfigurationManager.ConnectionStrings["DbConn"].ConnectionString, CommonDbHelper.DbConnType.SQLServer);

            // 指定其他資料庫
            // DAH.DbOther = new CommonDbHelper(ConfigurationManager.ConnectionStrings["ConnDbOther"].ConnectionString, CommonDbHelper.DbConnType.SQLServer);
            #endregion

            #region 連接資料庫，讀取系統參數
            // 設定系統參數讀取並設定的method
            CommonHelper.InitSysConfig += InitSysConfig;
            try
            {
                // 讀取並設定系統參數
                InitSysConfig();
            }
            catch (DbException ode)
            {
                string FileName = "Error_" + DateTime.Now.ToString("yyyyMMdd");
                LogHelper.WriteLog("Global:Application_start，發生錯誤: 無法連接資料庫!" + Environment.NewLine + ode.StackTrace, FileName, "System");
            }
            #endregion
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            if (!CustomHelper.IsDebugMode)
            {
                // 如果不是debug模式，則紀錄錯誤資料，並顯示自訂的錯誤頁面給使用者
                Exception ex = Server.GetLastError();
                if (ex != null)
                    ErrorHelper.ErrorProcess(ex, Request, Server, Response);
                Response.End();
            }
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        #region 系統初始化參數
        /// <summary>
        /// 初始化系統參數
        /// </summary>
        private void InitSysConfig()
        {
            var info = DataAccess.Common.CommonData.GetSysConfig();
            Application[CommonHelper.SysConfigApplicationKey] = info;
            CommonHelper.SysConfigUpdtime = Settings.SystemDb.GetDbNowTime();

            #region Init MailHelper 信件發送
            MailHelper.Init(new MailHelper.ConfigModel()
            {
                Host = info.SMTP_HOST,
                Port = info.SMTP_PORT.ToIntOrZero(),
                UserId = info.SMTP_USER,
                Password = info.SMTP_PWD,
                SSL = info.SMTP_SSL == "Y"
            });
            #endregion

            #region Init ErrorHelper 錯誤紀錄
            ErrorHelper.Init(
                writeLogFile: info.ERROR_HANDLER_NEED_WRITE_LOG == "Y",
                mailNotify: info.ERROR_HANDLER_NOTIFY_EMAIL == "Y",
                mails: info.ERROR_HANDLER_NOTIFY_EMAIL
            );
            #endregion

            #region Init OnlineUserCounter 設定線上人數計數
            OnlineUserCounter.Init(
                info.ONLINE_USER_COUNTER_ENABLE == "Y" ? true : false,
                info.AP_IP,
                CommonConvert.GetIntOrZero(info.ONLINE_USER_COUNTER_TIMEOUT_INTERVAL),
                CommonConvert.GetIntOrZero(info.ONLINE_USER_COUNTER_DROP_INTERVAL),
                CommonConvert.GetIntOrZero(info.ONLINE_USER_COUNTER_SYNC_INTERVAL));
            #endregion
        }
        #endregion
    }
}