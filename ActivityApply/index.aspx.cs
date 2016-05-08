using BusinessLayer.Web;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ActivityApply
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Redirect("activity_List.aspx?act_class=0");
        }

        //接從前台傳過來想要搜尋活動的資料，並回傳JSON字串
        [System.Web.Services.WebMethod]
        public static string getActivityTopfive()
        {
            indexBL _bl = new indexBL();
            DataTable ActivityAllList = _bl.getActivityTopfive();
            string json_data = JsonConvert.SerializeObject(ActivityAllList);
            return json_data;
        }
    }
}