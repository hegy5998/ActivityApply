using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer.UserControls;
using Model;
using Util;
using System.Text;
using DataAccess;

namespace Web.UserControls
{
    public partial class UCRoleChg : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
                // 取得使用者具有的所有身份
                auth_ddl.Items.Clear();
                var lst = CommonHelper.GetLoginUser().Role_lst.GroupBy(x => new { x.Sys_rid, x.Sys_rname, x.Sys_uid, x.Sys_uname });
                foreach (var t in lst)
                {
                    ListItem item = new ListItem();
                    item.Value = t.Key.Sys_rid + "," + t.Key.Sys_uid;
                    item.Text = t.Key.Sys_rname + ((t.Key.Sys_uid != AuthData.GlobalSymbol) ? "-" + t.Key.Sys_uname : "");
                    auth_ddl.Items.Add(item);
                }
                var act_info = CommonHelper.GetLoginUser();
                auth_ddl.SelectedValue = act_info.Login_sys_rid + "," + act_info.Login_sys_uid;

                if (auth_ddl.Items.Count == 1)
                {
                    auth_ddl.Visible = false;
                    auth_lbl.Text = auth_ddl.SelectedItem.Text;
                }
            }
        }

        #region 切換身份
        protected void auth_ddl_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 設定為需要重新取得權限
            var v = auth_ddl.SelectedValue.Split(',');
            Session["Chg_sys_rid"] = v[0];
            Session["Chg_sys_uid"] = v[1];

            StringBuilder path_sb = new StringBuilder();
            path_sb.Append(Request.Path);

            // 導回同一個頁面
            string sys_id = Request["sys_id"] as string;
            if (sys_id != null)
            {
                path_sb.Append("?sys_id=" + sys_id);

                string sys_pid = Request["sys_pid"] as string;
                if (sys_pid != null)
                    path_sb.Append("&sys_pid=" + sys_pid);
            }

            Response.Redirect(path_sb.ToString());
        }
        #endregion
    }
}