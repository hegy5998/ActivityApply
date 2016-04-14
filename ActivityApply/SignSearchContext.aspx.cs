using AjaxControlToolkit;
using BusinessLayer.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Util;

namespace ActivityApply
{
    public partial class SignSearchContext : System.Web.UI.Page
    {
        private SignSearchContextBL _bl;
        private SignSearchContextBL BL { get { if (_bl == null) _bl = new SignSearchContextBL(); return _bl; } }
        private string _dataCacheKey;

        protected void Page_Init(object sender, EventArgs e)
        {
            _dataCacheKey = "SignSearchContext_" + Session.SessionID;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridView(GetData(true));
            }

        }

        #region --- 查詢 ---
        /// <summary>
        /// 取得資料
        /// </summary>
        /// <param name="isRefresh">是否重新取得DB資料</param>
        /// <returns>資料</returns>
        private DataTable GetData(bool isRefresh)
        {
            DataTable dt;
            if (isRefresh || Cache[_dataCacheKey] == null)
            {
                dt = BL.GetActivityData(aa_email_hf.Value);

                CommonHelper.SetCache(_dataCacheKey, dt);
            }
            else
                dt = (DataTable)Cache[_dataCacheKey];
            return dt;
        }
        #endregion

        #region --- Gridview 事件 ---
        /// <summary>
        /// Bind資料
        /// </summary>
        /// <param name="lst">資料</param>
        private void BindGridView(DataTable dt)
        {
            main_gv.DataSource = dt;
            main_gv.DataBind();
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void main_gv_Sorting(object sender, GridViewSortEventArgs e)
        {
            GridView gv = (GridView)sender;
            var dt = GridViewHelper.SortGridView(gv, e, GetData(false));
            BindGridView(dt);
            CommonHelper.SetCache(_dataCacheKey, dt);
        }

        /// <summary>
        /// RowDataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void main_gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }

        /// <summary>
        /// 命令處理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void main_gv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            DataTable dt = (DataTable)Cache[_dataCacheKey];
            
            switch (e.CommandName)
            {
                case "Custom_Edit"://修改報名資料
                    //ModalPopupExtender顯示
                    password_pop.Show();
                    //抓取GridView隱藏欄位資料
                    GridViewRow gvr = sender as GridViewRow;
                    int rowIndex = Convert.ToInt32(e.CommandArgument);
                    HiddenField Act_idn_hf = main_gv.Rows[rowIndex].FindControl("Act_idn_hf") as HiddenField;
                    HiddenField As_idn_hf = main_gv.Rows[rowIndex].FindControl("As_idn_hf") as HiddenField;
                    HiddenField Act_class_hf = main_gv.Rows[rowIndex].FindControl("Act_class_hf") as HiddenField;
                    Label Act_title_lbl = main_gv.Rows[rowIndex].FindControl("Act_title_lbl") as Label;
                    //設定資料
                    act_idn_hf.Value = Act_idn_hf.Value;
                    as_idn_hf.Value = As_idn_hf.Value;
                    act_class_hf.Value = Act_class_hf.Value;
                    act_title_hf.Value = Act_title_lbl.Text;
                    break;
            }
        }

        #endregion

        #region 刪除
        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void main_gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            GridViewRow gvr = (sender as GridView).Rows[e.RowIndex];
            Dictionary<string, object> data_dict = new Dictionary<string, object>();
            //data_dict["aa_email"] = CommonConvert.GetStringOrEmptyString(WkPt_hf.Value);
            //data_dict["aa_act"] = CommonConvert.GetStringOrEmptyString(WkOkMonth1_hf.Value + WkOkMonth2_hf.Value);
            //data_dict["aa_name"] = CommonConvert.GetStringOrEmptyString((gvr.FindControl("Ap_central_hf") as HiddenField).Value.Trim());
            //data_dict["aa_idn"] = CommonConvert.GetStringOrEmptyString((gvr.FindControl("Ap_share_hf") as HiddenField).Value.Trim());

            // var res = BL.DeleteData(data_dict);
            // if (res.IsSuccess)
            // {
            //     // 刪除成功，切換回一般模式
            //     BindGridView(GetData(true));
            //     ShowPopupMessage(ITCEnum.PopupMessageType.Success, ITCEnum.DataActionType.Delete);
            // }
            // else
            // {
            //     // 刪除失敗，顯示錯誤訊息
            //     ShowPopupMessage(ITCEnum.PopupMessageType.Error, ITCEnum.DataActionType.Delete, res.Message);
            //}
        }
        #endregion

        #region 搜尋按鈕事件
        protected void search_btn_Click(object sender, EventArgs e)
        {
            aa_email_hf.Value = aa_email_txt.Text.Trim();
            DataTable dt = GetData(true);
            if (dt.Rows.Count != 0)
            {
                forget_password_btn.Visible = true;
                change_password_btn.Visible = true;
            }
            else
            {
                forget_password_btn.Visible = false;
                change_password_btn.Visible = false;
            }
            BindGridView(dt);
        }
        #endregion

        #region 更換密碼按鈕確認事件
        protected void change_password_ok_btn_Click(object sender, EventArgs e)
        {
            
        }
        #endregion

        #region 忘記密碼按鈕確認事件
        protected void get_password_ok_btn_Click(object sender, EventArgs e)
        {
            
        }
        #endregion

        #region 修改報名資料、刪除按鈕，輸入密碼確認事件
        protected void password_ok_btn_Click(object sender, EventArgs e)
        {
            string aae_password = password_txt.Text;

            SignSearchContextBL _bl = new SignSearchContextBL();
            DataTable dt = _bl.GetEmailData(aa_email_hf.Value, aae_password);
            if(dt.Rows.Count != 0)
            {
                Response.Redirect("SignChange.aspx?act_idn=" + act_idn_hf.Value + "&as_idn=" + as_idn_hf.Value + "&act_class=" + act_class_hf.Value + "&act_title=" + act_title_hf.Value);
            }
        }
        #endregion

    }
}