using BusinessLayer.S02;
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
        //活動ID
        public static int ACT_IDN;
        protected void Page_Load(object sender, EventArgs e)
        {
            ACT_IDN = Int32.Parse(Request["act_idn"]);
        }
        #region 抓取區塊資料
        [System.Web.Services.WebMethod]
        public static string getSectionList()
        {
            S020101BL _bl = new S020101BL();
            List<Activity_sectionInfo> sectionList = _bl.GetSectionList(ACT_IDN);
            string json_data = JsonConvert.SerializeObject(sectionList);
            return json_data;
        }
        #endregion

        #region 抓取題目資料
        [System.Web.Services.WebMethod]
        public static string getQuestionList()
        {
            S020101BL _bl = new S020101BL();
            List<Activity_columnInfo> questionList = _bl.GetQuestionList(ACT_IDN);
            string json_data = JsonConvert.SerializeObject(questionList);
            return json_data;
        }
        #endregion

    }


}