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
        [System.Web.Services.WebMethod]
        public static string getActivityAllList()
        {
            indexBL _bl = new indexBL();
            DataTable ActivityAllList = _bl.GetActivityAllList();
            string json_data = JsonConvert.SerializeObject(ActivityAllList);
            return json_data;
        }
    }
}