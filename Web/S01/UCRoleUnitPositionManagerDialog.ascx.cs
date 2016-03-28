using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.S01
{
    public partial class UCRoleUnitPositionManagerDialog : System.Web.UI.UserControl
    {
        public Action AfterCloseDialog;

        public void Show(string sys_rid, string sys_uid)
        {
            ucRoleUnitPositionManager.Show(sys_rid, sys_uid);
            popupWindow_mpe.Show();
            if (sys_uid == DataAccess.AuthData.GlobalSymbol)
                popupWindow_tilte_lbl.Text = "通用職位設定";
            else
                popupWindow_tilte_lbl.Text = "單位專用職位設定";
        }

        #region 關閉視窗時
        protected void popupWindow_cancel_btn_Click(object sender, EventArgs e)
        {
            if (AfterCloseDialog != null)
                AfterCloseDialog();
        }
        #endregion
    }
}