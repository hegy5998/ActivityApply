using BusinessLayer.Web;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Util;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Model.Common;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace ActivityApply
{
    public partial class SignChange : System.Web.UI.Page
    {
        static int ACT_IDN;
        static int AS_IDN;
        static int AA_IDN;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            //ACT_IDN = Int32.Parse(Request["act_idn"]);
            //AS_IDN = Int32.Parse(Request["as_idn"]);
            //AA_IDN = Int32.Parse(Request["aa_idn"]);
            //AA_IDN  = Session["aa_idn"];
            //if (!IsPostBack)
            //{
            if (Session["aa_idn"] != null)
            {
                ACT_IDN = Int32.Parse(Session["act_idn"].ToString());
                AS_IDN = Int32.Parse(Session["as_idn"].ToString());
                AA_IDN = Int32.Parse(Session["aa_idn"].ToString());
            }
            else
            {
                string error_msg = "請重新輸入密碼!";
                Response.Write("<script language='javascript'>alert('" + error_msg + "');</script>");
                Response.Redirect("SignSearchContext.aspx");
            }
            //}

        }

        #region 取得場次資料
        [System.Web.Services.WebMethod]
        public static string getSectionList()
        {
            SignChangeBL _bl = new SignChangeBL();
            List<Activity_sectionInfo> sectionList = _bl.GetSectionList(ACT_IDN, AS_IDN);
            if (sectionList.Count == 0)
                return "false";
            else
            {
                string json_data = JsonConvert.SerializeObject(sectionList);
                return json_data;
            }
        }
        #endregion

        #region 取得報名表問題
        [System.Web.Services.WebMethod]
        public static string getQuestionList()
        {
            SignChangeBL _bl = new SignChangeBL();
            List<Activity_columnInfo> questionList = _bl.GetQuestionList(ACT_IDN,AS_IDN);
            if (questionList.Count == 0)
                return "false";
            else
            {
                string json_data = JsonConvert.SerializeObject(questionList);
                return json_data;
            }
        }
        #endregion

        #region 取得報名資料
        [System.Web.Services.WebMethod]
        public static string getApplyDetailList()
        {
            SignChangeBL _bl = new SignChangeBL();
            List<Activity_apply_detailInfo> ApplyDetailList = _bl.getApplyDetailList(AA_IDN);
            string json_data = JsonConvert.SerializeObject(ApplyDetailList);
            return json_data;
        }
        #endregion

        #region 更新報名資料
        [System.Web.Services.WebMethod]
        public static string saveUserData(List<UserData> userData)
        {
            if (AA_IDN > 0)
            {
                SignChangeBL _bl = new SignChangeBL();
                CommonResult result;
                Dictionary<String, Object> old_Activity_apply = new Dictionary<string, object>();
                string name;
                string email;
                //舊的報名資料
                old_Activity_apply["aa_idn"] = userData[0].Aad_apply_id;
                old_Activity_apply["aa_act"] = ACT_IDN;
                old_Activity_apply["aa_as"] = AS_IDN;
                Dictionary<String, Object> new_Activity_apply = new Dictionary<string, object>();
                //新的報名資料
                new_Activity_apply["aa_name"] = name = userData.Where(data => data.Aad_title.Contains("姓名")).Select(data => data.Aad_val).ToList()[0];
                new_Activity_apply["aa_email"] = email = userData.Where(data => data.Aad_title.Contains("Email")).Select(data => data.Aad_val).ToList()[0];

                result = _bl.UpdateApplyData(old_Activity_apply, new_Activity_apply);

                if (result.IsSuccess)
                {
                    Dictionary<String, Object> old_Activity_apply_detail = new Dictionary<string, object>();
                    Dictionary<String, Object> new_Activity_apply_detail = new Dictionary<string, object>();
                    Dictionary<String, Object> Activity_apply_detail = new Dictionary<string, object>();
                    for (int i = 0; i < userData.Count; i++)
                    {
                        //判斷填寫的問題為新問題或舊問題
                        if (userData[i].ifnewqus == 0)
                        {
                            //舊的報名資料
                            old_Activity_apply_detail["aad_apply_id"] = userData[i].Aad_apply_id;
                            old_Activity_apply_detail["aad_col_id"] = userData[i].Aad_col_id;
                            //新的報名資料
                            new_Activity_apply_detail["aad_val"] = userData[i].Aad_val;

                            result = _bl.UpdateApplyDetailData(old_Activity_apply_detail, new_Activity_apply_detail);
                        }
                        else if(userData[i].ifnewqus == 1)
                        {
                            Activity_apply_detail["aad_apply_id"] = userData[i].Aad_apply_id;
                            Activity_apply_detail["aad_col_id"] = userData[i].Aad_col_id;
                            Activity_apply_detail["aad_val"] = userData[i].Aad_val;
                            result = _bl.InsertData_Activity_apply_detail(Activity_apply_detail);
                        }
                        
                    }
                    if (result.IsSuccess)
                    {
                        //寄信通知報名資料變更
                        SystemConfigInfo config_info = CommonHelper.GetSysConfig();
                        DataTable dt = _bl.GetActivityData(ACT_IDN, AS_IDN);
                        string act_title = dt.Rows[0]["act_title"].ToString();
                        CustomHelper.SendMail(config_info.SMTP_FROM_MAIL, config_info.SMTP_FROM_NAME, email, getMailSubject(act_title), getMailContnet(dt, name));
                        return "save success";
                    }
                    else
                    {
                        return "save detail fail";
                    }
                }
                else {
                    return "save fail";
                }
            }
            return "save fail";
        }
        #endregion

        #region 信件內容
        public static string getMailContnet(DataTable dt, string name)
        {
            string act_title = dt.Rows[0]["act_title"].ToString();                  // 活動名稱
            string act_unit = dt.Rows[0]["act_unit"].ToString();                    // 主辦單位
            string act_contact_name = dt.Rows[0]["act_contact_name"].ToString();    // 負責人
            string act_contact_phone = dt.Rows[0]["act_contact_phone"].ToString();  // 負責人電話
            string act_short_link = dt.Rows[0]["act_short_link"].ToString();        // 短網址
            string as_title = dt.Rows[0]["as_title"].ToString();                    // 場次標題
            string as_position = dt.Rows[0]["as_position"].ToString();              // 場次地點
            string as_date_start = dt.Rows[0]["as_date_start"].ToString();          // 活動開始時間
            string as_date_end = dt.Rows[0]["as_date_end"].ToString();              // 活動結束時間
            string content = "<p>" + name + "您好：</p>" +
                                "<p>您更改了活動 《" + act_title + "》的報名資料，以下是您報名的場次資訊：" +
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
        public static string getMailSubject(string ActivityName)
        {
            return "您已更改【" + ActivityName + "】的報名資料";
        }
        #endregion

        protected void print_ApplyProve(object sender, EventArgs e)
        {
            Sign_UpBL _bl = new Sign_UpBL();
            DataTable dt = _bl.GetApplyProve(AA_IDN);
            ReportDocument rd = new ReportDocument();
            //載入該報表
            rd.Load(Server.MapPath("~/applyProve.rpt"));
            //設定資料
            rd.SetDataSource(dt);
            rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "applyProve");
            //rd.ExportToDisk(ExportFormatType.PortableDocFormat, "applyProve.pdf");
        }

        [Table("UserData")]
        public class UserData {
            [Column("aad_apply_id")]
            public Int32 Aad_apply_id { get; set; }
            [Column("aad_col_id")]
            public Int32 Aad_col_id { get; set; }
            [Column("aad_title")]
            public String Aad_title { get; set; }
            [Column("aad_val")]
            public String Aad_val { get; set; }
            [Column("ifnewqus")]
            public Int32 ifnewqus { get; set; }
        }
    }
}