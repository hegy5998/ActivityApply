using System;
using System.Collections.Generic;
using Model;
using Util;
using BusinessLayer.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ActivityApply
{
    public partial class Sign_Up : System.Web.UI.Page
    {
        
        static int ACTIVITY ;
        protected void Page_Load(object sender, EventArgs e)
        {
            ACTIVITY = Int32.Parse(Request["act_idn"]);
            
        }
        
        [System.Web.Services.WebMethod]
        public static string getSectionList()
        {
            Sign_UpBL _bl = new Sign_UpBL();
            List<Activity_sectionInfo> sectionList = _bl.GetSectionList(ACTIVITY);
            string json_data = JsonConvert.SerializeObject(sectionList);
            return json_data;
        }

        [System.Web.Services.WebMethod]
        public static string getQuestionList()
        {
            Sign_UpBL _bl = new Sign_UpBL();
            List<Activity_columnInfo> questionList = _bl.GetQuestionList(ACTIVITY);
            string json_data = JsonConvert.SerializeObject(questionList);
            return json_data;
        }
    }
}