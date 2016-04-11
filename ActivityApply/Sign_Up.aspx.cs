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

namespace ActivityApply
{
    public partial class Sign_Up : System.Web.UI.Page
    {
<<<<<<< HEAD
        
        static int ACTIVITY ;
        protected void Page_Load(object sender, EventArgs e)
        {
            ACTIVITY = Int32.Parse(Request["act_idn"]);
            
=======
        static int ACT_IDN;
        static int AS_IDN;
        protected void Page_Load(object sender, EventArgs e)
        {
            ACT_IDN = Int32.Parse(Request["act_idn"]);
            AS_IDN = Int32.Parse(Request["as_idn"]);
>>>>>>> refs/remotes/origin/Web
        }
        
        [System.Web.Services.WebMethod]
        public static string getSectionList()
        {
            Sign_UpBL _bl = new Sign_UpBL();
            List<Activity_sectionInfo> sectionList = _bl.GetSectionList(ACT_IDN);
            
            string json_data = JsonConvert.SerializeObject(sectionList);
            return json_data;
        }

        [System.Web.Services.WebMethod]
        public static string getQuestionList()
        {
            Sign_UpBL _bl = new Sign_UpBL();
            List<Activity_columnInfo> questionList = _bl.GetQuestionList(ACT_IDN);
            string json_data = JsonConvert.SerializeObject(questionList);
            return json_data;
        }

        [System.Web.Services.WebMethod]
        public static string saveUserData(List<UserData> userData)
        {
            Sign_UpBL _bl = new Sign_UpBL();
            CommonResult result;
            Dictionary<String, Object> save_Activity_apply = new Dictionary<string, object>();

            save_Activity_apply["aa_act"] = ACT_IDN;
            save_Activity_apply["aa_as"] = AS_IDN;
            save_Activity_apply["aa_name"] = userData.Where(data => data.Aad_title.Contains("姓名")).Select(data => data.Aad_val).ToList()[0];
            save_Activity_apply["aa_email"] = userData.Where(data => data.Aad_title.Contains("Email")).Select(data => data.Aad_val).ToList()[0];

            result = _bl.InsertData_Activity_apply(save_Activity_apply);

            if (result.IsSuccess)
            {
                int aad_apply_id = Int32.Parse(result.Message);
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
        [Table("UserData")]
        public class UserData {
            [Column("aad_col_id")]
            public Int32 Aad_col_id { get; set; }
            [Column("aad_title")]
            public String Aad_title { get; set; }
            [Column("aad_val")]
            public String Aad_val { get; set; }
        }
    }
}