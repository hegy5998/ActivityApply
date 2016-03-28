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

namespace Web.S01
{
    public partial class S01000601 : CommonPages.BasePage
    {
        S010006BL _bl = new S010006BL();

        protected void Page_Load(object sender, EventArgs e)
        {
            ucGridViewPager.GridView = main_gv;
            ucGridViewPager.BindDataHandler += () => { BindGridView(GetData()); };
            ucRoleUnitManager.AfterInsertOrDelete = () => { BindGridView(GetData()); };

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
        private List<Model.S01.S010006Info.Main> GetData()
        {
            return _bl.GetData();
        }
        #endregion

        #region Gridview(main_gv)事件
        /// <summary>
        /// Bind資料
        /// </summary>
        /// <param name="lst">資料</param>
        private void BindGridView(List<Model.S01.S010006Info.Main> lst)
        {
            main_gv.DataSource = GridViewHelper.SortData<Model.S01.S010006Info.Main>(main_gv, lst);
            main_gv.DataBind();
            if (main_gv.Rows.Count == 0)
            {
                var new_lst = new List<Model.S01.S010006Info.Main>();
                var info = new Model.S01.S010006Info.Main();
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
            var lst = GridViewHelper.SortGridView<Model.S01.S010006Info.Main>(sender as GridView, e, GetData());
            BindGridView(lst);
        }
        protected void main_gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var item = e.Row.DataItem as Model.S01.S010006Info.Main;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowState.ToString().Contains("Edit") == false)
                {
                    // 一般資料列
                    (e.Row.FindControl("delete_btn") as Button).OnClientClick = "if (!confirm(\"角色：" + item.Sys_rid + " - " + item.Sys_rname + "\\n\\n確定要刪除嗎?\")) return false";
                }
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
                data_dict["sys_rid"] = (gvr.FindControl("sys_rid_txt") as TextBox).Text.Trim();
                data_dict["sys_rname"] = (gvr.FindControl("sys_rname_txt") as TextBox).Text.Trim();
                data_dict["sys_rnote"] = (gvr.FindControl("sys_rnote_txt") as TextBox).Text.Trim();

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
                foreach (DictionaryEntry item in e.Keys) oldData_dict[item.Key.ToString()] = item.Value;

                var newData_dict = new Dictionary<string, object>();
                newData_dict["sys_rid"] = (gvr.FindControl("sys_rid_txt") as TextBox).Text.Trim();
                newData_dict["sys_rname"] = (gvr.FindControl("sys_rname_txt") as TextBox).Text.Trim();
                newData_dict["sys_rnote"] = (gvr.FindControl("sys_rnote_txt") as TextBox).Text.Trim();

                var res = _bl.UpdateData(oldData_dict, newData_dict);
                if (res.IsSuccess)
                {
                    // 更新成功，切換回一般模式
                    GridViewHelper.ChgGridViewMode(GridViewHelper.GVMode.Normal, main_gv);
                    BindGridView(GetData());
                    ShowPopupMessage(ITCEnum.PopupMessageType.Success, ITCEnum.DataActionType.Update);
                    
                    // 隱藏單位設定視窗，避免異動角色時時，單位畫面仍停留在原角色代碼結果
                    ucRoleUnitManager.Visible = false;
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
                foreach (DictionaryEntry item in e.Keys) data_dict[item.Key.ToString()] = item.Value;
                var res = _bl.DeleteData(data_dict);
                if (res.IsSuccess)
                {
                    // 刪除成功，切換回一般模式
                    BindGridView(GetData());
                    ShowPopupMessage(ITCEnum.PopupMessageType.Success, ITCEnum.DataActionType.Delete);

                    // 隱藏單位設定視窗，避免異動角色時時，單位畫面仍停留在原角色代碼結果
                    ucRoleUnitManager.Visible = false;
                }
                else
                {
                    // 刪除失敗，顯示錯誤訊息
                    ShowPopupMessage(ITCEnum.PopupMessageType.Error, ITCEnum.DataActionType.Delete, res.Message);
                }
            }
        }

        #region 設定角色單位按紐
        protected void setRoleUnit_btn_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            ucRoleUnitManager.Show(btn.CommandArgument);
            ucRoleUnitManager.Visible = true;
            var gvr = btn.NamingContainer as GridViewRow;
            var gv = gvr.NamingContainer as GridView;
            gv.SelectedIndex = gvr.RowIndex;
        }
        #endregion
        #endregion

    }
}