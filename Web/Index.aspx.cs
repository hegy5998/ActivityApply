using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using Util;
using Model;
using AjaxControlToolkit;
using System.Text;

namespace Web
{
    public partial class Index : System.Web.UI.Page
    {
        public string news_html, systemList_html;
        SystemMenuBL _bl = new SystemMenuBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            // 檢查使用者是否登入
            if (CommonHelper.GetLoginUser() == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                try
                {
                    // 檢查資料庫連線是否正常
                    DAH.Db.TryConnection();

                    if (!IsPostBack)
                    {
                        var res = _bl.IsShowSystemMenu();
                        if (res.Item1)
                        {
                            // 設定系統選單
                            systemList_html = _bl.GetSystemListHtml();

                            #region 訊息公告設定
                            var news_sb = new StringBuilder();
                            foreach (var item in _bl.GetNewsList())
                            {
                                var date = item.Sys_date.Value.ToYMD_ROC();
                                if (item.Sys_url.IsNullOrWhiteSpace())
                                    news_sb.Append("<div class='news'>[" + date + "] " + item.Sys_title + "</div>");
                                else
                                    news_sb.Append("<a class='news' href=\"" + item.Sys_url + "\" target=\"_blank\">[" + date + "] " + item.Sys_title + "</a>");
                            }
                            news_html = news_sb.ToString();
                            #endregion

                            // 設定網頁標題
                            Page.Title = CommonHelper.GetSysConfig().SOLUTION_NAME;
                        }
                        else
                        {
                            Response.Redirect(res.Item2, false);
                        }

                    }

                    // 線上人數統記
                    OnlineUserCounter.UpdateInsertSession();
                }
                catch (Exception ex)
                {
                    if (CustomHelper.IsDebugMode)
                        throw ex;
                    Response.Redirect("~/Login.aspx");
                }
            }

            // 清除原頁面的jgrowl訊息
            ToolkitScriptManager.RegisterClientScriptBlock(this, this.GetType(), "jgrowl_close",  "$('div.jGrowl').find('div.jGrowl-notification').children().parent().remove();", true);
        }
    }
}