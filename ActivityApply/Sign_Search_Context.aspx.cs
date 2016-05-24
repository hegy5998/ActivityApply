﻿using AjaxControlToolkit;
using BusinessLayer.Web;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Model.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Util;

namespace ActivityApply
{
    public partial class Sign_Search_Context : System.Web.UI.Page
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
        private static string as_title;
        //報名人姓名
        private static string name;
        //按鈕事件
        private static string gridview_event;

        protected void Page_Init(object sender, EventArgs e)
        {
            _dataCacheKey = "SignSearchContext_" + Session.SessionID;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Remove("aa_idn");
            Session.Remove("act_idn");
            Session.Remove("as_idn");
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
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("OnMouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#C0C3C9'");
                e.Row.Attributes.Add("OnMouseout", "this.style.background=c");

                e.Row.Cells[2].Style.Add("word-break", "break-all");
                GridViewRow gvr = e.Row;
                HiddenField As_apply_end = gvr.FindControl("As_apply_end_hf") as HiddenField;
                DateTime dt = Convert.ToDateTime(As_apply_end.Value);

                DateTime currentTime = new DateTime();
                currentTime = DateTime.Now;
                //判斷如果報名日期已結束則不修改報名資料以及取消報名
                if (DateTime.Compare(currentTime, dt) > 0)
                {
                    Button edit_btn = gvr.FindControl("edit_btn") as Button;
                    Button delete_btn = gvr.FindControl("delete_btn") as Button;
                    edit_btn.Visible = false;
                    delete_btn.Visible = false;
                }
            }
        }

        /// <summary>
        /// 命令處理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void main_gv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Page") return;
            DataTable dt = (DataTable)Cache[_dataCacheKey];
            //取得選擇的row
            GridViewRow gvr = sender as GridViewRow;
            //取得選擇的row的Index
            int rowIndex = Convert.ToInt32(e.CommandArgument)%10;
            
            //抓取活動序號隱藏欄位
            HiddenField Act_idn_hf = main_gv.Rows[rowIndex].FindControl("Act_idn_hf") as HiddenField;
            //抓取場次序號隱藏欄位
            HiddenField As_idn_hf = main_gv.Rows[rowIndex].FindControl("As_idn_hf") as HiddenField;
            //抓取分類隱藏欄位
            HiddenField Act_class_hf = main_gv.Rows[rowIndex].FindControl("Act_class_hf") as HiddenField;
            //抓取報名序號隱藏欄位
            HiddenField Aa_idn_hf = main_gv.Rows[rowIndex].FindControl("Aa_idn_hf") as HiddenField;
            //抓取報名人姓名
            HiddenField Aa_name_hf = main_gv.Rows[rowIndex].FindControl("Aa_name_hf") as HiddenField;
            //抓取活動標題隱藏欄位
            HiddenField Act_title_hf = main_gv.Rows[rowIndex].FindControl("Act_title_hf") as HiddenField;

            HiddenField As_title_hf = main_gv.Rows[rowIndex].FindControl("As_title_hf") as HiddenField;
            switch (e.CommandName)
            {
                case "Custom_Edit"://修改報名資料

                    //設定資料
                    act_idn = Act_idn_hf.Value;
                    as_idn = As_idn_hf.Value;
                    act_class = Act_class_hf.Value;
                    act_title = Act_title_hf.Value;
                    aa_idn = Aa_idn_hf.Value;
                    //設定使用者選擇了修改
                    gridview_event = "edit";

                    //ModalPopupExtender顯示
                    if (Session["user_password"] == null)
                    {
                        password_pop.Show();
                        password_txt.Focus();
                    }
                    else
                        password_ok_btn_Click(sender, e);
                    break;
                case "Custom_Delete":

                    //設定資料
                    act_idn = Act_idn_hf.Value;
                    as_idn = As_idn_hf.Value;
                    act_class = Act_class_hf.Value;
                    act_title = Act_title_hf.Value;
                    aa_idn = Aa_idn_hf.Value;
                    name = Aa_name_hf.Value;
                    //設定使用者選擇了刪除
                    gridview_event = "delete";

                    //ModalPopupExtender顯示
                    if (Session["user_password"] == null)
                    {
                        password_pop.Show();
                        password_txt.Focus();
                    }
                    else
                        password_ok_btn_Click(sender, e);
                    break;
                case "Custom_dowload":
                    aa_idn = Aa_idn_hf.Value;
                    act_title = Act_title_hf.Value;
                    as_title = As_title_hf.Value;
                    gridview_event = "Custom_dowload";

                    //if (Session["user_password"] == null)
                    //{
                    //    password_pop.Show();
                    //    password_txt.Focus();
                    //}
                    //else
                    //    password_ok_btn_Click(sender, e);

                    Sign_UpBL _bl = new Sign_UpBL();
                    DataTable dd = _bl.GetApplyProve(Int32.Parse(aa_idn));
                    //ReportDocument rd = new ReportDocument();

                    using (ReportDocument rd = new ReportDocument())
                    {
                        //載入該報表
                        rd.Load(Server.MapPath("~/applyProve.rpt"));
                        //設定資料
                        rd.SetDataSource(dd);
                        rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, act_title + "_" + as_title + "_報名資訊");
                        rd.Close();
                    }
                    break;
            }
        }
        #endregion

        #region 活動資訊下載
        protected void print_ApplyProve(int AA_IDN)
        {
            Sign_UpBL _bl = new Sign_UpBL();
            DataTable dt = _bl.GetApplyProve(AA_IDN);
            ReportDocument rd = new ReportDocument();
            //載入該報表
            rd.Load(Server.MapPath("~/applyProve.rpt"));
            //設定資料
            rd.SetDataSource(dt);
            rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, act_title + "_" + as_title + "_活動資訊");
            rd.Close();
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
            Session.Remove("user_password");
            //判斷是否有報名資料
            if (dt.Rows.Count != 0)
            {
                //顯示忘記密碼按鈕、更改密碼按鈕
                forget_password_btn.Visible = true;
                change_password_btn.Visible = true;
                //抓取使用者密碼
                DataTable dt_password = BL.GetEmailData(aa_email_hf.Value);
                //儲存使用者密碼
                if (dt_password.Rows.Count != 0)
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
            if (user_password == old_password_txt.Text && new_password_txt.Text == new_password_check_txt.Text)
            {
                Dictionary<string, object> oldpassworddict = new Dictionary<string, object>();
                oldpassworddict["aae_email"] = aa_email_hf.Value;
                oldpassworddict["aae_password"] = user_password;
                Dictionary<string, object> newpassworddict = new Dictionary<string, object>();
                newpassworddict["aae_password"] = user_password = new_password_txt.Text;
                var upres = BL.UpdateData(oldpassworddict, newpassworddict);
                if (upres.IsSuccess)
                {
                    string msg = "成功更改密碼!";
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<script language='javascript'>");
                    sb.Append("alert('" + msg + "')");
                    sb.Append("</script>");
                    ClientScript.RegisterStartupScript(this.GetType(), "LoadPicScript", sb.ToString());
                }
                string email = aa_email_hf.Value;
                SystemConfigInfo config_info = CommonHelper.GetSysConfig();
                CustomHelper.SendMail(config_info.SMTP_FROM_MAIL, config_info.SMTP_FROM_NAME, email, getMailSubjectChange(email), getMailContnetChange());
            }
            else if (user_password != old_password_txt.Text)
            {
                string error_msg = "密碼錯誤!";
                StringBuilder sb = new StringBuilder();
                sb.Append("<script language='javascript'>");
                sb.Append("alert('" + error_msg + "')");
                sb.Append("</script>");
                ClientScript.RegisterStartupScript(this.GetType(), "LoadPicScript", sb.ToString());
            }
            else if (new_password_txt.Text != new_password_check_txt.Text)
            {
                string error_msg = "新密碼不相同!";
                StringBuilder sb = new StringBuilder();
                sb.Append("<script language='javascript'>");
                sb.Append("alert('" + error_msg + "')");
                sb.Append("</script>");
                ClientScript.RegisterStartupScript(this.GetType(), "LoadPicScript", sb.ToString());
            }
        }
        #endregion

        #region 忘記密碼按鈕確認事件
        protected void get_password_ok_btn_Click(object sender, EventArgs e)
        {
            string email = email_txt.Text;
            //寄信
            SystemConfigInfo config_info = CommonHelper.GetSysConfig();
            DataTable dt_password = BL.GetEmailData(email);
            if (dt_password.Rows.Count > 0)
            {
                CustomHelper.SendMail(config_info.SMTP_FROM_MAIL, config_info.SMTP_FROM_NAME, email, getMailSubject(email), getMailContnet(dt_password));
                //Page.RegisterStartupScript("Show", "<script language=\"JavaScript\">closeme();</script>");
                string error_msg = "已寄送Email到 : " + email;
                StringBuilder sb = new StringBuilder();
                sb.Append("<script language='javascript'>");
                sb.Append("alert('" + error_msg + "')");
                sb.Append("</script>");
                ClientScript.RegisterStartupScript(this.GetType(), "LoadPicScript", sb.ToString());
            }
            else
            {
                string error_msg = "無此信箱資料!";
                StringBuilder sb = new StringBuilder();
                sb.Append("<script language='javascript'>");
                sb.Append("alert('" + error_msg + "')");
                sb.Append("</script>");
                ClientScript.RegisterStartupScript(this.GetType(), "LoadPicScript", sb.ToString());
            }

        }
        #endregion

        #region 信件內容
        public static string getMailContnet(DataTable dt_password)
        {
            string aae_password = dt_password.Rows[0]["aae_password"].ToString();                  // 活動名稱
            string content = "<p> 您好：</p>" +
                                "<br/>--------------------------------------------------------------------------------------" +
                                "<p>您的密碼為 : " + aae_password + "" +
                                "<br/>為了您的帳戶安全，請您立即更改您的密碼" +
                                "<br/>--------------------------------------------------------------------------------------" +
                                "<p>※這是由系統自動發出的通知信，請勿回覆，感謝您的配合。</p>";
            return content;
        }
        public static string getMailContnetDelete(DataTable dt_email, string name)
        {
            string act_title = dt_email.Rows[0]["act_title"].ToString();                  // 活動名稱
            string act_unit = dt_email.Rows[0]["act_unit"].ToString();                    // 主辦單位
            string act_contact_name = dt_email.Rows[0]["act_contact_name"].ToString();    // 負責人
            string act_contact_phone = dt_email.Rows[0]["act_contact_phone"].ToString();  // 負責人電話
            string act_short_link = dt_email.Rows[0]["act_short_link"].ToString();        // 短網址
            string as_title = dt_email.Rows[0]["as_title"].ToString();                    // 場次標題
            string as_position = dt_email.Rows[0]["as_position"].ToString();              // 場次地點
            string as_date_start = dt_email.Rows[0]["as_date_start"].ToString();          // 活動開始時間
            string as_date_end = dt_email.Rows[0]["as_date_end"].ToString();              // 活動結束時間
            string content = "<p>" + name + "您好：</p>" +
                                "<p>您已取消報名 " + act_title + "，以下是您取消報名的場次資訊：" +
                                "<br/>--------------------------------------------------------------------------------------" +
                                "<br/>&nbsp;&nbsp;&nbsp;《" + as_title + "》" +
                                "<br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;時間：" + as_date_start + " ~ " + as_date_end +
                                "<br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;地點：" + as_position +
                                "<br/>--------------------------------------------------------------------------------------</p>" +
                                "<p>欲查詢活動詳情，點選下列網址將會導向活動資訊頁面：" +
                                "<br/>" + act_short_link + "</p>" +
                                (act_contact_name.Equals("") ? "<p>" : "<p>聯絡人：" + act_contact_name) +
                                (act_contact_phone.Equals("") ? "<br/></p>" : "<br/>電&nbsp;&nbsp;&nbsp;話：" + act_contact_phone + "</p>") +
                                (act_unit.Equals("") ? "<p></p>" : "<p>" + act_unit + "&nbsp;&nbsp;敬上 </p>") +
                                "<p>※這是由系統自動發出的通知信，請勿回覆。如果您對此活動有任何疑問，請直接與主辦單位聯繫，感謝您的配合。</p>";
            return content;
        }
        public static string getMailContnetChange()
        {
            string content = "<p> 您好：</p>" +
                                "<br/>--------------------------------------------------------------------------------------" +
                                "<p>您的密碼已經被更改為:&nbsp;" + user_password + "&nbsp;，請確認是否是您本人" +
                                "<br/>為了您的安全，如果不是您請立即更改您的密碼" +
                                "<br/>--------------------------------------------------------------------------------------" +
                                "<p>※這是由系統自動發出的通知信，請勿回覆，感謝您的配合。</p>";
            return content;
        }
        public static string getMailSubject(string email)
        {
            return "忘記密碼【" + email + "】";
        }
        public static string getMailSubjectDelete()
        {
            return "取消報名【" + act_title + "】";
        }
        public static string getMailSubjectChange(string email)
        {
            return "更改密碼【" + email + "】";
        }
        #endregion

        #region 修改報名資料、刪除按鈕，輸入密碼確認事件
        protected void password_ok_btn_Click(object sender, EventArgs e)
        {
            if (user_password == password_txt.Text && Session["user_password"] == null)
                Session["user_password"] = password_txt.Text;

            //判斷使用者密碼是否正確以及是按了修改還是刪除按鈕
            if (Session["user_password"] != null && Session["user_password"].ToString() == user_password && gridview_event == "edit")
            {
                Session["aa_idn"] = aa_idn;
                Session["act_idn"] = act_idn;
                Session["as_idn"] = as_idn;
                Response.Redirect("Sign_Change.aspx");
                //Server.Transfer("SignChange.aspx",true);
                //Page.RegisterStartupScript("Show", "<script language=\"JavaScript\">change();</script>");
                //Response.Redirect("SignChange.aspx?act_idn=" + act_idn + "&as_idn=" + as_idn);
            }
            else if (Session["user_password"] != null && Session["user_password"].ToString() == user_password && gridview_event == "delete")
            {
                DataTable columndt = BL.GetColumnData(act_idn);
                for (int count = 0; count < columndt.Rows.Count; count++)
                {
                    Dictionary<string, object> detaildict = new Dictionary<string, object>();
                    detaildict["aad_apply_id"] = aa_idn;
                    detaildict["aad_col_id"] = columndt.Rows[count][0].ToString();
                    var detailres = BL.DeleteDetailData(detaildict);

                }
                Dictionary<string, object> applydict = new Dictionary<string, object>();
                applydict["aa_idn"] = aa_idn;
                var applyres = BL.DeleteApplyData(applydict);
                //如果刪除成功則寄信通知
                if (applyres.IsSuccess)
                {
                    string email = aa_email_hf.Value;
                    SystemConfigInfo config_info = CommonHelper.GetSysConfig();
                    DataTable dt_email = _bl.GetActivityData(Int32.Parse(act_idn), Int32.Parse(as_idn));
                    CustomHelper.SendMail(config_info.SMTP_FROM_MAIL, config_info.SMTP_FROM_NAME, email, getMailSubjectDelete(), getMailContnetDelete(dt_email, name));
                }
                DataTable dt = GetData(true);
                if (dt.Rows.Count == 0)
                {
                    forget_password_btn.Visible = false;
                    change_password_btn.Visible = false;
                }
                BindGridView(GetData(true));
            }
            else if (Session["user_password"] != null && Session["user_password"].ToString() == user_password && gridview_event == "Custom_dowload")
            {
                //password_pop.Hide();
                Page.RegisterStartupScript("Show", "<script language=\"JavaScript\">download();</script>");
                //print_ApplyProve(Int32.Parse(aa_idn));
            }
            else
            {
                string error_msg = "密碼錯誤!";
                StringBuilder sb = new StringBuilder();
                sb.Append("<script language='javascript'>");
                sb.Append("alert('" + error_msg + "')");
                sb.Append("</script>");
                ClientScript.RegisterStartupScript(this.GetType(), "LoadPicScript", sb.ToString());
            }
        }
        #endregion

        #region 下載報名資訊
        protected void download_Click(object sender, EventArgs e)
        {
            Sign_UpBL _bl = new Sign_UpBL();
            DataTable dt = _bl.GetApplyProve(Int32.Parse(aa_idn));
            //ReportDocument rd = new ReportDocument();

            using (ReportDocument rd = new ReportDocument())
            {
                //載入該報表
                rd.Load(Server.MapPath("~/applyProve.rpt"));
                //設定資料
                rd.SetDataSource(dt);
                rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, act_title + "_" + as_title + "_報名資訊");
                rd.Close();
            }
        }
        #endregion

        protected void main_gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            main_gv.PageIndex = e.NewPageIndex;
            BindGridView(GetData(false));
        }
    }
}