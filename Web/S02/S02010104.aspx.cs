using System;
using System.Collections.Generic;
using BusinessLayer.Web;
using Model;
using Newtonsoft.Json;
using System.Data;
using System.Web;
using BusinessLayer.S01;
using BusinessLayer.S02;

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
            S020104BL _S020104Bl = new S020104BL();
            List<ActivityInfo> AvtivityList = _S020104Bl.GetActivityList(ACTIVITY);
            if (AvtivityList.Count > 0)
            {
                Act_desc_lbl.Text = HttpUtility.UrlDecode(AvtivityList[0].Act_desc);
                person_data.Text = HttpUtility.UrlDecode(_bl.GetStateMent(ACTIVITY).Rows[0]["ast_content"].ToString());
            }
            else
                Response.Redirect("../DefaultSystemIndex.aspx");
        }

        [System.Web.Services.WebMethod]
        public static string getActivityList()
        {
            activityBL _bl = new activityBL();
            S020104BL _S020104Bl = new S020104BL();
            List<ActivityInfo> ActivityList = _S020104Bl.GetActivityList(ACTIVITY);
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