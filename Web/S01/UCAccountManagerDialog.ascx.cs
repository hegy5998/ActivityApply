using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.S01
{
    public partial class UCAccountManagerDialog : System.Web.UI.UserControl
    {
        public Action AfterCloseDialog;

        protected void Page_Load(object sender, EventArgs e)
        {
            ucAccountManager.AfterInsertOrUpdate = () => { 
                popupWindow_cancel_btn_Click(popupWindow_cancel_btn, new EventArgs());
                popupWindow_mpe.Hide();
            };
        }

        #region 新增帳號
        public void Add()
        {
            ucAccountManager.Add();
            popupWindow_mpe.Show();
            popupWindow_tilte_lbl.Text = "新增帳號";
        }
        #endregion

        #region 編輯帳號
        public void Edit(string act_id)
        {
            ucAccountManager.Edit(act_id);
            popupWindow_mpe.Show();
            popupWindow_tilte_lbl.Text = "編輯帳號";
        }
        #endregion

        #region 關閉視窗時
        protected void popupWindow_cancel_btn_Click(object sender, EventArgs e)
        {
            if (AfterCloseDialog != null)
                AfterCloseDialog();
        }
        #endregion
    }
}