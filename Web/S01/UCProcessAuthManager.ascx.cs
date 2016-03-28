using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using Util;
using System.ComponentModel.DataAnnotations;
using System.Web.ModelBinding;
using AjaxControlToolkit;
using Web.App_Code;
using Web.UserControls;
using System.Text;
using DataAccess;
using System.Collections;


namespace Web.S01
{
    public partial class UCProcessAuthManager : System.Web.UI.UserControl
   {
        private BusinessLayer.S01.UCProcessAuthManagerBL _bl = new BusinessLayer.S01.UCProcessAuthManagerBL();
        protected Sys_processInfo info = new Sys_processInfo();

        public Action AfterInsertOrDelete;

        #region 初始化並顯示畫面
        /// <summary>
        /// 初始化並顯示畫面
        /// </summary>
        /// <param name="sys_pid">作業代碼</param>
        public void Show(string sys_pid)
        {
            Sys_processInfo info = _bl.GetProcessInfo(sys_pid);
            sys_id_lbl.Text = info.Sys_id;
            sys_name_lbl.Text = info.Sys_name;
            sys_mid_lbl.Text = info.Sys_mid;
            sys_mname_lbl.Text = info.Sys_mname;
            sys_pid_lbl.Text = info.Sys_pid;
            sys_pname_lbl.Text = info.Sys_pname;
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

        #region 關閉畫面並釋放資源
        public void Close()
        { 
            
        }
        #endregion

        #region GridView事件
        private List<Model.S01.UCProcessAuthManagerInfo.Main> GetData()
        {
            return _bl.GetList(sys_pid_lbl.Text);
        }
        private void BindGridView(List<Model.S01.UCProcessAuthManagerInfo.Main> lst)
        {
            main_gv.DataSource = lst;
            main_gv.DataBind();
            if (main_gv.Rows.Count == 0)
            {
                var new_lst = new List<Model.S01.UCProcessAuthManagerInfo.Main>() { new Model.S01.UCProcessAuthManagerInfo.Main() };
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
            var lst = GridViewHelper.SortGridView<Model.S01.UCProcessAuthManagerInfo.Main>(sender as GridView, e, GetData());
            BindGridView(lst);
        }
        protected void main_gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var item = e.Row.DataItem as Model.S01.UCProcessAuthManagerInfo.Main;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowState.ToString().Contains("Edit") == false)
                {
                    #region 一般資料列
                    #region 設定刪除按鈕
                    (e.Row.FindControl("delete_btn") as Button).OnClientClick =
                        string.Format("if (!confirm(\"角色：{0} {1}\\n單位：{2} {3}\\n職務：{4} {5}\\n\\n確定要刪除這個身分的使用權限嗎?\")) return false",
                            item.Sys_rid, item.Sys_rname, item.Sys_uid, item.Sys_uname, item.Sys_rpid, item.Sys_rpname);
                    #endregion

                    #region 設定權限文字
                    (e.Row.FindControl("sys_modify_lbl") as Label).Text = (item.Sys_modify == "Y") ? "瀏覽、異動" : "僅瀏覽";
                    #endregion

                    #region 設定權限說明文字
                    (e.Row.FindControl("note_lbl") as Label).Text = GetAuthNote(item);
                    #endregion
                    #endregion
                }
                else
                {
                    #region 編輯列
                    #region 設定角色選單
                    var ucRoleDDL = e.Row.FindControl("ucRoleDDL") as UCRoleDDL;
                    ucRoleDDL.BindData();
                    ucRoleDDL.SelectedValue = item.Sys_rid;
                    ucRoleDDL_SelectedIndexChanged(ucRoleDDL, new EventArgs());
                    #endregion

                    #region 設定單位選單
                    var ucRoleUnitDDL = e.Row.FindControl("ucRoleUnitDDL") as UCRoleUnitDDL;
                    ucRoleUnitDDL.SelectedValue = item.Sys_uid;
                    ucRoleUnitDDL_SelectedIndexChanged(ucRoleUnitDDL, new EventArgs());
                    #endregion

                    #region 設定職位選單
                    var ucRoleUnitPositionDDL = e.Row.FindControl("ucRoleUnitPositionDDL") as UCRoleUnitPositionDDL;
                    ucRoleUnitPositionDDL.SelectedValue = item.Sys_rpid;
                    ucRoleUnitPositionDDL_SelectedIndexChanged(ucRoleUnitPositionDDL, new EventArgs());
                    #endregion

                    #region 設定權限選單
                    (e.Row.FindControl("sys_modify_ddl") as DropDownList).SelectedValue = item.Sys_modify;
                    #endregion

                    #region 設定權限說明文字
                    (e.Row.FindControl("note_lbl") as Label).Text = GetAuthNote(item);
                    #endregion
                    #endregion
                }
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                #region 新增列
                if ((sender as GridView).ShowFooter)
                {
                    #region 設定角色選單，並連棟單位、職位、權限選單
                    var ucRoleDDL = e.Row.FindControl("ucRoleDDL") as UCRoleDDL;
                    var ucRoleUnitDDL = e.Row.FindControl("ucRoleUnitDDL") as UCRoleUnitDDL;
                    var ucRoleUnitPositionDDL = e.Row.FindControl("ucRoleUnitPositionDDL") as UCRoleUnitPositionDDL;
                    ucRoleDDL.BindData();
                    ucRoleDDL_SelectedIndexChanged(ucRoleDDL, new EventArgs());

                    #region 若cookies中有預設資料，則預先帶入選取的資料
                    {
                        var pass = true;
                        #region 角色
                        var sys_rid_cookies = Request.Cookies[CommonHelper.GetCurSysPid() + "sys_rid"];
                        if (pass && sys_rid_cookies != null && ucRoleDDL.Items.FindByValue(sys_rid_cookies.Value) != null)
                        {
                            ucRoleDDL.SelectedValue = sys_rid_cookies.Value;
                            ucRoleDDL_SelectedIndexChanged(ucRoleDDL, new EventArgs());
                        }
                        else
                            pass = false;
                        #endregion

                        #region 單位
                        var sys_uid_cookies = Request.Cookies[CommonHelper.GetCurSysPid() + "sys_uid"];
                        if (pass && sys_uid_cookies != null && ucRoleUnitDDL.Items.FindByValue(sys_uid_cookies.Value) != null)
                        {
                            ucRoleUnitDDL.SelectedValue = sys_uid_cookies.Value;
                            ucRoleUnitDDL_SelectedIndexChanged(ucRoleUnitDDL, new EventArgs());
                        }
                        else
                            pass = false;
                        #endregion

                        #region 職位
                        var sys_rpid_cookies = Request.Cookies[CommonHelper.GetCurSysPid() + "sys_rpid"];
                        if (pass && sys_rpid_cookies != null && ucRoleUnitPositionDDL.Items.FindByValue(sys_rpid_cookies.Value) != null)
                        {
                            ucRoleUnitPositionDDL.SelectedValue = sys_rpid_cookies.Value;
                            ucRoleUnitPositionDDL_SelectedIndexChanged(ucRoleUnitPositionDDL, new EventArgs());
                        }
                        else
                            pass = false;
                        #endregion

                        #region 權限
                        var sys_modify_cookies = Request.Cookies[CommonHelper.GetCurSysPid() + "sys_modify"];
                        if (sys_modify_cookies != null)
                        {
                            (e.Row.FindControl("sys_modify_ddl") as DropDownList).SelectedValue = sys_modify_cookies.Value;
                        }
                        #endregion
                    }
                    #endregion
                    #endregion
                }
                #endregion
            }
        }

        #region 角色、單位、職位、權限選單連動
        protected void ucRoleDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ucRoleDDL = sender as UCRoleDDL;
            var gvr = ucRoleDDL.NamingContainer as GridViewRow;
            var ucRoleUnitDDL = gvr.FindControl("ucRoleUnitDDL") as UCRoleUnitDDL;

            ucRoleUnitDDL.BindData(ucRoleDDL.SelectedValue);
            ucRoleUnitDDL_SelectedIndexChanged(ucRoleUnitDDL, new EventArgs());
        }
        protected void ucRoleUnitDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ucRoleUnitDDL = sender as UCRoleUnitDDL;
            var gvr = ucRoleUnitDDL.NamingContainer as GridViewRow;
            var ucRoleDDL = gvr.FindControl("ucRoleDDL") as UCRoleDDL;
            var ucRoleUnitPositionDDL = gvr.FindControl("ucRoleUnitPositionDDL") as UCRoleUnitPositionDDL;

            ucRoleUnitPositionDDL.BindData(ucRoleDDL.SelectedValue, ucRoleUnitDDL.SelectedValue);
            ucRoleUnitPositionDDL_SelectedIndexChanged(ucRoleUnitPositionDDL, new EventArgs());
        }
        protected void ucRoleUnitPositionDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ucRoleUnitPositionDDL = sender as UCRoleUnitPositionDDL;
            var gvr = ucRoleUnitPositionDDL.NamingContainer as GridViewRow;
            var ucRoleDDL = gvr.FindControl("ucRoleDDL") as UCRoleDDL;
            var ucRoleUnitDDL = gvr.FindControl("ucRoleUnitDDL") as UCRoleUnitDDL;
            var sys_modify_ddl = gvr.FindControl("sys_modify_ddl") as DropDownList;

            sys_modify_ddl_SelectedIndexChanged(sys_modify_ddl, new EventArgs());
        }
        protected void sys_modify_ddl_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 變更權限說明文字
            var sys_modify_ddl = sender as DropDownList;
            var gvr = sys_modify_ddl.NamingContainer as GridViewRow;
            var ucRoleDDL = gvr.FindControl("ucRoleDDL") as UCRoleDDL;
            var ucRoleUnitDDL = gvr.FindControl("ucRoleUnitDDL") as UCRoleUnitDDL;
            var ucRoleUnitPositionDDL = gvr.FindControl("ucRoleUnitPositionDDL") as UCRoleUnitPositionDDL;

            var info = new Model.S01.UCProcessAuthManagerInfo.Main()
            {
                Sys_rid = ucRoleDDL.SelectedValue,
                Sys_rname = ucRoleDDL.SelectedItem.Text.Substring(ucRoleDDL.SelectedItem.Text.IndexOf(' ')+1),
                Sys_uid = ucRoleUnitDDL.SelectedValue,
                Sys_uname = (ucRoleUnitDDL.SelectedItem != null) ? ucRoleUnitDDL.SelectedItem.Text.Substring(ucRoleUnitDDL.SelectedItem.Text.IndexOf(' ') + 1) : "",
                Sys_rpid = ucRoleUnitPositionDDL.SelectedValue,
                Sys_rpname = (ucRoleUnitPositionDDL.SelectedItem != null) ? ucRoleUnitPositionDDL.SelectedItem.Text.Substring(ucRoleUnitPositionDDL.SelectedItem.Text.IndexOf(' ') + 1) : "",
                Sys_modify = sys_modify_ddl.SelectedValue
            };

            var note_lbl = gvr.FindControl("note_lbl") as Label;
            note_lbl.Text = GetAuthNote(info);
        }
        #endregion

        #region 取得權限說明文字
        /// <summary>
        /// 取得權限說明文字
        /// </summary>
        /// <param name="info">權限物件</param>
        /// <returns></returns>
        private string GetAuthNote(Model.S01.UCProcessAuthManagerInfo.Main info)
        {
            StringBuilder note_sb = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(info.Sys_rid + info.Sys_uid + info.Sys_rpid))
            {
                if (info.Sys_uid == AuthData.GlobalSymbol && info.Sys_rpid == AuthData.GlobalSymbol)
                    note_sb.Append(string.Format("全部的「<span class='alert'>{0}</span>」", info.Sys_rname));
                else if (info.Sys_uid == AuthData.GlobalSymbol)
                    note_sb.Append(string.Format("全部擔任「<span class='alert'>{0}</span>」的「<span class='alert'>{1}</span>」", info.Sys_rpname, info.Sys_rname));
                else if (info.Sys_rpid == AuthData.GlobalSymbol)
                    note_sb.Append(string.Format("全部「<span class='alert'>{0}</span>」的「<span class='alert'>{1}</span>」", info.Sys_uname, info.Sys_rname));
                else
                    note_sb.Append(string.Format("在「<span class='alert'>{0}</span>」擔任「<span class='alert'>{1}</span>」的「<span class='alert'>{2}</span>」", info.Sys_uname, info.Sys_rpname, info.Sys_rname));

                if (info.Sys_modify == "Y")
                    note_sb.Append("，具有「<span class='alert'>瀏覽、異動</span>」的權限。");
                else
                    note_sb.Append("，僅有「<span class='alert'>瀏覽</span>」的權限。");
            }

            return note_sb.ToString();
        }
        #endregion

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
            dict["sys_pid"] = sys_pid_lbl.Text;
            dict["sys_rid"] = (gvr.FindControl("ucRoleDDL") as UCRoleDDL).SelectedValue;
            dict["sys_uid"] = (gvr.FindControl("ucRoleUnitDDL") as UCRoleUnitDDL).SelectedValue;
            dict["sys_rpid"] = (gvr.FindControl("ucRoleUnitPositionDDL") as UCRoleUnitPositionDDL).SelectedValue;
            dict["sys_modify"] = (gvr.FindControl("sys_modify_ddl") as DropDownList).SelectedValue;

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
                Response.SetCookie(new HttpCookie(CommonHelper.GetCurSysPid() + "sys_rid", dict["sys_rid"].ToString()));
                Response.SetCookie(new HttpCookie(CommonHelper.GetCurSysPid() + "sys_uid", dict["sys_uid"].ToString()));
                Response.SetCookie(new HttpCookie(CommonHelper.GetCurSysPid() + "sys_rpid", dict["sys_rpid"].ToString()));
                Response.SetCookie(new HttpCookie(CommonHelper.GetCurSysPid() + "sys_modify", dict["sys_modify"].ToString()));

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
        protected void main_gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow gvr = (sender as GridView).Rows[e.RowIndex];

            var oldData_dict = new Dictionary<string, object>();
            foreach (DictionaryEntry item in e.Keys) oldData_dict[item.Key.ToString()] = item.Value;

            var newData_dict = new Dictionary<string, object>();
            newData_dict["sys_rid"] = (gvr.FindControl("ucRoleDDL") as UCRoleDDL).SelectedValue;
            newData_dict["sys_uid"] = (gvr.FindControl("ucRoleUnitDDL") as UCRoleUnitDDL).SelectedValue;
            newData_dict["sys_rpid"] = (gvr.FindControl("ucRoleUnitPositionDDL") as UCRoleUnitPositionDDL).SelectedValue;
            newData_dict["sys_modify"] = (gvr.FindControl("sys_modify_ddl") as DropDownList).SelectedValue;

            var res = _bl.UpdateData(oldData_dict, newData_dict);
            if (res.IsSuccess)
            {
                // 更新成功，切換回一般模式
                GridViewHelper.ChgGridViewMode(GridViewHelper.GVMode.Normal, main_gv);
                BindGridView(GetData());
                WebHelper.ShowPopupMessage(ITCEnum.PopupMessageType.Success, ITCEnum.DataActionType.Update);

                // 記憶所選的資料於cookies中
                Response.SetCookie(new HttpCookie(CommonHelper.GetCurSysPid() + "sys_rid", newData_dict["sys_rid"].ToString()));
                Response.SetCookie(new HttpCookie(CommonHelper.GetCurSysPid() + "sys_uid", newData_dict["sys_uid"].ToString()));
                Response.SetCookie(new HttpCookie(CommonHelper.GetCurSysPid() + "sys_rpid", newData_dict["sys_rpid"].ToString()));
                Response.SetCookie(new HttpCookie(CommonHelper.GetCurSysPid() + "sys_modify", newData_dict["sys_modify"].ToString()));
            }
            else
            {
                // 更新失敗，顯示錯誤訊息
                e.Cancel = true;
                WebHelper.ShowPopupMessage(ITCEnum.PopupMessageType.Error, ITCEnum.DataActionType.Update, res.Message);
            }
        }
        #endregion
   }
}