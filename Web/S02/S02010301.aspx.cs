using BusinessLayer.S01;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Util;
using System.Collections;
using Web.CommonPages;

namespace Web.S02
{
    public partial class S02010301 : BasePage
    {
        S020103BL _bl = new S020103BL();
        private S020103BL BL { get { if (_bl == null) _bl = new S020103BL(); return _bl; } }
        private string _dataCacheKey;

        protected void Page_Init(object sender, EventArgs e)
        {
            _dataCacheKey = "S020103_" + Session.SessionID;
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
        private List<Activity_classInfo> GetData(bool isRefresh)
        {
            List<Activity_classInfo> dt = new List<Activity_classInfo>();
            if (isRefresh || Cache[_dataCacheKey] == null)
            {
                dt = BL.GetClassList();

                CommonHelper.SetCache(_dataCacheKey, dt);
            }
            else
                dt = (List<Activity_classInfo>)Cache[_dataCacheKey];
            return dt;
        }
        #endregion

        #region --- Gridview 事件 ---
        /// <summary>
        /// 編輯前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void main_gv_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView gv = sender as GridView;
            gv.EditIndex = e.NewEditIndex;

            BindGridView(GetData(true));
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void main_gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewHelper.ChgGridViewMode(GridViewHelper.GVMode.Normal, main_gv);
            BindGridView(GetData(true));
        }
        /// <summary>
        /// Bind資料
        /// </summary>
        /// <param name="lst">資料</param>
        private void BindGridView(List<Activity_classInfo> dt)
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
            List<Activity_classInfo> dt = (List<Activity_classInfo>)Cache[_dataCacheKey];
            //取得選擇的row
            GridViewRow gvr = sender as GridViewRow;
            //取得選擇的row的Index
            //int rowIndex = Convert.ToInt32(e.CommandArgument);

            switch (e.CommandName)
            {
                case "Add":
                    GridViewHelper.ChgGridViewMode(GridViewHelper.GVMode.Insert, main_gv);
                    BindGridView(GetData(true));
                    break;
                case "AddSave":     // 儲存新增資料
                    AddSave();
                    break;
            }
        }
        /// <summary>
        /// 新增
        /// </summary>
        private void AddSave()
        {
            GridViewRow gvr = main_gv.FooterRow;
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict["ac_title"] = (gvr.FindControl("Ac_title_txt") as TextBox).Text.Trim();
            dict["ac_desc"] = (gvr.FindControl("Ac_desc_txt") as TextBox).Text.Trim();
            dict["ac_seq"] = (gvr.FindControl("Ac_seq_txt") as TextBox).Text.Trim();
            // 新增資料
            Boolean titleRepaet = false;
            
            if(dict["ac_title"].ToString() == "")
                ShowPopupMessage(ITCEnum.PopupMessageType.Error, ITCEnum.DataActionType.Insert, "請填寫分類名稱");
            else if (dict["ac_seq"].ToString() == "")
                ShowPopupMessage(ITCEnum.PopupMessageType.Error, ITCEnum.DataActionType.Insert, "請填寫分類順序");
            else
            {
                List<Activity_classInfo> classlist = BL.GetClassList();
                for (int count = 0; count < classlist.Count; count++)
                {
                    if (classlist[count].Ac_title == dict["ac_title"].ToString())
                        titleRepaet = true;
                }
                if (titleRepaet != true)
                {
                    var res = _bl.InsertData(dict);
                    if (res.IsSuccess)
                    {
                        // 新增成功，切換回一般模式
                        GridViewHelper.ChgGridViewMode(GridViewHelper.GVMode.Normal, main_gv);
                        BindGridView(GetData(true));
                        main_gv.DataBind();
                        ShowPopupMessage(ITCEnum.PopupMessageType.Success, ITCEnum.DataActionType.Insert);
                    }
                    else
                    {
                        // 新增失敗，顯示錯誤訊息
                        ShowPopupMessage(ITCEnum.PopupMessageType.Error, ITCEnum.DataActionType.Insert, res.Message);
                    }
                }
                else
                    ShowPopupMessage(ITCEnum.PopupMessageType.Error, ITCEnum.DataActionType.Insert, "標題名稱重複");
            }
        }

        #region 刪除
        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void main_gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow gvr = (sender as GridView).Rows[e.RowIndex];
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict["ac_idn"] = (gvr.FindControl("Ac_idn_hf") as HiddenField).Value.Trim();
            // 刪除資料
            var res = _bl.DeleteData(dict);
            if (res.IsSuccess)
            {
                // 刪除成功，切換回一般模式
                GridViewHelper.ChgGridViewMode(GridViewHelper.GVMode.Normal, main_gv);
                BindGridView(GetData(true));
                main_gv.DataBind();
                ShowPopupMessage(ITCEnum.PopupMessageType.Success, ITCEnum.DataActionType.Delete);
            }
            else
            {
                // 刪除失敗，顯示錯誤訊息
                ShowPopupMessage(ITCEnum.PopupMessageType.Error, ITCEnum.DataActionType.Delete, res.Message);
            }
        }
        #endregion
        #endregion

        #region 更新
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void main_gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow gvr = (sender as GridView).Rows[e.RowIndex];
            Dictionary<string, object> old_dict = new Dictionary<string, object>();
            old_dict["ac_idn"] = (gvr.FindControl("Ac_idn_hf") as HiddenField).Value.Trim();

            Dictionary<string, object> new_dict = new Dictionary<string, object>();
            new_dict["ac_title"] = (gvr.FindControl("Ac_title_txt") as TextBox).Text.Trim();
            new_dict["ac_desc"] = (gvr.FindControl("Ac_desc_txt") as TextBox).Text.Trim();
            new_dict["ac_seq"] = (gvr.FindControl("Ac_seq_txt") as TextBox).Text.Trim();
            // 更新資料
            var res = _bl.UpdateData(old_dict,new_dict);
            if (res.IsSuccess)
            {
                // 更新資料，切換回一般模式
                GridViewHelper.ChgGridViewMode(GridViewHelper.GVMode.Normal, main_gv);
                BindGridView(GetData(true));
                main_gv.DataBind();
                ShowPopupMessage(ITCEnum.PopupMessageType.Success, ITCEnum.DataActionType.Update);
            }
            else
            {
                // 更新失敗，顯示錯誤訊息
                ShowPopupMessage(ITCEnum.PopupMessageType.Error, ITCEnum.DataActionType.Update, res.Message);
            }
        }
        #endregion


    }
}