using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Util;
using DataAccess;
using Model;
using System.IO;
using System.Text.RegularExpressions;
using System.IO.Compression;
using BusinessLayer;

namespace Web.CommonPages
{
    public partial class BasePage : System.Web.UI.Page
    {
        #region Private Member
        /// <summary>
        /// 頁面異動權限
        /// </summary>
        private bool _processModifyAuth = false;
        /// <summary>
        /// 須權限控管的所有元件
        /// </summary>
        private List<Sys_processcontrolInfo> _processControl = new List<Sys_processcontrolInfo>();
        /// <summary>
        /// 有使用權限的元件
        /// </summary>
        private List<Sys_processcontrolInfo> _processControlAuth = new List<Sys_processcontrolInfo>();
        //協作者權限
        private List<Account_copperateInfo> _copperate = new List<Account_copperateInfo>();
        //協作者Data
        private Account_copperateData _copperateData = new Account_copperateData();
        #endregion

        #region Property
        /// <summary>
        /// 頁面異動權限
        /// </summary>
        protected bool ProcessModifyAuth
        {
            get { return _processModifyAuth; }
        }
        /// <summary>
        /// jquery ajax呼叫時，識別參數名稱 XXXX.aspx?[JqueryAjaxTypeName]=[method name]
        /// </summary>
        protected string JqueryAjaxTypeName = "action";
        public string Sys_id { get { return (string.IsNullOrWhiteSpace(Request["Sys_id"]) ? "" : Request["Sys_id"]); } }
        public string Sys_pid { get { return (string.IsNullOrWhiteSpace(Request["Sys_pid"]) ? "" : Request["Sys_pid"]); } }
        #endregion

        ///<summary>
        /// 建構
        ///</summary>
        public BasePage()
        {
            this.Load += new EventHandler(PageBase_Load);
        }

        protected virtual void PageBase_Load(object sender, EventArgs e)
        {
            // Init 權限
            InitAuth();

            // 清除原頁面的jgrowl訊息
            CallJavascript("jgrowl_close", "$('div.jGrowl').find('div.jGrowl-notification').children().parent().remove();");

            if (!IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(Request["msg"]))
                {
                    // 顯示傳入的訊息
                    ShowPopupMessage(ITCEnum.PopupMessageType.Success, Request["msg"]);
                }
            }
        }

        #region 在UpdataPanel Response後執行Javascript
        /// <summary>
        /// 在UpdataPanel Response後執行Javascript
        /// </summary>
        /// <param name="key">script key</param>
        /// <param name="script">javascript(不用輸入＜script＞＜/script＞)</param>
        protected void CallJavascript(string key, string script)
        {
            ToolkitScriptManager.RegisterClientScriptBlock(this, this.GetType(), key, script, true);
        }
        #endregion

        #region 顯示資料處理提示訊息
        protected void ShowPopupMessage(ITCEnum.PopupMessageType pmType, ITCEnum.DataActionType dataActType, string msg = "")
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
        protected void ShowPopupMessage(ITCEnum.PopupMessageType type, string header, string msg = "")
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
            CallJavascript("jgrowl", @"
                $.jGrowl('" + msg + @"', {
                    theme: '" + theme + @"',
                    header: '" + header + @"',
                    sticky: " + sticky + @",
                    position: 'center',
                    speed: '" + speed + @"',
                    beforeOpen: function(e, m) {
                        $('div.jGrowl').find('div.jGrowl-notification').children().parent().remove();
                    }
                })");
        }
        #endregion

        #region 權限控管
        /// <summary>
        /// Init 權限
        /// </summary>
        private void InitAuth()
        {
            var auth_lst = AuthBL.GetUserRoleUnitPositionAuth();
            var process_lst = auth_lst.Where(x => x.Sys_pid == Sys_pid);

            if (Sys_pid != "")
            {
                _processModifyAuth = true;

                // 取得頁面異動權限
                _processModifyAuth = (process_lst.Where(x => string.IsNullOrWhiteSpace(x.Sys_cid)).First().Sys_modify == "Y") ? true : false;

                // 取得所有須權限控管的子功能
                string processControlCacheKey = "ProcessControl_" + Sys_pid + "_" + Session.SessionID;
                // 於頁面第一次載入或Cache消失時，重新從DB取得資料
                _processControl = Cache[processControlCacheKey] as List<Sys_processcontrolInfo>;
                if (!IsPostBack || _processControl == null)
                {
                    _processControl = new Sys_processcontrolData().GetList(Sys_pid);
                    // 存入Cache
                    CommonHelper.SetCache(processControlCacheKey, _processControl, Convert.ToDouble(CommonHelper.GetSysConfig().SOLUTION_CACHE_DURATION_MENU));
                }

                // 取得具有權限的子功能
                foreach (var t in process_lst.Where(x => !string.IsNullOrWhiteSpace(x.Sys_cid)))
                {
                    var i = new Sys_processcontrolInfo();
                    i.Sys_pid = t.Sys_pid;
                    i.Sys_cid = t.Sys_cid;
                    _processControlAuth.Add(i);
                }
            }
        }

        /// <summary>
        /// 將沒有使用權限的功能按鈕隱藏(頁面上須控管權限的元件，須將此Method設定在PreRender事件)
        /// </summary>
        protected void ManageControlAuth(object sender)
        {
            Control ctrl = sender as Control;

            if (_processControl.Where(s => s.Sys_cid == ctrl.ID).Count() > 0)
            {
                // 子功能元件
                if (_processControlAuth.Where(s => s.Sys_cid == ctrl.ID).Count() == 0)
                {
                    ctrl.Visible = false;
                }
            }
            else if (_processModifyAuth == false)
            {
                // 一般頁面元件
                ctrl.Visible = false;
            }
        }

        //活動列表(協作者)判斷權限
        protected void ManageControlCopperate(object sender)
        {
            Control ctrl = sender as Control;
            Button button = sender as Button;

            if (button != null)
            {
                _copperate = _copperateData.GetDataList(button.CommandArgument.ToString());

                //若是有子功能
                for (int j = 0; j < _processControl.Count; j++)
                {
                    // 子功能元件
                    if (_processControl[j].Sys_cid == ctrl.ID)
                    {
                        for (int i = 0; i < _copperate.Count; i++)
                        {
                            //判斷是否為協作者
                            if (_copperate[i].Cop_id == CommonHelper.GetLoginUser().Act_id)
                            {
                                //判斷協作者的權限
                                if (_copperate[i].Cop_authority == "閱讀")
                                {
                                    button.Enabled = false;
                                    button.CssClass = "btn-link";
                                    button.Style["color"] = "black";
                                }

                                //協作者不能編輯協作者
                                if (_processControl[j].Sys_cid == "set_btn")
                                {
                                    button.Enabled = false;
                                    button.CssClass = "btn-link";
                                    button.Style["color"] = "black";
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (_processControl.Where(s => s.Sys_cid == ctrl.ID).Count() > 0)
                {
                    // 子功能元件
                    if (_processControlAuth.Where(s => s.Sys_cid == ctrl.ID).Count() == 0)
                    {
                        ctrl.Visible = false;
                    }
                }
                else if (_processModifyAuth == false)
                {
                    // 一般頁面元件
                    ctrl.Visible = false;
                }
            }
        }

        /// <summary>
        /// 取得特定元件ID是否有使用權限
        /// </summary>
        /// <param name="ControlID">元件ID</param>
        /// <returns>是否有使用權限</returns>
        protected bool GetProcessControlAuth(string ControlID)
        {
            bool ret = false;

            if (_processControlAuth.Where(s => s.Sys_cid == ControlID).Count() > 0)
                ret = true;

            return ret;
        }
        #endregion

        #region 檢查是否為ajax呼叫method，如果是，則執行該method
        /// <summary>
        /// 檢查是否為ajax呼叫method，如果是，則執行該method
        /// </summary>
        /// <param name="page">網頁page</param>
        protected bool CheckAndDoAjaxAction(Page page)
        {
            bool ret = false;
            string action = Request[JqueryAjaxTypeName];
            if (Request[JqueryAjaxTypeName] != null)
            {
                Response.Clear();
                page.GetType().GetMethod(action).Invoke(this, null);
                Response.End();
            }
            return ret;
        }
        #endregion

        #region Ladp異質系統連接
        public void LdapOut(string targetUrl, bool openNewPage = true)
        {
            var dl = new Sys_loginldapData();
            string sessionId = SessionHelper.GetSession().SessionID;
            HttpContext curContext = HttpContext.Current;
            var data_dict = new Dictionary<string, object>();
            data_dict["log_session"] = sessionId;
            
            // 1.刪除ldap table該Session資料
            var res = dl.DeleteData(data_dict);

            // 2.新增ldap table該Session資料
            if (res.IsSuccess)
            {
                // 加入帳號及登入時間資料
                data_dict["act_id"] = CommonHelper.GetLoginUser().Act_id;
                data_dict["log_time"] = CommonHelper.GetDBDateTime();

                res = new Sys_loginldapData().InsertData(data_dict);
            }

            // 3.導向介接系統
            if (res.IsSuccess)
            {
                string script = "";
                if (openNewPage)
                    script = "var win=window.open('" + targetUrl + (targetUrl.Contains("?") ? "&" : "?") + "msg=" + sessionId + "', '_blank');win.focus();";
                else
                    script = "var win=window.open('" + targetUrl + (targetUrl.Contains("?") ? "&" : "?") + "msg=" + sessionId + "', '_self');win.focus();";
                ToolkitScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ldapout", script, true);
            }
        }
        #endregion
    }
}