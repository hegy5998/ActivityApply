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
    public partial class S01001001 : CommonPages.BasePage
    {
        S010010BL _bl = new S010010BL();

        protected void Page_Load(object sender, EventArgs e)
        {
            ucGridViewPager.GridView = main_gv;
            ucGridViewPager.BindDataHandler += () => { BindGridView(GetData()); };
            ucAccountManagerDialog.AfterCloseDialog = () => { BindGridView(GetData()); };

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
        private List<Model.S01.S010010Info.Main> GetData()
        {
            var cond_dict = new Dictionary<string, string>();
            return _bl.GetList(cond_dict);
        }
        #endregion

        #region Gridview(main_gv)事件
        /// <summary>
        /// Bind資料
        /// </summary>
        /// <param name="lst">資料</param>
        private void BindGridView(List<Model.S01.S010010Info.Main> lst)
        {
            main_gv.DataSource = GridViewHelper.SortData<Model.S01.S010010Info.Main>(main_gv, lst);
            main_gv.DataBind();
            if (main_gv.Rows.Count == 0)
            {
                var new_lst = new List<Model.S01.S010010Info.Main>();
                var info = new Model.S01.S010010Info.Main();
                new_lst.Add(info);
                main_gv.DataSource = new_lst;
                main_gv.DataBind();
                main_gv.Rows[0].Attributes["style"] = "display: none";
            }
        }
        protected void main_gv_Sorting(object sender, GridViewSortEventArgs e)
        {
            var lst = GridViewHelper.SortGridView<Model.S01.S010010Info.Main>(sender as GridView, e, GetData());
            BindGridView(lst);
        }
        protected void main_gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var item = e.Row.DataItem as Model.S01.S010010Info.Main;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowState.ToString().Contains("Edit") == false)
                {
                    // 一般資料列
                    (e.Row.FindControl("delete_btn") as Button).OnClientClick = "if (!confirm(\"帳號：" + item.Act_id + " - " + item.Act_name + "\\n\\n確定要刪除嗎?\")) return false";
                }
            }
        }
        protected void main_gv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Add":         // 新增
                    ucAccountManagerDialog.Add();
                    break;
            }
        }
        protected void main_gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
            }
            else
            {
                // 刪除失敗，顯示錯誤訊息
                ShowPopupMessage(ITCEnum.PopupMessageType.Error, ITCEnum.DataActionType.Delete, res.Message);
            }
        }

        protected void main_gv_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            GridViewRow gvr = (sender as GridView).Rows[e.NewSelectedIndex];
            (gvr.NamingContainer as GridView).SelectedIndex = e.NewSelectedIndex;
            ucAccountManagerDialog.Edit((gvr.FindControl("act_id_lbl") as Label).Text);
        }
        #endregion


    }
}