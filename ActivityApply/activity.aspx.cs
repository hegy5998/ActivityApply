using System;
using System.Collections.Generic;
using BusinessLayer.Web;
using Model;
using Newtonsoft.Json;
using System.Data;
using System.Web;
using System.Web.UI.HtmlControls;

namespace ActivityApply
{
    public partial class Activity : System.Web.UI.Page
    {
        //儲存活動ID
        static int ACTIVITY ;
        protected void Page_Load(object sender, EventArgs e)
        {
            //儲存活動ID
            ACTIVITY = Int32.Parse(Request["act_idn"]);
            activityBL _bl = new activityBL();
            //抓取活動資料
            List<ActivityInfo> AvtivityList = _bl.GetActivityList(ACTIVITY);
            //判斷是否有抓到資料
            if (AvtivityList.Count > 0)
            {
                //設定活動簡介
                Act_desc_lbl.Text = HttpUtility.UrlDecode(AvtivityList[0].Act_desc);
                //設定個資聲明
                person_data.Text = HttpUtility.UrlDecode(_bl.GetStateMent(ACTIVITY).Rows[0]["ast_content"].ToString());
            }
            else
                Response.Redirect("index.aspx");
        }

        #region 抓取活動資料，並回傳JSON字串
        [System.Web.Services.WebMethod]
        public static string getActivityList()
        {
            activityBL _bl = new activityBL();
            List<ActivityInfo> ActivityList = _bl.GetActivityList(ACTIVITY);
            string json_data = JsonConvert.SerializeObject(ActivityList);
            return json_data;
        }
        #endregion

        #region 抓取場次資料，並回傳JSON字串
        [System.Web.Services.WebMethod]
        public static string getSessionList()
        {
            activityBL _bl = new activityBL();
            DataTable sessionList = _bl.GetSessionList(ACTIVITY);
            string json_data = JsonConvert.SerializeObject(sessionList);
            return json_data;
        }
        #endregion
    }
}