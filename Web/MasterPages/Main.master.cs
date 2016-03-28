using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using Util;
using BusinessLayer.MasterPages;
using AjaxControlToolkit;

namespace Web.MasterPages
{
    public partial class Main : System.Web.UI.MasterPage
    {
        MainBL _bl = new MainBL();
        public string ShowSysId = "";
        public string Sys_url = "";
        public Sys_processInfo CurProcessInfo = new Sys_processInfo();
        public string jqueryAjaxTypeName = "action"; // jQuery ajax呼叫方法時的參數名稱
        public string HeaderBannerStyle;

        protected void Page_Init(object sender, EventArgs e)
        {
            // 檢查使用者是否登入
            if (CommonHelper.GetLoginUser() == null)
            {
                // 如果為使用jquery ajax，則須修改狀態碼，使得client js可以判斷Session timeout，並導向到登入頁
                if (Request[jqueryAjaxTypeName] != null)
                {
                    Response.Clear();
                    Response.StatusCode = 401;
                    Response.End();
                }
                else
                    Response.Redirect("~/Login.aspx");
            }
            else
            {
                // 線上人數統記
                OnlineUserCounter.UpdateInsertSession();

                // 取得目前要顯示的作業資訊
                Sys_processInfo process_info = _bl.GetShowProcessInfo();

                // 檢查使用權限
                if (process_info == null)
                {
                    // 無使用權限者，導回首頁
                    Response.Redirect("~/Index.aspx");
                }
                else
                {
                    // 設定Banner
                    HeaderBannerStyle = "style='background-image:url(" + ResolveUrl("~/Images/SystemBanner/" + process_info.Sys_bannerimg) + ")'";

                    // 產生系統首頁網址
                    Sys_url = ResolveUrl(_bl.GetSystemIndexPageURL());

                    // 產生系統名稱
                    mainHeaderSysName_l.Text = process_info.Sys_name;

                    // 產生左方模組及作業選單
                    mainMenu_l.Text = _bl.GetModuleAndProcessHTML();

                    // 設定要顯示的系統選單
                    ShowSysId = process_info.Sys_id;

                    // 產生作業Header
                    if (Request.FilePath.Contains("/Index.aspx")
                        || Request.FilePath.Contains("/DefaultSystemIndex.aspx"))
                        processName_lbl.Text = "首頁";
                    else
                        processName_lbl.Text = process_info.Sys_pname;

                    Page.Title = processName_lbl.Text + " - " + CommonHelper.GetSysConfig().SOLUTION_NAME;

                    CurProcessInfo = process_info;

                    // 記錄作業登入資訊
                    if (Request[jqueryAjaxTypeName] == null && process_info.Sys_pid != null)
                    {
                        // 登入作業時記錄
                        BusinessLayer.CommonBL.WriteLoginOrProcessLog(process_info.Sys_pid, Sys_login_logInfo.StatusType.Success);
                    }
                }
            }
        }
    }
}