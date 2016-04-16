using AjaxControlToolkit;
using BusinessLayer.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Util;

namespace ActivityApply
{
    public partial class SignSearchContext : System.Web.UI.Page
    {
        private SignSearchContextBL _bl;
        private SignSearchContextBL BL { get { if (_bl == null) _bl = new SignSearchContextBL(); return _bl; } }
        private string _dataCacheKey;
        //儲存使用者密碼
        private static string user_password;
        //活動序號
        private static string act_idn;
        //場次序號
        private static string as_idn;
        //分類
        private static string act_class;
        //活動標題
        private static string act_title;
        //報名序號
        private static string aa_idn;
        //按鈕事件
        private static string gridview_event;

        protected void Page_Init(object sender, EventArgs e)
        {
            _dataCacheKey = "SignSearchContext_" + Session.SessionID;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridView(GetData(true));
            }
        }

        #region --- 查詢 ---
        /// <summary>
        /// 取得資料
        /// </summary>
        /// <param name="isRefresh">是否重新取得DB資料</param>
        /// <returns>資料</returns>
        private DataTable GetData(bool isRefresh)
        {
            DataTable dt;
            if (isRefresh || Cache[_dataCacheKey] == null)
            {
                dt = BL.GetActivityData(aa_email_hf.Value);

                CommonHelper.SetCache(_dataCacheKey, dt);
            }
            else
                dt = (DataTable)Cache[_dataCacheKey];
            return dt;
        }
        #endregion

        #region --- Gridview 事件 ---
        /// <summary>
        /// Bind資料
        /// </summary>
        /// <param name="lst">資料</param>
        private void BindGridView(DataTable dt)
        {
            main_gv.DataSource = dt;
            main_gv.DataBind();
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void main_gv_Sorting(object sender, GridViewSortEventArgs e)
        {
            GridView gv = (GridView)sender;
            var dt = GridViewHelper.SortGridView(gv, e, GetData(false));
            BindGridView(dt);
            CommonHelper.SetCache(_dataCacheKey, dt);
        }

        /// <summary>
        /// RowDataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void main_gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }

        /// <summary>
        /// 命令處理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void main_gv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            DataTable dt = (DataTable)Cache[_dataCacheKey];
            //取得選擇的row
            GridViewRow gvr = sender as GridViewRow;
            //取得選擇的row的Index
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            //抓取活動序號隱藏欄位
            HiddenField Act_idn_hf = main_gv.Rows[rowIndex].FindControl("Act_idn_hf") as HiddenField;
            //抓取場次序號隱藏欄位
            HiddenField As_idn_hf = main_gv.Rows[rowIndex].FindControl("As_idn_hf") as HiddenField;
            //抓取分類隱藏欄位
            HiddenField Act_class_hf = main_gv.Rows[rowIndex].FindControl("Act_class_hf") as HiddenField;
            //抓取報名序號隱藏欄位
            HiddenField Aa_idn_hf = main_gv.Rows[rowIndex].FindControl("Aa_idn_hf") as HiddenField;
            //抓取活動標題隱藏欄位
            Label Act_title_lbl = main_gv.Rows[rowIndex].FindControl("Act_title_lbl") as Label;
            switch (e.CommandName)
            {
                case "Custom_Edit"://修改報名資料
                    //ModalPopupExtender顯示
                    password_pop.Show();
                    //設定資料
                    act_idn = Act_idn_hf.Value;
                    as_idn = As_idn_hf.Value;
                    act_class = Act_class_hf.Value;
                    act_title = Act_title_lbl.Text;
                    aa_idn = Aa_idn_hf.Value;
                    //設定使用者選擇了修改
                    gridview_event = "edit";
                    break;
                case "Custom_Delete":
                    //ModalPopupExtender顯示
                    password_pop.Show();
                    //設定資料
                    act_idn = Act_idn_hf.Value;
                    as_idn = As_idn_hf.Value;
                    act_class = Act_class_hf.Value;
                    act_title = Act_title_lbl.Text;
                    aa_idn = Aa_idn_hf.Value;
                    //設定使用者選擇了刪除
                    gridview_event = "delete";
                    break;
            }
        }

        #endregion

        #region 刪除
        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void main_gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
        }
        #endregion

        #region 搜尋按鈕事件
        protected void search_btn_Click(object sender, EventArgs e)
        {
            //抓取使用者輸入Email值
            aa_email_hf.Value = aa_email_txt.Text.Trim();
            //抓取報名資料
            DataTable dt = GetData(true);
            //判斷是否有報名資料
            if (dt.Rows.Count != 0)
            {
                //顯示忘記密碼按鈕、更改密碼按鈕
                forget_password_btn.Visible = true;
                change_password_btn.Visible = true;
                //抓取使用者密碼
                DataTable dt_password = BL.GetEmailData(aa_email_hf.Value);
                //儲存使用者密碼
                user_password = dt_password.Rows[0]["aae_password"].ToString();
            }
            else
            {
                string error_msg = "無報名資料!";
                Response.Write("<script language='javascript'>alert('" + error_msg + "');</script>");
                user_password = null;
                forget_password_btn.Visible = false;
                change_password_btn.Visible = false;
            }
            BindGridView(dt);
        }
        #endregion

        #region 更換密碼按鈕確認事件
        protected void change_password_ok_btn_Click(object sender, EventArgs e)
        {
            //判斷使用者輸入密碼資料是否正確
            if(user_password == old_password_txt.Text && new_password_txt.Text == new_password_check_txt.Text)
            {
                Dictionary<string, object> oldpassworddict = new Dictionary<string, object>();
                oldpassworddict["aae_email"] = aa_email_hf.Value;
                oldpassworddict["aae_password"] = user_password;
                Dictionary<string, object> newpassworddict = new Dictionary<string, object>();
                newpassworddict["aae_password"] = new_password_txt.Text;
                BL.UpdateData(oldpassworddict, newpassworddict);
            }
            else if(user_password != old_password_txt.Text)
            {
                string error_msg = "密碼錯誤!";
                Response.Write("<script language='javascript'>alert('" + error_msg + "');</script>");
            }
            else if(new_password_txt.Text != new_password_check_txt.Text)
            {
                string error_msg = "新密碼不相同!";
                Response.Write("<script language='javascript'>alert('" + error_msg + "');</script>");
            }
        }
        #endregion

        #region 忘記密碼按鈕確認事件
        protected void get_password_ok_btn_Click(object sender, EventArgs e)
        {
            
        }
        #endregion

        #region 修改報名資料、刪除按鈕，輸入密碼確認事件
        protected void password_ok_btn_Click(object sender, EventArgs e)
        {
            
            //判斷使用者密碼是否正確以及是按了修改還是刪除按鈕
            if (user_password == password_txt.Text && gridview_event == "edit")
            {
                Response.Redirect("SignChange.aspx?act_idn=" + act_idn + "&as_idn=" + as_idn + "&aa_idn="+ aa_idn +"&act_class=" + act_class + "&act_title=" + act_title);
            }
            else if(user_password == password_txt.Text && gridview_event == "delete")
            {
                DataTable columndt = BL.GetColumnData(act_idn);
                for(int count = 0; count < columndt.Rows.Count;count++)
                {
                    Dictionary<string, object> detaildict = new Dictionary<string, object>();
                    detaildict["aad_apply_id"] = aa_idn;
                    detaildict["aad_col_id"] = columndt.Rows[count][0].ToString();
                    var detailres = BL.DeleteDetailData(detaildict);
                }
                Dictionary<string, object> applydict = new Dictionary<string, object>();
                applydict["aa_idn"] = aa_idn;
                var applyres = BL.DeleteApplyData(applydict);
                DataTable dt = GetData(true);
                if (dt.Rows.Count == 0)
                {
                    forget_password_btn.Visible = false;
                    change_password_btn.Visible = false;
                }
                BindGridView(GetData(true));
            }
            else
            {
                string error_msg = "密碼錯誤!";
                Response.Write("<script language='javascript'>alert('" + error_msg + "');</script>");
            }
        }
        #endregion

    }
}