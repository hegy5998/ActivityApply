using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer.S02;
using System.Data;

namespace Web.S02
{
    public partial class S02010102 : System.Web.UI.Page
    {
        S020101BL _bl = new S020101BL();

        DataTable data;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int i = Convert.ToInt32(Request.QueryString["i"]);

                data = GetData(i);

                //Response.Write(i.ToString());

                //test.Add("a");
                //test.Add("b");
                //test.Add("c");

                //aab.Text = "<div>"+ test[0] + "</div>";
            }
        }

        // 取得報名資料
        private DataTable GetData(int i)
        {
            return _bl.GetApplyData(i);
        }
    }
}