using Model;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Web.S02
{
    public partial class S02010202 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string rawId = Request["act_idn"];
            Response.Write(rawId);
        }
        [System.Web.Services.WebMethod]
        public static string get(string ID)
        {
            List<Activity_columnInfo> activity_Form = new List<Activity_columnInfo>();

            Activity_columnInfo a = new Activity_columnInfo();

            a.Acc_title = "123";
            //string szRtnJSON = activity_Form.t
            activity_Form.Add(a);
            string response = "Acc_title";
            return activity_Form.ToString();
        }

    }

    
}