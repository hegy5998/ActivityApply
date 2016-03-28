using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using Util;
using System.Data;
using System.Text;
using BusinessLayer.S01;
using Web.App_Code;

namespace Web.S01
{
    public partial class S01000501 : CommonPages.BasePage
    {
        S010005BL _bl = new S010005BL();

        protected void Page_Load(object sender, EventArgs e)
        {
            // 設定UserControl
            ucSystemModule.SelectedIndexChanged += () =>
            {
                GridViewHelper.ChgGridViewMode(GridViewHelper.GVMode.Normal, main_gv);
                BindMainGridView(GetMainData());
            };

            // 取消GridView分頁
            main_gv.AllowPaging = false;
            controlSet_gv.AllowPaging = false;

            if (!IsPostBack)
                BindMainGridView(GetMainData());
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
        private List<Sys_processInfo> GetMainData()
        {
            return _bl.GetProcessList(ucSystemModule.Sys_id, ucSystemModule.Sys_mid);
        }
        #endregion

        #region Main GridView事件
        private void BindMainGridView(List<Sys_processInfo> lst)
        {
            main_gv.DataSource = GridViewHelper.SortData<Sys_processInfo>(main_gv, lst); ;
            main_gv.DataBind();
            if (main_gv.Rows.Count == 0)
            {
                // 避免無資料列時，無法顯示footer新增資料
                var new_lst = new List<Sys_processInfo>();
                var info = new Sys_processInfo();
                info.Sys_show = "N";
                info.Sys_enable = "N";
                new_lst.Add(info);
                main_gv.DataSource = new_lst;
                main_gv.DataBind();
                main_gv.Rows[0].Attributes.Add("style", "display: none");
            }
        }
        protected void main_gv_Sorting(object sender, GridViewSortEventArgs e)
        {
            var lst = GridViewHelper.SortGridView<Sys_processInfo>((GridView)sender, e, GetMainData());
            BindMainGridView(lst);
        }
        protected void main_gv_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewHelper.ChgGridViewMode(GridViewHelper.GVMode.Edit, main_gv, e.NewEditIndex);
            BindMainGridView(GetMainData());
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void main_gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewHelper.ChgGridViewMode(GridViewHelper.GVMode.Normal, main_gv);
            BindMainGridView(GetMainData());
        }
        /// <summary>
        /// 命令處理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void main_gv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Add":         // 新增
                    GridViewHelper.ChgGridViewMode(GridViewHelper.GVMode.Insert, main_gv);
                    BindMainGridView(GetMainData());
                    break;
                case "AddSave":     // 儲存新增資料
                    AddSave();
                    break;
                case "Set":         // 子功能設定controlSet_sys_pname_lbl
                    InitControlSetPopupWindow(sender, e);
                    break;
            }
        }
        /// <summary>
        /// RowDataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void main_gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowState.ToString().Contains("Edit"))
                {
                    // 編輯列
                    DropDownList sys_id_ddl = e.Row.FindControl("sys_id_ddl") as DropDownList;
                    DropDownList sys_mid_ddl = e.Row.FindControl("sys_mid_ddl") as DropDownList;
                    var module_lst = _bl.GetModuleList(sys_id_ddl.SelectedValue);
                    sys_mid_ddl.DataSource = module_lst;
                    sys_mid_ddl.DataBind();
                    sys_mid_ddl.SelectedValue = (e.Row.FindControl("old_sys_mid_hf") as HiddenField).Value;
                }
                else
                {
                    // 一般資料列
                    string sys_id = (e.Row.FindControl("sys_id_lbl") as Label).Text;
                    string sys_name = (e.Row.FindControl("sys_name_lbl") as Label).Text;
                    string sys_mid = (e.Row.FindControl("sys_mid_lbl") as Label).Text;
                    string sys_mname = (e.Row.FindControl("sys_mname_lbl") as Label).Text;
                    string sys_pid = (e.Row.FindControl("sys_pid_lbl") as Label).Text;
                    string sys_pname = (e.Row.FindControl("sys_pname_lbl") as Label).Text;
                    (e.Row.FindControl("delete_btn") as Button).OnClientClick = "if (!confirm(\"系統：" + sys_name + "(" + sys_id + ")\\n模組：" + sys_mname + "(" + sys_mid + ")\\n作業：" + sys_pname + "(" + sys_pid + ")\\n\\n確定要刪除嗎?\")) return false";
                }
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                // 新增列
                DropDownList sys_id_ddl = e.Row.FindControl("sys_id_ddl") as DropDownList;
                sys_id_ddl.SelectedValue = ucSystemModule.Sys_id;
                (e.Row.FindControl("sys_id_lbl") as Label).Text = sys_id_ddl.SelectedValue;

                DropDownList sys_mid_ddl = e.Row.FindControl("sys_mid_ddl") as DropDownList;
                var module_lst = _bl.GetModuleList(sys_id_ddl.SelectedValue);
                sys_mid_ddl.DataSource = module_lst;
                sys_mid_ddl.DataBind();
                if (ucSystemModule.Sys_mid.Length > 0)
                    sys_mid_ddl.SelectedValue = ucSystemModule.Sys_mid;
                (e.Row.FindControl("sys_mid_lbl") as Label).Text = sys_mid_ddl.SelectedValue;

                (e.Row.FindControl("sys_pid_txt") as TextBox).Text = sys_id_ddl.SelectedValue;
                (e.Row.FindControl("sys_purl_txt") as TextBox).Text = "~/" + sys_id_ddl.SelectedValue + "/" + sys_id_ddl.SelectedValue + ".aspx";
            }
        }
        /// <summary>
        /// GridView 系統選單連動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void sys_id_ddl_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList sys_id_ddl = sender as DropDownList;
            GridViewRow gvr = sys_id_ddl.NamingContainer as GridViewRow;

            // 連動系統代碼
            Label sys_id_lbl = gvr.FindControl("sys_id_lbl") as Label;
            sys_id_lbl.Text = sys_id_ddl.SelectedValue;

            // 連動模組名稱及代碼
            DropDownList sys_mid_ddl = gvr.FindControl("sys_mid_ddl") as DropDownList;
            var module_lst = _bl.GetModuleList(sys_id_ddl.SelectedValue);
            sys_mid_ddl.DataSource = module_lst;
            sys_mid_ddl.DataBind();

            Label sys_mid_lbl = gvr.FindControl("sys_mid_lbl") as Label;
            sys_mid_lbl.Text = sys_mid_ddl.SelectedValue;
        }
        /// <summary>
        /// GridView 模組選單連動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void sys_mid_ddl_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList sys_mid_ddl = sender as DropDownList;
            GridViewRow gvr = (sender as DropDownList).NamingContainer as GridViewRow;

            // 連動模組代碼
            Label sys_mid_lbl = gvr.FindControl("sys_mid_lbl") as Label;
            sys_mid_lbl.Text = sys_mid_ddl.SelectedValue;
        }
        /// <summary>
        /// 新增
        /// </summary>
        private void AddSave()
        {
            if (ProcessModifyAuth)
            {
                GridViewRow gvr = main_gv.FooterRow;
                var dict = new Dictionary<string, object>();
                dict["sys_mid"] = (gvr.FindControl("sys_mid_ddl") as DropDownList).SelectedValue;
                dict["sys_pid"] = (gvr.FindControl("sys_pid_txt") as TextBox).Text.Trim();
                dict["sys_pname"] = (gvr.FindControl("sys_pname_txt") as TextBox).Text.Trim();
                dict["sys_purl"] = (gvr.FindControl("sys_purl_txt") as TextBox).Text.Trim();
                dict["sys_seq"] = CommonConvert.GetIntOrNull((gvr.FindControl("sys_seq_txt") as TextBox).Text.Trim());
                dict["sys_show"] = (gvr.FindControl("sys_show_ddl") as DropDownList).SelectedValue;
                dict["sys_enable"] = (gvr.FindControl("sys_enable_ddl") as DropDownList).SelectedValue;

                // 新增資料
                var res = _bl.InsertData(dict);
                if (res.IsSuccess)
                {
                    // 新增成功，切換回一般模式
                    GridViewHelper.ChgGridViewMode(GridViewHelper.GVMode.Normal, main_gv);
                    BindMainGridView(GetMainData());
                    ShowPopupMessage(ITCEnum.PopupMessageType.Success, ITCEnum.DataActionType.Insert);
                }
                else
                {
                    // 新增失敗，顯示錯誤訊息
                    ShowPopupMessage(ITCEnum.PopupMessageType.Error, ITCEnum.DataActionType.Insert, res.Message);
                }
            }
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void main_gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (ProcessModifyAuth)
            {
                GridViewRow gvr = main_gv.Rows[e.RowIndex];
                var oldData_dict = new Dictionary<string, object>();
                oldData_dict["sys_pid"] = (gvr.FindControl("old_sys_pid_hf") as HiddenField).Value;

                var newData_dict = new Dictionary<string, object>();
                newData_dict["sys_mid"] = (gvr.FindControl("sys_mid_ddl") as DropDownList).SelectedValue;
                newData_dict["sys_pid"] = (gvr.FindControl("sys_pid_txt") as TextBox).Text.Trim();
                newData_dict["sys_pname"] = (gvr.FindControl("sys_pname_txt") as TextBox).Text.Trim();
                newData_dict["sys_purl"] = (gvr.FindControl("sys_purl_txt") as TextBox).Text.Trim();
                newData_dict["sys_seq"] = CommonConvert.GetIntOrNull((gvr.FindControl("sys_seq_txt") as TextBox).Text.Trim());
                newData_dict["sys_show"] = (gvr.FindControl("sys_show_ddl") as DropDownList).SelectedValue;
                newData_dict["sys_enable"] = (gvr.FindControl("sys_enable_ddl") as DropDownList).SelectedValue;

                var res = _bl.UpdateData(oldData_dict, newData_dict);
                if (res.IsSuccess)
                {
                    // 更新成功，切換回一般模式
                    GridViewHelper.ChgGridViewMode(GridViewHelper.GVMode.Normal, main_gv);
                    BindMainGridView(GetMainData());
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
        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void main_gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (ProcessModifyAuth)
            {
                GridViewRow gvr = (sender as GridView).Rows[e.RowIndex];
                var dict = new Dictionary<string, object>();
                dict["sys_pid"] = CommonConvert.GetStringOrEmptyString((gvr.FindControl("sys_pid_lbl") as Label).Text);
                var res = _bl.DeleteData(dict);
                if (res.IsSuccess)
                {
                    BindMainGridView(GetMainData());
                    ShowPopupMessage(ITCEnum.PopupMessageType.Success, ITCEnum.DataActionType.Delete);
                }
                else
                    ShowPopupMessage(ITCEnum.PopupMessageType.Error, ITCEnum.DataActionType.Delete, res.Message);
            }
        }
        /// <summary>
        /// Init 子功能設定視窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InitControlSetPopupWindow(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow gvr = (e.CommandSource as Button).NamingContainer as GridViewRow;

            string sys_pid = (gvr.FindControl("old_sys_pid_hf") as HiddenField).Value;
            Sys_processInfo process_info = _bl.GetProcessData(sys_pid);
            controlSet_tilte_lbl.Text = "子功能設定";
            controlSet_sys_pid_lbl.Text = process_info.Sys_pid;
            controlSet_sys_pname_lbl.Text = process_info.Sys_pname;

            GridViewHelper.ChgGridViewMode(GridViewHelper.GVMode.Normal, controlSet_gv);
            BindControlSetGridView(GetControlSetData());
            controlSet_mpe.Show();  // Popup Window
            controlSet_pl.Visible = true;
        }
        #endregion

        #region ControlSet GridView相關
        /// <summary>
        /// 子功能視窗關閉
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void controlSet_cancel_btn_Click(object sender, EventArgs e)
        {
            BindMainGridView(GetMainData());
        }
        /// <summary>
        /// 取得子功能資料
        /// </summary>
        private List<Sys_processcontrolInfo> GetControlSetData()
        {
            return _bl.GetControlList(controlSet_sys_pid_lbl.Text);
        }
        /// <summary>
        /// Bind資料
        /// </summary>
        private void BindControlSetGridView(List<Sys_processcontrolInfo> lst)
        {
            controlSet_gv.DataSource = lst;
            controlSet_gv.DataBind();

            if (controlSet_gv.Rows.Count == 0)
            {
                // 避免無資料列時，無法顯示footer新增資料
                var new_lst = new List<Sys_processcontrolInfo>();
                new_lst.Add(new Sys_processcontrolInfo());
                controlSet_gv.DataSource = new_lst;
                controlSet_gv.DataBind();
                controlSet_gv.Rows[0].Attributes.Add("style", "display: none");
            }
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void controlSet_gv_Sorting(object sender, GridViewSortEventArgs e)
        {
            GridView gv = (GridView)sender;
            var lst = GridViewHelper.SortGridView<Sys_processcontrolInfo>(gv, e, _bl.GetControlList(controlSet_sys_pid_lbl.Text));
            BindControlSetGridView(lst);
        }
        /// <summary>
        /// 編輯前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void controlSet_gv_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewHelper.ChgGridViewMode(GridViewHelper.GVMode.Edit, controlSet_gv, e.NewEditIndex);
            BindControlSetGridView(GetControlSetData());
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void controlSet_gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewHelper.ChgGridViewMode(GridViewHelper.GVMode.Normal, controlSet_gv);
            BindControlSetGridView(GetControlSetData());
        }
        /// <summary>
        /// RowDataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void controlSet_gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.DataRow:
                    if (e.Row.RowState.ToString().Contains("Edit") == false)
                    {
                        // 一般資料列
                        string sys_cid = (e.Row.FindControl("sys_cid_lbl") as Label).Text;
                        string sys_cnote = (e.Row.FindControl("sys_cnote_lbl") as Label).Text;
                        (e.Row.FindControl("delete_btn") as Button).OnClientClick = "if (!confirm(\"元件名稱：" + sys_cid + "\\n元件描述：" + sys_cnote + "\\n\\n確定要刪除嗎?\")) return false";
                    }
                    break;
            }
        }
        /// <summary>
        /// 命令處理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void controlSet_gv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Add":
                    GridViewHelper.ChgGridViewMode(GridViewHelper.GVMode.Insert, controlSet_gv);
                    BindControlSetGridView(GetControlSetData());
                    break;
                case "AddSave":
                    AddSaveControl();
                    break;
            }
        }
        /// <summary>
        /// 新增子功能
        /// </summary>
        private void AddSaveControl()
        {
            if (ProcessModifyAuth)
            {
                GridViewRow gvr = controlSet_gv.FooterRow;
                var dict = new Dictionary<string, object>();
                dict["sys_pid"] = controlSet_sys_pid_lbl.Text;
                dict["sys_cid"] = (gvr.FindControl("sys_cid_txt") as TextBox).Text.Trim();
                dict["sys_cnote"] = (gvr.FindControl("sys_cnote_txt") as TextBox).Text.Trim();

                // 新增資料
                var res = _bl.InsertControlData(dict);
                if (res.IsSuccess)
                {
                    // 新增成功，切換回一般模式
                    GridViewHelper.ChgGridViewMode(GridViewHelper.GVMode.Normal, controlSet_gv);
                    BindControlSetGridView(GetControlSetData());
                    ShowPopupMessage(ITCEnum.PopupMessageType.Success, ITCEnum.DataActionType.Insert);
                }
                else
                {
                    // 新增失敗，顯示錯誤訊息
                    ShowPopupMessage(ITCEnum.PopupMessageType.Error, ITCEnum.DataActionType.Insert, res.Message);
                }
            }
        }
        /// <summary>
        /// 更新子功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void controlSet_gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (ProcessModifyAuth)
            {
                GridViewRow gvr = controlSet_gv.Rows[e.RowIndex];
                var newData_dict = new Dictionary<string, object>();
                newData_dict["sys_pid"] = (gvr.FindControl("old_sys_pid_hf") as HiddenField).Value;
                newData_dict["sys_cid"] = (gvr.FindControl("sys_cid_txt") as TextBox).Text.Trim();
                newData_dict["sys_cnote"] = (gvr.FindControl("sys_cnote_txt") as TextBox).Text.Trim();

                var oldData_dict = new Dictionary<string, object>();
                oldData_dict["sys_pid"] = (gvr.FindControl("old_sys_pid_hf") as HiddenField).Value;
                oldData_dict["sys_cid"] = (gvr.FindControl("old_sys_cid_hf") as HiddenField).Value;

                var res = _bl.UpdateControlData(oldData_dict, newData_dict);
                if (res.IsSuccess)
                {
                    // 更新成功，切換回一般模式
                    GridViewHelper.ChgGridViewMode(GridViewHelper.GVMode.Normal, controlSet_gv);
                    BindControlSetGridView(GetControlSetData());
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
        /// <summary>
        /// 刪除子功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void controlSet_gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (ProcessModifyAuth)
            {
                GridViewRow gvr = (sender as GridView).Rows[e.RowIndex];
                var dict = new Dictionary<string, object>();
                dict["sys_pid"] = controlSet_sys_pid_lbl.Text;
                dict["sys_cid"] = (gvr.FindControl("sys_cid_lbl") as Label).Text;
                var res = _bl.DeleteControlData(dict);
                if (res.IsSuccess)
                {
                    BindControlSetGridView(GetControlSetData());
                    ShowPopupMessage(ITCEnum.PopupMessageType.Success, ITCEnum.DataActionType.Delete);
                }
                else
                    ShowPopupMessage(ITCEnum.PopupMessageType.Error, ITCEnum.DataActionType.Delete, res.Message);
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
            if (ucSystemModule.Sys_mid.IsNullOrWhiteSpace())
                ShowPopupMessage(ITCEnum.PopupMessageType.Error, "請先選擇要設定順序的[模組]!");
            else
            {
                var data = GetMainData().OrderBy(x => x.Sys_seq).ToList();
                order_rt.DataSource = data;
                order_rt.DataBind();

                popupWindow_mpe.Show();
                WebHelper.CallJavascript("sortable", "$('#orderList').sortable();");
            }
        }
        protected void setOrderOK_btn_Click(object sender, EventArgs e)
        {
            var items = orderList_hf.Value.Split(',').Where(x => x.IsNullOrWhiteSpace() == false).ToList();
            _bl.SetOrder(items);
            WebHelper.ShowPopupMessage(ITCEnum.PopupMessageType.Success, ITCEnum.DataActionType.Update);
            BindMainGridView(GetMainData());
            WebHelper.GetMainMasterPageContentPanel(sender as Control).Update();
            popupWindow_mpe.Hide();
        }
        #endregion
    }
}