using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Model;
using System.Web.UI.WebControls;
using Util;
using Web.App_Code;
using AjaxControlToolkit;

namespace Web.S02
{
    public partial class UCActivityStatement : System.Web.UI.UserControl
    {
        BusinessLayer.S02.UCActivityStatementBL _bl = new BusinessLayer.S02.UCActivityStatementBL();
        public Action AfterInsertOrUpdate;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                ToolkitScriptManager.RegisterClientScriptBlock(this, this.GetType(), "myFunc", "init()", true);
            }
        }

        #region 新增聲明
        public void Add()
        {
            main_fv.ChangeMode(FormViewMode.Insert);
            main_fv.DataBind();
            main_fv.FindControl("ast_title_txt").Focus();            
        }
        #region 新增
        protected void main_fv_ItemInserting(object sender, FormViewInsertEventArgs e)
        {
            var res = new CommonResult(true);
            var fv = sender as FormView;            

            if (res.IsSuccess)
            {
                var dict = new Dictionary<string, object>();
                dict["ast_title"] = e.Values["Ast_title"].ToString().Trim();
                dict["ast_desc"] = e.Values["Ast_desc"].ToString().Trim();
                dict["ast_content"] = e.Values["Ast_content"].ToString().Trim();
                dict["ast_term"] = e.Values["Ast_term"].ToString().Trim();
                dict["ast_public"] = e.Values["Ast_public"].ToString().Trim();
                res = _bl.InsertData(dict);
            }

            if (res.IsSuccess)
            {
                WebHelper.ShowPopupMessage(ITCEnum.PopupMessageType.Success, ITCEnum.DataActionType.Insert);
                if (AfterInsertOrUpdate != null)
                    AfterInsertOrUpdate();

                // 強制更新畫面
                WebHelper.GetMainMasterPageContentPanel(this).Update();
            }
            else
            {
                e.Cancel = true;
                WebHelper.ShowPopupMessage(ITCEnum.PopupMessageType.Error, ITCEnum.DataActionType.Insert, res.Message); 
            }
        }
        #endregion
        #endregion

        #region 編輯聲明
        public void Edit(int ast_id)
        {
            var lst = new List<Activity_statementInfo>();
            var act = _bl.GetData(ast_id);
            lst.Add(act);
            main_fv.ChangeMode(FormViewMode.Edit);
            main_fv.DataSource = lst;
            main_fv.DataBind();
        }
        protected void main_fv_ItemUpdating(object sender, FormViewUpdateEventArgs e)
        {
            var res = new CommonResult(true);
            var fv = sender as FormView;

            if (res.IsSuccess)
            {
                var old_dict = new Dictionary<string, object>();
                old_dict["ast_id"] = e.Keys["ast_id"];

                var new_dict = new Dictionary<string, object>();
                new_dict["ast_title"] = e.NewValues["Ast_title"];
                new_dict["ast_desc"] = e.NewValues["Ast_desc"];
                new_dict["ast_content"] = e.NewValues["Ast_content"];
                new_dict["ast_term"] = e.NewValues["Ast_term"];
                new_dict["ast_public"] = e.NewValues["Ast_public"];
                res = _bl.UpdateData(old_dict, new_dict);
            }

            if (res.IsSuccess)
            {
                WebHelper.ShowPopupMessage(ITCEnum.PopupMessageType.Success, ITCEnum.DataActionType.Update);
                if (AfterInsertOrUpdate != null)
                    AfterInsertOrUpdate();

                // 強制更新畫面
                WebHelper.GetMainMasterPageContentPanel(this).Update();
            }
            else
            {
                e.Cancel = true;
                WebHelper.ShowPopupMessage(ITCEnum.PopupMessageType.Error, ITCEnum.DataActionType.Update, res.Message);
            }
        }
        #endregion

        #region 取消
        protected void cancel_btn_Click(object sender, EventArgs e)
        {
            if (AfterInsertOrUpdate != null)
                AfterInsertOrUpdate();
        }
        #endregion

    }
}