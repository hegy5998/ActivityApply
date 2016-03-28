using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using Util;
using BusinessLayer.S01;
using System.Data;
using System.Collections;
using AjaxControlToolkit;
using Web.App_Code;

namespace Web.S01
{
    public partial class S01000301 : CommonPages.BasePage
    {
        S010003BL _bl = new S010003BL();

        protected void Page_Load(object sender, EventArgs e)
        {
            // 取消GridView分頁
            main_gv.AllowPaging = false;

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
        private List<Sys_systemInfo> GetData()
        {
            return _bl.GetData();
        }
        #endregion

        #region Gridview(main_gv)事件
        /// <summary>
        /// Bind資料
        /// </summary>
        /// <param name="lst">資料</param>
        private void BindGridView(List<Sys_systemInfo> lst)
        {
            main_gv.DataSource = GridViewHelper.SortData<Sys_systemInfo>(main_gv, lst);
            main_gv.DataBind();
            if (main_gv.Rows.Count == 0)
            {
                var new_lst = new List<Sys_systemInfo>();
                var info = new Sys_systemInfo();
                new_lst.Add(info);
                main_gv.DataSource = new_lst;
                main_gv.DataBind();
                main_gv.Rows[0].Attributes["style"] = "display: none";
            }
        }
        protected void main_gv_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewHelper.ChgGridViewMode(GridViewHelper.GVMode.Edit, sender as GridView, e.NewEditIndex);
            BindGridView(GetData());
        }
        protected void main_gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewHelper.ChgGridViewMode(GridViewHelper.GVMode.Normal, main_gv);
            BindGridView(GetData());
        }
        protected void main_gv_Sorting(object sender, GridViewSortEventArgs e)
        {
            var lst = GridViewHelper.SortGridView<Sys_systemInfo>(sender as GridView, e, GetData());
            BindGridView(lst);
        }
        protected void main_gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowState.ToString().Contains("Edit") == false)
                {
                    // 一般資料列
                    string sys_id = (e.Row.FindControl("sys_id_lbl") as Label).Text;
                    string sys_name = (e.Row.FindControl("sys_name_lbl") as Label).Text;

                    (e.Row.FindControl("delete_btn") as Button).OnClientClick = "if (!confirm(\"系統：" + sys_id + " - " + sys_name + "\\n\\n確定要刪除嗎?\")) return false";
                }
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                // 新增列
                (e.Row.FindControl("sys_id_txt") as TextBox).Text = "S";
                (e.Row.FindControl("sys_url_txt") as TextBox).Text = "~/DefaultSystemIndex.aspx";
            }
        }
        protected void main_gv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Add":         // 新增
                    GridViewHelper.ChgGridViewMode(GridViewHelper.GVMode.Insert, main_gv);
                    BindGridView(GetData());
                    break;
                case "AddSave":     // 儲存新增資料
                    AddSave();
                    break;
            }
        }
        /// <summary>
        /// 新增
        /// </summary>
        private void AddSave()
        {
            if (ProcessModifyAuth)
            {
                GridViewRow gvr = main_gv.FooterRow;
                var data_dict = new Dictionary<string, object>();
                data_dict["sys_id"] = (gvr.FindControl("sys_id_txt") as TextBox).Text.Trim();
                data_dict["sys_name"] = (gvr.FindControl("sys_name_txt") as TextBox).Text.Trim();
                data_dict["sys_url"] = (gvr.FindControl("sys_url_txt") as TextBox).Text.Trim();
                data_dict["sys_menuimg"] = (gvr.FindControl("sys_menuimg_txt") as TextBox).Text.Trim();
                data_dict["sys_bannerimg"] = (gvr.FindControl("sys_bannerimg_txt") as TextBox).Text.Trim();
                data_dict["sys_seq"] = CommonConvert.GetIntOrNull((gvr.FindControl("sys_seq_txt") as TextBox).Text.Trim());
                data_dict["sys_enable"] = (gvr.FindControl("sys_enable_rbl") as RadioButtonList).SelectedValue;

                // 新增資料
                var res = _bl.InsertData(data_dict);
                if (res.IsSuccess)
                {
                    // 新增成功，切換回一般模式
                    GridViewHelper.ChgGridViewMode(GridViewHelper.GVMode.Normal, main_gv);
                    BindGridView(GetData());
                    main_gv.DataBind();
                    ShowPopupMessage(ITCEnum.PopupMessageType.Success, ITCEnum.DataActionType.Insert);
                }
                else
                {
                    // 新增失敗，顯示錯誤訊息
                    ShowPopupMessage(ITCEnum.PopupMessageType.Error, ITCEnum.DataActionType.Insert, res.Message);
                }
            }
        }
        protected void main_gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (ProcessModifyAuth)
            {
                GridViewRow gvr = (sender as GridView).Rows[e.RowIndex];

                var oldData_dict = new Dictionary<string, object>();
                oldData_dict["sys_id"] = CommonConvert.GetStringOrEmptyString((gvr.FindControl("sys_id_hf") as HiddenField).Value);

                var newData_dict = new Dictionary<string, object>();
                newData_dict["sys_id"] = CommonConvert.GetStringOrEmptyString(e.NewValues["Sys_id"]);
                newData_dict["sys_name"] = CommonConvert.GetStringOrEmptyString(e.NewValues["Sys_name"]);
                newData_dict["sys_url"] = CommonConvert.GetStringOrEmptyString(e.NewValues["Sys_url"]);
                newData_dict["sys_menuimg"] = CommonConvert.GetStringOrEmptyString(e.NewValues["Sys_menuimg"]);
                newData_dict["sys_bannerimg"] = CommonConvert.GetStringOrEmptyString(e.NewValues["Sys_bannerimg"]);
                newData_dict["sys_seq"] = CommonConvert.GetIntOrNull(e.NewValues["Sys_seq"]);
                newData_dict["sys_enable"] = CommonConvert.GetStringOrEmptyString(e.NewValues["Sys_enable"]);

                var res = _bl.UpdateData(oldData_dict, newData_dict);
                if (res.IsSuccess)
                {
                    // 更新成功，切換回一般模式
                    GridViewHelper.ChgGridViewMode(GridViewHelper.GVMode.Normal, main_gv);
                    BindGridView(GetData());
                    ShowPopupMessage(ITCEnum.PopupMessageType.Success, ITCEnum.DataActionType.Update);
                }
                else
                {
                    // 更新失敗，顯示錯誤訊息
                    e.Cancel = true;
                    ShowPopupMessage(ITCEnum.PopupMessageType.Error, ITCEnum.DataActionType.Update, res.Message);
                }
            }
        }
        protected void main_gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (ProcessModifyAuth)
            {
                GridViewRow gvr = (sender as GridView).Rows[e.RowIndex];
                var data_dict = new Dictionary<string, object>();
                data_dict["sys_id"] = CommonConvert.GetStringOrEmptyString((gvr.FindControl("sys_id_hf") as HiddenField).Value);
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
        }
        #endregion

        #region 設定順序
        protected void popupWindow_cancel_btn_Click(object sender, EventArgs e)
        {
            popupWindow_mpe.Hide();
        }
        protected void setOrder_btn_Click(object sender, EventArgs e)
        {
            var data = GetData().OrderBy(x => x.Sys_seq).ToList();
            order_rt.DataSource = data;
            order_rt.DataBind();

            popupWindow_mpe.Show();
            WebHelper.CallJavascript("sortable", "$('#orderList').sortable();");
        }
        protected void setOrderOK_btn_Click(object sender, EventArgs e)
        {
            var items = orderList_hf.Value.Split(',').Where(x => x.IsNullOrWhiteSpace() == false).ToList();
            _bl.SetOrder(items);
            WebHelper.ShowPopupMessage(ITCEnum.PopupMessageType.Success, ITCEnum.DataActionType.Update);
            BindGridView(GetData());
            WebHelper.GetMainMasterPageContentPanel(sender as Control).Update();
            popupWindow_mpe.Hide();
        }
        #endregion


    }
}