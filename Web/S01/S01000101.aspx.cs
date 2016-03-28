using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Util;

namespace Web.S01
{
    public partial class S01000101 : CommonPages.BasePage
    {
        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            // Set gridview pager
            //ucGridViewPager.InitGridView(main_gv, () => { main_gv.DataBind(); });
            main_gv.AllowPaging = false;
        }
        #endregion

        #region Control權限管理
        protected void ManageControlAuth(object sender, EventArgs e)
        {
            ManageControlAuth(sender);
        }
        #endregion

        #region GridView事件
        protected void main_gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (ProcessModifyAuth == false)
                e.Cancel = false;
        }

        protected void main_gv_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            ShowPopupMessage(ITCEnum.PopupMessageType.Success, ITCEnum.DataActionType.Update);
        }
        #endregion

        #region 重新取得系統參數
        protected void refresh_btn_Click(object sender, EventArgs e)
        {
            CommonHelper.GetSysConfig(true);
            ShowPopupMessage(ITCEnum.PopupMessageType.Success, ITCEnum.DataActionType.Update, "重新取得成功!");
        }
        #endregion
    }
}