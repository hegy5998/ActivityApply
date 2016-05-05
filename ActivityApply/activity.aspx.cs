using System;
using System.Collections.Generic;
using BusinessLayer.Web;
using Model;
using Newtonsoft.Json;
using System.Data;
using System.Web;

namespace ActivityApply
{
    public partial class activity : System.Web.UI.Page
    {
        //static int ACTIVITY = 2037;
        //static int ACTIVITY = 2085;
        static int ACTIVITY ;
        protected void Page_Load(object sender, EventArgs e)
        {
            ACTIVITY = Int32.Parse(Request["act_idn"]);
            activityBL _bl = new activityBL();
            List<ActivityInfo> AvtivityList = _bl.GetActivityList(ACTIVITY);
            if (AvtivityList.Count > 0)
                Act_desc_lbl.Text = HttpUtility.UrlDecode(AvtivityList[0].Act_desc);
            else
                Response.Redirect("index.aspx");
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
            activityBL _bl = new activityBL();
            DataTable sessionList = _bl.GetSessionList(ACTIVITY);
            string json_data = JsonConvert.SerializeObject(sessionList);
            return json_data;
        }
    }
}