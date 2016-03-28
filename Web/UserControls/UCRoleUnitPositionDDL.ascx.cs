using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.UserControls
{
    public partial class UCRoleUnitPositionDDL : System.Web.UI.UserControl
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
        /// 若不包含"*不分"時，是否顯示"不分"的選項
        /// </summary>
        public bool ShowItemAll { get; set; }
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
        /// <param name="sys_rid">選單對應的角色代碼</param>
        /// <param name="sys_uid">選單對應的單位代碼</param>
        public void BindData(string sys_rid, string sys_uid)
        {
            ddl.Items.Clear();
            var data_lst = new Sys_role_positionData().GetListByRoleUnit(sys_rid, sys_uid);

            // 有對應的資料時，才綁定選單
            if (data_lst.Count > 0)
            {
                if (ShowItemAll)
                    ddl.Items.Add(new ListItem(AuthData.GlobalSymbol + " 不分", AuthData.GlobalSymbol));
                foreach (var item in data_lst)
                {
                    // 避免重複顯示"不分"的選單
                    if (ShowItemAll == false || item.Sys_rpid != AuthData.GlobalSymbol)
                    {
                        var li = new ListItem();
                        li.Value = item.Sys_rpid;
                        li.Text = (ItemTextHideCode) ? item.Sys_rpname : item.Sys_rpid + " " + item.Sys_rpname;
                        ddl.Items.Add(li);
                    }
                }
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