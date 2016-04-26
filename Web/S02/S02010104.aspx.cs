using System;
using System.Collections.Generic;
using BusinessLayer.Web;
using Model;
using Newtonsoft.Json;
using System.Data;
using System.Web;
using BusinessLayer.S01;

namespace Web.S02
{
    public partial class S02010104 : System.Web.UI.Page
    {
        //static int ACTIVITY = 2037;
        //static int ACTIVITY = 2085;
        static int ACTIVITY ;
        protected void Page_Load(object sender, EventArgs e)
        {
            ACTIVITY = Int32.Parse(Request["i"]);
            activityBL _bl = new activityBL();
            List<ActivityInfo> AvtivityList = _bl.GetActivityList(ACTIVITY);
            Act_desc_lbl.Text = HttpUtility.UrlDecode(AvtivityList[0].Act_desc);
        }

        [System.Web.Services.WebMethod]
        public static string getActivityList()
        {
            activityBL _bl = new activityBL();
            List<ActivityInfo> ActivityList = _bl.GetActivityList(ACTIVITY);
            string json_data = JsonConvert.SerializeObject(ActivityList);
            return json_data;
        }

        [System.Web.Services.WebMethod]
        public static string getSessionList()
        {
            S020104BL _bl = new S020104BL();
            DataTable sessionList = _bl.GetSessionList(ACTIVITY);
            string json_data = JsonConvert.SerializeObject(sessionList);
            return json_data;
        }
    }
}