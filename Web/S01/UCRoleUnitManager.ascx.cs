using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using Util;
using Model;
using System.Text;
using Web.UserControls;
using Web.App_Code;
using System.Collections;

namespace Web.S01
{
    public partial class UCRoleUnitManager : System.Web.UI.UserControl
    {
        BusinessLayer.S01.UCRoleUnitManagerBL _bl = new BusinessLayer.S01.UCRoleUnitManagerBL();

        public Action AfterInsertOrDelete;

        #region 初始化並顯示畫面
        /// <summary>
        /// 初始化並顯示畫面
        /// </summary>
        /// <param name="sys_rid">角色代碼</param>
        public void Show(string sys_rid)
        {
            var info = _bl.GetRoleInfo(sys_rid);
            ViewState["sys_rid"] = sys_rid;
            sys_rid_lbl.Text = info.Sys_rid;
            sys_rname_lbl.Text = info.Sys_rname;
            GridViewHelper.ChgGridViewMode(GridViewHelper.GVMode.Normal, main_gv);
            BindGridView(GetData());
            pl.Visible = true;
        }
        #endregion

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            ucGridViewPager.GridView = main_gv;
            ucGridViewPager.BindDataHandler += () => { BindGridView(GetData()); };

            ucRoleUnitPositionManagerDialog.AfterCloseDialog = () => {
                BindGridView(GetData());
                if (AfterInsertOrDelete != null) AfterInsertOrDelete(); 
            };
        }
        #endregion

        #region GridView事件
        private List<Model.S01.UCRoleUnitManagerInfo.Main> GetData()
        {
            return _bl.GetRoleUnitList(ViewState["sys_rid"].ToString());
        }
        private void BindGridView(List<Model.S01.UCRoleUnitManagerInfo.Main> lst)
        {
            main_gv.DataSource = GridViewHelper.SortData<Model.S01.UCRoleUnitManagerInfo.Main>(main_gv, lst); ;
            main_gv.DataBind();
            if (main_gv.Rows.Count == 0)
            {
                var new_lst = new List<Model.S01.UCRoleUnitManagerInfo.Main>() { new Model.S01.UCRoleUnitManagerInfo.Main() };
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
            var lst = GridViewHelper.SortGridView<Model.S01.UCRoleUnitManagerInfo.Main>(sender as GridView, e, GetData());
            BindGridView(lst);
        }
        protected void main_gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var item = e.Row.DataItem as Model.S01.UCRoleUnitManagerInfo.Main;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowState.ToString().Contains("Edit") == false)
                {
                    #region 一般資料列
                    #region 設定刪除按鈕
                    (e.Row.FindControl("delete_btn") as Button).OnClientClick =
                        string.Format("if (!confirm(\"角色：{0} {1}\\n單位：{2} {3}\\n\\n確定要刪除這個身分的使用權限嗎?\")) return false",
                            sys_rid_lbl.Text, sys_rname_lbl.Text, item.Sys_uid, item.Sys_uname);
                    #endregion
                    #endregion
                }
                else
                {
                    #region 編輯列
                    #endregion
                }
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                #region 新增列
                if ((sender as GridView).ShowFooter)
                {
                    #region 設定角色選單，並連動單位、職位、權限選單
                    var ucUnitDDL = e.Row.FindControl("ucUnitDDL") as UCUnitDDL;
                    ucUnitDDL.BindData();
                    #endregion
                }
                #endregion
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
        private void AddSave()
        {
            GridViewRow gvr = main_gv.FooterRow;
            var dict = new Dictionary<string, object>();
            dict["sys_rid"] = ViewState["sys_rid"].ToString();
            dict["sys_uid"] = (gvr.FindControl("ucUnitDDL") as UCUnitDDL).SelectedValue;

            // 新增資料
            var res = _bl.InsertData(dict);
            if (res.IsSuccess)
            {
                // 新增成功，切換回一般模式
                GridViewHelper.ChgGridViewMode(GridViewHelper.GVMode.Normal, main_gv);
                BindGridView(GetData());
                main_gv.DataBind();
                WebHelper.ShowPopupMessage(ITCEnum.PopupMessageType.Success, ITCEnum.DataActionType.Insert);

                // 記憶所選的資料於cookies中
                Response.SetCookie(new HttpCookie(CommonHelper.GetCurSysPid() + "sys_uid", dict["sys_uid"].ToString()));

                if (AfterInsertOrDelete != null)
                    AfterInsertOrDelete();
            }
            else
            {
                // 新增失敗，顯示錯誤訊息
                WebHelper.ShowPopupMessage(ITCEnum.PopupMessageType.Error, ITCEnum.DataActionType.Insert, res.Message);
            }
        }
        protected void main_gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var data_dict = new Dictionary<string, object>();
            foreach (DictionaryEntry item in e.Keys)
            {
                data_dict[item.Key.ToString()] = item.Value;
            }

            var res = _bl.DeleteData(data_dict);
            if (res.IsSuccess)
            {
                // 刪除成功，切換回一般模式
                BindGridView(GetData());
                WebHelper.ShowPopupMessage(ITCEnum.PopupMessageType.Success, ITCEnum.DataActionType.Delete);
                if (AfterInsertOrDelete != null)
                    AfterInsertOrDelete();
            }
            else
            {
                // 刪除失敗，顯示錯誤訊息
                WebHelper.ShowPopupMessage(ITCEnum.PopupMessageType.Error, ITCEnum.DataActionType.Delete, res.Message);
            }
        }
        #endregion

        #region 職位設定按鈕
        protected void setRoleUnitPosition_btn_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            var args = btn.CommandArgument.Split(',');
            ucRoleUnitPositionManagerDialog.Show(args[0], args[1]);
            ucRoleUnitPositionManagerDialog.Visible = true;
            var gvr = btn.NamingContainer as GridViewRow;
            var gv = gvr.NamingContainer as GridView;
            gv.SelectedIndex = gvr.RowIndex;
        }
        #endregion

        #region 設定通用職位按鈕
        protected void setRoleCommonPosition_btn_Click(object sender, EventArgs e)
        {
            ucRoleUnitPositionManagerDialog.Show(ViewState["sys_rid"].ToString(), DataAccess.AuthData.GlobalSymbol);
            ucRoleUnitPositionManagerDialog.Visible = true;
        }
        #endregion
    }
}