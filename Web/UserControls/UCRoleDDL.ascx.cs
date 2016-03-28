using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.UserControls
{
    public partial class UCRoleDDL : System.Web.UI.UserControl
    {
        #region Properties
        /// <summary>
        /// AutoPostBack
        /// </summary>
        public bool AutoPostBack { get { return ddl.AutoPostBack; } set { ddl.AutoPostBack = value; } }
        /// <summary>
        /// 取得選單資料
        /// </summary>
        public ListItemCollection Items { get { return ddl.Items; } }
        /// <summary>
        /// 取得或設定選取的代碼
        /// </summary>
        public string SelectedValue { get { return ddl.SelectedValue; } set { ddl.SelectedValue = value; } }
        /// <summary>
        /// 取得選取的資料
        /// </summary>
        public ListItem SelectedItem { get { return ddl.SelectedItem; } }
        /// <summary>
        /// 選單的文字部分，是否隱藏代碼
        /// </summary>
        public bool ItemTextHideCode { get; set; }
        #endregion

        #region Event
        public delegate void SelectedIndexChangedHandler(object sender, EventArgs e);
        public event SelectedIndexChangedHandler SelectedIndexChanged;
        #endregion

        #region 綁定資料
        /// <summary>
        /// 綁定資料
        /// </summary>
        public void BindData()
        {
            ddl.Items.Clear();
            foreach (var item in new Sys_roleData().GetList())
            {
                var li = new ListItem();
                li.Value = item.Sys_rid;
                li.Text = (ItemTextHideCode) ? item.Sys_rname : item.Sys_rid + " " + item.Sys_rname;
                ddl.Items.Add(li);
            }
        }
        #endregion

        #region 選單選取
        protected void ddl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedIndexChanged != null)
                SelectedIndexChanged(this, e);
        }
        #endregion
    }
}