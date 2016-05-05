using BusinessLayer.S01;
using BusinessLayer.Web;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Web.S02
{
    public partial class S02010105 : System.Web.UI.Page
    {
        public static int ACT_IDN;
        protected void Page_Load(object sender, EventArgs e)
        {
            ACT_IDN = Int32.Parse(Request["act_idn"]);
        }
        [System.Web.Services.WebMethod]
        public static string getSectionList()
        {
            S020105BL _bl = new S020105BL();
            List<Activity_sectionInfo> sectionList = _bl.GetSectionList(ACT_IDN);
            string json_data = JsonConvert.SerializeObject(sectionList);
            return json_data;
        }

        [System.Web.Services.WebMethod]
        public static string getQuestionList()
        {
            S020105BL _bl = new S020105BL();
            List<Activity_columnInfo> questionList = _bl.GetQuestionList(ACT_IDN);
            string json_data = JsonConvert.SerializeObject(questionList);
            return json_data;
        }

    }


}