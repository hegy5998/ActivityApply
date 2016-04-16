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
    public partial class SignChange : System.Web.UI.Page
    {
        static int ACT_IDN;
        static int AS_IDN;
        static int AA_IDN;
        protected void Page_Load(object sender, EventArgs e)
        {
            ACT_IDN = Int32.Parse(Request["act_idn"]);
            AS_IDN = Int32.Parse(Request["as_idn"]);
            AA_IDN = Int32.Parse(Request["aa_idn"]);
        }
        
        [System.Web.Services.WebMethod]
        public static string getSectionList()
        {
            SignChangeBL _bl = new SignChangeBL();
            List<Activity_sectionInfo> sectionList = _bl.GetSectionList(ACT_IDN);
            
            string json_data = JsonConvert.SerializeObject(sectionList);
            return json_data;
        }

        [System.Web.Services.WebMethod]
        public static string getQuestionList()
        {
            SignChangeBL _bl = new SignChangeBL();
            List<Activity_columnInfo> questionList = _bl.GetQuestionList(ACT_IDN);
            string json_data = JsonConvert.SerializeObject(questionList);
            return json_data;
        }

        [System.Web.Services.WebMethod]
        public static string getApplyDetailList()
        {
            SignChangeBL _bl = new SignChangeBL();
            List<Activity_apply_detailInfo> ApplyDetailList = _bl.getApplyDetailList(AA_IDN);
            string json_data = JsonConvert.SerializeObject(ApplyDetailList);
            return json_data;
        }

        [System.Web.Services.WebMethod]
        public static string saveUserData(List<UserData> userData)
        {
            SignChangeBL _bl = new SignChangeBL();
            CommonResult result;
            Dictionary<String, Object> old_Activity_apply = new Dictionary<string, object>();
            //舊的報名資料
            old_Activity_apply["aa_idn"] = userData[0].Aad_apply_id;
            old_Activity_apply["aa_act"] = ACT_IDN;
            old_Activity_apply["aa_as"] = AS_IDN;
            Dictionary<String, Object> new_Activity_apply = new Dictionary<string, object>();
            //新的報名資料
            new_Activity_apply["aa_name"] = userData.Where(data => data.Aad_title.Contains("姓名")).Select(data => data.Aad_val).ToList()[0];
            new_Activity_apply["aa_email"] = userData.Where(data => data.Aad_title.Contains("Email")).Select(data => data.Aad_val).ToList()[0];

            result = _bl.UpdateApplyData(old_Activity_apply, new_Activity_apply);

            if (result.IsSuccess)
            {
                Dictionary<String, Object> old_Activity_apply_detail = new Dictionary<string, object>();
                Dictionary<String, Object> new_Activity_apply_detail = new Dictionary<string, object>();
                for (int i = 0; i < userData.Count; i++)
                {
                    //舊的報名資料
                    old_Activity_apply_detail["aad_apply_id"] = userData[0].Aad_apply_id;
                    old_Activity_apply_detail["aad_col_id"] = userData[i].Aad_col_id;
                    //新的報名資料
                    new_Activity_apply_detail["aad_val"] = userData[i].Aad_val;

                    result = _bl.UpdateApplyDetailData(old_Activity_apply_detail, new_Activity_apply_detail);
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
            [Column("aad_apply_id")]
            public Int32 Aad_apply_id { get; set; }
            [Column("aad_col_id")]
            public Int32 Aad_col_id { get; set; }
            [Column("aad_title")]
            public String Aad_title { get; set; }
            [Column("aad_val")]
            public String Aad_val { get; set; }
        }
    }
}