using BusinessLayer.Web;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Register
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //接從前台傳過來想要搜尋活動的資料，並回傳JSON字串
        [System.Web.Services.WebMethod]
        public static string getActivityAllList(string act_title,string act_class)
        {
            indexBL _bl = new indexBL();
            DataTable ActivityAllList = _bl.GetActivityAllList(act_title, act_class);
            string json_data = JsonConvert.SerializeObject(ActivityAllList);
            return json_data;
        }
    }
}