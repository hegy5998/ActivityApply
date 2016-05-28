using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.CommonPages
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
#if DEBUG
            // Debug 模式時，發生錯誤會顯示錯誤訊息
            Response.Clear();

            if (Session["ErrorStack"] != null)
            {
                Response.Write((Session["ErrorStack"] as string).Replace(Environment.NewLine, "<br/>"));
            }

            Response.Write("<br/><hr/><br/><a href=\"" + ResolveUrl("~/Index.aspx") + "\">Go /Index.aspx</a>");
            Response.End();
#endif
        }
    }
}