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
    public partial class UCProcessSubFuncAuthManager : System.Web.UI.UserControl
   {
        private BusinessLayer.S01.UCProcessSubFuncAuthManagerBL _bl = new BusinessLayer.S01.UCProcessSubFuncAuthManagerBL();

        #region 初始化並顯示畫面
        /// <summary>
        /// 初始化並顯示畫面
        /// </summary>
        /// <param name="sys_pid">作業代碼</param>
        public void Show(string sys_pid)
        {
            Sys_processInfo info = new Sys_processData().GetInfo(sys_pid);
            sys_id_lbl.Text = info.Sys_id;
            sys_name_lbl.Text = info.Sys_name;
            sys_mid_lbl.Text = info.Sys_mid;
            sys_mname_lbl.Text = info.Sys_mname;
            sys_pid_lbl.Text = info.Sys_pid;
            sys_pname_lbl.Text = info.Sys_pname;
            GridViewHelper.ChgGridViewMode(GridViewHelper.GVMode.Normal, main_gv);
            BindMainGridView(GetMainData());
            pl.Visible = true;
            mv.SetActiveView(main_view);
        }
        #endregion

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            ucGridViewPager.GridView = main_gv;
            ucGridViewPager.BindDataHandler += () => { BindMainGridView(GetMainData()); };
        }
        #endregion

        #region 關閉畫面並釋放資源
        public void Close()
        { 
            
        }
        #endregion
        
        #region 取得資料
        /// <summary>
        /// 取得資料
        /// </summary>
        /// <returns>資料</returns>
        private List<Model.S01.UCProcessSubFuncAuthManagerInfo.Main> GetMainData()
        {
            return new DataAccess.S01.UCProcessSubFuncAuthManagerData().GetList(sys_pid_lbl.Text);
        }
        #endregion

        #region Main GridView事件
        #region BindMainGridView
        private void BindMainGridView(List<Model.S01.UCProcessSubFuncAuthManagerInfo.Main> lst)
        {
            main_gv.DataSource = lst;
            main_gv.DataBind();
            if (main_gv.Rows.Count == 0)
            {
                // 避免無資料列時，無法顯示footer新增資料
                var new_lst = new List<Model.S01.UCProcessSubFuncAuthManagerInfo.Main>();
                var info = new Model.S01.UCProcessSubFuncAuthManagerInfo.Main();
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
                var item = e.Row.DataItem as Model.S01.UCProcessSubFuncAuthManagerInfo.Main;
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
                    sys_cid_lbl.Text = gv.DataKeys[gvr.RowIndex].Values["sys_cid"].ToString();
                    sys_cnote_lbl.Text = (gvr.FindControl("sys_cnote_lbl") as Label).Text;
                    ucProcessSubFuncAuthManagerAuthView.Show(sys_pid_lbl.Text, sys_cid_lbl.Text);
                    mv.SetActiveView(auth_view);
                    break;
            }
        }
        #endregion

        #region 返回作業列表
        protected void back_btn_Click(object sender, EventArgs e)
        {
            BindMainGridView(GetMainData());
            mv.SetActiveView(main_view);
        }
        #endregion
        #endregion
   }
}