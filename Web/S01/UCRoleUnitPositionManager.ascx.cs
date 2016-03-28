using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Util;
using Web.App_Code;
using Web.UserControls;

namespace Web.S01
{
    public partial class UCRoleUnitPositionManager : System.Web.UI.UserControl
    {
        BusinessLayer.S01.UCRoleUnitPositionManagerBL _bl = new BusinessLayer.S01.UCRoleUnitPositionManagerBL();

        #region 初始化並顯示畫面
        /// <summary>
        /// 初始化並顯示畫面
        /// </summary>
        /// <param name="sys_rid">角色代碼</param>
        /// <param name="sys_uid">單位代碼</param>
        public void Show(string sys_rid, string sys_uid)
        {
            var info = _bl.GetRoleUnitInfo(sys_rid, sys_uid);
            ViewState["sys_rid"] = sys_rid;
            ViewState["sys_uid"] = sys_uid;
            sys_rid_lbl.Text = info.Sys_rid;
            sys_rname_lbl.Text = info.Sys_rname;
            sys_uid_lbl.Text = info.Sys_uid;
            sys_uname_lbl.Text = info.Sys_uname;
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
        }
        #endregion

        #region GridView事件
        private List<Model.S01.UCRoleUnitManagerPositionInfo.Main> GetData()
        {
            return _bl.GetRoleUnitPositionList(ViewState["sys_rid"].ToString(), ViewState["sys_uid"].ToString());
        }
        private void BindGridView(List<Model.S01.UCRoleUnitManagerPositionInfo.Main> lst)
        {
            main_gv.DataSource = GridViewHelper.SortData<Model.S01.UCRoleUnitManagerPositionInfo.Main>(main_gv, lst);
            main_gv.DataBind();
            if (main_gv.Rows.Count == 0)
            {
                var new_lst = new List<Model.S01.UCRoleUnitManagerPositionInfo.Main>() { new Model.S01.UCRoleUnitManagerPositionInfo.Main() };
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
            var lst = GridViewHelper.SortGridView<Model.S01.UCRoleUnitManagerPositionInfo.Main>(sender as GridView, e, GetData());
            BindGridView(lst);
        }
        protected void main_gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var item = e.Row.DataItem as Model.S01.UCRoleUnitManagerPositionInfo.Main;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowState.ToString().Contains("Edit") == false)
                {
                    #region 一般資料列
                    #region 設定刪除按鈕
                    (e.Row.FindControl("delete_btn") as Button).OnClientClick =
                        string.Format("if (!confirm(\"職位：{0} {1}\\n\\n確定要刪除這個職位嗎?\")) return false",
                            item.Sys_rpid, item.Sys_rpname);
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
            dict["sys_uid"] = ViewState["sys_uid"].ToString();
            dict["sys_rpname"] = (gvr.FindControl("sys_rpname_txt") as TextBox).Text.Trim();
            dict["sys_seq"] = CommonConvert.GetIntOrNull((gvr.FindControl("sys_seq_txt") as TextBox).Text.Trim());

            // 新增資料
            var res = _bl.InsertData(dict);
            if (res.IsSuccess)
            {
                // 新增成功，切換回一般模式
                GridViewHelper.ChgGridViewMode(GridViewHelper.GVMode.Normal, main_gv);
                BindGridView(GetData());
                main_gv.DataBind();
                WebHelper.ShowPopupMessage(ITCEnum.PopupMessageType.Success, ITCEnum.DataActionType.Insert);
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
            }
            else
            {
                // 刪除失敗，顯示錯誤訊息
                WebHelper.ShowPopupMessage(ITCEnum.PopupMessageType.Error, ITCEnum.DataActionType.Delete, res.Message);
            }
        }
        protected void main_gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow gvr = (sender as GridView).Rows[e.RowIndex];

            var oldData_dict = new Dictionary<string, object>();
            foreach (DictionaryEntry item in e.Keys) oldData_dict[item.Key.ToString()] = item.Value;

            var newData_dict = new Dictionary<string, object>();
            newData_dict["sys_rpname"] = (gvr.FindControl("sys_rpname_txt") as TextBox).Text.Trim();
            newData_dict["sys_seq"] = CommonConvert.GetIntOrNull((gvr.FindControl("sys_seq_txt") as TextBox).Text.Trim());

            var res = _bl.UpdateData(oldData_dict, newData_dict);
            if (res.IsSuccess)
            {
                // 更新成功，切換回一般模式
                GridViewHelper.ChgGridViewMode(GridViewHelper.GVMode.Normal, main_gv);
                BindGridView(GetData());
                WebHelper.ShowPopupMessage(ITCEnum.PopupMessageType.Success, ITCEnum.DataActionType.Update);
            }
            else
            {
                // 更新失敗，顯示錯誤訊息
                e.Cancel = true;
                WebHelper.ShowPopupMessage(ITCEnum.PopupMessageType.Error, ITCEnum.DataActionType.Update, res.Message);
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
            _bl.SetOrder(sys_rid_lbl.Text, sys_uid_lbl.Text, items);
            WebHelper.ShowPopupMessage(ITCEnum.PopupMessageType.Success, ITCEnum.DataActionType.Update);
            BindGridView(GetData());
            popupWindow_mpe.Hide();
        }
        #endregion
    }
}