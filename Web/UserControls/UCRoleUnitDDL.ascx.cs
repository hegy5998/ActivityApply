using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.UserControls
{
    public partial class UCRoleUnitDDL : System.Web.UI.UserControl
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
        public void BindData(string sys_rid)
        {
            ddl.Items.Clear();
            var data_lst = new Sys_unitData().GetListByRole(sys_rid);

            // 有角色的對應單位資料時，才綁定選單
            if (data_lst.Count > 0)
            {
                if (ShowItemAll)
                    ddl.Items.Add(new ListItem(AuthData.GlobalSymbol + " 不分", AuthData.GlobalSymbol));
                foreach (var item in data_lst)
                {
                    // 避免重複顯示"不分"的選單
                    if (ShowItemAll == false || item.Sys_uid != AuthData.GlobalSymbol)
                    {
                        var li = new ListItem();
                        li.Value = item.Sys_uid;
                        li.Text = (ItemTextHideCode) ? item.Sys_uname : item.Sys_uid + " " + item.Sys_uname;
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