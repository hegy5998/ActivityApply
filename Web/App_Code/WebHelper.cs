using AjaxControlToolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Util;

namespace Web.App_Code
{
    public class WebHelper
    {
        #region 在UpdataPanel Response後執行Javascript
        /// <summary>
        /// 在UpdataPanel Response後執行Javascript
        /// </summary>
        /// <param name="key">script key</param>
        /// <param name="script">javascript(不用輸入＜script＞＜/script＞)</param>
        public static void CallJavascript(string key, string script)
        {
            var p = HttpContext.Current.Handler as Page;
            ToolkitScriptManager.RegisterClientScriptBlock(p, p.GetType(), key, script, true);
        }
        #endregion

        #region 顯示資料處理提示訊息
        /// <summary>
        /// 顯示資料處理提示訊息
        /// </summary>
        /// <param name="pmType">訊息類型</param>
        /// <param name="dataActType">標題類型</param>
        /// <param name="msg">內容</param>
        public static void ShowPopupMessage(ITCEnum.PopupMessageType pmType, ITCEnum.DataActionType dataActType, string msg = "")
        {
            string header = "";
            switch (dataActType)
            {
                case ITCEnum.DataActionType.Insert:
                    header = "新增";
                    break;
                case ITCEnum.DataActionType.Update:
                    header = "儲存";
                    break;
                case ITCEnum.DataActionType.Delete:
                    header = "刪除";
                    break;
            }
            switch (pmType)
            {
                case ITCEnum.PopupMessageType.Success:
                    header += "成功!";
                    break;
                case ITCEnum.PopupMessageType.Error:
                    header += "失敗!";
                    break;
            }
            ShowPopupMessage(pmType, header, msg);
        }
        /// <summary>
        /// 顯示資料處理提示訊息
        /// </summary>
        /// <param name="type">訊息類型</param>
        /// <param name="header">標題</param>
        /// <param name="msg">內容</param>
        public static void ShowPopupMessage(ITCEnum.PopupMessageType type, string header, string msg = "")
        {
            string theme = "";          // CSS
            string sticky = "false";    // 是否保持顯示
            string speed = "normal";    // 顯示速度
            switch (type)
            {
                case ITCEnum.PopupMessageType.Success:  // 成功(綠色)
                    theme = "success";
                    speed = "fast";
                    break;
                case ITCEnum.PopupMessageType.Error:    // 錯誤(紅色)
                    theme = "error";
                    sticky = "true";
                    speed = "fast";
                    break;
                case ITCEnum.PopupMessageType.Warning:  // 警告(黃色)
                    theme = "warning";
                    sticky = "true";
                    speed = "fast";
                    break;
                case ITCEnum.PopupMessageType.Info:     // 資訊(藍色)
                    theme = "info";
                    sticky = "true";
                    speed = "fast";
                    break;
            }

            string script = @"
            $.jGrowl('" + msg + @"', {
                theme: '" + theme + @"',
                header: '" + header + @"',
                sticky: " + sticky + @",
                position: 'center',
                speed: '" + speed + @"',
                beforeOpen: function(e, m) {
                    $('div.jGrowl').find('div.jGrowl-notification').children().parent().remove();
                }
            })";
            CallJavascript("jgrowl", script);
        }
        #endregion

        #region 取得Main.master主版頁面的物件
        /// <summary>
        /// 取得主版頁面物件
        /// </summary>
        /// <param name="ctrl">主版頁面內的控制項</param>
        /// <returns></returns>
        public static Control GetMainMasterPage(Control ctrl)
        {
            int maxTryTimes = 50;
            int c = 0;
            while (true)
            {
                if (ctrl.TemplateControl != null && ctrl.TemplateControl.ToString() == "ASP.masterpages_base_master")
                    return ctrl;
                else
                    ctrl = ctrl.NamingContainer;

                c++;
                if (c >= maxTryTimes)
                    throw new Exception("無法取得Main.master主版頁面");
            }
            throw new Exception("無法取得Main.master主版頁面");
        }
        /// <summary>
        /// 取得主版頁面的主要內容區塊UpdatePanel(mainContent_upl)
        /// </summary>
        /// <param name="ctrl">主版頁面內的控制項</param>
        /// <returns></returns>
        public static UpdatePanel GetMainMasterPageContentPanel(Control ctrl)
        {
            return GetMainMasterPage(ctrl).FindControl("mainContent_upl") as UpdatePanel;
        }
        /// <summary>
        /// 取得主版頁面的子功能區塊UpdatePanel(mainContentSubFunction_upl)
        /// </summary>
        /// <param name="ctrl">主版頁面內的控制項</param>
        /// <returns></returns>
        public static UpdatePanel GetMainMasterPageSubFuncPanel(Control ctrl)
        {
            return GetMainMasterPage(ctrl).FindControl("mainContentSubFunction_upl") as UpdatePanel;
        }
        #endregion
    }
}