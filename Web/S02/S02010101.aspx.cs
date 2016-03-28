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
using System.IO;

namespace Web.S02
{
    public partial class S02010101 : CommonPages.BasePage
    {
        S020101BL _bl = new S020101BL();

        protected void Page_Load(object sender, EventArgs e)
        {
            //ucGridViewPager.GridView = main_gv;
            //ucGridViewPager.BindDataHandler += () => { BindGridView(GetData()); };
            
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
        private DataTable GetData()
        {
            Dictionary<string, object> Cond = new Dictionary<string, object>();

            if (!string.IsNullOrWhiteSpace(q_keyword_tb.Text))
            {
                Cond.Add("keyword", q_keyword_tb.Text);
            }

            /*
            if (Cond.Count == 0)
            {
                ShowPopupMessage(ITCEnum.PopupMessageType.Error, "請至少輸入一項條件！");
                return new DataTable();
            }
            */

            ucGridViewPager.Visible = true;
            return _bl.GetData(Cond);
        }
        #endregion

        #region Gridview(main_gv)事件
        /// <summary>
        /// Bind資料
        /// </summary>
        /// <param name="lst">資料</param>
        private void BindGridView(DataTable lst)
        {
            main_gv.DataSource = lst;
            main_gv.DataBind();

            ready_gv.DataSource = lst;
            ready_gv.DataBind();

            end_gv.DataSource = lst;
            end_gv.DataBind();
        }
        protected void main_gv_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewHelper.ChgGridViewMode(GridViewHelper.GVMode.Edit, sender as GridView, e.NewEditIndex);
        }
        protected void main_gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //GridViewHelper.ChgGridViewMode(GridViewHelper.GVMode.Normal, main_gv);
            BindGridView(GetData());
        }
        protected void main_gv_Sorting(object sender, GridViewSortEventArgs e)
        {
            var lst = GridViewHelper.SortGridView(sender as GridView, e, GetData());
            BindGridView(lst);
        }
        protected void main_gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowState.ToString().Contains("Edit") == false)
                {
                    // 一般資料列
                    /*
                    string pstid = (e.Row.FindControl("pstid_hf") as HiddenField).Value;
                    string pstnam = (e.Row.FindControl("pstnam_lbl") as Label).Text;

                    (e.Row.FindControl("delete_btn") as Button).OnClientClick = "if (!confirm(\"用電戶： " + pstnam + " ( " + pstid + " )\\n\\n確定要刪除嗎?\")) return false";
                    */
                }
            }
        }
        protected void main_gv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                // 新增
                case "Add":
                    Response.Redirect("S02010102.aspx?sys_id=S01&sys_pid=S02010102");
                    Response.End();
                    break;
            }
        }
        protected void main_gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (ProcessModifyAuth)
            {
                GridViewRow gvr = (sender as GridView).Rows[e.RowIndex];
                var data_dict = new Dictionary<string, object>();
                data_dict["act_idn"] = CommonConvert.GetStringOrEmptyString((gvr.FindControl("act_idn_hf") as HiddenField).Value);

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

        protected void Back_btn_Click(object sender, EventArgs e)
        {
            BindGridView(GetData());
        }

        #region 查詢按鈕
        protected void q_query_btn_Click(object sender, EventArgs e)
        {
            BindGridView(GetData());
        }
        #endregion

        #region 修改按鈕
        protected void editActivity_btn_Click(object sender, EventArgs e)
        {
            Response.Redirect("S02010103.aspx?sys_id=S01&sys_pid=S02010103");
            Response.End();
        }
        #endregion

        #region 協作者
        protected void saveHelp_btn_Click(object sender, EventArgs e)
        {
            Response.Redirect("S02010101.aspx?sys_id=S01&sys_pid=S02010101");
            Response.End();
        }
        #endregion

        #region 測試用的送出_活動
        protected void test_submit_click(object sender, EventArgs e)
        {
            Dictionary<string, object> insert = new Dictionary<string, object>();

            insert["act_title"] = act_title.Text;
            insert["act_isopen"] = act_isopen.Text;

            _bl.InsertData(insert);
        }
        #endregion

        #region 測試用的修改_活動
        protected void editTest_btn_click(object sender, EventArgs e)
        {
            Dictionary<string, object> update = new Dictionary<string, object>();
            Dictionary<string, object> key = new Dictionary<string, object>();

            key["act_idn"] = act_idn.Text;

            update["act_title"] = act_title.Text;
            update["act_isopen"] = act_isopen.Text;

            _bl.UpdateData(key, update);
        }
        #endregion

        #region 測試用的刪除_活動
        protected void delTest_btn_click(object sender, EventArgs e)
        {
            Dictionary<string, object> key = new Dictionary<string, object>();

            key["act_idn"] = act_idn.Text;

            _bl.DeleteData(key);
        }
        #endregion

        protected void saveTestSession_btn_click(object sender, EventArgs e)
        {
            Dictionary<string, object> insert = new Dictionary<string, object>();

            insert["as_act"] = as_act.Text;
            insert["as_num_limit"] = as_num_limit.Text;

            _bl.InsertData_session(insert);
        }

        protected void saveTestApply_btn_click(object sender, EventArgs e)
        {
            Dictionary<string, object> insert = new Dictionary<string, object>();

            insert["aa_act"] = aa_act.Text;
            insert["aa_as"] = aa_as.Text;

            _bl.InsertData_apply(insert);
            
        }
    }
}