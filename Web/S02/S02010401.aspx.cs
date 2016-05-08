using BusinessLayer.S01;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.S02
{
    public partial class S02010401 : System.Web.UI.Page
    {
        S020104BL _bl = new S020104BL();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void set_bt_Click(object sender, EventArgs e)
        {
            List<Activity_apply_emailInfo> ls = _bl.setPassword(email_txt.Text);
            if(ls.Count > 0)
                password_lb.Text = ls[0].Aae_password + " (請立即請使用者更改密碼!!)";
            else
                password_lb.Text = "無此信箱";
        }
    }
}