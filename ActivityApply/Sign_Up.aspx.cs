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
using System.IO;
using CrystalDecisions.Shared;
using System.Web.UI;

namespace ActivityApply
{
    public partial class Sign_Up : System.Web.UI.Page
    {
        static int ACT_IDN;
        static int AS_IDN;
        static int AA_IDN;   
        
        protected void Page_Load(object sender, EventArgs e)
        {
            ACT_IDN = Int32.Parse(Request["act_idn"]);
            AS_IDN = Int32.Parse(Request["as_idn"]);
        }

        [System.Web.Services.WebMethod]
        public static string getSectionList()
        {
            Sign_UpBL _bl = new Sign_UpBL();
            List<Activity_sectionInfo> sectionList = _bl.GetSectionList(ACT_IDN,AS_IDN);
            if(sectionList.Count == 0)
                return "false";
            else
            {
                string json_data = JsonConvert.SerializeObject(sectionList);
                return json_data;
            }
            
        }

        [System.Web.Services.WebMethod]
        public static string getQuestionList()
        {
            Sign_UpBL _bl = new Sign_UpBL();
            List<Activity_columnInfo> questionList = _bl.GetQuestionList(ACT_IDN, AS_IDN);
            if (questionList.Count == 0)
                return "false";
            else
            {
                string json_data = JsonConvert.SerializeObject(questionList);
                return json_data;
            }
            
        }

        [System.Web.Services.WebMethod]
        public static string saveUserData(List<UserData> userData)
        {
            Sign_UpBL _bl = new Sign_UpBL();
            CommonResult result;            
            string name = userData.Where(data => data.Aad_title.Contains("姓名")).Select(data => data.Aad_val).ToList()[0];
            string email = userData.Where(data => data.Aad_title.Contains("Email")).Select(data => data.Aad_val).ToList()[0];

            // 判斷是否在報名日期內
            if (!_bl.isBetweenApplyDate(AS_IDN)) {
                return "Error:請於報名時間內報名活動！";
            }
            // 判斷場次是否開放
            if (!_bl.isOpen(AS_IDN))
            {
                return "Error:活動尚未開放！";
            }
            // 判斷報名人數是否額滿
            if (_bl.isFull(AS_IDN))
            {
                return "Error:報名人數已額滿！";
            }
            // 判斷是否重複報名
            if (_bl.isRepeatApply(AS_IDN, email, name))
            {
                return "Error:您已報名此場次！";
            }
            // 判斷是否超過報名限制
            if (_bl.isOverApplyLimit(ACT_IDN, email, name))
            {
                return "Error:您已超過活動報名限制名額！";
            }

            Dictionary<String, Object> save_Activity_apply = new Dictionary<string, object>();
            save_Activity_apply["aa_act"] = ACT_IDN;
            save_Activity_apply["aa_as"] = AS_IDN;
            save_Activity_apply["aa_name"] = name;
            save_Activity_apply["aa_email"] = email;

            result = _bl.InsertData_Activity_apply(save_Activity_apply);

            if (result.IsSuccess)
            {
                int aad_apply_id = AA_IDN = Int32.Parse(result.Message);
                Dictionary<String, Object> save_Activity_apply_detai = new Dictionary<string, object>();
                for (int i = 0; i < userData.Count; i++)
                {
                    save_Activity_apply_detai["aad_apply_id"] = aad_apply_id;
                    save_Activity_apply_detai["aad_col_id"] = userData[i].Aad_col_id;
                    save_Activity_apply_detai["aad_val"] = userData[i].Aad_val;

                    result = _bl.InsertData_Activity_apply_detail(save_Activity_apply_detai);
                }
                if (result.IsSuccess)
                {
                    // 報名成功，發送Email
                    SystemConfigInfo config_info = CommonHelper.GetSysConfig();
                    DataTable dt = _bl.GetActivityData(ACT_IDN, AS_IDN);
                    string act_title = dt.Rows[0]["act_title"].ToString();
                    CustomHelper.SendMail(config_info.SMTP_FROM_MAIL, config_info.SMTP_FROM_NAME, email, getMailSubject(act_title), getMailContnet(dt, name));
                    return "Success:報名成功！";
                }
                else
                {
                    return "Error:報名失敗，請稍後再試！";
                }
            }
            else {
                return "Error:報名失敗，請稍後再試！";
            }

        }

        [System.Web.Services.WebMethod]
        public static bool isSignUp(string email)
        {
            Sign_UpBL _bl = new Sign_UpBL();
            DataTable dt = _bl.GetEmailData(email);
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [System.Web.Services.WebMethod]
        public static bool savePassword(List<Activity_apply_emailInfo> Activity_apply_emailInfo)
        {
            Sign_UpBL _bl = new Sign_UpBL();

            Dictionary<String, Object> save_Activity_apply_email = new Dictionary<string, object>();
            save_Activity_apply_email["aae_email"] = Activity_apply_emailInfo[0].Aae_email;
            save_Activity_apply_email["aae_password"] = Activity_apply_emailInfo[0].Aae_password;

            var result = _bl.InsertData_Password(save_Activity_apply_email);
            if (result.IsSuccess)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

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
                                "<p>感謝您報名 " + act_title + "，以下是您報名的場次資訊：" +
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
            return "您已報名【" + ActivityName + "】";
        }

        [Table("UserData")]
        public class UserData
        {
            [Column("aad_col_id")]
            public Int32 Aad_col_id { get; set; }
            [Column("aad_title")]
            public String Aad_title { get; set; }
            [Column("aad_val")]
            public String Aad_val { get; set; }
        }

        protected void print_ApplyProve(object sender, EventArgs e)
        {
            Sign_UpBL _bl = new Sign_UpBL();
            DataTable dt = _bl.GetApplyProve(AA_IDN);
            ReportDocument rd = new ReportDocument();
            //載入該報表
            rd.Load(Server.MapPath("~/applyProve.rpt"));
            //設定資料
            rd.SetDataSource(dt);
            rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, dt.Rows[0]["act_title"] + "_" +dt.Rows[0]["as_title"] + "_活動資訊");
        }
    }
}