using ActivityApply.CommonPages;
using AjaxControlToolkit;
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
    public partial class Sign_Search_Context : BasePage
    {
        private SignSearchContextBL _bl;
        private SignSearchContextBL BL { get { if (_bl == null) _bl = new SignSearchContextBL(); return _bl; } }
        private string _dataCacheKey;
        //儲存使用者密碼
        //private static string user_password;
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
        //場次標題
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
            //判斷是分是否是頁事件
            if (e.CommandName == "Page") return;
            DataTable dt = (DataTable)Cache[_dataCacheKey];
            //取得選擇的row
            GridViewRow gvr = sender as GridViewRow;
            //取得選擇的row的Index
            int rowIndex = Convert.ToInt32(e.CommandArgument) % 10;

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
            if(aa_email_hf.Value != "")
            {
                //抓取報名資料
                DataTable dt = GetData(true);
                Session.Remove("user_password");
                //判斷是否有報名資料
                if (dt.Rows.Count != 0)
                {
                    //顯示忘記密碼按鈕、更改密碼按鈕
                    forget_password_btn.Visible = true;
                    change_password_btn.Visible = true;
                    ////抓取使用者密碼

                    //DataTable dt_password = BL.GetEmailData(aa_email_hf.Value);
                    ////儲存使用者密碼
                    //if (dt_password.Rows.Count != 0)
                    //    user_password = dt_password.Rows[0]["aae_password"].ToString();
                }
                else
                {
                    string msg = "無報名資料!";
                    ShowPopupMessage(msg);

                    forget_password_btn.Visible = false;
                    change_password_btn.Visible = false;
                }
                BindGridView(dt);
            }
            else
            {
                string msg = "請輸入信箱!";
                ShowPopupMessage(msg);

                forget_password_btn.Visible = false;
                change_password_btn.Visible = false;
            }

            
        }
        #endregion

        #region 更換密碼按鈕確認事件
        protected void change_password_ok_btn_Click(object sender, EventArgs e)
        {
            string captcha = (string)Session[CommonPages.Captcha.CaptchaSessionKey];    // 產生的驗證碼
            //判斷使用者輸入密碼資料是否正確
            if (ValidPassword(aa_email_hf.Value, old_password_txt.Text) && new_password_txt.Text == new_password_check_txt.Text && !captcha.IsNullOrWhiteSpace() && confirm_txt_pf.Value.Trim().EqualsIgnoreCase(captcha.ToUpper()))
            {
                string password = new_password_txt.Text;
                string salt = BL.GetEmailData(aa_email_hf.Value).Rows[0]["aae_salt"].ToString().Trim();

                byte[] passwordAndSaltBytes = System.Text.Encoding.UTF8.GetBytes(password + salt);
                byte[] hashBytes = new System.Security.Cryptography.SHA256Managed().ComputeHash(passwordAndSaltBytes);

                string hashString = Convert.ToBase64String(hashBytes);

                Dictionary<string, object> oldpassworddict = new Dictionary<string, object>();
                oldpassworddict["aae_email"] = aa_email_hf.Value;
                Dictionary<string, object> newpassworddict = new Dictionary<string, object>();
                newpassworddict["aae_password"] = hashString;
                var upres = BL.UpdateData(oldpassworddict, newpassworddict);
                if (upres.IsSuccess)
                {
                    string msg = "成功更改密碼!";
                    ShowPopupMessage(msg);
                }
                string email = aa_email_hf.Value;
                SystemConfigInfo config_info = CommonHelper.GetSysConfig();
                CustomHelper.SendMail(config_info.SMTP_FROM_MAIL, config_info.SMTP_FROM_NAME, email, getMailSubjectChange(email), getMailContnetChange());
            }
            else if (!ValidPassword(aa_email_hf.Value, old_password_txt.Text))
            {
                Page.RegisterStartupScript("Show", "<script language=\"JavaScript\">modal1_open();</script>");
                string msg = "密碼錯誤!";
                ShowPopupMessage(msg);
            }
            else if (new_password_txt.Text != new_password_check_txt.Text)
            {
                Page.RegisterStartupScript("Show", "<script language=\"JavaScript\">modal1_open();</script>");
                string msg = "新密碼不相同!";
                ShowPopupMessage(msg);
            }
            else if (!confirm_txt_pf.Value.Trim().EqualsIgnoreCase(captcha.ToUpper()))
            {
                Page.RegisterStartupScript("Show", "<script language=\"JavaScript\">modal1_open();</script>");
                string msg = "驗證碼錯誤!";
                ShowPopupMessage(msg);
            }
        }
        #endregion

        #region 忘記密碼按鈕確認事件
        protected void get_password_ok_btn_Click(object sender, EventArgs e)
        {
            string email = email_txt.Text;
            Random rd = new Random();//亂數種子
            //int password_rd = rd.Next(0, 1000000);//回傳0-999999的亂數
            string password_rd = getRandStringEx(6);
            //寄信
            SystemConfigInfo config_info = CommonHelper.GetSysConfig();

            string password = password_rd;
            string salt = BL.GetEmailData(aa_email_hf.Value).Rows[0]["aae_salt"].ToString().Trim();

            byte[] passwordAndSaltBytes = System.Text.Encoding.UTF8.GetBytes(password + salt);
            byte[] hashBytes = new System.Security.Cryptography.SHA256Managed().ComputeHash(passwordAndSaltBytes);

            string hashString = Convert.ToBase64String(hashBytes);

            Dictionary<string, object> old_dt = new Dictionary<string, object>();
            old_dt["aae_email"] = email;
            Dictionary<string, object> new_dt = new Dictionary<string, object>();
            new_dt["aae_password"] = hashString;
            CommonResult res = BL.UpdateData(old_dt, new_dt);
            if (res.IsSuccess)
            {
                CustomHelper.SendMail(config_info.SMTP_FROM_MAIL, config_info.SMTP_FROM_NAME, email, getMailSubject(email), getMailContnet(password_rd));
                string msg = "已寄送Email到 : " + email;
                ShowPopupMessage(msg);
            }
            else
            {
                email_txt.Text = "";
                string msg = "無此信箱!!";
                ShowPopupMessage(msg);
            }

        }
        #endregion

        #region 信件內容
        public static string getMailContnet(string password_rd)
        {
            string content = "<p> 您好：</p>" +
                                "<br/>--------------------------------------------------------------------------------------" +
                                "<p>您的新密碼為:"+password_rd +
                                "<br/>為了您的帳戶安全，請立即更改密碼。" +
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
                                "<p>您的密碼已被更改" +
                                "<br/>為了您的帳戶安全，若非本人修改，請立即聯絡客服人員!" +
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
            
            string captcha = (string)Session[CommonPages.Captcha.CaptchaSessionKey];    // 產生的驗證碼
            
            if((!captcha.IsNullOrWhiteSpace() && confirm_txt_pf.Value.Trim().EqualsIgnoreCase(captcha.ToUpper())) || Session["user_password"] != null)
            {
                if (ValidPassword(aa_email_hf.Value , password_txt.Text) || Session["user_password"] != null)
                {
                    if(Session["user_password"] == null)
                        Session["user_password"] = password_txt.Text;
                    //判斷使用者密碼是否正確以及是按了修改還是刪除按鈕
                    if (gridview_event == "edit")
                    {
                        Session["aa_idn"] = aa_idn;
                        Session["act_idn"] = act_idn;
                        Session["as_idn"] = as_idn;
                        password_pop.Hide();
                        Response.Redirect("Sign_Change.aspx");
                    }
                    else if (gridview_event == "delete")
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
                            string msg = "取消報名成功!";
                            ShowPopupMessage(msg);
                        }
                        DataTable dt = GetData(true);
                        if (dt.Rows.Count == 0)
                        {
                            forget_password_btn.Visible = false;
                            change_password_btn.Visible = false;
                        }
                        BindGridView(GetData(true));
                    }
                }
                else 
                {
                    //password_pop.Show();
                    //password_txt.Focus();
                    Session["user_password"] = null;
                    string msg = "密碼錯誤!";
                    ShowPopupMessage(msg);
                }
            }
            else if (!confirm_txt_pf.Value.Trim().EqualsIgnoreCase(captcha.ToUpper()))
            {
                ClientScriptManager cs = Page.ClientScript;
                //password_pop.Show();
                //password_txt.Focus();
                Session["user_password"] = null;
                string msg = "驗證碼錯誤!";
                ShowPopupMessage(msg);
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

        #region GridView分頁事件
        protected void main_gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            main_gv.PageIndex = e.NewPageIndex;
            BindGridView(GetData(false));
        }
        #endregion

        #region 英數亂數產生
        public static String getRandStringEx(int length)
        {
            char[] charList = { '0','1','2','3','4','5','6','7','8','9',
                                'A','B','C','D','E','F','G','H','I','J','K','L','M',
                                'N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
                                'a','b','c','d','e','f','g','h','i','j','k','l','m',
                                'n','o','p','q','r','s','t','u','v','w','x','y','z'};
            char[] rev = new char[length];
            Random f = new Random();
            for (int i = 0; i < length; i++)
            {
                rev[i] = charList[Math.Abs(f.Next(1,61))];
            }
            return new String(rev);
        }
        #endregion

        /// <summary> 
        /// 驗證使用者密碼 
        /// </summary> 
        /// <param name="email">信箱</param> 
        /// <param name="password">密碼</param> 
        /// <returns></returns> 
        protected bool ValidPassword(string email, string password)
        {
            DataTable dt = BL.GetEmailData(email);
            string salt = dt.Rows[0]["aae_salt"].ToString().Trim();
            string pwd = dt.Rows[0]["aae_password"].ToString().Trim();
            byte[] passwordAndSaltBytes = System.Text.Encoding.UTF8.GetBytes(password + salt);
            byte[] hashBytes = new System.Security.Cryptography.SHA256Managed().ComputeHash(passwordAndSaltBytes);
            string hashString = Convert.ToBase64String(hashBytes);

            if (hashString == pwd)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}