using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Util;
using Web.App_Code;

namespace Web.S01
{
    public partial class UCAccountManager : System.Web.UI.UserControl
    {
        BusinessLayer.S01.UCAccountManagerBL _bl = new BusinessLayer.S01.UCAccountManagerBL();
        public Action AfterInsertOrUpdate;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region 新增帳號
        public void Add()
        {
            main_fv.ChangeMode(FormViewMode.Insert);
            main_fv.DataBind();
            main_fv.FindControl("act_id_txt").Focus();
            (main_fv.FindControl("ucAccountRoleManager") as UCAccountRoleManager).BindData();
        }
        #region 新增
        protected void main_fv_ItemInserting(object sender, FormViewInsertEventArgs e)
        {
            var res = new CommonResult(true);
            var fv = sender as FormView;

            if (CommonConvert.GetStringOrEmptyString(e.Values["Act_pwd"]) != CommonConvert.GetStringOrEmptyString(e.Values["Act_pwd_confirm"]))
            {
                res.IsSuccess = false;
                res.Message = "兩次輸入的密碼不一致!";
            }

            if (res.IsSuccess)
            {
                var dict = new Dictionary<string, object>();
                dict["act_id"] = e.Values["Act_id"].ToString().ToUpper();
                dict["act_name"] = e.Values["Act_name"];
                dict["act_pwd"] = e.Values["Act_pwd"];
                dict["act_mail"] = e.Values["Act_mail"];
                res = _bl.InsertData(dict, (main_fv.FindControl("ucAccountRoleManager") as UCAccountRoleManager).GetData());
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

        #region 編輯帳號
        public void Edit(string act_id)
        {
            var lst = new List<Model.S01.UCAccountManagerInfo.Main>();
            var act = _bl.GetData(act_id);
            lst.Add(act);
            main_fv.ChangeMode(FormViewMode.Edit);
            main_fv.DataSource = lst;
            main_fv.DataBind();
            (main_fv.FindControl("ucAccountRoleManager") as UCAccountRoleManager).BindData(act);
        }
        protected void main_fv_ItemUpdating(object sender, FormViewUpdateEventArgs e)
        {
            var res = new CommonResult(true);
            var fv = sender as FormView;

            var act_pwd = (fv.FindControl("act_pwd_txt") as TextBox).Text.Trim();
            var act_pwd_confirm = (fv.FindControl("act_pwd_confirm_txt") as TextBox).Text.Trim();
            if (CommonConvert.GetStringOrEmptyString(act_pwd) != CommonConvert.GetStringOrEmptyString(act_pwd_confirm))
            {
                res.IsSuccess = false;
                res.Message = "兩次輸入的密碼不一致!";
            }

            if (res.IsSuccess)
            {
                var old_dict = new Dictionary<string, object>();
                old_dict["act_id"] = e.Keys["act_id"];

                var new_dict = new Dictionary<string, object>();
                new_dict["act_name"] = e.NewValues["Act_name"];
                new_dict["act_pwd"] = act_pwd;
                new_dict["act_mail"] = e.NewValues["Act_mail"];
                res = _bl.UpdateData(old_dict, new_dict, (main_fv.FindControl("ucAccountRoleManager") as UCAccountRoleManager).GetData());
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