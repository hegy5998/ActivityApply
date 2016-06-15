using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using Util;
using BusinessLayer.S02;
using System.Data;
using System.Collections;
using AjaxControlToolkit;

namespace Web.S02
{
    public partial class S02010501 : CommonPages.BasePage
    {
        S020105BL _bl = new S020105BL();

        protected void Page_Load(object sender, EventArgs e)
        {
            ucGridViewPager.GridView = main_gv;
            ucGridViewPager.BindDataHandler += () => { BindGridView(GetData()); };
            ucActivityStatementDialog.AfterCloseDialog = () => { BindGridView(GetData()); };

            if (!IsPostBack)
            {
                BindGridView(GetData());
            }
        }

        #region Control權限管理
        protected void ManageControlAuth(object sender, EventArgs e)
        {
            ManageControlAuth(sender);
        }
        #endregion

        #region 取得資料
        /// <summary>
        /// 取得資料
        /// </summary>
        /// <returns>資料</returns>
        private List<Activity_statementInfo> GetData()
        {
            string act_id = CommonHelper.GetLoginUser().Act_id;
            return _bl.GetList(act_id);
        }
        #endregion

        #region Gridview(main_gv)事件
        /// <summary>
        /// Bind資料
        /// </summary>
        /// <param name="lst">資料</param>
        private void BindGridView(List<Activity_statementInfo> lst)
        {
            main_gv.DataSource = GridViewHelper.SortData<Activity_statementInfo>(main_gv, lst);
            main_gv.DataBind();
            if (main_gv.Rows.Count == 0)
            {
                var new_lst = new List<Activity_statementInfo>();
                var info = new Activity_statementInfo();
                new_lst.Add(info);
                main_gv.DataSource = new_lst;
                main_gv.DataBind();
                main_gv.Rows[0].Attributes["style"] = "display: none";
            }
        }
        protected void main_gv_Sorting(object sender, GridViewSortEventArgs e)
        {
            var lst = GridViewHelper.SortGridView<Activity_statementInfo>(sender as GridView, e, GetData());
            BindGridView(lst);
        }
        protected void main_gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var item = e.Row.DataItem as Activity_statementInfo;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField ast_public_hf = e.Row.FindControl("ast_public_hf") as HiddenField;
                Label ast_public_lbl = e.Row.FindControl("ast_public_lbl") as Label;
                if (ast_public_hf.Value.Equals("0"))
                    ast_public_lbl.Style.Add("display", "none");                
            }
        }
        protected void main_gv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Add":         // 新增
                    ucActivityStatementDialog.Add();
                    break;
            }
        }
        protected void main_gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow gvr = (sender as GridView).Rows[e.RowIndex];
            var data_dict = new Dictionary<string, object>();
            data_dict["ast_id"] = (gvr.FindControl("ast_id_hf") as HiddenField).Value.Trim();
            DataTable ifdelete = _bl.GetCountData((gvr.FindControl("ast_id_hf") as HiddenField).Value.Trim());
            if (Convert.ToInt32(ifdelete.Rows[0]["num"].ToString()) == 0)
            {
                var res = _bl.DeleteData(data_dict);
                if (res.IsSuccess)
                {
                    // 刪除成功，切換回一般模式
                    BindGridView(GetData());
                    ShowPopupMessage(ITCEnum.PopupMessageType.Success, ITCEnum.DataActionType.Delete);
                }
                else
                {
                    // 刪除失敗，顯示錯誤訊息
                    ShowPopupMessage(ITCEnum.PopupMessageType.Error, ITCEnum.DataActionType.Delete, res.Message);
                }
            }
            else
                ShowPopupMessage(ITCEnum.PopupMessageType.Error, ITCEnum.DataActionType.Delete, "尚有活動使用該聲明!!");
        }

        protected void main_gv_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            GridViewRow gvr = (sender as GridView).Rows[e.NewSelectedIndex];
            (gvr.NamingContainer as GridView).SelectedIndex = e.NewSelectedIndex;
            ucActivityStatementDialog.Edit(Convert.ToInt32((gvr.FindControl("ast_id_hf") as HiddenField).Value));
        }
        #endregion


    }
}