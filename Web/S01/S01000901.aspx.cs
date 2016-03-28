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

namespace Web.S01
{
    public partial class S01000901 : CommonPages.BasePage
    {
        S010009BL _bl = new S010009BL();

        protected void Page_Load(object sender, EventArgs e)
        {
            // 設定UserControl
            ucSystemModule.SelectedIndexChanged += () =>
            {
                GridViewHelper.ChgGridViewMode(GridViewHelper.GVMode.Normal, main_gv);
                BindMainGridView(GetMainData());
            };

            ucGridViewPager.GridView = main_gv;
            ucGridViewPager.BindDataHandler += () => { BindMainGridView(GetMainData()); };

            ucProcessAuthManager.AfterInsertOrDelete = () => { BindMainGridView(GetMainData()); };

            if (!IsPostBack)
            {
                BindMainGridView(GetMainData());
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
        private List<Model.S01.S010009Info.Main> GetMainData()
        {
            return _bl.GetProcessList(ucSystemModule.Sys_id, ucSystemModule.Sys_mid);
        }
        #endregion

        #region Main GridView事件
        #region BindMainGridView
        private void BindMainGridView(List<Model.S01.S010009Info.Main> lst)
        {
            main_gv.DataSource = lst;
            main_gv.DataBind();
            if (main_gv.Rows.Count == 0)
            {
                // 避免無資料列時，無法顯示footer新增資料
                var new_lst = new List<Model.S01.S010009Info.Main>();
                var info = new Model.S01.S010009Info.Main();
                new_lst.Add(info);
                main_gv.DataSource = new_lst;
                main_gv.DataBind();
                main_gv.Rows[0].Attributes.Add("style", "display: none");
            }
        }
        #endregion

        #region RowDataBound
        protected void main_gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var item = e.Row.DataItem as Model.S01.S010009Info.Main;

                // 若有子功能，則顯示子功能權限設定按鈕
                if (item.Sys_cid_count.GetValueOrDefault(0) > 0)
                {
                    var subFunc_btn = e.Row.FindControl("subFunc_btn") as Button;
                    subFunc_btn.Visible = true;
                }
            }
        }
        #endregion

        #region RowCommand
        protected void main_gv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var gvr = (e.CommandSource as Control).NamingContainer as GridViewRow;
            var gv = gvr.NamingContainer as GridView;

            switch (e.CommandName)
            {
                case "Set": // 顯示作業權限設定視窗
                    gv.SelectedIndex = gvr.RowIndex;
                    ucProcessAuthManager.Show(e.CommandArgument.ToString());
                    break;
            }
        }
        #endregion

        #region 開始子功能設定視窗
        protected void subFunc_btn_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            ucProcessSubFuncAuthManagerDialog.Show(btn.CommandArgument);
        }
        #endregion
        #endregion

    }
}