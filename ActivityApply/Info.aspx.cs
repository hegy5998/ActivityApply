using BusinessLayer.Web;
using Newtonsoft.Json;
using System;
using System.Data;

namespace ActivityApply
{
    public partial class Info : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        #region 接從前台傳過來想要搜尋活動的資料，並回傳JSON字串
        [System.Web.Services.WebMethod]
        public static string getActivityAllList(string act_title, string act_class)
        {
            activity_ListBL _bl = new activity_ListBL();
            DataTable ActivityAllList = _bl.GetActivityAllList(act_title, act_class);
            string json_data = JsonConvert.SerializeObject(ActivityAllList);
            return json_data;
        }
        #endregion

        #region 接從前台傳過來想要搜尋活動的資料，並回傳JSON字串
        [System.Web.Services.WebMethod]
        public static string getActivityTopfive()
        {
            indexBL _bl = new indexBL();
            DataTable ActivityAllList = _bl.getActivityTopfive();
            string json_data = JsonConvert.SerializeObject(ActivityAllList);
            return json_data;
        }
        #endregion
    }
}